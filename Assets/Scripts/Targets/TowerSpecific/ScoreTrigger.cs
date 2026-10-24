using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreTrigger : MonoBehaviour, IScore
{
    //List of observers who need to be notified upon this object being hit
    private List<IObserver> listeners = new List<IObserver>();

    //Func for adding a observer to the listeners list
    public void StartListening(IObserver observer)
    {
        //Debug.Log($"Added listener {observer}"); //Debug message
        listeners.Add(observer); //Add the observer to the list
    }

    //Func for removing an observer from the listeners list
    public void StopListening(IObserver observer)
    {
        if (listeners.Contains(observer))
        {
            //Debug.Log($"Removed listener {observer}"); //Debug message
            listeners.Remove(observer); //Remove the observer if the list contains this observer
        }
        else
        {
            Debug.LogWarning($"{observer} not found in listener list!"); //If the list doesn't contain the observer pass it to this section to handle the error smoothly
        }
    }

    //Func for notifying the other listeners
    public void Notify()
    {
        //Debug.Log("Notified"); //Debug Message
        foreach (IObserver observer in listeners)
        {
            observer.ListenTrigger(this); //Trigger each observer in the listeners list
        }

        /*
         * Afterwards destroy this game object due to this only triggering upon the object being hit with a projectile.
         * I avoided adding another func as a listener for this because I believe that it will be added first to the list of observers
         * making it be destroyed before the other functions can take place
         */
        Destroy(this.gameObject);
    }

}
