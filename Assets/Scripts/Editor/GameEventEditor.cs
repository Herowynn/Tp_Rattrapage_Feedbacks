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
	private int _dragStartID = -1;
	private int _dragEndID = -1;

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
		string[] types = new string[] { "Add New Feedback", "Frame Change", "Start Animation", "Black Bars Visibility", "Camera Shake",
			"Camera Zoom", "Instantiate", "Change State", "Shoot Particles", "Wait" };

		int newItem = EditorGUILayout.Popup(0, types) - 1;

		Color color = Color.white;

		if (newItem >= 0)
		{
			Debug.Log(_types[newItem]);
			GameFeedback newFeedback = Activator.CreateInstance(_types[newItem]) as GameFeedback;

			EditorUtility.SetDirty(gameEvent);
			AssetDatabase.SaveAssets();
			gameEvent.Feedbacks.Add(newFeedback);

			_feedbacksFoldout.Add(false);
		}

		/*switch (_types[newItem].ToString())
		{
			case "AnimationFeedback":
				color = Color.cyan;
				break;
			case "WaitFeedback":
				color = Color.yellow;
				break;
			default:
				color = Color.white;
				break;
		}*/

		for (int i = 0; i < _feedbacks.arraySize; i++)
		{
			int controlId = GUIUtility.GetControlID(FocusType.Passive);
			SerializedProperty property = _feedbacks.GetArrayElementAtIndex(i);
			Rect horizontal = EditorGUILayout.BeginHorizontal();

			

			var backgroundRect = GUILayoutUtility.GetRect(5f, 17f);
			var offset = 4f;
			backgroundRect.xMax = 5;
			backgroundRect.xMin = 0;
			var foldoutRect = backgroundRect;
			foldoutRect.xMin += offset;
			foldoutRect.width = 300;
			foldoutRect.height = 17;
			var line = GUILayoutUtility.GetRect(1f, 1f);

			EditorGUI.DrawRect(backgroundRect, color);
			EditorGUI.DrawRect(line, Color.black);

			_feedbacksFoldout[i] = GUI.Toggle(foldoutRect, _feedbacksFoldout[i], gameEvent.Feedbacks[i].ToString(), EditorStyles.foldout);

			int indexRemove = -1;

			if (GUILayout.Button("-", EditorStyles.miniButton, GUILayout.Width(EditorStyles.miniButton.CalcSize(new GUIContent("-")).x)))
			{
				indexRemove = i;
			}

			EditorGUILayout.EndHorizontal();

			var eventCurrent = Event.current;

			if (eventCurrent.type == EventType.MouseDown)
			{
				if (horizontal.Contains(eventCurrent.mousePosition))
				{
					GUIUtility.hotControl = controlId;
					_dragStartID = i;
					eventCurrent.Use();
				}
			}

			if (_dragStartID == i)
			{
				color = new Color(0, 1, 0, 0.3f);
				EditorGUI.DrawRect(horizontal, color);
			}


			if (_feedbacksFoldout[i])
			{
				foreach (var item in GetChildren(property))
				{
					EditorGUILayout.PropertyField(item);
				}
			}

			if (horizontal.Contains(eventCurrent.mousePosition))
			{
				if (_dragStartID >= 0)
				{
					_dragEndID = i;

					Rect headerSplit = horizontal;
					headerSplit.height *= 0.5f;
					headerSplit.y += headerSplit.height;

					if (headerSplit.Contains(eventCurrent.mousePosition))
						_dragEndID = i + 1;
				}
			}

			if (_dragStartID >= 0 && _dragEndID >= 0)
			{
				if (_dragEndID != _dragStartID)
				{
					if (_dragEndID > _dragStartID)
						_dragEndID--;

					_feedbacks.MoveArrayElement(_dragStartID, _dragEndID);
					_dragStartID = _dragEndID;
				}
			}

			if (_dragStartID >= 0 || _dragEndID >= 0)
			{
				if (eventCurrent.type == EventType.MouseUp)
				{
					_dragStartID = -1;
					_dragEndID = -1;
					eventCurrent.Use();
				}
			}

			if (indexRemove != -1)
			{
				_feedbacks.DeleteArrayElementAtIndex(indexRemove);
				_feedbacksFoldout.RemoveAt(indexRemove);
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
