// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names
using System.Threading.Tasks;
using HomeAssistantGenerated;

namespace HassModel;

/// <summary>
///     Hello world showcase using the new HassModel API
/// </summary>
[NetDaemonApp]
public class LightsAutoOffAppApp
{
    public LightsAutoOffAppApp(Entities entities)
    {
        entities.Switch.ShellyToiletLamp.StateChanges().Where(e => e.New?.State != e.Old?.State)
            .Subscribe(async _ =>
            {
                if (entities.Light.ShellyToiletLamp.State == "on")
                {
                    await Task.Delay(TimeSpan.FromMinutes(5));

                    if (entities.Light.ShellyToiletLamp.State == "on")
                    {
                        entities.Light.ShellyToiletLamp.TurnOff();
                    }
                }
            });
        
        entities.Switch.ShellyTrapkastLamp.StateChanges().Where(e => e.New?.State != e.Old?.State)
            .Subscribe(async _ =>
            {
                if (entities.Light.ShellyTrapkastLamp.State == "on")
                {
                    await Task.Delay(TimeSpan.FromMinutes(5));

                    if (entities.Light.ShellyTrapkastLamp.State == "on")
                    {
                        entities.Light.ShellyTrapkastLamp.TurnOff();
                    }
                }
            });
    }
}