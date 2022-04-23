using MinecraftBdsManager.Configuration;
using System.Collections.Concurrent;

namespace MinecraftBdsManager.Managers
{
    internal class RestartManager
    {
        private static System.Timers.Timer _restartIntervalTimer = new() { Enabled = false };
        private static System.Timers.Timer _restartScheduleTimer = new() { Enabled = false };
        private static readonly ConcurrentQueue<TimeOnly> _scheduledTimesQueue = new();

        /// <summary>
        /// Creates, if needed, and starts the backup timer, enabling the interval based restarts
        /// </summary>
        internal static void EnableIntervalBasedRestart()
        {
            // Interval from the user should be in minutes...
            var restartTimerIntervalMinutes = Settings.CurrentSettings.RestartSettings.RestartIntervalInMinutes;
            var restartTimespan = TimeSpan.FromMinutes(restartTimerIntervalMinutes);

            // ... Interval on the timer is in milliseconds, so creating TimeSpan objects of both for easier comparison.
            var timerIntervalTimespan = TimeSpan.FromMilliseconds(_restartIntervalTimer.Interval);

            // Check to see if the intervals match (if they match the result will be 0).  If they don't recreate the timer with the new interval.
            if (timerIntervalTimespan.CompareTo(restartTimespan) != 0)
            {
                // The documentation on Timer has a bunch of screwy talk about how updating intervals after they've been set doing strange things like adding the remaining
                //  old interval to the new one and such, so I'm just going to kill the Timer outright and make a new one from scratch each time the interval is change
                //  to minimize the silliness.
                _restartIntervalTimer.Stop();
                _restartIntervalTimer.Dispose();

                _restartIntervalTimer = new(restartTimespan.TotalMilliseconds) { AutoReset = true };
                _restartIntervalTimer.Elapsed += RestartIntervalTimer_Elapsed;
            }

            if (!_restartIntervalTimer.Enabled)
            {
                _restartIntervalTimer.Start();
            }
        }

        /// <summary>
        /// Creates, if needed, and starts the backup timer, enabling the scehdule based restarts
        /// </summary>
        internal static void EnableScheduleBasedRestart()
        {
            // Interval from the user should be in minutes...
            var restartScheduledTimes = Settings.CurrentSettings.RestartSettings.RestartScheduleTimes24h;

            // Sort the schedule times in ascending order since the user could have put them in in a random order.  This is to ensure "next time" is in the future.
            var orderedRestartScheduleTimesAsc = restartScheduledTimes.OrderBy(t => t.Ticks).ToList();

            // Repopulate the tracker each time the feature is enabled and disabled in case the user has edited settings.
            _scheduledTimesQueue.Clear();

            // Establish a holder for the scheduled times that have already passed, in asc order, to add to the queue after the future times.
            var pastScheduledTimesAsc = new List<TimeOnly>(restartScheduledTimes.Length);

            // Grab the current, local time for comparisons in order to determine which scheduled time is "next".  Using local time because the user's context of scheduled times
            //  should be a local mindset.
            var now = DateTime.Now;
            foreach (var time in orderedRestartScheduleTimesAsc)
            {
                // If the time is now or has past then add it to the holder collection.  We're putting the == time in there as well because there's no way, or need, to fire that restart 
                //  since we're just now being enabled, most likely from a restart.
                if (time.ToTimeSpan() <= now.TimeOfDay)
                {
                    // We can simply do an add since the working set for this loop is already in asc order.
                    pastScheduledTimesAsc.Add(time);
                    continue;
                }

                // Time is in the future, so enqueue it.
                _scheduledTimesQueue.Enqueue(time);
            }

            // Now add all of the past times to the queue in asc order
            foreach (var pastTime in pastScheduledTimesAsc)
            {
                _scheduledTimesQueue.Enqueue(pastTime);
            }

            // Get the next time from the queue to calculate the timer internval
            var nextScheduledTime = GetNextScheduledTime();
            if (nextScheduledTime == null)
            {
                LogManager.LogError("Unable to find the next scheduled time.  Unable to setup the scheduled restart timer.");
                return;
            }

            // Compute the interval between now and the next scheduled restart time
            //  Be sure to handle the case if the nextScheduleTime is actually earlier than now
            TimeSpan restartInterval;
            var nextScheduleTimeTimeSpan = nextScheduledTime!.Value.ToTimeSpan();

            if (nextScheduleTimeTimeSpan < now.TimeOfDay)
            {
                var nextScheduleTimeTomorrow = now.Date.AddDays(1).Add(nextScheduleTimeTimeSpan);
                restartInterval = nextScheduleTimeTomorrow - now;
            }
            else
            {
                restartInterval = nextScheduleTimeTimeSpan - now.TimeOfDay;
            }

            LogManager.LogInformation($"Next restart will occur in {restartInterval} at {nextScheduledTime}.");

            // Recreate the timer since the scheduled times may not have a consistent interval between values.
            //
            // The documentation on Timer has a bunch of screwy talk about how updating intervals after they've been set doing strange things like adding the remaining
            //  old interval to the new one and such, so I'm just going to kill the Timer outright and make a new one from scratch each time the interval is change
            //  to minimize the silliness.
            _restartScheduleTimer.Stop();
            _restartScheduleTimer.Dispose();

            _restartScheduleTimer = new(restartInterval.TotalMilliseconds) { AutoReset = true };
            _restartScheduleTimer.Elapsed += RestartScheduleTimer_Elapsed;

            if (!_restartScheduleTimer.Enabled)
            {
                _restartScheduleTimer.Start();
            }
        }

        private static TimeOnly? GetNextScheduledTime()
        {
            bool wasAbleToGetNextScheduledTime = _scheduledTimesQueue.TryDequeue(out var nextScheduledTime);

            // Check to ensure we were able to get the next scheduled time (we always should be able to)
            if (!wasAbleToGetNextScheduledTime)
            {
                return null;
            }

            // Put the next scheduled time back into the queue since we want a circular queue
            _scheduledTimesQueue.Enqueue(nextScheduledTime);

            return nextScheduledTime;
        }

        private async static void RestartIntervalTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            LogManager.LogInformation("Beginning interval server restart.");

            var restartWasSuccessful = await RestartServerAsync();
            if (restartWasSuccessful)
            {
                LogManager.LogInformation("Interval server restart completed successfully.");
            }
            else
            {
                LogManager.LogError("Interval server restart failed.");
            }
        }

        private async static void RestartScheduleTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            LogManager.LogInformation("Beginning scheduled server restart.");

            var restartWasSuccessful = await RestartServerAsync();
            if (restartWasSuccessful)
            {
                LogManager.LogInformation("Server interval restart completed successfully.");
            }
            else
            {
                LogManager.LogError("Server interval restart failed.");
            }
        }

        private async static Task<bool> RestartServerAsync()
        {
            try
            {
                // Stop the server
                await BdsManager.StopAsync();

                // Wait a few beats for the shutdown to complete
                await Task.Delay(TimeSpan.FromSeconds(10));

                // Start the server again
                await BdsManager.StartAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
