using System.Threading.Tasks;
using HomeAssistantGenerated;

namespace FamBrok.Apps;

[NetDaemonApp]
public class DoorbellNotification
{
    public DoorbellNotification(Entities entities, ILogger<DoorbellNotification> logger, Services services)
    {
        entities.Switch.Deurbel.StateChanges()
            .Where(e => e.New?.State == "on")
            .Subscribe(_ =>
            {

                entities.Camera.Voordeur.Snapshot("/media/snapshots/voordeur/latest.jpg");

                logger.LogInformation("Kasper is not home, sending notification.");
                services.Notify.MobileAppSmA556b(
                    message: "Deurkbel gaat, er is iemand bij de voordeur",
                    title: "Deurbel Waarschuwing",
                    data:
                    new
                    {
                        entity_id = entities.Camera.Voordeur.EntityId,
                        image = "/media/local/snapshots/voordeur/latest.jpg",
                    }
                );

                entities.Switch.Deurbel.TurnOff();
            });
    }
}