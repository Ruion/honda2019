using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveSystem))]
public class SaveSystemEditor : Editor
{
    SaveSystem dm;
    public override void OnInspectorGUI()
    {
        dm = (SaveSystem)target;

        base.OnInspectorGUI();

        if (GUILayout.Button("Populate"))
        {
            dm.Populate();
        }

        if (GUILayout.Button("ClearPermanent"))
        {
            dm.ClearPermanentUser();
        }
        
        if (GUILayout.Button("ClearTemporary"))
        {
            dm.ClearPermanentUser();
        }

        if (GUILayout.Button("Get Synced"))
        {
            dm.LoadSyncedPlayer();
        }
    }
}
