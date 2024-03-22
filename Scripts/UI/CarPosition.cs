using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class CarPosition : MonoBehaviour {

//	public int totalCheckpointInGame;
	public GameObject Player;
	public GameObject AI1;
	public GameObject AI2;
	public GameObject AI3;


	public int PlayerPositionMAgnitude = 0;
	public int AI1PositionMAgnitude = 0;
	public int AI2PositionMAgnitude = 0;
	public int AI3PositionMAgnitude = 0;


	public float PlayerDistance = 0;
	public float AI1PositionDistance = 0;
	public float AI2PositionDistance = 0;
	public float AI3PositionDistance = 0;


	public int currentLapPlayer ;
	public int currentLapAI1 ;
	public int currentLapAI2 ;
	public int currentLapAI3 ;

	public Text Pos ;
	public Text TotalPos ;
	public int CurrentPos ;
	public int TotalPlayer ;
	public GameObject FinsheLine;

	public float[] Post = new float[0];


	void Start ()
	{
		if (Player)
			TotalPlayer = 1;
		if (AI1)
			TotalPlayer = 2;
		if (AI1 && AI2)
			TotalPlayer = 3;
		if (AI1 && AI2 && AI3)
			TotalPlayer = 4;

	} 



	void Update ()
	{
		Post = new float[TotalPlayer];
	//	Array.Sort(Post);

		
		for(var i = 0; i < TotalPlayer; i++)
		{

			if(i == 0)
			Post [0] = PlayerDistance;

			if(i == 1)
			Post[1] = AI1PositionDistance;
			
			if(i == 2)
			Post[2] = AI2PositionDistance;
			
			if(i == 3)
			Post[3] = AI3PositionDistance;



		}

		Array.Sort(Post);
	//	Array.Reverse(Post);


		CurrentPos = Array.IndexOf(Post, PlayerDistance) + 1 ;


		if(Pos)
		Pos.text = CurrentPos.ToString();

		if(TotalPos)
		TotalPos.text = TotalPlayer.ToString();



		if(!Player)
			Player = GameObject.FindGameObjectWithTag("Player");
		else
		{

    	PlayerPositionMAgnitude = Player.GetComponent<ISpeed>().waypoints.currentWaypoint; 
		currentLapPlayer  = Player.GetComponent<ISpeed>().waypoints.Lap; 
		PlayerDistance = Player.GetComponent<ISpeed>().waypoints.distance; 

			for (int i = 0; i < Player.GetComponent<ISpeed>().waypoints.waypoints2.Length; i++)
			{
				PlayerDistance = Vector3.Distance(Player.GetComponent<ISpeed>().waypoints.waypoints2[i].transform.position, Player.transform.position)+ Vector3.Distance(Player.GetComponent<ISpeed>().waypoints.closest.transform.position, Player.transform.position);
			}
		} 


		if(AI1)
		{
			for (int i = 0; i < AI1.GetComponent<ISpeed>().waypoints.waypoints2.Length; i++)
			{
				AI1PositionDistance = Vector3.Distance (AI1.GetComponent<ISpeed> ().waypoints.waypoints2 [i].transform.position, AI1.transform.position);
			}

			AI1PositionMAgnitude = AI1.GetComponent<ISpeed>().waypoints.currentWaypoint; 
			currentLapAI1  = AI1.GetComponent<ISpeed>().waypoints.Lap; 


		} 
		else
			AI1PositionDistance = 99999;
		
		if(AI2)
		{

			for (int i = 0; i < AI2.GetComponent<ISpeed>().waypoints.waypoints2.Length; i++)
			{
				AI2PositionDistance = Vector3.Distance (AI2.GetComponent<ISpeed> ().waypoints.waypoints2 [i].transform.position, AI2.transform.position);
			}

			AI2PositionMAgnitude = AI2.GetComponent<ISpeed>().waypoints.currentWaypoint; 
			currentLapAI2  = AI2.GetComponent<ISpeed>().waypoints.Lap; 

		} 
		else
			AI2PositionDistance = 99999;
		
		if (AI3) 
		{

			for (int i = 0; i < AI3.GetComponent<ISpeed>().waypoints.waypoints2.Length; i++)
			{
				AI3PositionDistance = Vector3.Distance (AI3.GetComponent<ISpeed> ().waypoints.waypoints2 [i].transform.position, AI3.transform.position);
			}

			AI3PositionMAgnitude = AI3.GetComponent<ISpeed> ().waypoints.currentWaypoint; 
			currentLapAI3 = AI3.GetComponent<ISpeed> ().waypoints.Lap; 

		}
		else
			AI3PositionDistance = 99999;


    }

}
