using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomFeedback : GameFeedback
{
	private Camera _camera;
	[SerializeField] private float _cameraZoom;
	[SerializeField] private bool _getBackToNormal;

	public override IEnumerator Execute(GameEvent gameEvent, GameObject gameObject)
	{
		_camera = Camera.main;
		if(!_getBackToNormal)
		{
			Vector3 pointToLook = GameManager.Instance.GetMidDistanceBetweenUnits();
			_camera.transform.LookAt(pointToLook);
			_camera.fieldOfView = _cameraZoom;
		}
		else
		{
			_camera.transform.position = GameManager.CameraOriginTransform.position;
			_camera.transform.rotation = GameManager.CameraOriginTransform.rotation;
			_camera.fieldOfView = GameManager.CameraOriginZoom;
		}
		
		yield break;
	}
}
