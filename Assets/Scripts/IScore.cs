using UnityEngine;

public interface IScore
{
    void Attach(IObserver observer);

    void Detach(IObserver observer);

    void Notify();
}
