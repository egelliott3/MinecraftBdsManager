using MinecraftBdsManager.Configuration;
using MinecraftBdsManager.Logging;
using System.Diagnostics;

namespace MinecraftBdsManager
{
    internal static class BdsManager
    {
        private static readonly string _hardcodedBdsExePath = @"C:\Users\eelliott\Downloads\bedrock-server-1.18.2.03\bedrock_server.exe";


        internal async static Task SendCommandAsync(string command, bool userSentCommand = false)
        {
            // If a null or blank command was sent then simply return as there is nothing to send
            if (string.IsNullOrWhiteSpace(command))
            {
                return;
            }

            var bdsProcess = ProcessManager.TrackedProcesses[ProcessName.BedrockDedicatedServer];
            // Check if the process is null. This should not ever happen, but covering bases.
            if (bdsProcess == null)
            {
                LogManager.LogError("Bedrock Dedicated Server process is not tracked properly.  Unable to continue");
                return;
            }

            var loggingLeadIn = userSentCommand ? LoggingLeadIn.UserSentMessage : LoggingLeadIn.SystemInfo;
            LogManager.LogInformation($"{loggingLeadIn} {command}");
            await bdsProcess.StandardInput.WriteAsync($"{command}\n");
        }

        internal static async Task<bool> StartAsync()
        {
            if (string.IsNullOrWhiteSpace(Settings.CurrentSettings.BedrockDedicateServerDirectoryPath))
            {
                // TODO : Consider loading the Settings form if this value is not present.

                LogManager.LogError("Unable to auto start Bedrock Dedicated Server as the path to bedrock_server.exe has not yet been specified.  Please check and update your BDS Manager settings.");
                return false;
            }


            bool newProcessStarted = ProcessManager.StartProcess(ProcessName.BedrockDedicatedServer, _hardcodedBdsExePath, string.Empty);

            if (!newProcessStarted)
            {
                await SendCommandAsync("start");
            }

            return true;
        }

        internal static async Task StopAsync()
        {
            await SendCommandAsync("stop");
        }
    }
}
