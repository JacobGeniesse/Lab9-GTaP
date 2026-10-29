using UnityEngine;

[System.Serializable]
public class TransformClass
{
    [SerializeField] public Vector3 localPosition;
    [SerializeField] public Quaternion localRotation;
    [SerializeField] public Vector3 localScale;

    public TransformClass(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        localPosition = position;
        localRotation = rotation;
        localScale = scale;
    }
}
