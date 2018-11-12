using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : MonoBehaviour {

    public int currentStage = 0; //0 - pos, 1 - leftVerticalMin, 2 - leftVerticalMax
	public GameObject player;
	public Transform correctPlayerPos;
    public GameObject playerController;
    public Transform avatar;
    public Transform leftHand;
    public Transform rightHand;


    public float heightInFeet = 6.0f;

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
        float meters = getMeters(heightInFeet);
        Debug.Log(playerController.transform.position.y + "; " + meters);
        playerController.transform.position = new Vector3(playerController.transform.position.x, meters, playerController.transform.position.z);
    }
  	
    
	void Update()
	{
       
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger) || OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) || Input.GetKeyUp(KeyCode.Space))
        if (Input.GetKeyUp (KeyCode.Space)) //put oculus touch bopttons back, but still need kinect functionality
        {
			Debug.Log(currentStage);

            switch (currentStage)
            {
                case 0:
                    
                    if (getDistance(player.transform, correctPlayerPos) <= 0.2 && getDistance(player.transform, correctPlayerPos) >= -0.2)
                    {
                        
                        

                        tv.text = "Next we will check the bounds for your left hand. Start by putting your LEFT hand inside the box.";
                        Instantiate(cubeHolder, leftCubeSpot.transform.position, Quaternion.identity);
                        
                        foreach (GameObject cube in cubes)
                        {
							cube.gameObject.SetActive (false);
							cube.GetComponent<BoxCollider> ().enabled = false;
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
                   
                    foreach (GameObject cube in cubes)
                    {
					    cube.gameObject.SetActive (false);
						cube.GetComponent<BoxCollider> ().enabled = false;
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
                    StartCoroutine(ExitGame());
                    break;
            }
            
        }
       
    }
    IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(2);
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }

    }
        
    float getMeters(float feet)
    {
        return feet * 0.3048f;
    }

	float getDistance(Transform x1, Transform x2)
	{
		Vector3 vectorToTarget = x1.position - x2.position;
		vectorToTarget.y = 0;
		float distanceToTarget = vectorToTarget.magnitude;
		
		return distanceToTarget;
	}
		 
	public void InvokeStage()
	{
		currentStage++;
	}
}
