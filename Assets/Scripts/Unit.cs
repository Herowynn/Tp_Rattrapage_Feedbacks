using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public enum UnitStates
	{
		Idle,
		Attack,
		Hit
	}

	[SerializeField] private UnitStates _currentState;
	[SerializeField] private bool _startAnimation;
	[SerializeField] private bool _isSelected;

	public UnitStates CurrentState
	{
		get { return _currentState; }
		set { _currentState = value; }
	}
	public bool StartAnimation 
	{
		get { return _startAnimation; }
		set { _startAnimation = value; }
	}
	public bool IsSelected 
	{
		get { return _isSelected; }
		set { _isSelected = value; }
	}

	private void Start()
	{
		_currentState = UnitStates.Idle;
		_startAnimation = false;
		_isSelected = false;
		GameEventsManager.Instance.PlayEvent("Idle", gameObject);
	}

	private void Update()
	{
		if(_startAnimation && _currentState == UnitStates.Idle)
		{
			GameEventsManager.Instance.PlayEvent("Idle", gameObject);
		}

		if (_currentState == UnitStates.Attack && _startAnimation)
		{
			_isSelected = false;
			GameEventsManager.Instance.PlayEvent("Attack", gameObject);
		}
		
		if (_currentState == UnitStates.Hit && _startAnimation)
		{
			_isSelected = false;
			GameEventsManager.Instance.PlayEvent("Hit", gameObject);
		}
	}
}
