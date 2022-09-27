using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{    private void Update()
    {
        Vector3 t = transform.position;

        if (t.z < 2)
        {
            t.z = 2;
            transform.position = Vector3.MoveTowards(transform.position, t, 3);
        }

        if (t.z > 8)
        {
            t.z = 8;
            transform.position = Vector3.MoveTowards(transform.position, t, 3);
        }
    }
}
