using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp : MonoBehaviour {

    void Update()
    {
        var pos = transform.position;
        if (gameObject.name.Equals("Left"))
        {
            pos.x = Mathf.Clamp(transform.position.x, -1f, 0f);
        }
        else if (gameObject.name.Equals("Right"))
        {
            pos.x = Mathf.Clamp(transform.position.x, 0f, 1f);
        }
        else if (gameObject.name.Equals("Top"))
        {
            pos.y = Mathf.Clamp(transform.position.y, 0f, 1f);
        }
        else if (gameObject.name.Equals("Bottom"))
        {
            pos.y = Mathf.Clamp(transform.position.y, -1f, 0f);
        }
        else if (gameObject.name.Equals("Forward"))
        {
            pos.z = Mathf.Clamp(transform.position.z, 0f, 1f);
        }

        transform.position = pos; 

    }
}
