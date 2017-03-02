using UnityEngine;

public enum Counter
{
    NOT_SET,
    START,
    FINISH
}

public class UImanager : MonoBehaviour {
    public GameObject toDisable;
    public GameObject toEnable;
    public RapaGalbena manager;
    public Counter StopWatch;

    private void OnTriggerEnter(Collider other)
    {
        manager.updateResetPosition(gameObject.transform);
        if (toDisable != null)
        {
            manager.Disable(toDisable);
        }
        if (toEnable != null)
        {
            manager.Enable(toEnable);
        }

        if (StopWatch != Counter.NOT_SET)
        {
            if (StopWatch == Counter.START)
            {
                manager.startTimer();
            } else
            {
                manager.stopTimer();
            }
        }

    }

}
