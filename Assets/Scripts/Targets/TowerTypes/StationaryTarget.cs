using UnityEngine;

public class StationaryTarget : TargetBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(targetSprite != null)
        {
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.sprite = targetSprite;
        }
        transform.position = new Vector3(Random.Range(-6, 6), Random.Range(0, 4), 0);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
