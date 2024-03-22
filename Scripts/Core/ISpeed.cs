using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



public class ISpeed : MonoBehaviour
{


	public ControlMode controlMode = ControlMode.AI;

	public Type type = Type.Traffic;
	public bool Mobile ;

	[System.Serializable]
	public enum Type
	{
		Traffic,
		Escape,
		Police,
		User,
	}

	public PoliceSetting Police;

	[System.Serializable]
	public class PoliceSetting
	{
		public GameObject Target;
		public float distance;
		public GameObject PoliceLights;
		public float RangeChase = 150f;
		public bool Chase = false;
	}

    public CarWheels Wheels;

    [System.Serializable]
    public class CarWheels
    {
		public GameObject brakeParticlePerfab;

        public ConnectWheel wheels;
        public WheelSetting setting;
		public HitGround[] hitGround;

    }


    [System.Serializable]
    public class ConnectWheel
	{		
		[HideInInspector]
        public bool frontWheelDrive = true;
		[Header("Front Wheels")]
        public Transform frontRight;
        public Transform frontLeft;

		[HideInInspector]
        public bool backWheelDrive = true;
		[Header("Back Wheels")]
        public Transform backRight;
        public Transform backLeft;

    }

    [System.Serializable]
    public class WheelSetting
    {
        public float Radius = 0.4f;
        public float Weight = 1000.0f;
        public float Distance = 0.2f;
    }


    // Lights Setting ////////////////////////////////

	public CarLights Lights;

    [System.Serializable]
    public class CarLights
    {
        public GameObject brakeLights;
		public GameObject reverseLights;
    }

    // Car sounds /////////////////////////////////

    public CarSounds Sounds;

    [System.Serializable]
    public class CarSounds
    {
		public AudioSource StartSound;
		public AudioSource Horn;
		public AudioSource crashSound;
		public AudioSource nitro;
		public AudioSource switchGear;
        public AudioSource IdleEngine;


    }

    // Car Particle /////////////////////////////////

	public CarParticles Nitro;

    [System.Serializable]
    public class CarParticles
    {
        public ParticleSystem shiftParticle1, shiftParticle2;
		public GameObject wheelParticle1;
		public GameObject wheelParticle2;

    }

    // Car Engine Setting /////////////////////////////////

    public CarSetting Setting;

    [System.Serializable]
    public class CarSetting
    {
		public bool Engine;
		public float StartTimer ;
        public Transform carSteer;
		public GameObject RayCenter;
		[HideInInspector]
        public float springs = 25000.0f;
		[HideInInspector]
        public float dampers = 1500.0f;
		[HideInInspector]
		public bool shifmotor;
		public float carPowerF= 120f;
		public float carPowerR= -20f;

        public float shiftPower = 150f;
        public float brakePower = 8000f;


        public Vector3 shiftCentre = new Vector3(0.0f, -0.8f, 0.0f);

        public float maxSteerAngle = 12.0f;
		public AnimationCurve MotorRPM  = AnimationCurve.Linear(0.0f, 5000.0f, 8000f, 0.0f);
		[HideInInspector]
		public float MotorRPM2  = 0.0f;
		public float idleRPM = 500.0f;

        public float shiftDownRPM = 1500.0f;
        public float shiftUpRPM = 2500.0f;
		[HideInInspector]
        public float stiffness = 2.0f;
		[HideInInspector]
        public bool automaticGear = true;
		public bool rayMod = true;

		public LayerMask HitLayer = -1;

		public float LimitBackwardSpeed = 60.0f;
		public float LimitForwardSpeed = 220.0f;
        public float[] gears = { -10f, 9f, 6f, 4.5f, 3f, 2.5f };



    }

	public WaypointsSetting waypoints;

	[System.Serializable]
	public class WaypointsSetting
	{
		public bool FindWaypoint = true;
		public WayRoad Waypoints;
		public int currentWaypoint = 0;
		[HideInInspector]
		public int currentWaypoint2 = 0;
		public int Lap;

		public bool Loop;
		public bool Parking ;
		public bool ParkingOnEnd ;
		public float distance;
		private float alt = 0;
		[Range(0f, 30f)]
		public float NextWay = 10f;
		[HideInInspector]
		public float stuck;

		[HideInInspector]
		public GameObject[] Enemys ;
//		[HideInInspector]
		public GameObject closest ;

		[HideInInspector]
		public GameObject[] waypoints2 ;
//		[HideInInspector]
//		public GameObject closest2 ;
		[HideInInspector]
		public float Dis ;
		[HideInInspector]
		public Vector3 TargetPosition;
		[HideInInspector]
		public Vector3 WaypointPosition;
		[HideInInspector]
		public Vector3 nextWaypointPosition;
	}


    [System.Serializable]
    public class HitGround
    {
       
        public string tag = "street";
        public bool grounded = false;
        public AudioClip brakeSound;
        public AudioClip groundSound;
        public Color brakeColor;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [HideInInspector]
	public float accelTmp = 0.0f;

    [HideInInspector]
    public float curTorque = 100f;
    [HideInInspector]
    public float powerShift = 100;
	[HideInInspector]
	public float torque = 100f;


	public Damage damage;
	[System.Serializable]
	public class Damage
	{
		public float maxMoveDelta = 1.0f; 
		public float maxCollisionStrength = 50.0f;
		[HideInInspector]
		public float YforceDamp = 0.1f; 
		public float demolutionRange = 0.5f;
		[HideInInspector]
		public float impactDirManipulator = 0.0f;
		public MeshFilter[] optionalMeshList;

		[HideInInspector]
		public MeshFilter[] meshfilters;
		[HideInInspector]
		public float sqrDemRange;
		[HideInInspector]
		public Vector3 colPointToMe;
		[HideInInspector]
		public float colStrength;
	}


