using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Obstacle), true), CanEditMultipleObjects]
public class UnitDataEditor:Editor
{
	private Obstacle _obstacle;

	private void OnEnable()
	{
		_obstacle = (Obstacle)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if(_obstacle.OverrideXMLSettings) {
			ShowScoreField();
		}
	}


	void ShowScoreField()
	{
		EditorGUILayout.BeginVertical();
		int newScore = EditorGUILayout.IntField("Score", _obstacle.Score);
		if(targets.Length > 1) {
			foreach(UnityEngine.Object obj in targets) {
				Obstacle obstacle = (Obstacle)obj;
				obstacle.Score = newScore;
			}
		} else {
			_obstacle.Score = newScore;
		}

		EditorGUILayout.EndVertical();
	}
}