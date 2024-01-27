using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIInspector : EditorWindow
{
    [MenuItem("AI/Inspector")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AIInspector));
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Actions", EditorStyles.boldLabel);

        if (GUILayout.Button("ViewAgent"))
        {
            Camera camera = Camera.main;
            GameObject go = Selection.activeGameObject;

            if(go.TryGetComponent<AINavAgent>(out AINavAgent agent))
            {
                camera.transform.parent = agent.transform;
                camera.transform.localPosition = Vector3.back * 5 + Vector3.up * 2;
                camera.transform .localRotation = Quaternion.identity;
            }
        }
        GUILayout.EndHorizontal();
    }
}
