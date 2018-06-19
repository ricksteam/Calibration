using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp : MonoBehaviour {

    void Update()
    {
        var pos = transform.localPosition;
        if (gameObject.name.Equals("Left"))
        {
            pos.x = Mathf.Clamp(transform.localPosition.x, -0.58f, 0.1f);
        }
        else if (gameObject.name.Equals("Right"))
        {
            pos.x = Mathf.Clamp(transform.localPosition.x, 0.1f, 0.61f);
        }
        else if (gameObject.name.Equals("Top"))
        {
            pos.y = Mathf.Clamp(transform.localPosition.y, 0.1f, 0.52f);
        }
        else if (gameObject.name.Equals("Bottom"))
        {
            pos.y = Mathf.Clamp(transform.localPosition.y, -0.72f, 0.1f);
        }
        else if (gameObject.name.Equals("Forward"))
        {
            pos.z = Mathf.Clamp(transform.localPosition.z, 0.1f, 0.45f);
        }
        else if (gameObject.name.Equals("Back"))
        {
            pos.z = Mathf.Clamp(transform.localPosition.z, -0.4f, 0.1f);
        }

        transform.localPosition = pos; 

    }
}
