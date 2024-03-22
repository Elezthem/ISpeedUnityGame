using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;



[ExecuteInEditMode]
public class WayRoad : MonoBehaviour {

	public Color colorRoad = new Color(0,1,0, 0.5f);
	public bool ShowLabels;
	public bool ShowLines;

	public Transform[] waypoints;

	void Start ()        
	{
		waypoints = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			waypoints[i] = transform.GetChild(i).gameObject.transform;          
			RaycastHit hit;


		}
		Next();
	}

	void  Next (){
		foreach(Transform child in transform) 
		{
//			child.gameObject.tag = "Way";
		}

	}

	void  OnDrawGizmos (){

		Transform[] waypoints = gameObject.GetComponentsInChildren<Transform>();

		for(int i = 0; i < waypoints.Length; i++){

		 foreach( Transform wayroad in waypoints ) {

			Gizmos.color = colorRoad;

			Gizmos.DrawWireCube(waypoints[i].position, new Vector3(1, 1, 1));

			if(ShowLines == true && i < waypoints.Length-2)
			Gizmos.DrawLine(waypoints[i+1].position, waypoints[i+2].position) ;
				


			
	     }
       }
	}
}
