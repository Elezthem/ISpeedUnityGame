using UnityEngine;
using System.Collections;

public class Count : MonoBehaviour {

	private int mob;
	public GameObject MobilePads;

    public GameObject C1;
	public GameObject C2;
	public GameObject C3;
	public GameObject GO;
	public GameObject UI_CAR;

	public float timer=0.0f;

	void Start () {
		C1.SetActive (false);
		C2.SetActive (false);
		C3.SetActive (false);
		GO.SetActive (false);
		UI_CAR.SetActive (false);


		mob = PlayerPrefs.GetInt("mobile");

		if (mob == 1)
			MobilePads.SetActive (true);
		else
			MobilePads.SetActive (false);
		

	}


	void FixedUpdate () {


		timer = timer + Time.deltaTime / 1.3f;

		if (timer > 1 && timer < 2){
		C1.SetActive (true);
		C2.SetActive (false);
		C3.SetActive (false);
		}
		if (timer > 2 && timer < 3){
		C1.SetActive (false);
		C2.SetActive (true);
		C3.SetActive (false);
		}
		if (timer > 3 && timer < 4){
		C1.SetActive (false);
		C2.SetActive (false);
		C3.SetActive (true);
		}
		if (timer > 4){
		C1.SetActive (false);
		C2.SetActive (false);
		C3.SetActive (false);
		GO.SetActive (true);
		UI_CAR.SetActive (true);

		if (timer > 5f)
		GO.SetActive (false);

		if (timer > 5f)
		Destroy (this);
		}


     }


}