	public Info info;
	[System.Serializable]
	public class Info
	{
    public float speed = 0f;
	[Range(-3f, 3f)]
	public float steer = 0;
	public float accel = 0.0f;
	public float carPower = 0;

	public bool slow;
	public bool brake;
	public bool FrontRay;
	public bool SideRaysR;
	public bool SideRaysL;

	public bool turbo ;
	public bool shift;
	//  [HideInInspector]
	public Vector3 velocity;
	public Vector3 localVel;
	public int currentGear = 0;
	}

	public ui UI;

	[System.Serializable]
	public class ui
	{
		public GameObject MirrorCamera;
		public GameObject icon;


	}




    private float lastSpeed = -10.0f;
    private bool shifting = false;

	float[] efficiencyTable = { 0.6f, 0.65f, 0.7f, 0.75f, 0.8f, 0.85f, 0.9f, 1.0f, 1.0f, 0.95f, 0.80f, 0.70f, 0.60f, 0.5f, 0.45f, 0.40f, 0.36f, 0.33f, 0.30f, 0.20f, 0.10f, 0.05f };
    float efficiencyTableStep = 250.0f;
    private float Pitch;
    private float PitchDelay;
    private float shiftTime = 0.0f;
    private float shiftDelay = 0.0f;

    [HideInInspector]
    public bool NeutralGear = true;

    [HideInInspector]
    public float motorRPM = 0.0f;

    [HideInInspector]
    public bool Backward = false;

    [HideInInspector]
    public float steerAmount = 0.0f;




    private float wantedRPM = 0.0f;
    private float w_rotate;
    private float slip, slip2 = 0.0f;


    private GameObject[] Particle = new GameObject[4];

    private Vector3 steerCurAngle;

    private Rigidbody myRigidbody;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    private WheelComponent[] wheels;



    private class WheelComponent
    {

        public Transform wheel;
        public WheelCollider collider;
        public Vector3 startPos;
        public float rotation = 0.0f;
        public float rotation2 = 0.0f;
        public float maxSteer;
        public bool drive;
        public float pos_y = 0.0f;
    }


    private WheelComponent SetWheelComponent(Transform wheel, float maxSteer, bool drive, float pos_y)
    {


        WheelComponent result = new WheelComponent();
        GameObject wheelCol = new GameObject(wheel.name + "WheelCollider");

        wheelCol.transform.parent = transform;
        wheelCol.transform.position = wheel.position;
        wheelCol.transform.eulerAngles = transform.eulerAngles;
        pos_y = wheelCol.transform.localPosition.y;

        WheelCollider col = (WheelCollider)wheelCol.AddComponent(typeof(WheelCollider));

        result.wheel = wheel;
        result.collider = wheelCol.GetComponent<WheelCollider>();
        result.drive = drive;
        result.pos_y = pos_y;
        result.maxSteer = maxSteer;
        result.startPos = wheelCol.transform.localPosition;

        return result;

    }

	public enum ControlMode
	{
	PC = 1,
	AI = 2,
	}


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Start()
	{


		if (UI.icon)
			UI.icon.SetActive (true);

		if(controlMode == ControlMode.PC){
			this.transform.gameObject.tag = "Player";
			UI.MirrorCamera.SetActive(true);

			if(Mobile)
				PlayerPrefs.SetInt("mobile",1);
			else
				PlayerPrefs.SetInt("mobile",0);
			


			if (UI.icon)
			{
				SpriteRenderer conColor = UI.icon.GetComponent<SpriteRenderer>();
				conColor.color = Color.green;
			}

		}
		else
		{
			if(type == Type.Escape)
			    this.transform.gameObject.tag = "Enemy";

			if(type == Type.Police)
			    this.transform.gameObject.tag = "Police";

			if(type == Type.Escape)
				this.transform.gameObject.tag = "Car";

			if (UI.MirrorCamera)
			UI.MirrorCamera.SetActive(false);
			
			if (UI.icon && UI.icon.GetComponent<SpriteRenderer>())
			{
				SpriteRenderer conColor = UI.icon.GetComponent<SpriteRenderer>();

				conColor.color = Color.red;
			}
		}


		if (Sounds.crashSound)
			Sounds.crashSound.mute = false;

		if (Sounds.Horn)
			Sounds.Horn.mute = false;

		if (Sounds.nitro)
			Sounds.nitro.mute = false;

		if (Sounds.StartSound)
			Sounds.StartSound.mute = false;

		if (Sounds.switchGear)
			Sounds.switchGear.mute = false;


		if (Setting.StartTimer > 0)
			Setting.Engine = false;
		else
			Setting.Engine = true;
		

	//	if (controlMode == ControlMode.AI)
	//	{
			FindClosestWay ();

		accelTmp = info.accel;






		if(waypoints.Waypoints == null && waypoints.FindWaypoint == true)
			waypoints.Waypoints = FindObjectOfType(typeof(WayRoad)) as WayRoad;
    

		if (damage.optionalMeshList.Length > 0)
			damage.meshfilters = damage.optionalMeshList;
		else
			damage.meshfilters = GetComponentsInChildren<MeshFilter>();

		damage.sqrDemRange = damage.demolutionRange * damage.demolutionRange;

	//	}
	}


