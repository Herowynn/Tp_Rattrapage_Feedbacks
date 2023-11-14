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

	public static GameObject BlackBars => _instance._blackBars;

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
		}
	}

	private void Start()
	{
		GameEventsManager.Instance.PlayEvent("BlackBars", gameObject);
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
