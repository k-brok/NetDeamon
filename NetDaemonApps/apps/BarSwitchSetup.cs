using System.Threading.Tasks;
using HomeAssistantGenerated;

namespace HassModel;

[NetDaemonApp]
public class BarSwitchSetup
{
    public BarSwitchSetup(Entities entities)
    {
        entities.BinarySensor.ShellyWoonkamerSchaklaarsBarSchakelaar1Input.StateChanges()
            .Where(e => e.New?.State != e.Old?.State)
            .Subscribe(_ =>
            {
                entities.Light.ShellyOverloopLamp.Toggle();
            });
        
        entities.BinarySensor.ShellyWoonkamerSchaklaarsBarSchakelaar3Input.StateChanges()
            .Where(e => e.New?.State != e.Old?.State)
            .Subscribe(_ =>
            {
                entities.Light.ShellyWoonkamerSerreLamp.Toggle();
            });
    }
}