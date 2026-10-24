using UnityEngine;

public interface IScore
{
    //Func to attach a script to the list of observers
    void StartListening(IObserver observer);

    //Func to remove a script from the list of observers
    void StopListening(IObserver observer);

    //Func to notify the attached scripts
    void Notify();
}
