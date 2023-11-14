using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform _target;
	[SerializeField] private float _speed;
	private Rigidbody _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (_target != null)
		{
			/*var step = _speed * Time.deltaTime; // calculate distance to move
			transform.position = Vector3.MoveTowards(transform.position, _target.position, step);*/
		}
	}

	private void FixedUpdate()
	{
		if (_target != null)
			transform.position = Vector3.MoveTowards(transform.position, _target.position/* + new Vector3(0, 1f, 0)*/, _speed);
	}

	public void SetTarget(Transform unit)
    {
        _target = unit;
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.TryGetComponent<Unit>(out Unit unit) && unit == _target.GetComponent<Unit>())
		{
			Destroy(gameObject);
			unit.CurrentState = Unit.UnitStates.Hit;
		}
	}
}
