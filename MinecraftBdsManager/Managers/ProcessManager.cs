using System.Collections.Concurrent;
using System.Diagnostics;


namespace MinecraftBdsManager.Managers
{
    internal class ProcessManager : IDisposable
    {
        internal static ConcurrentDictionary<ProcessName, Process?> TrackedProcesses = new ConcurrentDictionary<ProcessName, Process?>();
        private static bool disposedValue;

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        internal static void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var processEntry in TrackedProcesses)
                    {
                        // TODO : Consider a cleaner shutdown process for BDS

                        var process = processEntry.Value;

                        if (process != null && !process.HasExited)
                        {
                            // If this is the BDS process we need to be sure that we gracefully shut down the process to avoid orphans
                            //  TODO: Consider an adoption process to check for runing BDS processes and coopt it.

                            process.Kill();
                            process?.Dispose();
                        }
                    }
                }

                disposedValue = true;
            }
        }

        public static bool StartProcess(ProcessName processName, string executablePath, string arguments)
        {
            if (processName == ProcessName.Unknown)
            {
                throw new ArgumentNullException(nameof(processName));
            }

            if (string.IsNullOrWhiteSpace(executablePath))
            {
                throw new ArgumentNullException(nameof(executablePath));
            }

            if (processName == ProcessName.FireAndForget)
            {
                return StartFireAndForgetProcess(executablePath, arguments);
            }

            Trace.TraceInformation($"Starting process {processName}.");

            // Check to see if we are already tracking this process...
            if (TrackedProcesses.ContainsKey(processName) && processName != ProcessName.FireAndForget)
            {
                var process = TrackedProcesses[processName];

                // ... and if so, check if the process object is instantiated and currently running.
                if (process != null && !process.HasExited)
                {
                    // Requested process is already running so report out status and report start failure.
                    Trace.TraceWarning($"Process {processName} is already tracked and running.  Unable to start a new instance of {processName} until old instance is stopped.");
                    return false;
                }

                // ... and if so, check if the process object is instantiated and currently exited
                if (process != null && process.HasExited)
                {
                    // We will not be able to reuse this process and reconnect all of the listeners properly so dispose of this one and allow a new onw to be created.
                    process.Dispose();
                    TrackedProcesses[processName] = null;
                }
            }

            var processStartInfo = new ProcessStartInfo
            {
                FileName = executablePath,
                Arguments = arguments,
                WorkingDirectory = Path.GetDirectoryName(executablePath),
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            var newProcess = new Process
            {
                StartInfo = processStartInfo
            };

            newProcess.OutputDataReceived += NewProcess_OutputDataReceived;

            _ = TrackedProcesses.AddOrUpdate(processName, newProcess, (key, oldProcess) => newProcess);

            newProcess.Start();

            newProcess.BeginOutputReadLine();

            return true;
        }

        private static bool StartFireAndForgetProcess(string executablePath, string arguments)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = executablePath,
                Arguments = arguments,
                WorkingDirectory = Path.GetDirectoryName(executablePath),
            };

            var newProcess = new Process
            {
                StartInfo = processStartInfo
            };

            newProcess.Start();

            return true;
        }

        private static void NewProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Trace.TraceInformation(e.Data);
        }
    }
}
