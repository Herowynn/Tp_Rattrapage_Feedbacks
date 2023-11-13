using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	[SerializeField] private LayerMask _unitLayerMask; 

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
		}
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _unitLayerMask) && hit.collider.TryGetComponent<Unit>(out Unit unit))
			{
				unit.IsSelected = !unit.IsSelected;
				unit.CurrentState = Unit.UnitStates.Attack;
			}
		}
		
		if(Input.GetMouseButtonDown(1))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _unitLayerMask) && hit.collider.TryGetComponent<Unit>(out Unit unit))
			{
				unit.IsSelected = !unit.IsSelected;
				unit.CurrentState = Unit.UnitStates.Hit;
			}
		}
	}
}
