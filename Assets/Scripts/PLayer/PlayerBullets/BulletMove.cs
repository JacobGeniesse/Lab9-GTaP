using UnityEngine;

public class BulletMove : MonoBehaviour
{
    //Code modified from the refactored lab 4 scripts

    [SerializeField] private float laserSpeed = 8f; //speed laser travels
    [SerializeField] private float upperLimit = 11f; //Destruction point for laser

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * laserSpeed); //move laser upwards

        if (transform.position.y > upperLimit)
        {
            ObjectPool.instance.Destroy(this.gameObject); //If hitting upper limit destroy the laser
        }
    }
}
