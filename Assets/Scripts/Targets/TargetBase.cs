using UnityEngine;

public class TargetBase : MonoBehaviour
{
    [SerializeField] private float targetSpeed;
    [SerializeField] private float pointValue;
    [SerializeField] private Sprite targetSprite;

    public void SetSpeed(float speed)
    {
        targetSpeed = speed;
    }

    public void SetPoints(float points)
    {
        pointValue = points;
    }

    public void SetSprite(Sprite sprite)
    {
        targetSprite = sprite;
    }
}
