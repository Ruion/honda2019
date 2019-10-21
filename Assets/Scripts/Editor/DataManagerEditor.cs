using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataManager))]
public class DataManagerEditor : Editor
{
    DataManager dm;
    public override void OnInspectorGUI()
    {
        dm = (DataManager)target;

        base.OnInspectorGUI();

        if (GUILayout.Button("Get Unsync"))
        {
            dm.GetUnSyncUsers();
        }
    }
}
