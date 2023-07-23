using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardDebug : MonoBehaviour
{
    private void Update()
    {
        Vector3 forward = transform.forward * 3;

        Debug.DrawRay(transform.position, forward, Color.red, 10f);

        forward = new Vector3(transform.forward.x, 0, transform.forward.z);

        Debug.DrawRay(transform.position, forward, Color.yellow, 10f);
    }


}
