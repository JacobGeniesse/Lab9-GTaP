using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveManager : MonoBehaviour
{

    [SerializeField] private InputActionAsset inputList;

    private InputAction save;

    void OnEnable()
    {
        try
        {
            save = inputList["Save"];
        }
        catch
        {
            throw new ArgumentException("Input list is not assigned!", nameof(SaveManager));
        }

        save.performed += SaveGame;
    }

    void OnDisable()
    {
        save.performed -= SaveGame;
    }


    void SaveGame(InputAction.CallbackContext context)
    {
        //Debug.Log("Saving to: " + Path.Combine(Application.persistentDataPath, "savedata"));
        SavingService.SaveGame("savedata.json");
    }
}
