using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public interface ISaveable
{
    string SaveID { get; }
    Dictionary<string, Vector3> SavedData { get; }
    void LoadFromData(Dictionary<string, Vector3> data);

}
