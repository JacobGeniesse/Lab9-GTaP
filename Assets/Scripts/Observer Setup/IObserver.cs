using UnityEngine;

public interface IObserver
{
    //Func to call by the notify function on the observed script
    public void ListenTrigger(IScore masterScore);
}
