using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] Prefab;
	public int Range = 25;
	public float Time = 10f;
	public int Count;
	public int CountMax = 5;

	void Start () {
		StartCoroutine (SpawnCO());
	}
	
	IEnumerator SpawnCO () {
		yield return new WaitForSeconds (Time);

		if(Count<CountMax){
			Instantiate (Prefab[Random.Range(0, Prefab.Length)], new Vector3(Random.Range(-Range, Range), 0, Random.Range(-Range, Range)), Quaternion.Euler(new Vector3(0,0,0)));
		Count++;
		}
		StartCoroutine (SpawnCO());
	}
}
