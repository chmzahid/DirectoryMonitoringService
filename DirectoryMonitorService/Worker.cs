namespace DirectoryMonitorService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private FileSystemWatcher _watcher;
        private readonly string _watchPath = @"C:\WatchedFolder";
        private readonly string _logFile = @"C:\WatchedFolder\log.txt";

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is starting.");

            if (!Directory.Exists(_watchPath))
                Directory.CreateDirectory(_watchPath);

            _watcher = new FileSystemWatcher
            {
                Path = _watchPath,
                Filter = "*.*",
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime
            };

            _watcher.Created += OnCreated;
            _watcher.EnableRaisingEvents = true;

            return base.StartAsync(cancellationToken);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            string message = $"New file created: {e.Name} at {DateTime.Now}";
            _logger.LogInformation(message);

            try
            {
                File.AppendAllText(_logFile, $"{DateTime.Now:G}: {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to log to file: {ex.Message}");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // No long-running logic; watcher handles events.
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is stopping.");
            _watcher.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
