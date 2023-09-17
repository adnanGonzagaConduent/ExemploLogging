namespace ExampleBackgroundService;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _config;
    private int _count = 0;
    public Worker(ILogger<Worker> logger,IConfiguration configuration)
    {
        _logger = logger;
        _config = configuration;
    }
    private async Task SyncFile(){
        using var conn = new SqlConnection(_config.GetConnectionString("Default"));
        var lines = File.ReadLines("C:\\Users\\adnan\\LogFiles\\Application\\log.txt");
        var offset = _count;
        var limit = 2000;
        var ended = false;
        while (!ended)
        {
            var batch = lines.Skip(offset).Take(limit).ToArray();
            await conn.ExecuteAsync("INSERT INTO Logs (Message) VALUES(@Message)",new { 
                Message = batch
            });
        }
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var delay = TimeSpan.FromSeconds(5);
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(delay, stoppingToken);
            _logger.LogInformation("{time} -> Syncing file", DateTimeOffset.Now);
            await SyncFile();
        }
    }
}
