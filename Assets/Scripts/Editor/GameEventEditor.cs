using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
	private List<bool> _feedbacksFoldout;
	private SerializedProperty _feedbacks;
	private System.Type[] _types;
	private string[] _typeStr;

	private void OnEnable()
	{
		_feedbacksFoldout = new List<bool>();
		_feedbacks = serializedObject.FindProperty("Feedbacks");

		if (_feedbacks != null)
		{
			for (int i = 0; i < _feedbacks.arraySize; i++)
			{
				_feedbacksFoldout.Add(false);
			}
		}

		List<string> types = new List<string>();
		types.Add("Add new Feedback");

		List<System.Type> gameEventTypes = (from domainAssembly in System.AppDomain.CurrentDomain.GetAssemblies()
											from assemblyType in domainAssembly.GetTypes()
											where assemblyType.IsSubclassOf(typeof(GameFeedback))
											select assemblyType).ToList();

		_types = gameEventTypes.ToArray();

		foreach (var item in gameEventTypes)
		{
			types.Add(item.ToString());
		}

		_typeStr = types.ToArray();
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		GameEvent gameEvent = target as GameEvent;

		string[] types = new string[] { "Add New Feedback", "Instantiate", "Wait" };

		int newItem = EditorGUILayout.Popup(0, types) - 1;

		if (newItem >= 0)
		{
			GameFeedback newFeedback = Activator.CreateInstance(_types[newItem]) as GameFeedback;

			EditorUtility.SetDirty(gameEvent);
			AssetDatabase.SaveAssets();
			gameEvent.Feedbacks.Add(newFeedback);
			_feedbacksFoldout.Add(false);
		}
		for (int i = 0; i < _feedbacks.arraySize; i++)
		{
			SerializedProperty property = _feedbacks.GetArrayElementAtIndex(i);

			var backgroundRect = GUILayoutUtility.GetRect(5f, 17f);
			var offset = 4f;
			backgroundRect.xMax = 5;
			backgroundRect.xMin = 0;
			var foldoutRect = backgroundRect;
			foldoutRect.xMin += offset;
			foldoutRect.width = 300;
			foldoutRect.height = 17;
			var line = GUILayoutUtility.GetRect(1f, 1f);

			EditorGUI.DrawRect(backgroundRect, Color.white);
			EditorGUI.DrawRect(line, Color.black);

			_feedbacksFoldout[i] = GUI.Toggle(foldoutRect, _feedbacksFoldout[i], gameEvent.Feedbacks[i].ToString(), EditorStyles.foldout);

			if (_feedbacksFoldout[i])
			{
				foreach(var item in GetChildren(property))
				{
					EditorGUILayout.PropertyField(item);
				}
			}

		}

		serializedObject.ApplyModifiedProperties();
	}

	public IEnumerable<SerializedProperty> GetChildren(SerializedProperty serializedProperty)
	{
		SerializedProperty currentProperty = serializedProperty.Copy();
		SerializedProperty nextSiblingProperty = serializedProperty.Copy();
		{
			nextSiblingProperty.Next(false);
		}

		if (currentProperty.Next(true))
		{
			do
			{
				if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
					break;

				yield return currentProperty;
			}
			while (currentProperty.Next(false));
		}
	}
}