    void Awake()
    {

		if(Mobile)
			PlayerPrefs.SetInt("mobile",1);
		else
			PlayerPrefs.SetInt("mobile",0);
		
        if (Setting.automaticGear) NeutralGear = false;

        myRigidbody = transform.GetComponent<Rigidbody>();

        wheels = new WheelComponent[4];

        wheels[0] = SetWheelComponent(Wheels.wheels.frontRight, Setting.maxSteerAngle, Wheels.wheels.frontWheelDrive, Wheels.wheels.frontRight.position.y);
        wheels[1] = SetWheelComponent(Wheels.wheels.frontLeft, Setting.maxSteerAngle, Wheels.wheels.frontWheelDrive, Wheels.wheels.frontLeft.position.y);

        wheels[2] = SetWheelComponent(Wheels.wheels.backRight, 0, Wheels.wheels.backWheelDrive, Wheels.wheels.backRight.position.y);
        wheels[3] = SetWheelComponent(Wheels.wheels.backLeft, 0, Wheels.wheels.backWheelDrive, Wheels.wheels.backLeft.position.y);

        if (Setting.carSteer)
        steerCurAngle = Setting.carSteer.localEulerAngles;

        foreach (WheelComponent w in wheels)
        {


            WheelCollider col = w.collider;
            col.suspensionDistance = Wheels.setting.Distance;
            JointSpring js = col.suspensionSpring;
            js.spring = Setting.springs;
            js.damper = Setting.dampers;
            col.suspensionSpring = js;


            col.radius = Wheels.setting.Radius;

            col.mass = Wheels.setting.Weight;


            WheelFrictionCurve fc = col.forwardFriction;

            fc.asymptoteValue = 5000.0f;
            fc.extremumSlip = 2.0f;
            fc.asymptoteSlip = 20.0f;
            fc.stiffness = Setting.stiffness;
            col.forwardFriction = fc;
            fc = col.sidewaysFriction;
            fc.asymptoteValue = 7500.0f;
            fc.asymptoteSlip = 2.0f;
            fc.stiffness = Setting.stiffness;
            col.sidewaysFriction = fc;


        }


    }




    public void ShiftUp()
    {
        float now = Time.timeSinceLevelLoad;

		if (now < shiftDelay) return;

        if (info.currentGear < Setting.gears.Length - 1)
        {

		  if(Sounds.switchGear)
			if (!Sounds.switchGear.isPlaying )
                Sounds.switchGear.GetComponent<AudioSource>().Play();


                if (!Setting.automaticGear)
            {
                if (info.currentGear == 0)
                {
                    if (NeutralGear){info.currentGear++;NeutralGear = false;}
                    else
                    { NeutralGear = true;}
                }
                else
                {
                    info.currentGear++;
                }
            }
            else
            {
                info.currentGear++;
            }


           shiftDelay = now + 1.0f;
           shiftTime = 1.5f;
        }
    }




    public void ShiftDown()
    {
        float now = Time.timeSinceLevelLoad;

       if (now < shiftDelay) return;

        if (info.currentGear > 0 || NeutralGear)
        {

			if (!Sounds.switchGear.isPlaying && Sounds.switchGear )
                Sounds.switchGear.GetComponent<AudioSource>().Play();

                if (!Setting.automaticGear)
            {

                if (info.currentGear == 1)
                {
                    if (!NeutralGear){info.currentGear--;NeutralGear = true;}
                }
                else if (info.currentGear == 0){NeutralGear = false;}else{info.currentGear--;}
            }
            else
            {
                info.currentGear--;
            }


            shiftDelay = now + 0.1f;
            shiftTime = 2.0f;
        }
    }



