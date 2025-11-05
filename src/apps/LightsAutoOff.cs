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
        entities.Switch.WcLampSwitch0.StateChanges().Where(e => e.New?.State == "on")
            .Subscribe(async _ =>
            {
                await Task.Delay(TimeSpan.FromMinutes(5));
                
                if(entities.Light.Wclamp.State == "on")
                {
                    entities.Light.Wclamp.TurnOff();
                }
            });
    }
}