using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : MonoBehaviour {

    private int currentStage = 0; //0 - pos, 1 - leftVerticalMin, 2 - leftVerticalMax
	public GameObject player;
	public Transform correctPlayerPos;

    public Transform avatar;
    public Transform leftHand;
    public Transform rightHand;


    public GameObject cubeHolder;
    public Transform leftCubeSpot;
    public Transform rightCubeSpot;
    
	public Text tv;
	// Use this for initialization
	void Start () 
	{
		tv.text = "Please move to the blue square on the ground. (Press a button to <continue>)";
        //cubeHolder.gameObject.SetActive(false);
    }
  	
	void Update()
	{
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger) || OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) || Input.GetKeyUp(KeyCode.Space))
        {
            switch (currentStage)
            {
                case 0:
                    //Debug.Log (Vector3.Distance (player.transform.position, correctPlayerPos.position));
                    if (getDistance(player.transform, correctPlayerPos) <= 0.2 && getDistance(player.transform, correctPlayerPos) >= -0.2)
                    {
                        tv.text = "Next we will check the bounds for your left hand. Start by putting your LEFT hand inside the box.";
                        Instantiate(cubeHolder, leftCubeSpot.transform.position, Quaternion.identity);

                        rightHand.gameObject.SetActive(false);
                        currentStage++;
                    }

                    break;

                case 2:
                    tv.text = "OK, next we are going to repeat that with the other hand. Place your RIGHT hand in the box.";
                    GameObject prev = GameObject.Find("CubeHolder(Clone)");
                    Destroy(prev);
                    Instantiate(cubeHolder, rightCubeSpot.transform.position, Quaternion.identity);
                    
                    rightHand.gameObject.SetActive(true);
                    leftHand.gameObject.SetActive(false);
                    currentStage++;
                    break;
                case 4:
                    tv.text = "Done for now!";
                    GameObject prev1 = GameObject.Find("CubeHolder(Clone)");
                    Destroy(prev1);
                    rightHand.gameObject.SetActive(true);
                    leftHand.gameObject.SetActive(false);
                    break;
            }
            
        }
        if (Vector3.Distance(leftHand.position, cubeHolder.transform.position) <= 0.2 && currentStage == 1)
        {
            tv.text = "Now push the little boxes away from your LEFT hand.";
            currentStage++;
        }
        if (Vector3.Distance(rightHand.position, cubeHolder.transform.position) <= 0.2 && currentStage == 3)
        {
            tv.text = "Now push the little boxes away from your RIGHT hand.";
            currentStage++;
        }
    }


	float getDistance(Transform x1, Transform x2)
	{
		Vector3 vectorToTarget = x1.position - x2.position;
		vectorToTarget.y = 0;
		float distanceToTarget = vectorToTarget.magnitude;
		Debug.Log (distanceToTarget);
		return distanceToTarget;
	}
}
