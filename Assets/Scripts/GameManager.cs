using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	[SerializeField] private LayerMask _unitLayerMask;
	[SerializeField] private Unit _shootUnit;
	[SerializeField] private Unit _hitUnit;
	[SerializeField] private GameObject _blackBars;
	private Transform _cameraOriginTransform;
	private float _cameraOriginZoom;

	public static GameObject BlackBars => _instance._blackBars;
	public static GameManager Instance => _instance;
	public static Transform CameraOriginTransform => _instance._cameraOriginTransform;
	public static float CameraOriginZoom => _instance._cameraOriginZoom;
	public static Vector3 HitUnitPosition => _instance._hitUnit.gameObject.transform.position;

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
			_cameraOriginTransform = Camera.main.transform;
			_cameraOriginZoom = Camera.main.fieldOfView;
		}
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _unitLayerMask) && hit.collider.TryGetComponent<Unit>(out Unit unit))
			{
				if (_shootUnit == null)
				{
					_shootUnit = unit;
					unit.IsSelected = true;
				}
				else if (_shootUnit == unit)
				{
					_shootUnit = null;
					_hitUnit = null;
				}
				else if(_hitUnit == null)
				{
					unit.IsSelected = true;
					_hitUnit = unit;
					GameEventsManager.Instance.PlayEvent("Camera", gameObject);
					_shootUnit.CurrentState = Unit.UnitStates.Attack;
					_hitUnit.CurrentState = Unit.UnitStates.Hit;
					_hitUnit.IsSelected = false;
					_shootUnit.IsSelected = false;
					_hitUnit = null;
					_shootUnit = null;
				}
			}
		}
	}
}
