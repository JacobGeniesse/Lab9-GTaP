using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class SavingService
{
    //Function for saving the game
    public static void SaveGame(string fileName)
    {
        int currentEntry = 0; //Current entry within the saved objects. Serves to allow multiple objects with the same key to be stored by appending a value to the end of it
        SaveWrapper result = new SaveWrapper(new Dictionary<string, TransformClass>()); //Create a save wrapper class for storing all of the values pulled from the various game objects
        var saveableObjects = Object.FindObjectsByType<MonoBehaviour>().OfType<ISaveable>(); //Find every game object with a ISaveable interface attached
        
        //If that group of objects is greater than 0
        if (saveableObjects.Count() > 0)
        {
            Dictionary<string, TransformClass> savedObjects = new Dictionary<string, TransformClass>(); //Temporary storage for the pulled data
            
            //For each entry in saveable objects
            foreach (var so in saveableObjects)
            {
                //Grab the saved data value from the current saveableObject
                var data = so.SavedData;

                //If a valid key is found
                if (data.ContainsKey("standardTrans") || data.ContainsKey("playerTrans") || data.ContainsKey("fastTrans"))
                {
                    foreach (string key in data.Keys)
                    {
                        //Check if the key picked already exists, if so append the current entry to the end of it to prevent errors
                        if (savedObjects.ContainsKey(key))
                        {
                            currentEntry++; //Increase the current entry
                            savedObjects.Add(key + $"{currentEntry}", data[key]); //Add the dictionary entry to the savedObjects dictionary with the appended thing
                        }
                        else
                        {
                            savedObjects.Add(key, data[key]); //If the object's key isn't already saved add it to the savedObjects dictionary with no strings attached
                        }
                    }
                }
                else
                {
                    //Error handling if an invalid key was passed
                    var behavior = so as MonoBehaviour;
                    Debug.LogWarningFormat(behavior, "{0}'s save data does not have a valid ID attached", behavior.name);
                }
            }
            result = new SaveWrapper(savedObjects); //Set the result equal to a new SaveWrapper with the complete savedObjects contained within
        }
        else
        {
            Debug.LogWarningFormat("This scene did not include any savable objects."); //Error message if no savable objects are in the scene
        }

        //Actually saving that data
        var outputPath = Path.Combine(Application.persistentDataPath, fileName); //Get the file name/file path

        //Write the result to the file
        using (StreamWriter stream = File.CreateText(outputPath))
        {
            //Write the information to the file with Json formatting
            stream.WriteLine(JsonUtility.ToJson(result, true));
            Debug.LogFormat($"Wrote saved game to {outputPath}", outputPath); //Debug message showing where the information was written to
        }
        result = null; //Clear the results for future use
        System.GC.Collect(); //Garbage collection
    }

    //Function for loading saved data
    public static Dictionary<string, TransformClass> LoadPositions(string fileName)
    {
        var dataPath = Path.Combine(Application.persistentDataPath, fileName); //Define filepath

        if (File.Exists(dataPath) == false) { Debug.LogErrorFormat("No file exists at {0}", dataPath); return null; } //Check to ensure the file actually exists


        SaveWrapper data = null; //Define a savewrapper to file

        //Using a stream reader pull information from the designated file
        using (StreamReader stream = new StreamReader(dataPath))
        {
            string readData = stream.ReadToEnd(); //Read the file's info
            data = JsonUtility.FromJson<SaveWrapper>(readData); //Format the data
            Debug.LogFormat($"Loaded saved data from {dataPath}", dataPath); //Debug message showing where the information was loaded from
        }

        //If the data is null return an error
        if (data == null)
        {
            Debug.LogErrorFormat("Unable to pull save data at {0}", dataPath);
            return null;
        }

        Dictionary<string, TransformClass> pulledContent = new Dictionary<string, TransformClass>(); //Define the pulled content list for what's actually returned
        
        //As long as the keys and entry lists are equal, proceed.
        if (data.saveDataKeys.Count > 0 && data.saveDataKeys.Count == data.saveDataContent.Count)
        {
            //For each of the key entries
            for (int i = 0; i < data.saveDataKeys.Count; i++)
            {
                //Add a slot of the dictionary with the corresponding data
                pulledContent.Add(data.saveDataKeys[i], data.saveDataContent[i]);
            }
            System.GC.Collect(); //Garbage collection
        }
        else
        {
            Debug.LogError("Saving issue occured! Unable to load data!"); //Say there's an error in loading
        }

        //Error message just in case
        if(pulledContent == null)
        {
            Debug.LogError("Failed to load saved data!");
        }
        return pulledContent; //Return the dictionary stored in SaveWrapepr
    }
}

[System.Serializable]
public class SaveWrapper
{
    Dictionary<string, TransformClass> savedDataWhole = new Dictionary<string, TransformClass>(); //Incoming dictionary


    /*
     * Because dictionaries are not a serializable type in unity. I took the approach of separating them into their
     * keys and content to save the data. Upon being loaded both lists are recombined into a dictionary.
     */
    [SerializeField] public List<string> saveDataKeys = new List<string>(); //List for keys
    [SerializeField] public List<TransformClass> saveDataContent = new List<TransformClass>(); //List for content

    public SaveWrapper(Dictionary<string, TransformClass> incomingData)
    {
        savedDataWhole = incomingData; //Set the save data whole
        saveDataKeys.AddRange(savedDataWhole.Keys); //Define the key list
        saveDataContent.AddRange(savedDataWhole.Values); //Define the content list
    }
}