    void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.root.GetComponent<ISpeed>())
        {

            collision.transform.root.GetComponent<ISpeed>().slip2 = Mathf.Clamp(collision.relativeVelocity.magnitude, 0.0f, 10.0f);

            myRigidbody.angularVelocity = new Vector3(-myRigidbody.angularVelocity.x * 0.5f, myRigidbody.angularVelocity.y * 0.5f, -myRigidbody.angularVelocity.z * 0.5f);
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y * 0.5f, myRigidbody.velocity.z);

        }

		Vector3 colRelVel = collision.relativeVelocity;
		colRelVel.y *= damage.YforceDamp;


		if (collision.contacts.Length > 0)
		{

			damage.colPointToMe = transform.position - collision.contacts[0].point;
			damage.colStrength = colRelVel.magnitude * Vector3.Dot(collision.contacts[0].normal, damage.colPointToMe.normalized);

		if(Sounds.crashSound)
		 {

			if (damage.colPointToMe.magnitude > 1.0f && !Sounds.crashSound.isPlaying )
			{

				Sounds.crashSound.Play();
				Sounds.crashSound.volume = damage.colStrength / 200;
			}
		}
			OnMeshForce(collision.contacts[0].point, Mathf.Clamp01(damage.colStrength / damage.maxCollisionStrength));

			}
	}
    




    void OnCollisionStay(Collision collision)
    {

       if (collision.transform.root.GetComponent<ISpeed>())
            collision.transform.root.GetComponent<ISpeed>().slip2 = 5.0f;

    }




    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Update()
    {
//		myRigidbody.drag = Mathf.Clamp((info.steer / 50f), 0f, 0.2f);


		if (Setting.Engine == false && Setting.StartTimer > 0)
			Setting.StartTimer = Setting.StartTimer - 0.01f;

		if (Setting.Engine == false && Setting.StartTimer <= 0)
		{
			Setting.Engine = true;
		     Setting.StartTimer = 0;
			if (Sounds.IdleEngine)
				Sounds.IdleEngine.mute = false;
		}
	//	if(racer.Driver)
	//		racer.Driver.transform.position = racer.seatPosition.transform.position;
	//	 float Velocity = 100.0f;
	//	 float curentVelocity = 0.0f;

	//	if(curentVelocity != Velocity)
	//	{
	//		curentVelocity = Velocity;
	//		Velocity = Mathf.Clamp(Velocity, 60.0f, 360.0f);
	     	Setting.MotorRPM2 = Setting.MotorRPM.Evaluate(info.speed)  ;

	//	}
		Setting.idleRPM = Setting.MotorRPM2;

		if(!Setting.Engine && !info.turbo)
		{	
			info.brake = true;
			info.carPower = 0f;
				
		}

		if(type == Type.Police)
		{

			Police.Target = GameObject.FindWithTag("Player");

			if(Police.Target)
			Police.distance = Vector3.Distance(Police.Target.transform.position, transform.position);  

			if (Police.distance < Police.RangeChase)
			   {
				waypoints.WaypointPosition = Police.Target.transform.position;
				Police.Chase = true;

				if(Police.PoliceLights)
			    Police.PoliceLights.SetActive (true);
			   }
			else
			   {
				
				FindClosestWay();
				Police.Chase = false;

				if(Police.PoliceLights)
				Police.PoliceLights.SetActive (false);
			   }
				
		}


		if(waypoints.Waypoints)
			waypoints.distance = Vector3.Distance(waypoints.closest.transform.position, transform.position);

		if (controlMode == ControlMode.AI && Setting.Engine)
	    {
			
		if (Setting.rayMod == true )
		{


			RaycastHit hits;
			float RayAngle = 25F;
			float RaySize = (info.speed/4)+25;


			if(Setting.RayCenter == null)
				Setting.RayCenter = this.transform.gameObject;


			var ray = Setting.RayCenter.transform.TransformDirection (Vector3.forward) * RaySize ;
			var rayS = Setting.RayCenter.transform.TransformDirection (Vector3.forward) * RaySize/2 ;


			if (Physics.Raycast(Setting.RayCenter.transform.position , ray, out hits, RaySize, Setting.HitLayer) )
			{
				Debug.DrawRay(Setting.RayCenter.transform.position, ray, Color.red);
				info.brake = true;
				waypoints.stuck++;
				info.FrontRay = true;
				
					if(Sounds.Horn)
					{
						if((Physics.Raycast(Setting.RayCenter.transform.position , ray/2, out hits, RaySize, Setting.HitLayer)))
						Sounds.Horn.gameObject.SetActive(true);
					else
						Sounds.Horn.gameObject.SetActive(false);
					}

					info.carPower = 0f;
			



				if (waypoints.stuck > 250)
				{
					info.brake = false;
					Backward = true;
					info.accel = -info.accel;
				}
			//	else
			//	{
			//		info.accel = 0;
			//		info.brake = true;
			//	}

			}
			else
			{
				Debug.DrawRay(Setting.RayCenter.transform.position, ray, Color.blue);
				info.carPower =Setting.carPowerF;
				info.brake = false;
				waypoints.stuck = 0;
				info.FrontRay = false;;

				if(Sounds.Horn)
					Sounds.Horn.gameObject.SetActive(false);
			}

			if (waypoints.stuck > 500)
			{
				info.carPower =Setting.carPowerR;
				waypoints.stuck = 0;
			}



			Debug.DrawRay (Setting.RayCenter.transform.position, Quaternion.AngleAxis(-RayAngle/2, transform.up) * rayS , Color.blue);     
			if (Physics.Raycast (Setting.RayCenter.transform.position, Quaternion.AngleAxis(-RayAngle/2, transform.up) * rayS , RaySize/2 ,Setting.HitLayer))
			{
				Debug.DrawRay (Setting.RayCenter.transform.position, Quaternion.AngleAxis(-RayAngle/2, transform.up) * rayS , Color.red);
				info.brake = true;
				if(!info.SideRaysL)
				info.steer = -5;
				info.SideRaysR = true;
			} 
			else
			{
				info.SideRaysR = false;
			}

			Debug.DrawRay (Setting.RayCenter.transform.position, Quaternion.AngleAxis(RayAngle/2, transform.up) * rayS , Color.blue);     
			if (Physics.Raycast (Setting.RayCenter.transform.position, Quaternion.AngleAxis(RayAngle/2, transform.up) * rayS , RaySize/2 ,Setting.HitLayer))
			{
				Debug.DrawRay (Setting.RayCenter.transform.position, Quaternion.AngleAxis(RayAngle/2, transform.up) * rayS , Color.red);
				info.brake = true;

				if(!info.SideRaysR)
				info.steer = 5;
				info.SideRaysL = true;
			} 
			else
			{
				info.SideRaysL = false;
			}
		} 
	}

 }




    void FixedUpdate()
    {

			
        // info.speed of car
        info.speed = myRigidbody.velocity.magnitude * 2.7f;

		info.velocity = myRigidbody.velocity;
		info.localVel = transform.InverseTransformDirection(info.velocity);

		if (info.localVel.z > 0)
		{
			if(Input.GetAxis("Vertical") < 0)
				info.brake = true;
			
		}
		else
		{
			if(Lights.reverseLights)
			if (info.carPower < -0.1)
			{
				Lights.reverseLights.SetActive(true);
			}
			else
			{
				Lights.reverseLights.SetActive(false);
			}
		
		}

		if (Setting.Engine )
		Navigation ();

        if (info.speed < lastSpeed - 10 && slip < 10)
        {
            slip = lastSpeed / 15;
        }

        lastSpeed = info.speed;

		if (controlMode == ControlMode.PC && Setting.Engine)
		{

			info.accel = 0;
		//	info.brake = false;
			info.shift = false;

			if (Wheels.wheels.frontWheelDrive || Wheels.wheels.backWheelDrive)
			{
				if(!Mobile)
				{	
				info.steer = Mathf.MoveTowards(info.steer, Input.GetAxis("Horizontal") * 5, 10f);
				info.carPower = Input.GetAxis("Vertical") * 100;


				
				if(Input.GetButton("Jump") || (Input.GetAxis("Vertical") < 0 && info.speed > 10f))
					info.brake = true;
				else
					info.brake = false;


				info.shift = Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift);
				}
				else
				{	
					info.steer = Mathf.MoveTowards(info.steer,SimpleInput.GetAxis( "Horizontal" ) * 5, 10f);
					info.carPower =  SimpleInput.GetAxis( "Vertical" ) * 100;



					if(Input.GetButton("Jump") || ( SimpleInput.GetAxis( "Vertical" ) < 0 && info.speed > 10f))
						info.brake = true;
					else
						info.brake = false;


					info.shift = SimpleInput.GetButtonDown( "Jump" );
				}





			}

		}



        if (slip2 != 0.0f)
            slip2 = Mathf.MoveTowards(slip2, 0.0f, 0.1f);



        myRigidbody.centerOfMass = Setting.shiftCentre;




		info.accel = 1;
		info.steer = Mathf.MoveTowards(info.steer, steerAmount, 0.07f);




        if (!Wheels.wheels.frontWheelDrive && !Wheels.wheels.backWheelDrive)
            info.accel = 0.0f;



        if (Setting.carSteer )
            Setting.carSteer.localEulerAngles = new Vector3(steerCurAngle.x, steerCurAngle.y, steerCurAngle.z + (info.steer * -5.0f));



        if (Setting.automaticGear && (info.currentGear == 1) && (info.accel < 0.0f))
        {
            if (info.speed < 5.0f)
                ShiftDown();


        }
        else if (Setting.automaticGear && (info.currentGear == 0) && (info.accel > 0.0f))
        {
            if (info.speed < 5.0f)
                ShiftUp();

        }
        else if (Setting.automaticGear && (motorRPM > Setting.shiftUpRPM) && (info.accel > 0.0f) && info.speed > 10.0f && !info.brake)
        {

            ShiftUp();

        }
        else if (Setting.automaticGear && (motorRPM < Setting.shiftDownRPM) && (info.currentGear > 1))
        {
            ShiftDown();
        }



        if (info.speed < 1.0f) Backward = true;



        if (info.currentGear == 0 && Backward == true)
        {
          //  Setting.shiftCentre.z = -info.accel / -5;
            if (info.speed < Setting.gears[0] * -10)
                info.accel = -info.accel;
        }
        else
        {
            Backward = false;
         //   if (info.currentGear > 0)
         //   Setting.shiftCentre.z = -(info.accel / info.currentGear) / -5;
        }




      //  Setting.shiftCentre.x = -Mathf.Clamp(info.steer * (info.speed / 100), -0.03f, 0.03f);




		if(Lights.brakeLights)
		{	
		if (info.brake  )
            {
			Lights.brakeLights.SetActive(true);
            }
            else
            {
			Lights.brakeLights.SetActive(false);
            }
			/*
		if (info.slow && controlMode == ControlMode.AI)
			{
			Lights.brakeLights.SetActive(true);
			}
			else
			{
		//	Lights.brakeLights.SetActive(false);
			}

	  	if ((info.accel < 0 || info.speed < 1.0f ) && controlMode == ControlMode.AI)
		  {
			Lights.brakeLights.SetActive(true);
		  }
		  else
		  {
		//	Lights.brakeLights.SetActive(false);
		  }
			*/
		}


       






        wantedRPM = (5500.0f * info.accel) * 0.5f + wantedRPM * 0.9f;

        float rpm = 0.0f;
        int motorizedWheels = 0;
        bool floorContact = false;
        int currentWheel = 0;





        foreach (WheelComponent w in wheels)
        {
            WheelHit hit;
            WheelCollider col = w.collider;

            if (w.drive)
            {
                if (!NeutralGear && info.brake && info.currentGear < 2)
                {
                    rpm += info.accel * Setting.idleRPM;

                    /*
                    if (rpm > 1)
                    {
                        Setting.shiftCentre.z = Mathf.PingPong(Time.time * (info.accel * 10), 2.0f) - 1.0f;
                    }
                    else
                    {
                        Setting.shiftCentre.z = 0.0f;
                    }
                    */

                }
                else
                {
                    if (!NeutralGear)
                    {
                        rpm += col.rpm;
                    }else{
                        rpm += (Setting.idleRPM*info.accel);
                    }
                }


                motorizedWheels++;
            }




            if (info.brake || info.accel < 0.0f)
            {

                if ((info.accel < 0.0f) || (info.brake && (w == wheels[2] || w == wheels[3])))
                {

                    if (info.brake && (info.accel > 0.0f))
                    {
                        slip = Mathf.Lerp(slip, 5.0f, info.accel * 0.01f);
                    }
                    else if (info.speed > 1.0f)
                    {
                        slip = Mathf.Lerp(slip, 1.0f, 0.002f);
                    }
                    else
                    {
                        slip = Mathf.Lerp(slip, 1.0f, 0.02f);
                    }


                    wantedRPM = 0.0f;
                    col.brakeTorque = Setting.brakePower;
                    w.rotation = w_rotate;

                }
            }
            else
            {


                col.brakeTorque = info.accel == 0 || NeutralGear ? col.brakeTorque = 1000 : col.brakeTorque = 0;


                slip = info.speed > 0.0f ?
    (info.speed > 100 ? slip = Mathf.Lerp(slip, 1.0f + Mathf.Abs(info.steer), 0.02f) : slip = Mathf.Lerp(slip, 1.5f, 0.02f))
    : slip = Mathf.Lerp(slip, 0.01f, 0.02f);


                w_rotate = w.rotation;

            }



            WheelFrictionCurve fc = col.forwardFriction;



            fc.asymptoteValue = 5000.0f;
            fc.extremumSlip = 2.0f;
            fc.asymptoteSlip = 20.0f;
            fc.stiffness = Setting.stiffness / (slip + slip2);
            col.forwardFriction = fc;
            fc = col.sidewaysFriction;
            fc.stiffness = Setting.stiffness / (slip + slip2);


            fc.extremumSlip = 0.2f + Mathf.Abs(info.steer);

            col.sidewaysFriction = fc;




			if (!info.shift && (info.currentGear > 1 && info.speed > 50.0f) && Setting.shifmotor && Mathf.Abs(info.steer) < 0.2f && !info.brake)
            {
				info.shift = true;

				if (powerShift == 0)
				{ Setting.shifmotor = false; }

                powerShift = Mathf.MoveTowards(powerShift, 0.0f, Time.deltaTime * 10.0f);

				if (Sounds.nitro)
				 {	
                Sounds.nitro.volume = Mathf.Lerp(Sounds.nitro.volume, 0.5f, Time.deltaTime * 10.0f);

                if (!Sounds.nitro.isPlaying)
                  {
                    Sounds.nitro.GetComponent<AudioSource>().Play();

                  }

				}

				curTorque = powerShift > 0 ? Setting.shiftPower : info.carPower;

				if(Nitro.shiftParticle1)
                Nitro.shiftParticle1.emissionRate = Mathf.Lerp(Nitro.shiftParticle1.emissionRate, powerShift > 0 ? 50 : 0, Time.deltaTime * 10.0f);
				
				if(Nitro.shiftParticle2)
                Nitro.shiftParticle2.emissionRate = Mathf.Lerp(Nitro.shiftParticle2.emissionRate, powerShift > 0 ? 50 : 0, Time.deltaTime * 10.0f);
            }
            else
            {
				info.shift = false;

                if (powerShift > 20)
                {
					Setting.shifmotor = true;
                }
				if(Sounds.nitro){
                Sounds.nitro.volume = Mathf.MoveTowards(Sounds.nitro.volume, 0.0f, Time.deltaTime * 2.0f);

                if (Sounds.nitro.volume == 0)
                    Sounds.nitro.Stop();
				}

                powerShift = Mathf.MoveTowards(powerShift, 100.0f, Time.deltaTime * 5.0f);
				curTorque = info.carPower;

				if(Nitro.shiftParticle1)
                Nitro.shiftParticle1.emissionRate = Mathf.Lerp(Nitro.shiftParticle1.emissionRate, 0, Time.deltaTime * 10.0f);
				if(Nitro.shiftParticle2)
                Nitro.shiftParticle2.emissionRate = Mathf.Lerp(Nitro.shiftParticle2.emissionRate, 0, Time.deltaTime * 10.0f);
            }


            w.rotation = Mathf.Repeat(w.rotation + Time.deltaTime * col.rpm * 360.0f / 60.0f, 360.0f);
            w.rotation2 = Mathf.Lerp(w.rotation2,col.steerAngle,0.1f);
            w.wheel.localRotation = Quaternion.Euler(w.rotation,w.rotation2, 0.0f);



            Vector3 lp = w.wheel.localPosition;


            if (col.GetGroundHit(out hit))
            {


                if (Wheels.brakeParticlePerfab)
                {
                    if (Particle[currentWheel] == null)
                    {
						Particle[currentWheel] = Instantiate(Wheels.brakeParticlePerfab, w.wheel.position, Quaternion.identity) as GameObject;
                        Particle[currentWheel].name = "WheelParticle";
                        Particle[currentWheel].transform.parent = transform;
                        Particle[currentWheel].AddComponent<AudioSource>();
                        Particle[currentWheel].GetComponent<AudioSource>().maxDistance = 50;
                        Particle[currentWheel].GetComponent<AudioSource>().spatialBlend = 1;
                        Particle[currentWheel].GetComponent<AudioSource>().dopplerLevel = 5;
                        Particle[currentWheel].GetComponent<AudioSource>().rolloffMode = AudioRolloffMode.Custom;

                    }


                    var pc = Particle[currentWheel].GetComponent<ParticleSystem>();
                    bool WGrounded = false;


					for (int i = 0; i < (Wheels.hitGround.Length); i++)
                    {

						if (hit.collider.CompareTag(Wheels.hitGround[i].tag))
                        {
							WGrounded = Wheels.hitGround[i].grounded;
							Particle[currentWheel].GetComponent<ParticleSystem>().startColor = Wheels.hitGround[i].brakeColor;

							if(Wheels.hitGround[i].grounded){
								
								if( Nitro.wheelParticle1 && info.carPower < -1)
							     Nitro.wheelParticle1.GetComponent<ParticleSystem> ().startColor = Wheels.hitGround[i].brakeColor;
								if( Nitro.wheelParticle2 && info.carPower < -1)
								Nitro.wheelParticle2.GetComponent<ParticleSystem> ().startColor = Wheels.hitGround[i].brakeColor;
							}

                            if ((info.brake || Mathf.Abs(hit.sidewaysSlip) > 0.5f) && info.speed > 1)
                            {
								Particle[currentWheel].GetComponent<AudioSource>().volume= 0.01f;
								Particle[currentWheel].GetComponent<AudioSource>().clip = Wheels.hitGround[i].brakeSound;
                            }
							else if (Particle[currentWheel].GetComponent<AudioSource>().clip != Wheels.hitGround[i].groundSound && !Particle[currentWheel].GetComponent<AudioSource>().isPlaying)
                            {

								Particle[currentWheel].GetComponent<AudioSource>().clip = Wheels.hitGround[i].groundSound;
                            }


                        }


                    }




                    if (WGrounded && info.speed > 5 && !info.brake)
                    {

                        pc.enableEmission = true;

						if( Nitro.wheelParticle1 && info.carPower < 0)
							Nitro.wheelParticle1.GetComponent<ParticleSystem> ().startSize = info.speed;
						   

						if( Nitro.wheelParticle2 && info.carPower < 0)
							Nitro.wheelParticle2.GetComponent<ParticleSystem> ().startSize = info.speed;


                        Particle[currentWheel].GetComponent<AudioSource>().volume = 0.25f;

                        if (!Particle[currentWheel].GetComponent<AudioSource>().isPlaying)
                            Particle[currentWheel].GetComponent<AudioSource>().Play();

                    }
                    else if ((info.brake || Mathf.Abs(hit.sidewaysSlip) > 0.6f) && info.speed > 1)
                    {

                        if ((info.accel < 0.0f) || ((info.brake || Mathf.Abs(hit.sidewaysSlip) > 0.6f) && (w == wheels[2] || w == wheels[3])))
                        {

                            if (!Particle[currentWheel].GetComponent<AudioSource>().isPlaying)
                                Particle[currentWheel].GetComponent<AudioSource>().Play();
                            pc.enableEmission = true;
                            Particle[currentWheel].GetComponent<AudioSource>().volume = 0.2f;

							if( Nitro.wheelParticle1 && info.carPower < 0)
								Nitro.wheelParticle1.GetComponent<ParticleSystem> ().startSize = info.speed;

							if( Nitro.wheelParticle2&& info.carPower < 0)
								Nitro.wheelParticle2.GetComponent<ParticleSystem> ().startSize = info.speed;
                        }

                    }
                    else
                    {
						if (Nitro.wheelParticle1)
							Nitro.wheelParticle1.GetComponent<ParticleSystem> ().startSize = 0;

						if(Nitro.wheelParticle2)
							Nitro.wheelParticle2.GetComponent<ParticleSystem> ().startSize = 0;
						
                        pc.enableEmission = false;
                        Particle[currentWheel].GetComponent<AudioSource>().volume = Mathf.Lerp(Particle[currentWheel].GetComponent<AudioSource>().volume, 0, Time.deltaTime * 10.0f);
                    }

                }


                lp.y -= Vector3.Dot(w.wheel.position - hit.point, transform.TransformDirection(0, 1, 0) / transform.lossyScale.x) - (col.radius);
                lp.y = Mathf.Clamp(lp.y, -10.0f, w.pos_y);
                floorContact = floorContact || (w.drive);


            }
            else
            {

                if (Particle[currentWheel] != null)
                {
                    var pc = Particle[currentWheel].GetComponent<ParticleSystem>();
                    pc.enableEmission = false;

					if( Nitro.wheelParticle1 && info.carPower < 0)
						Nitro.wheelParticle1.GetComponent<ParticleSystem> ().startSize = info.speed;

					if( Nitro.wheelParticle2 && info.carPower < 0)
						Nitro.wheelParticle2.GetComponent<ParticleSystem> ().startSize = info.speed;
                }



                lp.y = w.startPos.y - Wheels.setting.Distance;

                myRigidbody.AddForce(Vector3.down * 5000);

            }

            currentWheel++;
            w.wheel.localPosition = lp;


        }

        if (motorizedWheels > 1)
        {
            rpm = rpm / motorizedWheels;
        }


        motorRPM = 0.95f * motorRPM + 0.05f * Mathf.Abs(rpm * Setting.gears[info.currentGear]);
        if (motorRPM > 5500.0f) motorRPM = 5200.0f;


        int index = (int)(motorRPM / efficiencyTableStep);
        if (index >= efficiencyTable.Length) index = efficiencyTable.Length - 1;
        if (index < 0) index = 0;



        float newTorque = curTorque * Setting.gears[info.currentGear] * efficiencyTable[index];

        foreach (WheelComponent w in wheels)
        {
            WheelCollider col = w.collider;

            if (w.drive)
            {

                if (Mathf.Abs(col.rpm) > Mathf.Abs(wantedRPM))
                {

                    col.motorTorque = 0;
                }
                else
                {
                    // 
                    float curTorqueCol = col.motorTorque;

                    if (!info.brake && info.accel != 0 && NeutralGear == false)
                    {
                        if ((info.speed < Setting.LimitForwardSpeed && info.currentGear > 0) ||
                            (info.speed < Setting.LimitBackwardSpeed && info.currentGear == 0))
                        {

                            col.motorTorque = curTorqueCol * 0.9f + newTorque * 1.0f;
                        }
                        else
                        {
                            col.motorTorque = 0;
                            col.brakeTorque = 2000;
                        }


                    }
                    else
                    {
                        col.motorTorque = 0;

                    }
                }

            }





            if (info.brake || slip2 > 2.0f)
            {
                col.steerAngle = Mathf.Lerp(col.steerAngle, info.steer * w.maxSteer, 0.02f);
            }
            else
            {

                float SteerAngle = Mathf.Clamp(info.speed / Setting.maxSteerAngle, 1.0f, Setting.maxSteerAngle);
                col.steerAngle = info.steer * (w.maxSteer / SteerAngle);


            }

        }

		if(!Setting.Engine)
		{
			if(Sounds.IdleEngine)
			Sounds.IdleEngine.mute = true;
		}
		else
		{
		    if(Sounds.IdleEngine)
			 Sounds.IdleEngine.mute = false;

		}	

		if(!info.brake)
        Pitch = Mathf.Clamp(1.5f + ((motorRPM - Setting.idleRPM) / (Setting.shiftUpRPM - Setting.idleRPM)), 1.0f, 3.0f);
		

        shiftTime = Mathf.MoveTowards(shiftTime, 0.0f, 0.1f);

        if (Pitch == 1 )
        {

			if(Sounds.IdleEngine)
            Sounds.IdleEngine.volume = Mathf.Lerp(Sounds.IdleEngine.volume, 0.05f, 0.5f)/2;
			



        }
        else
        {
			
            if ((Pitch > PitchDelay || info.accel > 0) && shiftTime == 0.0f)
            {
				
				if(Sounds.IdleEngine)
					Sounds.IdleEngine.volume = Mathf.Lerp(Sounds.IdleEngine.volume, 0.05f, 0.5f)/2;

            }




			if(Sounds.IdleEngine)
				Sounds.IdleEngine.pitch = Pitch/2;


            PitchDelay = Pitch;
        }

    }



	void Navigation (){

		if (waypoints.Waypoints != null ) {
			
			if (waypoints.currentWaypoint >= waypoints.Waypoints.waypoints.Length) {
				waypoints.currentWaypoint = 0;
				waypoints.Lap = waypoints.Lap + 1;
			}

			if(Police.Chase)
				waypoints.WaypointPosition = transform.InverseTransformPoint (new Vector3 (Police.Target.transform.position.x, Police.Target.transform.position.y, Police.Target.transform.position.z));
			else
			{
		    	waypoints.WaypointPosition = transform.InverseTransformPoint (new Vector3 (waypoints.Waypoints.waypoints [waypoints.currentWaypoint].position.x, transform.position.y, waypoints.Waypoints.waypoints [waypoints.currentWaypoint].position.z));

			if (waypoints.currentWaypoint < waypoints.Waypoints.waypoints.Length-1)
	    		waypoints.nextWaypointPosition = transform.InverseTransformPoint (new Vector3 (waypoints.Waypoints.waypoints [waypoints.currentWaypoint+1].position.x, waypoints.Waypoints.waypoints [waypoints.currentWaypoint+1].position.y, waypoints.Waypoints.waypoints [waypoints.currentWaypoint+1].position.z));
			}

		if(controlMode == ControlMode.AI)
		{	
			if(!info.SideRaysL ||!info.SideRaysR )
			info.steer = waypoints.WaypointPosition.x;
			
			if (waypoints.nextWaypointPosition.magnitude <  info.speed && info.speed > 50)
				info.brake = true;
			else
				info.brake = false;

			waypoints.Dis = waypoints.WaypointPosition.magnitude;


			if (info.steer > Setting.maxSteerAngle)
				info.steer = Setting.maxSteerAngle;

			if (info.steer < -Setting.maxSteerAngle)
				info.steer = -Setting.maxSteerAngle;


			if (waypoints.Dis < (info.speed)  && (waypoints.nextWaypointPosition.x > 10 || waypoints.nextWaypointPosition.x < -10 ) && info.speed > 35 )
			{
				info.slow = true;
			} 

			if (waypoints.Dis < (info.speed) && (waypoints.nextWaypointPosition.x > 20 || waypoints.nextWaypointPosition.x < -20 ) && info.accel > 0.01 && info.speed > 35 && info.slow == true )
			{

				Setting.shifmotor = false;
				info.turbo = false;

			} 
			else
			{
				info.slow = false;
				info.brake = false;
				Setting.shifmotor = true;
				info.turbo = true;
			} 

		}
			float random = Random.Range (waypoints.NextWay - 5, waypoints.NextWay + 5);

			if (waypoints.WaypointPosition.magnitude <  random)
				waypoints.currentWaypoint++;

		
			if(waypoints.closest && waypoints.waypoints2.Length > waypoints.currentWaypoint )
     			waypoints.closest = waypoints.Waypoints.waypoints[waypoints.currentWaypoint].gameObject;
		}
	   
	  }


	void FindClosestWay (){

	//	if(waypoints.currentWaypoint == waypoints.currentWaypoint2)
	//	{
			waypoints.waypoints2 = GameObject.FindGameObjectsWithTag("Way");
	    	waypoints.distance = Mathf.Infinity;
			var position = transform.position;

			foreach(GameObject go in waypoints.waypoints2)  {
				var diff = (go.transform.position - position);
				var alt = (go.transform.position.y - position.y);

				var curDistance2 = diff.sqrMagnitude;
			      if (curDistance2 < waypoints.distance ) {
					waypoints.closest = go;
					//	policeAddons.Target2 = go; 
				    waypoints.distance = curDistance2;
					int index = waypoints.closest.transform.GetSiblingIndex();

					//	if(currentWaypoint2 <= index)
				      waypoints.currentWaypoint = index ;

				//	waypoints.currentWaypoint = waypoints.currentWaypoint2;
		//		}
			}
		}
	}




	// if called by SendMessage(), we only have 1 param
	public void OnMeshForce(Vector4 originPosAndForce)
	{
		OnMeshForce((Vector3)originPosAndForce, originPosAndForce.w);

	}

	public void OnMeshForce(Vector3 originPos, float force)
	{
		// force should be between 0.0 and 1.0
		force = Mathf.Clamp01(force);






		for (int j = 0; j < damage.meshfilters.Length; ++j)
		{
			Vector3[] verts = damage.meshfilters[j].mesh.vertices;

			for (int i = 0; i < verts.Length; ++i)
			{
				Vector3 scaledVert = Vector3.Scale(verts[i], transform.localScale);
				Vector3 vertWorldPos = damage.meshfilters[j].transform.position + (damage.meshfilters[j].transform.rotation * scaledVert);
				Vector3 originToMeDir = vertWorldPos - originPos;
				Vector3 flatVertToCenterDir = transform.position - vertWorldPos;
				flatVertToCenterDir.y = 0.0f;


				// 0.5 - 1 => 45° to 0°  / current vertice is nearer to exploPos than center of bounds
				if (originToMeDir.sqrMagnitude < damage.sqrDemRange) //dot > 0.8f )
				{
					float dist = Mathf.Clamp01(originToMeDir.sqrMagnitude / damage.sqrDemRange);
					float moveDelta = force * (1.0f - dist) * damage.maxMoveDelta;

					Vector3 moveDir = Vector3.Slerp(originToMeDir, flatVertToCenterDir, damage.impactDirManipulator).normalized * moveDelta;

					verts[i] += Quaternion.Inverse(transform.rotation) * moveDir;


				}

			}

			damage.meshfilters[j].mesh.vertices = verts;
			damage.meshfilters[j].mesh.RecalculateBounds();
		}

	}

}