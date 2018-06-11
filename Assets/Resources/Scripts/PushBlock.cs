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
	 


	Calibration c;
	void Start()
	{
		c = GameObject.Find ("Room").GetComponent<Calibration> ();

	}
    void Update()
    { 

		if (c.leftContact) 
			savePositions ();

    }
  
    public void savePositions()
    {
		float maxLeft = 0.55f;

		//Debug.Log (leftParent.GetChild (0).gameObject.name);
		left = getDistance ("left", GameObject.Find("Left").transform);
		Debug.Log (left + "/" + maxLeft);
		tv.text = "Left: " + (left / maxLeft) * 100;
		/*right = getDistance ("right", rightParent.GetChild (0));
		up = getDistance ("top", topParent.GetChild (0));
		down = getDistance ("bottom", bottomParent.GetChild (0));
		forward = getDistance ("forward", forwardParent.GetChild (0));
		*/
        
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
    


}
