using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputList; //List of inputs

    private InputAction save; //Input for the player to save
    private InputAction load; //Input for the player to load

    private TargetBuilder builder; //Reference to the target builder
    private TargetDirector director; //Reference to the target director

    void OnEnable()
    {
        //Get reference to the save input from the master list
        try
        {
            save = inputList["Save"];
        }
        catch
        {
            throw new ArgumentException("Input list is not assigned!", nameof(SaveManager));
        }

        //Get reference to the load input from the master list
        try
        {
            load = inputList["Load"];
        }
        catch
        {
            throw new ArgumentException("Input list is not assigned!", nameof(SaveManager));
        }

        //If the builder doesn't have reference assign reference to it
        if (builder == null)
        {
            builder = FindAnyObjectByType<TargetBuilder>();
        }

        //If the director doesn't have reference assign reference to it
        if (director == null)
        {
            director = FindAnyObjectByType<TargetDirector>();
        }

        save.performed += SaveGame; //Add the SaveGameFunction to the performed actions list
        load.performed += LoadGame; //Add the LoadGameFunction to the performed actions list
    }

    void OnDisable()
    {
        save.performed -= SaveGame; //Remove the SaveGame Function to the performed actions list
        load.performed -= LoadGame; //Remove the SaveGame Function to the performed actions list
    }

    //Func for saving the game
    void SaveGame(InputAction.CallbackContext context)
    {
        SavingService.SaveGame("savedata.json"); //Save the game
        ScoreSaver.SaveScore("scoredata.txt", ScoreManager.instance.score); //Save the score
    }

    //Func for loading the game
    void LoadGame(InputAction.CallbackContext context)
    {

        Dictionary<string, TransformClass> pulledData = SavingService.LoadPositions("savedata.json"); //Loaded data from the save file


        /*
         * Theoretically a better approach would be to modify existing targets as opposed to creating a bunch of new ones.
         * However, this is what I found was the most pratical to do with the way I set things up
         */
        GameObject[] activeTargets = GameObject.FindGameObjectsWithTag("Target"); //Find all active targets

        //Destroy all active targets
        foreach(GameObject target in activeTargets)
        {
            Destroy(target);
        }

        //For each point of data
        foreach(string key in pulledData.Keys)
        {
            //If the pulled data references a standard target
            if (key.Contains("standardTrans"))
            {
                GameObject newEnemy = builder.Create(); //Create a new target
                director.BuildStandardTarget(); //Build the standard target
                if (newEnemy != null && newEnemy.GetComponent<TransformSaver>().LOCAL_TRANSFORM_KEY == "standardTrans")
                {
                    Transform freshTrans = newEnemy.GetComponent<Transform>(); //Reference the target's position
                    SetValues(freshTrans, pulledData, key); //Set the target's position equal to where the original target was
                }
                else
                {
                    Debug.LogWarning("Unable to load new enemy!"); //Debug message on failure
                }
            }
            else if(key.Contains("fastTrans"))
            {
                //If the data pulled references a fast target
                GameObject newEnemy = builder.Create(); //Create a new target
                director.BuildFastTarget(); //Create a fast target
                if (newEnemy != null && newEnemy.GetComponent<TransformSaver>().LOCAL_TRANSFORM_KEY == "fastTrans")
                {
                    Transform freshTrans = newEnemy.GetComponent<Transform>(); //Reference the target's position
                    SetValues(freshTrans, pulledData, key); //Set the target's position equal to where the original target was
                }
                else
                {
                    Debug.LogWarning("Unable to load new enemy!"); //Debug message on failure
                }
            }
            else if(key == "playerTrans")
            {
                GameObject playerObject = GameObject.Find("Player"); //Find the player's game object
                if (playerObject != null)
                {
                    Transform freshTrans = playerObject.GetComponent<Transform>(); //Reference the player's position
                    SetValues(freshTrans, pulledData, key); //Set player's position
                }
                else
                {
                    Debug.LogWarning("Unable to find player's game object, cannot apply loaded data!"); //Debug message on failure
                }
            }
        }
        director.RestartInterval(); //Restart the creation of new towers
        //Debug.Log(pulledData); //Debug message to prove success

        ScoreManager.instance.LoadScore();
    }

    //Func to set the transform data of a game object
    void SetValues(Transform freshTrans, Dictionary<string, TransformClass> dataToApply, string keyRef)
    {
        freshTrans.localPosition = dataToApply[keyRef].localPosition; //Set position
        freshTrans.localRotation = dataToApply[keyRef].localRotation; //Set rotation
        freshTrans.localScale = dataToApply[keyRef].localScale; //Set scale
    }
}
