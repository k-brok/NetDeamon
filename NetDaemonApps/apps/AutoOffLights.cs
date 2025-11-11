// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names
using System.Threading.Tasks;
using HomeAssistantGenerated;

namespace FamBrok.Apps;

public static class LightsAutoOffApp
{
    public static void Toilet10min(Entities entities)
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
    }

    public static void Trapkast2min(Entities entities)
    {
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