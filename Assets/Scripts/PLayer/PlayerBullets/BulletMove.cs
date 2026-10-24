using UnityEngine;

public class BulletMove : MonoBehaviour
{
    //Code modified from the refactored lab 4 scripts

    [SerializeField] private float bulletSpeed = 8f; //speed bullet travels
    [SerializeField] private float upperLimit = 10f; //Destruction point for laser

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * bulletSpeed); //move bullet upwards

        //If the bullet goes above a certain point return it to the object pool
        if (transform.position.y > upperLimit)
        {
            ObjectPool.instance.Destroy(this.gameObject); //If hitting upper limit destroy the laser
        }
    }
}
