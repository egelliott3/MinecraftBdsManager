namespace MinecraftBdsManager.Managers
{
    internal class ProcessResult
    {
        public ProcessResult()
        {
            ExecutablePath = string.Empty;
            ExecutableArguments = string.Empty;

            StandardOutputLines = new List<string>();
            StandardErrorLines = new List<string>();
        }

        public string ExecutablePath { get; set; }

        public string ExecutableArguments { get; set; }

        public int ExitCode { get; set; }

        public TimeSpan RunTime { get; set; }

        public List<string> StandardOutputLines { get; set; }

        public List<string> StandardErrorLines { get; set; }
    }
}
