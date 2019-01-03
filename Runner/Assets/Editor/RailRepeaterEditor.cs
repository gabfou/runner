using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(RailRepeater)), CanEditMultipleObjects]
public class RailRepeaterEditor : Editor
{
    RailRepeater railRepeater;

    void OnEnable()
    {
        railRepeater = target as RailRepeater;
    }

    public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
        if (GUILayout.Button("make rail"))
		{
            if (railRepeater.rail == null)
            {
                Debug.Log("No rail prefab");
                return ; 
            }
            List<Transform> childList = railRepeater.transform.Cast<Transform>().ToList();
            railRepeater.listMesh.Clear();
            foreach(Transform child in childList)
                GameObject.DestroyImmediate(child.gameObject);

            for (int i = 0; i < railRepeater.nbOfRail; i++)
                railRepeater.AddRail(i);
		}
	}
}
