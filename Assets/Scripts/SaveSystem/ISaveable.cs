using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public interface ISaveable
{
    TransformClass SaveID { get; } //Saved info
    Dictionary<string, TransformClass> SavedData { get; } //The saved data dictionary
}
