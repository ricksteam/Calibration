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

    private float xMax = 0.64f;
    private float xMin = -0.64f;
    private bool getCubes = true;

    public Text tv;
     Transform leftCube;
     Transform rightCube;
     Transform topCube;
     Transform bottomCube;
     Transform forwardCube;
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
                leftCube = GameObject.Find("Left").transform;
                rightCube = GameObject.Find("Right").transform;
                origin = GameObject.Find("Sphere").transform;
                getCubes = false;
            }
            
            savePositions();
        }
           

    }
  
    
    public void savePositions()
    {
        //Left (x)
        
        
        Debug.Log(rightCube.position.x);
        tv.text = "Left: " + scaleX(leftCube.position.x + 0.1f).ToString("f1") + "\nRight: " + scaleX(rightCube.position.x + 0.2f).ToString("f1") + "\n";

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


}
