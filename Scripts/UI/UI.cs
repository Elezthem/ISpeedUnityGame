using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
	public Text Speed;
	public Text Gear;
	public GameObject Player;

	private float thisAngle = -150;

	public Image tachometerNeedle;


	// Use this for initialization
	void Start () {

		Player = GameObject.FindWithTag("Player");

		if(Player == null)
		Player = GameObject.FindWithTag("Enemy");
		


		Speed.text = Player.gameObject.GetComponent<ISpeed>().info.speed.ToString();
		Gear.text = Player.gameObject.GetComponent<ISpeed>().info.currentGear.ToString();


	}
	
	// Update is called once per frame
	void Update () {

		Player = GameObject.FindWithTag("Player");

		if(Player == null)
			Player = GameObject.FindWithTag("Enemy");

		if(Player != null)
		{
		Speed.text = Player.gameObject.GetComponent<ISpeed>().info.speed.ToString();
		Gear.text = Player.gameObject.GetComponent<ISpeed>().info.currentGear.ToString();

		thisAngle = (Player.gameObject.GetComponent<ISpeed>().motorRPM / 20) - 175;
		thisAngle = Mathf.Clamp(thisAngle, -180, 90);
		tachometerNeedle.rectTransform.rotation = Quaternion.Euler(0, 0, -thisAngle);
	    }
	}
}
