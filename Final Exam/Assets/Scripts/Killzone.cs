using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    public static Vector3 _lastCheckPointPos = new Vector3 (7, 1, 5);
   
    [SerializeField]
    private GameObject _character;

    private void OnCollisionEnter(Collision collision)
    {
        _character.transform.position = _lastCheckPointPos;

        FindObjectOfType<AudioManager>().Play("Death");
        FindObjectOfType<AudioManager>().Play("Respawn");
    }
}
