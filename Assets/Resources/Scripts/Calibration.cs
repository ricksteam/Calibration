using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : MonoBehaviour {

	private string currentStage = "player"; //player, leftArmVertical, leftArmHorizontal, rightArmVertical, rightArmHorizontal

	public GameObject player;
	public Transform correctPlayerPos;
	 

	public Text tv;
	// Use this for initialization
	void Start () 
	{
		tv.text = "Please move to the blue square on the ground and press a button.";
	}
  	
	void Update()
	{
		if (OVRInput.Get (OVRInput.RawButton.LIndexTrigger) || OVRInput.Get (OVRInput.RawButton.RIndexTrigger) || Input.GetKey(KeyCode.Space)) {
			switch (currentStage) {
			case "player":
				//Debug.Log (Vector3.Distance (player.transform.position, correctPlayerPos.position));
				if (getPlayerDistance() <= 0.07){
					tv.text = "Next we will test your left arm's maximum vertical movement.";
					currentStage = "leftArmVertical";
				}

				break;
			case "leftArmVertical":


				break;
			}
		}
	}

	float getPlayerDistance()
	{
		Vector3 vectorToTarget = player.transform.position - correctPlayerPos.position;
		vectorToTarget.y = 0;
		float distanceToTarget = vectorToTarget.magnitude;
		Debug.Log (distanceToTarget);
		return distanceToTarget;
	}
}
