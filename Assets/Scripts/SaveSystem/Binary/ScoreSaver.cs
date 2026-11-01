using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class ScoreSaver
{
    /*
     * NOTE: Upon reviewing the documentation on the C# reference that microsoft provides
     * BinaryFormatters are very unsafe and should not be used, according to Microsoft anyway.
     * 
     * Because I couldn't figure out an alternative, I used it anyway since that was the example provided
     * and it's what I could figure out in the time I had to work on this.
     * 
     * Source: 
     */

    //Func for saving the score
    public static bool SaveScore(string fileName, float score)
    {
        var outputPath = Path.Combine(Application.persistentDataPath, fileName); //Define the filepath

        ScoreWrapper wrapper = new ScoreWrapper(score); //Create a reference to the score wrapper class
        BinaryFormatter formatter = new BinaryFormatter(); //Create a formatter to format the passed score

        //Using the filestream, write information to a file. Had to use filestream instead of filewriter due to the filewriter being incompatible with the binary formatter.
        //I also avoided using the memorystream since that needs the byte info to carry over between functions. Saving it to a file allows for easier access while keeping binary's difficult to modify nature
        using(FileStream stream = File.Create(outputPath))
        {
            formatter.Serialize(stream, wrapper); //Serialize the binary into the file
        }

        Debug.LogFormat($"Wrote saved score to {outputPath}", outputPath);

        System.GC.Collect(); //Garbage collection
        return true;
    }

    //Func for loading the score
    public static float LoadScore(string fileName)
    {
        var outputPath = Path.Combine(Application.persistentDataPath, fileName); //Find the file path

        //Check if the file exists if not send an impossible score as an indication of the error
        if (!File.Exists(outputPath))
        {
            Debug.Log("Desired file does not exist! Cannot pull from file path!");
            return -9999;
        }

        BinaryFormatter formatter = new BinaryFormatter(); //Create a binary formatter

        ScoreWrapper pulledScore = null; //Create a scorewrapepr

        //Using filestream for the same reasons as before, pull the score stored in the binary contained within the text file
        using (FileStream stream = File.Open(outputPath, FileMode.Open))
        {
            pulledScore = (ScoreWrapper)formatter.Deserialize(stream); //Deserialize the score from its unreadable form
           // Debug.Log(pulledScore); //Debug message
        }

        Debug.LogFormat($"Loaded saved score from {outputPath}", outputPath); //Show that the file was loaded

        System.GC.Collect(); //Garbage collection
        return pulledScore.savedScore; //Return the score stored within the score wrapper
    }
}

//Class for storing the savedScore
[System.Serializable]
public class ScoreWrapper
{
    [SerializeField] public float savedScore; //Variable saved

    //Constructor for setting the score
    public ScoreWrapper(float score)
    {
        savedScore = score;
    }
}
