//using System.IO;
//using System.Linq;
//using UnityEngine;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;

//public class SavingService
//{
//    private const string OBJECTS_KEY = "objects";
//    private const string SAVEID_KEY = "$saveID";

//    public static void SaveGame(string fileName)
//    {
//        Dictionary<string, string> result = new Dictionary<string, string>();
//        var saveableObjects = Object.FindObjectsByType<MonoBehaviour>().OfType<ISaveable>();
//        if (saveableObjects.Count() > 0)
//        {
//            Dictionary<string, Vector3> savedObjects = new Dictionary<string, Vector3>();
//            foreach (var so in saveableObjects)
//            {
//                var data = so.SavedData;
//                //if(data.IsObject){
//                try
//                {
//                }
//                catch
//                {

//                }
//                //}
//                //else{
//                //var behavior = so as MonoBehaviour;
//                //Debug.LogWarningFormat(behavior, "{0}'s save data is not a dictionary. The object was not saved!", behavior.name);
//                //}
//            }

//            //result[OBJECTS_KEY] = savedObjects;
//        }
//        else
//        {
//            Debug.LogWarningFormat("This scene did not include any savable objects.");
//        }

//        var outputPath = Path.Combine(Application.persistentDataPath, fileName);
//        using(StreamWriter stream = File.CreateText(outputPath))
//        {
//            stream.WriteLine(JsonUtility.ToJson(result, true));
//            Debug.LogFormat($"Wrote saved game to {outputPath}", outputPath);
//        }
//        result = null;
//        System.GC.Collect();
//    }

//    public static bool LoadGame(string fileName)
//    {
//        var dataPath = Path.Combine(Application.persistentDataPath, fileName);
//        if (File.Exists(dataPath) == false) { Debug.LogErrorFormat("No file exists at {0}", dataPath); return false; }
//        var text = File.ReadAllText(dataPath);
//        var data = JsonMapper.ToObject(text);
//        if (data == null || data.IsObject == false)
//        {
//            Debug.LogErrorFormat("Data at {0} is not a JSON object",
//        dataPath); return false;
//        }

//        if(data.ContainsKey(OBJECTS_KEY)) {
//            var objects = data[OBJECTS_KEY];
//            LoadObjectsAfterSceneLoad = (scene, loadSceneMode) => {
//                var allLoadableObjects = Object.FindObjectsByType<MonoBehaviour>().OfType<ISaveable>().ToDictionary(o => o.SaveID, o => o);
//                var objectsCount = objects.Count;
//                for (int i = 0; i < objectsCount; i++)
//                {
//                    var objectData = objects[i];
//                    var saveID = (string)objectData[SAVEID_KEY];
//                    if (allLoadableObjects.ContainsKey(saveID))
//                    {
//                        var loadableObject = allLoadableObjects[saveID];
//                        loadableObject.LoadFromData(objectData);
//                    }
//                }
//                SceneManager.sceneLoaded -= LoadObjectsAfterSceneLoad;
//                LoadObjectsAfterSceneLoad = null;
//                System.GC.Collect();
//            };
//            SceneManager.sceneLoaded += LoadObjectsAfterSceneLoad;
//        }
//        return true;
//    }
//}

