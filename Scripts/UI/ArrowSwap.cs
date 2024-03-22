using UnityEngine;
using System.Collections;

public class ArrowSwap : MonoBehaviour {



    public GameObject[] RedLights;
	public GameObject[] BlueLights;
	public bool active;
	public float time = 20;

	private float timer=0.0f;
	private int lightNum = 0;

    enum LightsMode {Active=1 , Inactive=2}
    private LightsMode lightsMode = LightsMode.Inactive;

	void Start () {




        if (lightsMode == LightsMode.Inactive)
        {
			Active ();

        }
        else if (lightsMode == LightsMode.Active)
        {
            lightsMode = LightsMode.Inactive;
		}
	}

	void Active () {
		
		if (lightsMode == LightsMode.Inactive)
			
		{
			lightsMode = LightsMode.Active;

		}
		else if (lightsMode == LightsMode.Active)
		{
			lightsMode = LightsMode.Inactive;
		}
	}
	void FixedUpdate () {


        if (lightsMode == LightsMode.Active)
        {
            timer = Mathf.MoveTowards(timer, 0.0f, Time.deltaTime * time);



            if (timer == 0)
            {
                lightNum++;
                if (lightNum > 12) { lightNum = 1; }
                timer = 1.0f;
            }





            if (lightNum == 1 || lightNum == 3)
            {

				foreach (GameObject RedLight in RedLights)
                {
					RedLight.SetActive(true);
                }

				foreach (GameObject BlueLight in BlueLights)
                {
					BlueLight.SetActive(false);
                }
            }

            if (lightNum == 5 || lightNum == 7)
            {

				foreach (GameObject BlueLight in BlueLights)
                {
					BlueLight.SetActive(true);
                }

				foreach (GameObject RedLight in RedLights)
                {
					RedLight.SetActive(false);
                }
            }



        }
        else
        {

			foreach (GameObject BlueLight in BlueLights)
            {
				BlueLight.SetActive(false);
            }

			foreach (GameObject RedLight in RedLights)
            {
				RedLight.SetActive(false);
            }


        }



	}



}
