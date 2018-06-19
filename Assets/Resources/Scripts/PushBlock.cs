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
    private float back = 0f;

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
        
		if (c.leftContact || c.rightContact)
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
        

        left = (scaleX(leftCube.localPosition.x) * 100);
        right = (scaleX(rightCube.localPosition.x) * 100);
        up = (scaleY(topCube.localPosition.y) * 100);
        down = (scaleY(bottomCube.localPosition.y) * 100);
        forward = (scaleZ(forwardCube.localPosition.z) * 100);
        back = (scaleZ(backCube.localPosition.z) * 100);

        if (c.leftContact)
        {
            PlayerPrefs.SetFloat("LLeft", left);
            PlayerPrefs.SetFloat("LRight", right);
            PlayerPrefs.SetFloat("LTop", up);
            PlayerPrefs.SetFloat("LBottom", down);
            PlayerPrefs.SetFloat("LForward", forward);
            PlayerPrefs.SetFloat("LBack", back);

        }
        else if (c.rightContact)
        {
            PlayerPrefs.SetFloat("RLeft", left);
            PlayerPrefs.SetFloat("RRight", right);
            PlayerPrefs.SetFloat("RTop", up);
            PlayerPrefs.SetFloat("RBottom", down);
            PlayerPrefs.SetFloat("RForward", forward);
            PlayerPrefs.SetFloat("RBack", back);

        }

        tv.text = "Left: " + left.ToString("f0") + "%" + "\nRight: " + right.ToString("f0") + "%" + "\n" +
            "Top: " + up.ToString("f0") + "%" + "\nBottom: " + down.ToString("f0") + "%" + "\nForward: " + forward.ToString("f0") + "%" +
            "\nBack: " + back.ToString("f0") + "%";

    

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

            return ((y - origin.position.y) / (yMax - origin.position.y));
        }
        else if (y < origin.position.y)
        {

            return (((y - yMin) / (yMax - yMin)) * 2) - 1;
        }
        return -555555;

    }
    float scaleZ(float z)
    {

        if (z > origin.position.z)
        {
            return ((z - origin.position.z) / (zMax - origin.position.z));
        } 
        else if (z < origin.position.z)
        {
            return (((z - zMin) / (zMax - zMin)) * 2) - 1;
        }
        return -555555;

    }


}
