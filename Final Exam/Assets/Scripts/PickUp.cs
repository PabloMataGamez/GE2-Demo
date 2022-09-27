using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class PickUp : MonoBehaviour
{
    // [SerializeField]
    //private AudioManager _audioManager;

    private void OnTriggerEnter(Collider collision)
    {        
        PointDisplay._points = PointDisplay._points + 10;

        //_audioManager.Play("Points"); //Doesnt work as needed

        FindObjectOfType<AudioManager>().Play("Points");

        GameObject.Destroy(gameObject);
    }   
}
