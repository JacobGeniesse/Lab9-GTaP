using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public interface ISaveable
{
    TransformClass SaveID { get; }
    Dictionary<string, TransformClass> SavedData { get; }
    void LoadFromData(Dictionary<string, TransformClass> data);

}
