using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(TrapAndPieceManager)), CanEditMultipleObjects]
public class TrapAndPieceManagerEditor : Editor
{
    TrapAndPieceManager trapAndPieceManager;

    void OnEnable()
    {
        trapAndPieceManager = target as TrapAndPieceManager;
    }

    public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
        // if (GUILayout.Button("make"))
		// {
        //     List<Transform> childList = trapAndPieceManager.transform.Cast<Transform>().ToList();
        //     trapAndPieceManager.listOfGameObject.foreach(l => l.Clear(););
        //     foreach(Transform child in childList)
        //         GameObject.DestroyImmediate(child.gameObject);

        //     for (int i = 0; i < trapAndPieceManager.nbOfRail; i++)
        //         trapAndPieceManager.AddRail(i);
		// }
	}
}
