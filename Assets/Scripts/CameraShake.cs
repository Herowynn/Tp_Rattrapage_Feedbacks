using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	[SerializeField] private Vector3 _cameraOriginPosition;
	[SerializeField] private bool _isShaking = false;
	[SerializeField] private float _shakeFactor;

	public bool IsShaking
	{
		set { _isShaking = value; }
	}

	public float ShakeFactor
	{
		set { _shakeFactor = value; }
	}


	private void Awake()
	{
		_cameraOriginPosition = transform.localPosition;
	}

	private void Update()
	{
		if (_isShaking)
		{
			transform.localPosition = _cameraOriginPosition + Random.insideUnitSphere * _shakeFactor;
		}
		else if (!_isShaking)
		{
			transform.position = _cameraOriginPosition;
		}
	}
}
