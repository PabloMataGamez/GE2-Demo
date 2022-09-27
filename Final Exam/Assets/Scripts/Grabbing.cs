using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    private Collider _collider;
    private bool _k;
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "SackBoy" && Input.GetKey(KeyCode.K))
        {
            PlayerMovement._grabbing = true;

            Vector3 t = collision.transform.position;
            //  float difference = transform.position.x - collision.transform.position.x; //Adjust offset, collider problem
            //  t.x = collision.transform.position.x + difference;
            transform.position = Vector3.MoveTowards(transform.position, t, 3);
        }
        else
        {
            PlayerMovement._grabbing = false;
        }
    }
}
