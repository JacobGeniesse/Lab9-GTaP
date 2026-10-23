using UnityEngine;
using static Score;

public class ScoreReward : MonoBehaviour, IObserver
{

    public delegate void TakeHit();
    public event TakeHit onHit;

    private void OnEnable()
    {
        onHit += IncrementScore;
        onHit += Die;
    }

    private void OnDisable()
    {
        onHit -= IncrementScore;
        onHit -= Die;
    }

    private void IncrementScore()
    {
        Debug.Log("+100 Points!");
    }

    private void Die()
    {
        Debug.Log("Kaput");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            onHit?.Invoke();
        }
    }

    public void ListenTrigger(IScore masterScore)
    {
        IncrementScore();
    }
}
