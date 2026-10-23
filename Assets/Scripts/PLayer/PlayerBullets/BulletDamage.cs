using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    //Code modified from the refactored lab 4 scripts

    //[SerializeField] private float damageAmount; //Amount of damage to deal to the meteor

    //If entering a trigger's hitbox, deal damage
    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        //Check if the object is a meteor
        if (whatIHit.tag == "Target" && whatIHit.TryGetComponent<ScoreTrigger>(out ScoreTrigger trigger))
        {
            //Trigger the score listener
            trigger.Notify();
            ObjectPool.instance.Destroy(this.gameObject); //After doing damage destroy this game object
        }
    }
}
