using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public static Manager manger;
	public GameObject Player;
	public float distance;
	public GameObject Target;
	public Text ScanDis;
	public GameObject SFX;
	public int Range = 25;
	public GameObject[] PoliceCars;
	public GameObject closestPolice;


	public GameObject level1;
	public GameObject level2;
	public GameObject level3;



	void Start () {

		Player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Player = GameObject.FindWithTag("Player");

		if(Player && closestPolice)
			distance = Vector3.Distance(Player.transform.position, closestPolice.transform.position);

		FindClosestEnemy ();

		if(distance > 250 || !Player || distance == 0)
		{

			level1.SetActive (false);
			level2.SetActive (false);
			level3.SetActive (false);


		}
		else if(distance > 150 && distance < 250)
		{

			level1.SetActive (true);
			level2.SetActive (false);
			level3.SetActive (false);

		}
		else if(distance > 75 && distance < 150)
		{

			level1.SetActive (true);
			level2.SetActive (true);
			level3.SetActive (false);

		}
		else if(distance > 1 && distance < 75)
		{

			level1.SetActive (true);
			level2.SetActive (true);
			level3.SetActive (true);

		}

	}

	void FindClosestEnemy() {

		PoliceCars = GameObject.FindGameObjectsWithTag("Police");
		GameObject closest;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in PoliceCars) {
			Vector3 diff = go.transform.position - Player.transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closestPolice = go;
				distance = curDistance;
			}
		}
	}


}
