using System.Reflection;
using Microsoft.Extensions.Hosting;
using NetDaemon.Extensions.Logging;
using NetDaemon.Extensions.Scheduler;
using NetDaemon.Extensions.Tts;
using NetDaemon.Runtime;
using HomeAssistantGenerated;
using FamBrok.Apps;
using NetDaemon.AppModel;

#pragma warning disable CA1812

try
{
    await Host.CreateDefaultBuilder(args)
        .UseNetDaemonAppSettings()
        .UseNetDaemonDefaultLogging()
        .UseNetDaemonRuntime()
        .UseNetDaemonTextToSpeech()
        .ConfigureServices((_, services) =>
            services
                .AddAppsFromAssembly(Assembly.GetExecutingAssembly())
                .AddNetDaemonStateManager()
                .AddNetDaemonScheduler()
                .AddHomeAssistantGenerated()
                .AddNetDaemonApp("Toilet10min",LightsAutoOffApp.Toilet10min)
                .AddNetDaemonApp("Trapkast2min",LightsAutoOffApp.Trapkast2min)
                .AddNetDaemonApp("SwitchOverloop",BarSwitchSetup.SwitchOverloop)
                .AddNetDaemonApp("SwitchSerre",BarSwitchSetup.SwitchSerre)
        )
        .Build()
        .RunAsync()
        .ConfigureAwait(false);
}
catch (Exception e)
{
    Console.WriteLine($"Failed to start host... {e}");
    throw;
}