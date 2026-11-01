using System.Collections.Generic;
using UnityEngine;

public class TransformSaver : MonoBehaviour, ISaveable
{
    [Tooltip("Key referenced in the save data. DO NOT EDIT DURING RUNTIME")]
    [SerializeField] public string LOCAL_TRANSFORM_KEY = "localTrans";

    [SerializeField, HideInInspector] private TransformClass transformData; //Saved transform data

    //Create the transform data by setting the variables within to equal their pratcial counterparts
    public TransformClass SaveID {
        
        get 
        {
            transformData.localPosition = transform.localPosition;
            transformData.localRotation = transform.localRotation;
            transformData.localScale = transform.localScale;
            return transformData;
        }
    }

    //Create a dictionary entry with the LOCAL_TRANSFORM_KEY as the key, and the SaveID as the value within
    public Dictionary<string, TransformClass> SavedData
    {
        get
        {
            var result = new Dictionary<string, TransformClass>(); //Make a dictionary for storing the data
            result.Add(LOCAL_TRANSFORM_KEY, SaveID); //Add the result to the stored dictionary
            //Debug.Log(result[LOCAL_TRANSFORM_KEY]); //Debug message
            return result; //return the created dictionary
        }
    }

    //Func used by enemy builder to determine what variety of enemy is being saved
    public void SetID(string id)
    {
        LOCAL_TRANSFORM_KEY = id;
    }

}
