using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PagesOfMakersUI))]
public class PagesOfMakerUIEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PagesOfMakersUI script = (PagesOfMakersUI)target;
        if (GUILayout.Button("UpdateText"))
        {
            script.UpdateText();
            UnityEditor.EditorUtility.SetDirty(script);
        }
    }

}
