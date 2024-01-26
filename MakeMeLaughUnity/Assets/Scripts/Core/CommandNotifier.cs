using UnityEngine;

public abstract class CommandNotifier : MonoBehaviour
{
    public abstract void NotifyMeDown(GameObject caller);
    public abstract void NotifyMeUp(GameObject caller);
}
