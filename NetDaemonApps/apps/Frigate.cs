using System.Text.Json;
using System.Threading.Tasks;
using HomeAssistantGenerated;

namespace FamBrok.Apps;

public class FrigateSetup
{
    private static DateTime _lastNotificationTime = DateTime.MinValue;
    public static void MotionNotificationVoorduer(IHaContext ha, ILogger<FrigateSetup> logger, Services services, Entities entities)
    {
        ha.Events
            .Where(e => e.EventType == "mobile_app_notification_action")
            .Subscribe(e =>
            {
                logger.LogInformation("Received event: " + e.DataElement.ToString());

                try
                {
                    var data = JsonSerializer.Deserialize<MobileAppEventData>(e.DataElement.ToString()!);
                    if (data == null)
                    {
                        logger.LogWarning("Kon eventdata niet parsen.");
                        return;
                    }

                    if (data.Action == "ALARM")
                    {
                        logger.LogInformation("Sounding alarm due to Frigate motion detection");
                        //entities.Switch.AlarmSirenTurnOn.TurnOn();
                    }
                    else
                    {
                        logger.LogInformation($"Geen alarmactie uitgevoerd (action = {data.Action}).");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error processing event data");
                }
            });
        

        entities.BinarySensor.VoordeurPersonOccupancy.StateChanges()
            .Where(e => e.New?.State == "on" && e.Old?.State != e.New?.State)
            .Subscribe(_ =>
            {
                if (DateTime.UtcNow - _lastNotificationTime < TimeSpan.FromMinutes(5))
                {
                    logger.LogInformation("Cooldown actief, geen nieuwe notificatie verzonden.");
                    return;
                }

                entities.Camera.Voordeur.Snapshot("/media/snapshots/voordeur/latest.jpg");

                _lastNotificationTime = DateTime.UtcNow;

                if (entities.Person.KasperBrok.State != "home")
                {
                    logger.LogInformation("Kasper is not home, sending notification.");
                    services.Notify.MobileAppSmA556b(
                        message: "Persoon gedetecteerd bij voordeur",
                        title: "Frigate Waarschuwing",
                        data:
                        new
                        {
                            entity_id = entities.Camera.Voordeur.EntityId,
                            image = "/media/local/snapshots/voordeur/latest.jpg",
                            actions = new[]
                            {
                                new
                                {
                                    action = "ALARM",
                                    title = "Alarm",
                                }
                            }
                        }
                    );
                }

                if (entities.Person.ChristelStravers.State != "home")
                {
                    logger.LogInformation("Christel is not home, sending notification.");
                    services.Notify.MobileAppXqCc54(
                        message: "Persoon gedetecteerd bij voordeur",
                        title: "Frigate Waarschuwing",
                        data:
                        new
                        {
                            entity_id = entities.Camera.Voordeur.EntityId,
                            image = "/media/local/snapshots/voordeur/latest.jpg",
                            actions = new[]
                            {
                                new
                                {
                                    action = "ALARM",
                                    title = "Alarm",
                                }
                            }
                        }
                    );
                }
            });
    }
}