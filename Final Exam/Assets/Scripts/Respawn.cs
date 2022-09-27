using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Renderer _mesh;

    private void OnTriggerEnter(Collider other)
    {
        Killzone._lastCheckPointPos = new Vector3 (transform.position.x, transform.position.y, 8); //Activate checkpoint

        //Change color to show activation
        Color color = _mesh.material.color;
        color.a = 200;
        _mesh.material.color = color;

       FindObjectOfType<AudioManager>().Play("CheckPoint");
    }
}
