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

    private float xMax = 0.61f;
    private float xMin = -0.58f;
    private float yMax = 0.53f;
    private float yMin = -0.72f;
    private float zMin = -0.4f;
    private float zMax = 0.45f;
    private bool getCubes = true;

    public Text tv;
     Transform leftCube;
     Transform rightCube;
     Transform topCube;
     Transform bottomCube;
     Transform forwardCube;
    Transform backCube;
     Transform origin;
	 


	Calibration c;
	void Start()
	{
		c = GameObject.Find ("Room").GetComponent<Calibration> ();
        

	}
    void Update()
    { 
        
		if (c.leftContact)
        {
            if (getCubes)
            {
                leftCube = GameObject.FindGameObjectWithTag("Left").transform;
                rightCube = GameObject.FindGameObjectWithTag("Right").transform;
                topCube = GameObject.FindGameObjectWithTag("Top").transform;
                bottomCube = GameObject.FindGameObjectWithTag("Bottom").transform;
                forwardCube = GameObject.FindGameObjectWithTag("Forward").transform;
                backCube = GameObject.FindGameObjectWithTag("Back").transform;
                origin = GameObject.Find("Sphere").transform;
                getCubes = false;
            }
            
            savePositions();
        }
           

    }
  
    
    public void savePositions()
    {
        //Left (x)
        //Debug.Log(forwardCube.localPosition.z + "; " + backCube.localPosition.z);

        //Debug.Log("Top: " + topCube.localPosition.y + "; Bottom: " + bottomCube.localPosition.y + "; Left: " + leftCube.localPosition.x + "; Right: " + rightCube.localPosition.x);
        tv.text = "Left: " + (scaleX(leftCube.localPosition.x) * 100).ToString("f0") + "%" + "\nRight: " + (scaleX(rightCube.localPosition.x) * 100).ToString("f0") + "%" + "\n" +
            "Top: " + (scaleY(topCube.localPosition.y) * 100).ToString("f0") + "%" + "\nBottom: " + (scaleY(bottomCube.localPosition.y) * 100).ToString("f0") + "%" + "\nForward: " + (scaleZ(forwardCube.localPosition.z) * 100).ToString("f0") + "%" +
            "\nBack: " + (scaleZ(backCube.localPosition.z) * 100).ToString("f0") + "%";

    

    }

    float scaleX(float x)
    {
        if (x > origin.position.x)
        {
            
            return ((x - origin.position.x) / (xMax - origin.position.x));
        }
        else if (x < origin.position.x)
        {
            
            return (((x - xMin) / (xMax - xMin)) * 2) - 1;
        }
        return -555555;
        
    }
    float scaleY(float y)
    {
        
        if (y > origin.position.y)
        {
            //Debug.Log("Top: " + y);
            return ((y - origin.position.y) / (yMax - origin.position.y));
        }
        else if (y < origin.position.y)
        {
            //Debug.Log("Bottom: " + y);
            return (((y - yMin) / (yMax - yMin)) * 2) - 1;
        }
        return -555555;

    }
    float scaleZ(float z)
    {

        if (z > origin.position.z)
        {
            //Debug.Log("Top: " + y);
            return ((z - origin.position.z) / (zMax - origin.position.z));
        } 
        else if (z < origin.position.z)
        {
            //Debug.Log("Bottom: " + y);
            return (((z - zMin) / (zMax - zMin)) * 2) - 1;
        }
        return -555555;

    }


}
