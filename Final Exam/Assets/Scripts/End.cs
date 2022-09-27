using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private bool _finished = false;
    private Vector3 _endPostion = new Vector3(130, 25, 5);

    [SerializeField]
    private GameObject _screen;

    [SerializeField]
    private Material _material;

    [SerializeField]
    private GameObject _points;

    private void OnTriggerEnter(Collider other)
    {
        if (!_finished)
        {
            _screen.GetComponent<MeshRenderer>().material = _material;
            FindObjectOfType<AudioManager>().Play("End");
            _finished = true;

            for (int i = 0; i < 5; i++)
            {
                _endPostion.x = _endPostion.x + i;
                Instantiate(_points, _endPostion, Quaternion.identity);
            }
        }       
    }
}
