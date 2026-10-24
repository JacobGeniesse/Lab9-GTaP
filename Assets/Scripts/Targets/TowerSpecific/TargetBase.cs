using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetBase : MonoBehaviour, IObserver
{
    [HideInInspector] public float targetSpeed; //Speed of the target
    [HideInInspector] public float pointValue; //Point value of the target
    [HideInInspector] public Sprite targetSprite; //Target's sprite

    private ScoreTrigger deathIncrement; //Observer death function

    private int movementMod = 1; //Horizontal Direction the enemy is moving

    private float horizontalScreenLimit = 10f; //Limits of the screen before the target bounces
    private float verticalScreenLimit = 6f; //Vertical limit before the target bounces

    //Upon being enabled add this function to the list of observers on the death incrementing function
    private void OnEnable()
    {
        deathIncrement = GetComponent<ScoreTrigger>();
        if (deathIncrement != null)
        {
            deathIncrement.StartListening(this);
        }

        if (ScoreManager.instance != null)
        {
            deathIncrement.StartListening(ScoreManager.instance);
        }
    }

    //On disable remove this function from that list to avoid crashes and other issues
    private void OnDisable()
    {
        if (deathIncrement != null)
        {
            deathIncrement.StopListening(this);
        }

        if (ScoreManager.instance != null)
        {
            deathIncrement.StopListening(ScoreManager.instance);
        }
    }


    private void Update()
    {
        transform.Translate(new Vector3(1 * movementMod, 0, 0) * Time.deltaTime * targetSpeed); //Move the target down the row

        //If the target hits the horizontal edge of the screen shift it down by one and reverse its movement direction
        if (transform.position.x > horizontalScreenLimit || transform.position.x < -horizontalScreenLimit)
        {
            movementMod *= -1; //Reverse move direction
            //Shift target down and to the opposite direction by 1 (Horizontal movement is done to prevent a bug with the faster targets)
            transform.position = new Vector3(transform.position.x + 1 * movementMod, transform.position.y - 1, transform.position.z);
        }

        //If the target hits the lowest edge of the screen delete it without awarding points
        if (transform.position.y <= -verticalScreenLimit)
        {
            Destroy(this.gameObject);
        }
    }

    //Set speed func
    public void SetSpeed(float speed)
    {
        targetSpeed = speed;
    }

    //Set point value func
    public void SetPoints(float points)
    {
        pointValue = points;
    }

    //Func for setting the sprite of the object
    public void SetSprite(Sprite sprite)
    {
        targetSprite = sprite;
        SpriteRenderer newSprite = GetComponentInChildren<SpriteRenderer>();
        newSprite.sprite = targetSprite;
    }

    //Function for incrementing point values which can be triggered via observer pattern
    public void ListenTrigger(IScore masterScore)
    {
        //Debug.Log("Adding Score"); //Debug Message
        ScoreManager.instance.UpdateScore(pointValue); //Update the point value, done here due to this being where the point value is stored
    }
}
