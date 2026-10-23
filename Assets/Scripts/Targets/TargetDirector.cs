using UnityEngine;

public class TargetDirector : MonoBehaviour
{
    [Header("Spawning Settings")]
    [SerializeField] private float initialDelay = 1f;
    [SerializeField] private float continuousDelay = 2f;


    [Header("Fast Target Settings")]
    [SerializeField] private float fastSpeed;
    [SerializeField] private float fastPoints;
    [SerializeField] private Sprite fastSprite;

    [Header("Standard Target Settings")]

    [SerializeField] private float standardSpeed;
    [SerializeField] private float standardPoints;
    [SerializeField] private Sprite standardSprite;

    public TargetBuilder builder;

    //public TargetBuilder Builder
    //{
    //    set { builder = value; }
    //}

    private void Start()
    {
        builder = GetComponent<TargetBuilder>();

        InvokeRepeating("SpawnTarget", initialDelay, continuousDelay);
    }


    private void SpawnTarget()
    {

        int targetType = Random.Range(0, 7);
        builder.Reset();

        if(targetType > 5)
        {
            BuildFastTarget();
        }
        else
        {
            BuildStandardTarget();
        }
    }



    public void BuildFastTarget()
    {
        this.builder.BuildSpeed(fastSpeed);
        this.builder.BuildPoints(fastPoints);
        this.builder.BuildSprite(fastSprite);
    }

    public void BuildStandardTarget()
    {
        this.builder.BuildSpeed(standardSpeed);
        this.builder.BuildPoints(standardPoints);
        this.builder.BuildSprite(standardSprite);
    }
}
