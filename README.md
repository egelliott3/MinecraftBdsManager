# Minecraft Bedrock Server Manager
This is my version of a manager for the Bedrock Dedicated Server (BDS) for Minecraft Bedrock.  It requires [.NET 6.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) to work properly.

Users who wish to self host their own BDS instance can use this manager as a simple front end for starting and stopping their server or take advantage of the various features of the tool.

The MinecraftBdsManager supports the following options, via the settings.json:
- Automatic backups on an interval
  - Including online and offline backups
    - Online backups can be set to only run if users are online/login
  - Backup expiration options
 
 - Automatically restart BDS
   - This can be done at specific, scheduled times or on a fixed interval
   
 - Supports writing BDS logs to a log file
   - A new log file is created on every server restart
   - Option to automatically delete log files that are over N days old
 
 - Supports map generation using Bedrock supported map generators like [papyrus](https://github.com/papyrus-mc/papyruscs)
   - Can be setup similar to backups where they can be generated on an interval and/or only when players have been online
