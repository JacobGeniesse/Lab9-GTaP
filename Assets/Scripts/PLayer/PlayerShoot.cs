using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab; //Prefab for laser, refactored to be a private var editable from the inspector so that it is no longer editable by other scripts
    private bool canShoot = true; //Bool for if the ship can shoot

    [SerializeField] private Vector3 shootOffset; //Vector3 for the shootOffset, refactored to use a variable instead of a raw number so that it is no longer hard-coded

    [SerializeField] private InputActionAsset inputList;
    [SerializeField] private float cooldownLength; //Var for the shot cooldown length, refactored to not be hard-coded

    private InputAction shoot;

    void OnEnable()
    {
        try
        {
            shoot = inputList["Shoot"];
        }
        catch
        {
            throw new ArgumentException("Unable to find inputList!", nameof(PlayerShoot));
        }

        shoot.performed += Shooting;
    }

    private void OnDisable()
    {
        shoot.performed -= Shooting;
    }

    void Shooting(InputAction.CallbackContext context)
    {
        //if the player presses the shoot button and they can shoot run the shooting func
        if (canShoot)
        {
            ObjectPool.instance.Create(laserPrefab, transform.position + shootOffset);
            canShoot = false; //Player can no longer shoot
            StartCoroutine("Cooldown"); //Start cooldown Coroutine
        }
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownLength); //Wait cooldown length
        canShoot = true; //Player can shoot again
    }
}
