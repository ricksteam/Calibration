using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushBlock : MonoBehaviour {

    private float left = 0f;
    private float right = 0f;
    private float up = 0f;
    private float down = 0f;
    private float forward = 0f;

    public Text tv;
    public Transform cubeParent;
     
    void Start()
    {
        
        
    }
    private void OnCollisionExit(Collision col)
    {

        if (col.gameObject.name.Equals("Left"))
        {
            float dis = getDistance("left", col.transform);
            left = normalize(dis, 0, 0.5f); //0.07

        }
        else if (col.gameObject.name.Equals("Right"))
        {
            float dis = getDistance("right", col.transform);
            right = normalize(dis, 0, 0.67f); //-0.5
        }
        else if (col.gameObject.name.Equals("Top"))
        {
            float dis = getDistance("top", col.transform);
            up = normalize(dis, 0, 0.61f);//-0.6
        }
        else if (col.gameObject.name.Equals("Bottom"))
        {
            float dis = getDistance("bottom", col.transform);
            down = normalize(dis, 0, 0.40f); //-0.6
        }
        else if (col.gameObject.name.Equals("Forward"))
        {
            float dis = getDistance("forward", col.transform);
            forward = normalize(dis, 0f, 0.35f); //-0.8
        }
        tv.text = "Left: " + left.ToString("F2") + "\nRight: " + right.ToString("F2") + "\nTop: " + up.ToString("F2") + "\nDown: " + down.ToString("F2") + "\nForward: " + forward.ToString("F2");
        PlayerPrefs.SetFloat("Left", left);
        PlayerPrefs.SetFloat("Right", right);
        PlayerPrefs.SetFloat("Up", up);
        PlayerPrefs.SetFloat("Down", down);
        PlayerPrefs.SetFloat("Forward", forward);

    }
    private float getDistance(string dir, Transform collision)
    {
        Vector3 vect = cubeParent.position - collision.position;
        
        switch (dir)
        {
            case "left":
                vect.z = 0;
                vect.y = 0;
                break;
            case "right":
                vect.z = 0;
                vect.y = 0;
                break;
            case "bottom":
                vect.z = 0;
                vect.x = 0;
                break;
            case "forward":
                vect.x = 0;
                vect.y = 0;
                break;
            case "top":
                vect.z = 0;
                vect.x = 0;
                break;
        }
        return vect.magnitude;
    }
    
   private float normalize(float value, float min, float oldMax)
    {
        return (((value - min) * (1 - 0)) / (oldMax - min)) + 0;
    }
   

}
