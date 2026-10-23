using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetBase : MonoBehaviour, IObserver
{
    [SerializeField] public float targetSpeed;
    [SerializeField] public float pointValue;
    [SerializeField] public Sprite targetSprite;

    private ScoreTrigger deathIncrement;

    private int movementMod = 1;

    private float horizontalScreenLimit = 10f; //Limits of the screen before the target bounces
    private float verticalScreenLimit = 6f; //Vertical limit before the target bounces

    private void Start()
    {
    }

    private void OnEnable()
    {
        deathIncrement = GetComponent<ScoreTrigger>();
        if (deathIncrement != null)
        {
            deathIncrement.Attach(this);
        }
    }

    private void OnDisable()
    {
        if (deathIncrement != null)
        {
            deathIncrement.Detach(this);
        }
    }


    private void Update()
    {
        transform.Translate(new Vector3(1 * movementMod, 0, 0) * Time.deltaTime * targetSpeed);

        if (transform.position.x > horizontalScreenLimit || transform.position.x < -horizontalScreenLimit)
        {
            movementMod *= -1;
            transform.position = new Vector3(transform.position.x + 1 * movementMod, transform.position.y - 1, transform.position.z);
        }
        if (transform.position.y <= -verticalScreenLimit)
        {
            Destroy(this.gameObject);
        }
    }


    public void SetSpeed(float speed)
    {
        targetSpeed = speed;
    }

    public void SetPoints(float points, Score score)
    {
        pointValue = points;
        deathIncrement.Attach(score);
    }

    public void SetSprite(Sprite sprite)
    {
        targetSprite = sprite;
        SpriteRenderer newSprite = GetComponentInChildren<SpriteRenderer>();
        newSprite.sprite = targetSprite;
    }

    public void ListenTrigger(IScore masterScore)
    {
        Debug.Log("Adding Score");
        Score.instance.UpdateScore(pointValue);
    }
}
