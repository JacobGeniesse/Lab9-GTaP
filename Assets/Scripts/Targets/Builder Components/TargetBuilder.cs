using UnityEditor;
using UnityEngine;

public class TargetBuilder : MonoBehaviour, IBuilder
{
    [SerializeField] public GameObject newTarget;  //The target prefab
    private TargetBase targetBase; //TargetBase base class to modify

    [SerializeField] private Vector3 spawnPoint; //Spawn point for the targets

    //Function for building a new target
    public GameObject Create()
    {
        //If a the prefab is valid
        if(newTarget != null)
        {
            GameObject newbie = Instantiate(newTarget, spawnPoint, Quaternion.identity); //Instantiate a new target
            newbie.name = newTarget.name;
            this.targetBase = newbie.GetComponent<TargetBase>(); //Grab its target base component
            return newbie;
        }
        else
        {
            Debug.LogWarning("newTarget not assigned!"); //Pass a warning if the prefab isn't valid
            return null;
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

    public void SetID(string tag)
    {
        this.targetBase.SetID(tag);
    }


}
