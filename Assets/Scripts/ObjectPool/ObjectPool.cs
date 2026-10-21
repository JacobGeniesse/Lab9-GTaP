using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance; //singleton instance

    private List<GameObject> pooledObjects = new List<GameObject>(); //List of pooled objects

    //Setting up the singleton instance
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log($"Object pool instance already exists! Duplicate Object pool on {this.gameObject}");
            Destroy(this);
        }
    }

    //Create function, returns the game object in case something calling it needs to reference it immediately upon creation
    public GameObject Create(GameObject newObj, Vector3 origin)
    {
        if(pooledObjects.Find(x => newObj))
        {
            //If there is a ready object in the pool pull it and create the game object
            int objLoc = pooledObjects.FindIndex(x => newObj); //Find the index of the pooled object
            //Debug.Log(objLoc); Debug func to check if it works
            GameObject spawnedObject = pooledObjects[objLoc]; //Create reference to that pooled object
            spawnedObject.SetActive(true); //Set the object to active again
            spawnedObject.transform.position = origin; //Set the pooled object's position to the origin
            pooledObjects.RemoveAt(objLoc); //Remove the pooled object from the pool
            return spawnedObject; //Return the spawned object
        }
        else
        {
            //If there isn't a ready object in the object pool, instantiate a new one
            GameObject newbie = Instantiate(newObj, origin, Quaternion.identity); //Instantiate the object
            return (newbie); //return reference to the object
        }
    }

    //Destroy func
    public void Destroy(GameObject returnedObj)
    {
        pooledObjects.Add(returnedObj); //Add the object to the object pool
        returnedObj.SetActive(false); //Set it to be inactive
    }
}
