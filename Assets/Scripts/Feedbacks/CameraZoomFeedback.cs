using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomFeedback : GameFeedback
{
	private Camera _camera;

	public override IEnumerator Execute(GameEvent gameEvent, GameObject gameObject)
	{
		_camera = Camera.main;
		Vector3 pointToLook = GameManager.Instance.GetMidDistanceBetweenUnits();
		_camera.transform.LookAt(pointToLook);
		yield break;
	}
}
