using UnityEngine;

public class TargetDirector : MonoBehaviour
{
    [Header("Fast Target Settings")]
    [SerializeField] private float fastSpeed;
    [SerializeField] private float fastPoints;
    [SerializeField] private Sprite sprite;



    private IBuilder builder;

    public IBuilder Builder
    {
        set { builder = value; }
    }

    public void BuildFastTarget()
    {
        this.builder.BuildSpeed(5);
        this.builder.BuildPoints(fastPoints);
        this.builder.BuildSprite(sprite);
    }
}
