using MinecraftBdsManager.Configuration;
using MinecraftBdsManager.Logging;
using System.IO.Pipes;

namespace MinecraftBdsManager.Managers
{
    internal static class MapManager
    {
        private static System.Timers.Timer _mapTimer = new() { Enabled = false };
        private static bool _mapGenerationCompleted = true;

        /// <summary>
        /// Stops the map timer, disabling the interval based map generation
        /// </summary>
        internal static void DisableIntervalBasedMapGeneration()
        {
            _mapTimer.Stop();
        }

        /// <summary>
        /// Creates, if needed, and starts the map timer, enabling the interval based map generation
        /// </summary>
        internal static void EnableIntervalBasedMapGeneration()
        {
            // Interval from the user should be in minutes...
            var mapTimerIntervalMinutes = Settings.CurrentSettings.MapSettings.MapGenerationIntervalInMinutes;
            var mapTimespan = TimeSpan.FromMinutes(mapTimerIntervalMinutes);

            // ... Interval on the timer is in milliseconds, so creating TimeSpan objects of both for easier comparison.
            var timerIntervalTimespan = TimeSpan.FromMilliseconds(_mapTimer.Interval);

            // Check to see if the intervals match (if they match the result will be 0).  If they don't recreate the timer with the new interval.
            if (timerIntervalTimespan.CompareTo(mapTimespan) != 0)
            {
                // The documentation on Timer has a bunch of screwy talk about how updating intervals after they've been set doing strange things like adding the remaining
                //  old interval to the new one and such, so I'm just going to kill the Timer outright and make a new one from scratch each time the interval is change
                //  to minimize the silliness.
                _mapTimer.Stop();
                _mapTimer.Dispose();

                _mapTimer = new(mapTimespan.TotalMilliseconds) { AutoReset = true };
                _mapTimer.Elapsed += MapTimer_Elapsed;
            }

            if (!_mapTimer.Enabled)
            {
                _mapTimer.Start();
            }
        }

        private async static void MapTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            // Check to see if the mapping process is still running.  If the world is big, and the mapping interval is small, the process can still be running
            if (ProcessManager.TrackedProcesses.ContainsKey(ProcessName.Mapping) 
                && ProcessManager.TrackedProcesses[ProcessName.Mapping] != null 
                && !ProcessManager.TrackedProcesses[ProcessName.Mapping]!.HasExited
                && !_mapGenerationCompleted)
            {
                return;
            }

            // Check settings to see if the user wanted to do a backup only if players had been online
            if (Settings.CurrentSettings.MapSettings.OnlyGenerateMapsIfUsersWereOnline)
            {
                // User entered backup interval
                var mapIntervalTimespan = TimeSpan.FromMinutes(Settings.CurrentSettings.MapSettings.MapGenerationIntervalInMinutes);

                var usersHaveBeenActiveOnTheServer = BdsManager.HaveUsersBeenOnInTheLastAmountOfTime(mapIntervalTimespan);

                // If no one is currently active or been on then just return
                if (!usersHaveBeenActiveOnTheServer)
                {
                    LogManager.LogInformation($"Skipping mapping operation(s) since there are no users have been active for over {mapIntervalTimespan.TotalMinutes} minutes.");
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(Settings.CurrentSettings.MapSettings.MapperExePath))
            {
                LogManager.LogWarning("Unable to generate maps since the MapperExePath is empty.  Please check settings.json.");
                return;
            }

            _mapGenerationCompleted = false;
            LogManager.LogInformation("Beginning map generation process(es).");

            // Get the map output path from settings.
            var mapDirectoryOutputPath = Path.GetFullPath(Settings.CurrentSettings.MapSettings.MapperOutputPath);

            if (!Directory.Exists(mapDirectoryOutputPath))
            {
                Directory.CreateDirectory(mapDirectoryOutputPath);
            }

            // Prepare world files for map generation.  They will need to be copied to a temporary location since the orignal files will be exclusively locked by BDS
            var mapTempDirectoryPath = Path.GetFullPath(Path.Combine(mapDirectoryOutputPath, "temp"));

            // Clean out the existing temp dir if it exists...
            if (Directory.Exists(mapTempDirectoryPath))
            {
                Directory.Delete(mapTempDirectoryPath, recursive:true);
            }
             
            // ... then be sure it exists again
            Directory.CreateDirectory(mapTempDirectoryPath);
            Directory.CreateDirectory(Path.Combine(mapTempDirectoryPath, "db"));

            try
            {
                // Copy the world files to this temp directory via backup manager since it already has the ability to do this
                var worldFilesDirectoryPath = Path.Combine(Settings.CurrentSettings.BedrockDedicateServerDirectoryPath, BdsManager.WorldDirectoryPath!);
                var copyWasSuccessful = BackupManager.CopyDirectoryContents(worldFilesDirectoryPath, mapTempDirectoryPath);

                if (!copyWasSuccessful)
                {
                    throw new Exception("Unable to copy files to temp directory. Please see previous errors for more information.");
                }

                // Check the settings to see if there are argument variations.
                string[] argumentVariations = Settings.CurrentSettings.MapSettings.MapperExeArgumentVariations;

                // If there are no variations then add a "dummy" one to make the following processing logic simpler
                if (argumentVariations.Length == 0)
                {
                    argumentVariations = new string[] { string.Empty };
                }

                // Setup a collection for each of the tasks that will be run as part of the mapping.
                List<Task> mapperTasks = new(argumentVariations.Length);

                // Run the mapper for each argumentVariation
                foreach (var variation in argumentVariations)
                {
                    var tokenReplacedArguments = Settings.CurrentSettings.MapSettings.MapperExeArguments.Replace("$WORLD_PATH", mapTempDirectoryPath).Replace("$OUTPUT_PATH", mapDirectoryOutputPath);
                    ProcessManager.StartProcess(ProcessName.Mapping, Settings.CurrentSettings.MapSettings.MapperExePath, $"{tokenReplacedArguments} {variation}", outputShouldBeRedirected: false);

                    // Wait for the process to complete.
                    await ProcessManager.TrackedProcesses[ProcessName.Mapping]!.WaitForExitAsync();
                }

            }
            catch (Exception ex)
            {
                LogManager.LogError($"Error occured while running the mapping tool. The error is {ex}");
            }
            finally
            {
                // Cleanup the temp path
                if (Directory.Exists(mapTempDirectoryPath))
                {
                    Directory.Delete(mapTempDirectoryPath, recursive: true);
                }

                LogManager.LogInformation("Completed map generation process(es).");

                _mapGenerationCompleted = true;
            }
        }
    }
}
