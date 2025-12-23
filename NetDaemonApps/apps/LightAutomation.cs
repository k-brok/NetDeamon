using System.Threading.Tasks;
using HomeAssistantGenerated;

namespace FamBrok.Apps;

[NetDaemonApp]
public class ZonsOndergang
{
    public ZonsOndergang(Entities entities, ILogger<ZonsOndergang> logger)
    {
        entities.Sun.Sun.StateChanges().Where(e => e.New?.State != e.Old?.State)
            .Subscribe(async _ =>
            {
                if(entities.Sun.Sun.State == "below_horizon")
                {
                    entities.Light.ShellyVoortuinLamp.TurnOn();
                    if(entities.InputBoolean.Kerstverlichting.State == "On")
                    {
                        entities.Switch.ShellyVoortuinStopcontact.TurnOn();
                        entities.Light.Kerstboom.TurnOn();
                    }
                }
                else
                {
                    entities.Light.ShellyVoortuinLamp.TurnOff();
                    if(entities.InputBoolean.Kerstverlichting.State == "On")
                    {
                        entities.Switch.ShellyVoortuinStopcontact.TurnOff();
                        entities.Light.Kerstboom.TurnOff();
                    }
                }
                
            });
    }
}