using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingPlataform : MonoBehaviour
{
	private Rigidbody _rigidbody;

    [SerializeField]
    private float _force = 1;

    void Start()
    {
		_rigidbody = GetComponent<Rigidbody>();
    }  
    void Update()
    {
        _rigidbody.MovePosition(new Vector3(transform.position.x, 15 + Mathf.Sin(Time.time) * _force, transform.position.z));
    }
}
