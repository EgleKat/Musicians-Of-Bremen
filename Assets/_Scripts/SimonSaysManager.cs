using System.Threading;
using System;
using System.Threading.Tasks;

public class SimonSaysManager
{
    public enum ReturnType { MistakeMade, Success };

    private int numberOfButtons = 3;
    private Random rng = new Random();

    public async Task<ReturnType> Start(int rounds)
    {
        for (int round = 0; round < rounds; round++)
        {
            EventManager.TriggerEvent(EventType.StartSimonSaysRound, null);
            int[] buttonHistory = new int[round + 3];
            for (int i = 0; i < round + 3; i++)
            {
                buttonHistory[i] = rng.Next(1, numberOfButtons + 1);
                await Wait.ForSeconds(1f);
                EventManager.TriggerEvent(EventType.PlaySound, "BR_" + buttonHistory[i]);
                EventManager.TriggerEvent(EventType.StartAlertSimonSays, "BR_" + buttonHistory[i]);
                await EventManager.WaitForEvent(EventType.EndAlertSimonSays);
            }
            EventManager.TriggerEvent(EventType.StartSimonSaysRecall, null);
            for (int i = 0; i < round + 3; i++)
            {
                string nextButtonRecall = (string)await EventManager.WaitForEvent(EventType.StartAlertSimonSays);
                if (nextButtonRecall != "BR_" + buttonHistory[i])
                {
                    EventManager.TriggerEvent(EventType.EndSimonSays, null);
                    await Wait.ForSeconds(1f);
                    EventManager.TriggerEvent(EventType.PlaySound, "incorrectSimon");
                    FlashAll();

                    await Wait.ForSeconds(0.3f);
                    EventManager.TriggerEvent(EventType.PlaySound, "incorrectSimon");
                    FlashAll();


                    return ReturnType.MistakeMade;
                }
            }
            await Wait.ForSeconds(1f);
            EventManager.TriggerEvent(EventType.PlaySound, "correctSimon");

            FlashAll();
        }

        EventManager.TriggerEvent(EventType.EndSimonSays, null);

        return ReturnType.Success;
    }

    private void FlashAll()
    {
        for (int buttonNumber = 1; buttonNumber <= numberOfButtons; buttonNumber++)
        {
            EventManager.TriggerEvent(EventType.StartAlertSimonSays, "BR_" + buttonNumber);
        }
    }
}
