namespace MinecraftBdsManager.Configuration
{
    internal class MapSettings
    {
        public bool EnableMapGeneration { get; set; } = false;

        public int MapGenerationIntervalInMinutes { get; set; } = 90;

        public string PapyrusExePath { get; set; } = string.Empty;

        public string PapyrusOutputPath { get; set; } = string.Empty;

        public string PapyrusExeArguments { get; set; } = "-w $WORLD_PATH -o $OUTPUT_PATH --htmlfile index.html -f png -q -1 --deleteexistingupdatefolder";

        public string[] PapyrusTasks { get; set; } = new string[] { "--dim 0", "--dim 1", "--dim 2" };
    }
}
