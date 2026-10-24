using UnityEngine;

public interface IBuilder
{
    //Function declaration for defining the speed of a target
    void BuildSpeed(float speed);

    //Function declaration for defining the point value of a target
    void BuildPoints(float points);

    //Function declaration for choosing the sprite of a target
    void BuildSprite(Sprite sprite);
}
