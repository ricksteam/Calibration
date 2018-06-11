using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : MonoBehaviour {

    public int currentStage = 0; //0 - pos, 1 - leftVerticalMin, 2 - leftVerticalMax
	public GameObject player;
	public Transform correctPlayerPos;

    public Transform avatar;
    public Transform leftHand;
    public Transform rightHand;


    public GameObject cubeHolder;
    public Transform sphere;
    public Transform leftCubeSpot;
    public Transform rightCubeSpot;

    public GameObject[] cubes;
	public bool leftContact = false;
	public bool rightContact = false;

    public Text tv;
	// Use this for initialization
	void Start () 
	{
		tv.text = "Please stand on the blue square on the ground. (Press the trigger to <continue>)";
        
        //cubeHolder.gameObject.SetActive(false);
    }
  	
    
	void Update()
	{
       
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger) || OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) || Input.GetKeyUp(KeyCode.Space))
        {
			Debug.Log(currentStage);

            switch (currentStage)
            {
                case 0:
                    //Debug.Log (Vector3.Distance (player.transform.position, correctPlayerPos.position));
                    if (getDistance(player.transform, correctPlayerPos) <= 0.2 && getDistance(player.transform, correctPlayerPos) >= -0.2)
                    {
                        tv.text = "Next we will check the bounds for your left hand. Start by putting your LEFT hand inside the box.";
                        Instantiate(cubeHolder, leftCubeSpot.transform.position, Quaternion.identity);
                        //cubes = GameObject.FindGameObjectsWithTag("Moveable");
                        foreach (GameObject cube in cubes)
                        {
							cube.gameObject.SetActive (false);
							cube.GetComponentInChildren<BoxCollider> ().enabled = false;
                        }
                        rightHand.gameObject.SetActive(false);
						Invoke ("InvokeStage", 1);
                    }

                    break;

			case 1:
				if (leftContact == false)
					return;
				
                    tv.text = "OK, next we are going to repeat that with the other hand. Place your RIGHT hand in the box.";
                    leftHand.GetComponent<PushBlock>().savePositions();

                    GameObject prev = GameObject.Find("CubeHolder(Clone)");
                    Destroy(prev);
                    Instantiate(cubeHolder, rightCubeSpot.transform.position, Quaternion.identity);
                    //cubes = GameObject.FindGameObjectsWithTag("Moveable");
                    foreach (GameObject cube in cubes)
                    {
					cube.gameObject.SetActive (false);
						cube.GetComponentInChildren<BoxCollider> ().enabled = false;
                    }
                    rightHand.gameObject.SetActive(true);
                    leftHand.gameObject.SetActive(false);
					Invoke ("InvokeStage", 1);
                    break;
			case 2:
				if (rightContact == false)
					return;
				
                    tv.text = "Done for now!";
                    rightHand.GetComponent<PushBlock>().savePositions();
                    GameObject prev1 = GameObject.Find("CubeHolder(Clone)");
                    Destroy(prev1);
                    rightHand.gameObject.SetActive(true);
                    leftHand.gameObject.SetActive(true);
                    break;
            }
            
        }
       
    }


	float getDistance(Transform x1, Transform x2)
	{
		Vector3 vectorToTarget = x1.position - x2.position;
		vectorToTarget.y = 0;
		float distanceToTarget = vectorToTarget.magnitude;
		//Debug.Log (distanceToTarget);
		return distanceToTarget;
	}
		 
	public void InvokeStage()
	{
		currentStage++;
	}
}
