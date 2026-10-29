using System.Collections.Generic;
using UnityEngine;

public class TransformSaver : MonoBehaviour, ISaveable
{
    private const string LOCAL_TRANSFORM_KEY = "localTrans";

    [SerializeField] private TransformClass transformData;


    public TransformClass SaveID {
        
        get 
        {
            transformData.localPosition = transform.localPosition;
            transformData.localRotation = transform.localRotation;
            transformData.localScale = transform.localScale;
            return transformData;
        }
    }

    public Dictionary<string, TransformClass> SavedData
    {
        get
        {
            var result = new Dictionary<string, TransformClass>();
            result.Add(LOCAL_TRANSFORM_KEY, SaveID);
            return result;
        }
    }

    public void LoadFromData(Dictionary<string, TransformClass> data)
    {
        
    }


}
