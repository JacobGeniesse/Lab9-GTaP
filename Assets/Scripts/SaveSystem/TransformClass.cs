using UnityEngine;

[System.Serializable]

//Class for holding transform data
public class TransformClass
{
    [SerializeField] public Vector3 localPosition; //storing local position
    [SerializeField] public Quaternion localRotation; //storing local rotation
    [SerializeField] public Vector3 localScale; //storing local scale

    //Creation method for a transform class, setting the position, rotation, and scale
    public TransformClass(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        localPosition = position;
        localRotation = rotation;
        localScale = scale;
    }
}
