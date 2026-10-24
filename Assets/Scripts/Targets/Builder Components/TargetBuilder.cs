using UnityEditor;
using UnityEngine;

public class TargetBuilder : MonoBehaviour, IBuilder
{
    [SerializeField] public GameObject newTarget;  //The target prefab
    private TargetBase targetBase; //TargetBase base class to modify

    [SerializeField] private Vector3 spawnPoint; //Spawn point for the targets

    //Function for building a new target
    public void Create()
    {
        //If a the prefab is valid
        if(newTarget != null)
        {
            GameObject newbie = Instantiate(newTarget, spawnPoint, Quaternion.identity); //Instantiate a new target
            this.targetBase = newbie.GetComponent<TargetBase>(); //Grab its target base component
        }
        else
        {
            Debug.LogWarning("newTarget not assigned!"); //Pass a warning if the prefab isn't valid
        }
    }

    //Function for building the target's speed, used by the director
    public void BuildSpeed(float speed)
    {
        this.targetBase.SetSpeed(speed);
    }

    //Function for building the target's point value, used by the director
    public void BuildPoints(float points)
    {
        this.targetBase.SetPoints(points);
    }

    //Function for building the target's sprite, used by the director
    public void BuildSprite(Sprite sprite)
    {
        this.targetBase.SetSprite(sprite);
    }

}
