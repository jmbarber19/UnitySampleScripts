using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

/// <summary>
/// A sample ScriptableObject, an oprhan object comprable to a prefab
/// Can be isntantiated in the project view by right clicking and using
///      Create > SampleScriptableObject
/// </summary>
[ExecuteInEditMode]
[CreateAssetMenu(fileName = "SampleScriptableObject", menuName = "SampleScriptableObject")]
public class SampleScriptableObject : ScriptableObject
{
    /* Scriptable Object Values */
    [SerializeField] private int a = 0;
    [SerializeField] private bool b = false;

    [SerializeField] private string readonlyLog = "";

    /// <summary>
    /// Updates thee readonlyLog to display updated realtime values
    /// </summary>
    void OnValidate() {
        readonlyLog = $"SampleScriptableObject: {a}, {b}";
    }
}
