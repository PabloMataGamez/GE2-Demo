using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private Transform _floor;

	[SerializeField]
	private float _moveSpeed = 5;

	[SerializeField]
	private float _jumpForce = 1;

    private bool _canJump;
    public static bool _grabbing = false;
	private bool _movingDown;
	private bool _movingUp;
	private float _newZPos;
	private float _zPos;
    private float _timer;
    private float _holDur = 3;
	private Rigidbody _rigidbody;
    private int _layerMask = 1 << 8;

    private void Start()
    {
		_rigidbody = GetComponent<Rigidbody>();       
    }

    private void FixedUpdate()
    {
        MoveXAxis();

        Jumping();
    }
	void Update()
    {
        ZPositionCalculation();

        MoveZAxis(_newZPos);

        AntyBugZ();

        Respawn();
    }

    private void ZPositionCalculation()
    {
        _zPos = transform.position.z; // Store Z position

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) // Get and set Z position
        {
            if (!Physics.Raycast(transform.position, -Vector3.forward, 3.1f, _layerMask)) //Position in grid is wall-free
            {
                if (_zPos > 4.9f && _zPos < 5.1f)
                {
                    _movingDown = true;
                    _newZPos = 2.0f;
                }
                else if (_zPos > 7.9f && _zPos < 8.1f)
                {
                    _movingDown = true;
                    _newZPos = 5.0f;
                }
            }           
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) //  Get and set Z position
        {
            if (!Physics.Raycast(transform.position, Vector3.forward, 3.1f, _layerMask)) //Position in grid is wall-free
            {
                if (_zPos > 1.9f && _zPos < 2.1f)
                {
                    _movingUp = true;
                    _newZPos = 5.0f;
                }
                else if (_zPos > 4.9f && _zPos < 5.1f)
                {
                    _movingUp = true;
                    _newZPos = 8.0f;
                }
            }
        }
    }

    private void MoveXAxis()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //Left Movement (X axis)
        {
            _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(Vector3.left) * Time.deltaTime * _moveSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //Right Movement (X axis)
        {
            _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(-Vector3.left) * Time.deltaTime * _moveSpeed);
        }
    }

    private void Jumping()
    {
        if (!_canJump) //Jump logic
        {
            _rigidbody.AddForce(0f, -9.8f, 0f);
        }
        else if (Input.GetKey(KeyCode.Space) && _canJump && !_grabbing)
        {
            _canJump = false;

            _rigidbody.AddForce(0f, 250f * _jumpForce, 0f);

            FindObjectOfType<AudioManager>().Play("Jump");
        }
    }
    
    void OnCollisionStay() //Player in floor = can jump
    {
   
        if (Physics.Raycast(transform.position, Vector3.down, 1.2f))
        {
            _canJump = true;
        }
    }

    void MoveZAxis(float target) //Fordward movement (Z axis)
	{		
		//Vector3 t = transform.position;
		//t.z = _newZPos;

		if (_movingDown)
		{
			if (_zPos <= target)
			{
				_movingDown = false; //movement finished
			}
			else
			{
				transform.position = transform.position + transform.TransformDirection(-Vector3.forward) * _moveSpeed * Time.deltaTime;
				//transform.position = Vector3.MoveTowards(transform.position, t, 3); //Doesnt work as needed
			}
		}

		if (_movingUp) //Backward movement (Z axis)
			{
			if (_zPos >= target)
			{
				_movingUp = false; //movement finished
			}
			else
			{
				transform.position = transform.position + transform.TransformDirection(Vector3.forward) * _moveSpeed * Time.deltaTime;
                //transform.position = Vector3.MoveTowards(transform.position, t,  3); //Doesnt work as needed
            }
        }
	}

	void AntyBugZ()
    {
        Vector3 t = transform.position;

        if (t.z < 2)
        {
            t.z = 2;
            transform.position = Vector3.MoveTowards(transform.position, t, 3);
        }

        if(!_movingDown && !_movingUp)
        {
           if (t.z >= 2.09 && t.z <= 3.5)
           {
                t.z = 2;
                transform.position = Vector3.MoveTowards(transform.position, t, 3);
           }

           if (t.z > 3.5 && t.z <= 4.91)
           {
                t.z = 5;
                transform.position = Vector3.MoveTowards(transform.position, t, 3);
           }

           if (t.z >= 5.09 && t.z <= 6.5)
           {
                t.z = 5;
                transform.position = Vector3.MoveTowards(transform.position, t, 3);
           }         

           if (t.z > 6.5 && t.z <= 7.91)
           {
                t.z = 8;
                transform.position = Vector3.MoveTowards(transform.position, t, 3);
           }
        }

        if (t.z > 8)
        {
            t.z = 8;
            transform.position = Vector3.MoveTowards(transform.position, t, 3);
        }
    }

    void Respawn()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _timer = Time.time;            
        }
        else if (Input.GetKey(KeyCode.R))
        {
            if (Time.time - _timer > _holDur)
            {
                _timer = float.PositiveInfinity;

                transform.position = Killzone._lastCheckPointPos;

                FindObjectOfType<AudioManager>().Play("Death");
                FindObjectOfType<AudioManager>().Play("Respawn");
            }
        }
        else
        {
            _timer = float.PositiveInfinity;
        }
    }
}
