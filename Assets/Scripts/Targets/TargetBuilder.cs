using UnityEngine;

public class TargetBuilder : MonoBehaviour, IBuilder
{
    private TargetBase targetBase;

    public TargetBuilder()
    {
        this.Reset();
    }

    public void Reset()
    {
        this.targetBase = new TargetBase();
    }

    public void BuildSpeed(float speed)
    {
        this.targetBase.SetSpeed(speed);
    }

    public void BuildPoints(float points)
    {
        this.targetBase.SetPoints(points);
    }

    public void BuildSprite(Sprite sprite)
    {
        this.targetBase.SetSprite(sprite);
    }
}
