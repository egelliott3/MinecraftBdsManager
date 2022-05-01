using System.Text.Json;
using System.Text.Json.Serialization;

namespace MinecraftBdsManager.Configuration
{
    internal class Settings
    {
        private static Settings? _instance;
        private static readonly JsonSerializerOptions _serializedOptions = new() { WriteIndented = true };
        private static readonly string _settingsFileName = "settings.json";
        private static readonly string _settingsFilePath = Path.Combine(Application.StartupPath, _settingsFileName);

        /// <summary>
        /// Static construction that is necessary to finish setting up the JsonSerializerOptions before they get used
        /// </summary>
        static Settings()
        {
            // The TimeOnly type, which is used in some of the settings is not yet supported by System.Text.Json natively so we have to build our own converter in the meantime
            _serializedOptions.Converters.Add(new TimeOnlyJsonConverter());
        }

        public Settings()
        {
            BackupSettings = new BackupSettings();
            LoggingSettings = new LoggingSettings();
            MapSettings = new MapSettings();
            RestartSettings = new RestartSettings();
        }

        /// <summary>
        /// Automatically start the BedrockDedicatedServer when MinecraftBdsManager is done loading.
        /// </summary>
        public bool AutoStartBedrockDedicatedServer { get; set; } = true;

        /// <summary>
        /// Path to the directory containing the files for the Bedrock Dedicated Server
        /// </summary>
        public string BedrockDedicateServerDirectoryPath { get; set; } = string.Empty;

        /// <summary>
        /// Settings that govern if and when world backups are taken.
        /// </summary>
        public BackupSettings BackupSettings { get; set; }

        /// <summary>
        /// Current instance of the settings that are in use
        /// </summary>
        [JsonIgnore]
        public static Settings CurrentSettings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = LoadSettings();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Settings that govern if and how logs will be written to the file system.
        /// </summary>
        public LoggingSettings LoggingSettings { get; set; }

        /// <summary>
        /// Settings that govern if and how map files will be generated of the world.
        /// </summary>
        public MapSettings MapSettings { get; set; }

        /// <summary>
        /// Settings that govern if and how the server is restarted automatically on an internval, schedule or as a result of a crash.
        /// </summary>
        public RestartSettings RestartSettings { get; set; }

        /// <summary>
        /// Read settings from the existing settings.json file or create a new one if it has not yet been generated (or is missing)
        /// </summary>
        /// <returns>The loaded instance of the Settings object, whether read from file or newly generated.</returns>
        public static Settings LoadSettings()
        {
            // Check if the settings file exists...
            if (File.Exists(_settingsFilePath))
            {
                // ...if it does then load the settings from the file
                try
                {
                    _instance = JsonSerializer.Deserialize<Settings>(File.ReadAllText(_settingsFilePath), _serializedOptions);
                }
                catch
                {
                    // Catch an suppress any "invalid file" such issues and just revert to a default instance
                }
            }

            // If the instance is still null (most likely because the settings file was missing or invalid), create a new instance from scratch
            if (_instance == null)
            {
                _instance = new Settings();
                // Write out the settings since they were missing either due to first run or file being deleted.  Avoid having write
                //  load up the settings again to avoid an infinite loop
                _instance.WriteSettings(loadSettingsAfterWrite: false);
            }

            return _instance;
        }

        /// <summary>
        /// Writes the settings to file from memory.
        /// </summary>
        /// <param name="loadSettingsAfterWrite">Optional.  Should generally be left at default.  Internally used flag to allow the WriteSettings method to skip reloading settings to avoid deadlock/looping issues.  </param>
        public void WriteSettings(bool loadSettingsAfterWrite = true)
        {
            // Persist the settings to disk
            using (var fs = new FileStream(_settingsFilePath, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, this, _serializedOptions);
            }

            if (loadSettingsAfterWrite)
            {
                // Then load them again to ensure we have the latest options active
                LoadSettings();
            }
        }
    }
}
