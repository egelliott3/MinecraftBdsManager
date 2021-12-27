namespace MinecraftBdsManager.Configuration
{
    internal class MapSettings
    {
        public bool EnableMapGeneration { get; set; } = false;

        public int MapGenerationIntervalInMinutes { get; set; } = 90;

        public string MapperExePath { get; set; } = string.Empty;

        public string MapperOutputPath { get; set; } = ".\\Maps";

        public string MapperExeArguments { get; set; } = "-w $WORLD_PATH -o $OUTPUT_PATH --htmlfile index.html -f png -q -1 --deleteexistingupdatefolder";

        public string[] MapperExeArgumentVariations { get; set; } = new string[] { "--dim 0", "--dim 1", "--dim 2" };
    }
}
