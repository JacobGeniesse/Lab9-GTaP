using UnityEditor;
using UnityEngine;

public class TargetBuilder : MonoBehaviour, IBuilder
{
    [SerializeField] public GameObject newTarget; 
    private TargetBase targetBase;
    private Score scoreManager;

    [SerializeField] private Vector3 spawnPoint;

    private void Awake()
    {
        scoreManager = GetComponent<Score>();
    }

    public void Reset()
    {
        if(newTarget != null)
        {
            GameObject newbie = Instantiate(newTarget, spawnPoint, Quaternion.identity);
            this.targetBase = newbie.GetComponent<TargetBase>();
        }
        else
        {
            Debug.LogWarning("newTarget not assigned!");
        }
    }

    public void BuildSpeed(float speed)
    {
        this.targetBase.SetSpeed(speed);
    }

    public void BuildPoints(float points)
    {
        this.targetBase.SetPoints(points, scoreManager);
    }

    public void BuildSprite(Sprite sprite)
    {
        this.targetBase.SetSprite(sprite);
    }

}
