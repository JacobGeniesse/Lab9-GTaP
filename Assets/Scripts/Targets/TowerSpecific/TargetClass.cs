using UnityEngine;

[System.Serializable]
public class TargetClass
{
    [SerializeField] public string targetType; //Target name
    [SerializeField] public float targetSpeed; //Target speed
    [SerializeField] public float pointValue; //point value of target
    [SerializeField] public Sprite targetSprite; //Sprite tied to target
}
