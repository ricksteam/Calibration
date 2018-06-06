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
    public Transform leftParent;
    public Transform rightParent;
    public Transform topParent;
    public Transform bottomParent;
    public Transform forwardParent;

    void Start()
    {


    }
    /*private void OnCollisionExit(Collision col)
    {

        if (col.gameObject.name.Equals("Left"))
        {
            float dis = getDistance("left", col.transform);
            Debug.Log("Left: " + dis);
            left = normalize(dis, 0, -0.13f); //0.07

        }
        else if (col.gameObject.name.Equals("Right"))
        {
            float dis = getDistance("right", col.transform);
            Debug.Log("Right: " + dis);
            right = normalize(dis, 0, 0.81f); //-0.5
        }
        else if (col.gameObject.name.Equals("Top"))
        {
            float dis = getDistance("top", col.transform);
            Debug.Log("Top: " + dis);
            up = normalize(dis, 0, 0.6f);//-0.6
        }
        else if (col.gameObject.name.Equals("Bottom"))
        {
            float dis = getDistance("bottom", col.transform);
            Debug.Log("Bottom: " + dis);
            down = normalize(dis, 0, -0.36f); //-0.6
        }
        else if (col.gameObject.name.Equals("Forward"))
        {
            float dis = getDistance("forward", col.transform);
            Debug.Log("Forward: " + dis);
            forward = normalize(dis, 0, 0.4f); //-0.8
        }
        tv.text = "Left: " + left.ToString("F2") + "\nRight: " + right.ToString("F2") + "\nTop: " + up.ToString("F2") + "\nDown: " + down.ToString("F2") + "\nForward: " + forward.ToString("F2") + "\n<continue>";
        PlayerPrefs.SetFloat("Left", left);
        PlayerPrefs.SetFloat("Right", right);
        PlayerPrefs.SetFloat("Up", up);
        PlayerPrefs.SetFloat("Down", down);
        PlayerPrefs.SetFloat("Forward", forward);

    }*/

    public void savePositions()
    {
        Transform leftObj = leftParent.GetChild(0);
        left = getDistance("left", leftObj);
        Debug.Log(left);
        
    }
    private float getDistance(string dir, Transform collision)
    {
        Vector3 vect = new Vector3(0, 0, 0);
        
        switch (dir)
        {
            case "left":
                vect = leftParent.position - collision.position;
                vect.z = 0;
                vect.y = 0;
                break;
            case "right":
                vect = rightParent.position - collision.position;
                vect.z = 0;
                vect.y = 0;
                break;
            case "bottom":
                vect = bottomParent.position - collision.position;
                vect.z = 0;
                vect.x = 0;
                break;
            case "forward":
                vect = forwardParent.position - collision.position;
                vect.x = 0;
                vect.y = 0;
                break;
            case "top":
                vect = topParent.position - collision.position;
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
