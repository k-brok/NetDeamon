using System.Threading.Tasks;
using HomeAssistantGenerated;

namespace FamBrok.Apps;

[NetDaemonApp]
public class LightsAutoOffApp
{
    public LightsAutoOffApp(Entities entities)
    {
        entities.Switch.ShellyToiletLamp.StateChanges().Where(e => e.New?.State != e.Old?.State)
            .Subscribe(async _ =>
            {
                if (entities.Light.ShellyToiletLamp.State == "on")
                {
                    await Task.Delay(TimeSpan.FromMinutes(10));

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
                    await Task.Delay(TimeSpan.FromMinutes(2));

                    if (entities.Light.ShellyTrapkastLamp.State == "on")
                    {
                        entities.Light.ShellyTrapkastLamp.TurnOff();
                    }
                }
            });
    }
}