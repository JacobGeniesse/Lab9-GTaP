using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreTrigger : MonoBehaviour, IScore
{
    private List<IObserver> listeners = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        //Debug.Log($"Added listener {observer}");
        listeners.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        if (listeners.Contains(observer))
        {
            //Debug.Log($"Removed listener {observer}");
            listeners.Remove(observer);
        }
        else
        {
            Debug.LogWarning($"{observer} not found in listener list!");
        }
    }

    public void Notify()
    {
        Debug.Log("Notified");
        foreach (IObserver observer in listeners)
        {
            observer.ListenTrigger(this);
        }
        Destroy(this.gameObject);
    }

}
