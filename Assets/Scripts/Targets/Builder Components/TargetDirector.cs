using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class TargetDirector : MonoBehaviour
{
    [Header("Spawning Settings")]
    [SerializeField] private float initialDelay = 1f; //How long before the first target is spawned
    [SerializeField] private float continuousDelay = 2f; //How long between each individual target is spawned


    //List of target classes which defines the types of targets which can be built
    [SerializeField] private List<TargetClass> targetTypes = new List<TargetClass>();


    //===Old method for assigning target variables===
    //[Header("Fast Target Settings")]
    //[SerializeField] private float fastSpeed;
    //[SerializeField] private float fastPoints;
    //[SerializeField] private Sprite fastSprite;

    //[Header("Standard Target Settings")]

    //[SerializeField] private float standardSpeed;
    //[SerializeField] private float standardPoints;
    //[SerializeField] private Sprite standardSprite;

    public TargetBuilder builder; //Reference to the builder script

    private void Start()
    {
        builder = GetComponent<TargetBuilder>(); //Get reference to the builder script

        InvokeRepeating("SpawnTarget", initialDelay, continuousDelay); //Infinitely create new targets for the player to hit
    }

    //Func to spawn new targets
    private void SpawnTarget()
    {
        //Pick a random number 0-6
        int targetType = Random.Range(0, 7);
        builder.Create(); //Spawn the target

        //If the a 6 is rolled spawn a fast target
        if(targetType > 5)
        {
            BuildFastTarget();
        }
        else
        {
            //If any other number is rolled spawn a standard target
            BuildStandardTarget();
        }
    }

    //Func for setting the attributes of a fast target
    public void BuildFastTarget()
    {
        this.builder.BuildSpeed(targetTypes[1].targetSpeed); //Use the builder's speed definition function
        this.builder.BuildPoints(targetTypes[1].pointValue); //Use the builder's points definition function
        this.builder.BuildSprite(targetTypes[1].targetSprite); //Use the builder's sprite definition function
    }

    //Func for setting the attributes of a standard target
    public void BuildStandardTarget()
    {
        this.builder.BuildSpeed(targetTypes[0].targetSpeed); //Use the builder's speed definition function
        this.builder.BuildPoints(targetTypes[0].pointValue); //Use the builder's points definition function
        this.builder.BuildSprite(targetTypes[0].targetSprite); //Use the builder's points definition function
    }
}
