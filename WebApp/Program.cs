using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.Events;
using System.Diagnostics;

var rng = new Random();
var getPerson = () => {
   return new {
        Id = Guid.NewGuid(),
        Name = "Some User!",
        Age = rng.Next(18,81)
   };
};

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());
var app = builder.Build();

var path = "C:\\Users\\adnan\\LogFiles\\Application\\log.txt";
using var log = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.MSSqlServer(
            connectionString: app.Configuration.GetConnectionString("Default")
            ,sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs",AutoCreateSqlTable = true })
        .WriteTo.File(
        path, 
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10 * 1024 * 1024,
        retainedFileCountLimit: 2,
        rollOnFileSizeLimit: true,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(1))
        .CreateLogger();
//Serilog.Debugging.SelfLog.Enable(msg =>
//{
//    Debug.Print(msg);
//    Debugger.Break();
//});
//Serilog.logger = log;
app.UseSerilogRequestLogging(opt => {    
    opt.Logger = log;
});
// app.UseSerilog();
app.MapGet("/", () => {
    log.Information("Passed on Hello world endpoint!");
    return "Hello World!";
});
app.MapGet("/date",() => DateTime.Now.ToString());
app.MapGet("/person",() => Results.Ok(getPerson()));
app.Run();
