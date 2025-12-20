using System.Threading.Tasks;
using HomeAssistantGenerated;

namespace FamBrok.Apps;

[NetDaemonApp]
public class BarSwitchSetup
{
    public static void SwitchOverloop(Entities entities)
    {
        entities.BinarySensor.ShellyWoonkamerSchaklaarsBarSchakelaar1Input.StateChanges()
            .Where(e => e.New?.State != e.Old?.State)
            .Subscribe(_ =>
            {
                entities.Light.ShellyOverloopLamp.Toggle();
            });
    }
    public static void  SwitchSerre(Entities entities)
    {
        
        entities.BinarySensor.ShellyWoonkamerSchaklaarsBarSchakelaar3Input.StateChanges()
            .Where(e => e.New?.State != e.Old?.State)
            .Subscribe(_ =>
            {
                entities.Light.ShellyWoonkamerSerreLamp.Toggle();
            });
    }
}