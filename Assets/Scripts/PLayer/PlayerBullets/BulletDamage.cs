using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    //Code modified from the refactored lab 4 scripts

    //If an object with score trigger enters this object's trigger activate the notification script in the object and destroy this object
    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        //Check if the object is a meteor
        if (whatIHit.tag == "Target" && whatIHit.TryGetComponent<ScoreTrigger>(out ScoreTrigger trigger))
        {
            //Trigger the score listener
            trigger.Notify(); //Trigger the notification func in the script within the attached object
            ObjectPool.instance.Destroy(this.gameObject); //After triggering this script add it back to the object pool
        }
    }
}
