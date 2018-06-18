using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMovement : MonoBehaviour {

    public Text tv;

    public GameObject[] cubes;
    public GameObject room;
    void Start()
    {

        tv = GameObject.Find("Text").GetComponent<Text>();
        room = GameObject.Find("Room");
    }
	void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Equals("hand_left"))
        {
            tv.text = "Now push the little boxes away from your LEFT hand.";
            //cubes = GameObject.FindGameObjectsWithTag("Moveable");
			foreach (GameObject cube in cubes)
			{
				cube.gameObject.SetActive (true);
				cube.GetComponent<BoxCollider> ().enabled = true;
			}
			room.GetComponent<Calibration> ().leftContact = true;
        }
        else if (col.gameObject.name.Equals("hand_right"))
        {
            tv.text = "Now push the little boxes away from your RIGHT hand.";
            //cubes = GameObject.FindGameObjectsWithTag("Moveable");
			foreach (GameObject cube in cubes)
			{
				cube.gameObject.SetActive (true);
				cube.GetComponent<BoxCollider> ().enabled = true;
			}
			room.GetComponent<Calibration> ().rightContact = true;
        }
         
    }
}
