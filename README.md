# DirectoryMonitoringService
windows service that monitor directory for any new files

# 🗂 Directory Monitor Windows Service (.NET Core)

This is a Windows service built using .NET Core Worker Service that monitors a directory and logs new file creation events.

## 📁 Monitored Directory
- Default: `C:\WatchedFolder`
- All new files created in this directory are logged to `log.txt`

## 🚀 How to Install the Service

1. **Publish the app**:
   ```bash
   dotnet publish -c Release -o ./publish



2. **Install the app**:

sc create DirectoryMonitorService binPath= "C:\full\path\to\publish\DirectoryMonitorService.exe"

3. **Start the app**:
net start DirectoryMonitorService

4. **Stop and Delete the app**:
net stop DirectoryMonitorService
sc delete DirectoryMonitorService
