using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f; //How fast the player can move
    private float horizontalScreenLimit = 10f; //Limits of the screen before the player is warped
    private float verticalScreenLimit = 6f; //Vertical limit before the player is warped

    private bool startMoving = false; //Is the player trying to move

    [SerializeField] private InputActionAsset inputList; //input master list ref
    private InputAction move; //input action for moving


    private void OnEnable()
    {
        //Try to assign move input value, if this fails throw an error
        try
        {
            move = inputList["Move"];
        }
        catch
        {
            throw new ArgumentException("Unable to find inputList", nameof(PlayerMovement));
        }

        //If move isn't null add start move and end move to .started, and .canceled respectively
        if (move != null)
        {
            move.started += StartMove;
            move.canceled += EndMove;
        }
    }

    private void OnDisable()
    {
        //If the player object is disabled, remove these two funcs from their respective points to avoid an error
        if (move != null)
        {
            move.started -= StartMove;
            move.canceled -= EndMove;
        }
    }

    void Update()
    {
        //While a movement key is pressed update the movement function
        if (startMoving == true)
        {
            Movement();
        }
    }

    void Movement()
    {
        //Get movement input
        Vector3 moveInput = move.ReadValue<Vector2>();

        //Move the player by the move input times time.deltatime and player speed
        transform.Translate(new Vector3(moveInput.x, 0, 0) * Time.deltaTime * speed);

        //If reaching certain points warp the player to the other side of a screen position
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1f, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    //Start listening for the movement values
    void StartMove(InputAction.CallbackContext context)
    {
        startMoving = true;
    }

    //Stop listening for the movement values
    void EndMove(InputAction.CallbackContext context)
    {
        startMoving = false;
    }
}
