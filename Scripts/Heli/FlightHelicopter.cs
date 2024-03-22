using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]


public class FlightHelicopter : MonoBehaviour
{
	public AIState AIstate = AIState.Patrol;
	private TargetBehavior targetHavior;

	public string[] TargetTag;
	public GameObject Target;
	public float TargetDistance;
	private float TimeToLock;
	private float AttackDirection = 0.5f;
	private float DistanceLock = float.MaxValue;
	public float DistanceAttack = 300;
//	public float FlyDistance = 1000;

	[HideInInspector]
//	public int WeaponSelected = 0;
	public int AttackRate = 80; 

	private float timestatetemp;
	private float timetolockcount;
	private bool attacking;
	private Vector3 directionTurn;
	private Vector3 targetpositionTemp;


	private float distance = 10.0f;
	private float heightDamping = 2.0f;
	private float rotationDamping = 3.0f;



	public GameObject MainRotor;
	public GameObject Spot;

	public float Speed = 50.0f;
	public float SpeedMax = 60.0f;
	public float height = 0.0f;
	public float heightMin = 40.0f;

	public float RotationSpeed = 50.0f;
	private float SpeedPitch = 2;
	private float SpeedRoll = 3;
	private float SpeedYaw = 1;
	private float DampingTarget = 10.0f;
	private bool AutoPilot = false;
	private float MoveSpeed = 10;
	private Rigidbody rb;
	[HideInInspector]
	public bool SimpleControl = false;
	[HideInInspector]
	public bool FollowTarget = false;
	[HideInInspector]
	private Vector3 PositionTarget = Vector3.zero;
	private Vector3 positionTarget = Vector3.zero;
	private Quaternion mainRot = Quaternion.identity;
	[HideInInspector]
	public float roll = 0;
	[HideInInspector]
	public float pitch = 0;
	[HideInInspector]
	private float yaw = 0;
	private Vector2 LimitAxisControl = new Vector2 (2, 1);
	private bool FixedX;
	private bool FixedY;
	private bool FixedZ;
	private float Mess = 30;
	private bool DirectVelocity = true;
	private float DampingVelocity = 5;
	private float rotationZ = 0f;
	private float dot;



	public enum AIState
	{
		Idle,
		Patrol,
		Chase,
		Turn,
	}

	public enum TargetBehavior
	{
		Static,
		Moving,
		Flying,
	}

	void Start ()
	{

		mainRot = this.transform.rotation;
		GetComponent<Rigidbody>().mass = Mess;

		if(Terrain.activeTerrain)
			height = Mathf.Round(transform.position.y) - Terrain.activeTerrain.SampleHeight(transform.position);
		else
			height = this.transform.position.y;


		timetolockcount = Time.time;
		AutoPilot = true;
		timestatetemp = 0;

	}

	void TargetBehaviorCal ()
	{
		if (Target) {
			Vector3 delta = (targetpositionTemp - Target.transform.position);
			float deltaheight = Mathf.Abs (targetpositionTemp.y - Target.transform.position.y); 
			targetpositionTemp = Target.transform.position;

			if (Spot)
				Spot.transform.LookAt (Target.transform);

			if (delta == Vector3.zero) {
				targetHavior = TargetBehavior.Static;	
			} else {
				targetHavior = TargetBehavior.Moving;
				if (deltaheight > 0.5f) {
					targetHavior = TargetBehavior.Flying;	
				}
			}
		}

	}

	void Update ()
	{
		MainRotor.transform.Rotate (new Vector3 (0, 100, 0) * Time.deltaTime * 20);


		TargetBehaviorCal ();

		switch (AIstate) {
		case AIState.Patrol:
			for (int t = 0; t<TargetTag.Length; t++) {
				if (GameObject.FindGameObjectsWithTag (TargetTag [t]).Length > 0) {
					GameObject[] objs = GameObject.FindGameObjectsWithTag (TargetTag [t]);
					float distance = int.MaxValue;
					for (int i = 0; i < objs.Length; i++) {
						if (objs [i]) {
							if (timetolockcount + TimeToLock < Time.time) {

								float dis = Vector3.Distance (objs [i].transform.position, transform.position);
								if (DistanceLock > dis) {
									if (!Target) {
										if (distance > dis && Random.Range (0, 100) > 80) {
											distance = dis;
											Target = objs [i];
											FollowTarget = true;
											Speed = 1f;
											if(TargetDistance < DistanceAttack)
											AIstate = AIState.Chase;
											
											timestatetemp = Time.time;
										}
									}
								}
							}
							shootTarget (objs [i].transform.position);
						}
					}
				}
			}
			break;
		case AIState.Idle:
		//	if (Vector3.Distance (PositionTarget, this.transform.position) <= DistanceAttack) {
			if(TargetDistance > DistanceAttack){
				AIstate = AIState.Patrol;
				timestatetemp = Time.time;
			}

			break;
		case AIState.Chase:
			if (Target) {

				Speed = SpeedMax;
				PositionTarget = Target.transform.position;
				if (!shootTarget (PositionTarget)) {

					if (attacking) {
						if (Time.time > timestatetemp + 5) {
							turn();
						}	
					} else {
						if (Time.time > timestatetemp + 7) {
							turn ();
						}		
					}
				}

			} else {
				AIstate = AIState.Patrol;
				timestatetemp = Time.time;

			}
			break;
		case AIState.Turn:
			if (Time.time > timestatetemp + 7) {
				timestatetemp = Time.time;

				if(TargetDistance < DistanceAttack)
					AIstate = AIState.Chase;
			}

			float height = PositionTarget.y;
			if (targetHavior == TargetBehavior.Static) {
				directionTurn.y = 0;
				PositionTarget += (this.transform.forward + directionTurn) * Speed;
				PositionTarget.y = height;
				PositionTarget.y += Speed/2;
			} else {
				PositionTarget += (this.transform.forward + directionTurn) * Speed;
				PositionTarget.y = height;
				PositionTarget.y += Speed/2;
			}
			break;
		}
	}

	void FixedUpdate ()
	{
		if (!this.GetComponent<Rigidbody>())
			return;

		Quaternion AddRot = Quaternion.identity;
		Vector3 velocityTarget = Vector3.zero;

		if(Terrain.activeTerrain)
		height = Mathf.Round(transform.position.y) - Terrain.activeTerrain.SampleHeight(transform.position);
		else
		height = this.transform.position.y;

		if(Target)
			TargetDistance = Vector3.Distance (Target.transform.position, this.transform.position);	


		if (AutoPilot) {


			if(heightMin > height)
			{
			transform.position += Vector3.up * Time.deltaTime * Speed;
			transform.Translate(Vector3.forward*Time.deltaTime * Speed * -2);
			}
			else
			{
			if (FollowTarget) {

				transform.position -= Vector3.up * Time.deltaTime * Speed;

				rb = GetComponent<Rigidbody>();

				positionTarget = Vector3.Lerp (positionTarget, PositionTarget, Time.fixedDeltaTime * DampingTarget);
				Vector3 relativePoint = this.transform.InverseTransformPoint (positionTarget).normalized;
				mainRot = Quaternion.LookRotation (positionTarget - this.transform.position);
				rotationZ = Mathf.Clamp (rotationZ, -25, 2);
				rb.rotation = Quaternion.Lerp (rb.rotation, mainRot, Time.fixedDeltaTime * (RotationSpeed * 0.1f));
				this.rb.rotation *= Quaternion.Euler (-relativePoint.y * 1, 0, -relativePoint.x * 0.1f);
				velocityTarget = (rb.rotation * Vector3.forward) * (Speed + MoveSpeed);
				rb.AddRelativeForce(Vector3.forward * Mathf.Max(0f, Speed * rb.mass));
			}
			}
			velocityTarget = (GetComponent<Rigidbody>().rotation * Vector3.forward) * (Speed + MoveSpeed);
		} else {
			AddRot.eulerAngles = new Vector3 (pitch, yaw, -roll);
			mainRot *= AddRot;
			
			if (SimpleControl) {
				Quaternion saveQ = mainRot;
				
				Vector3 fixedAngles  = new Vector3 (mainRot.eulerAngles.x, mainRot.eulerAngles.y, mainRot.eulerAngles.z);
				
				if(FixedX)
					fixedAngles.x = 1;
				if(FixedY)
					fixedAngles.y = 1;
				if(FixedZ)
					fixedAngles.z = 1;
				
				saveQ.eulerAngles = fixedAngles;
				
				
				mainRot = Quaternion.Lerp (mainRot, saveQ, Time.fixedDeltaTime * 2);
			}
			
			
			GetComponent<Rigidbody>().rotation = Quaternion.Lerp (GetComponent<Rigidbody>().rotation, mainRot, Time.fixedDeltaTime * RotationSpeed);
			velocityTarget = (GetComponent<Rigidbody>().rotation * Vector3.forward) * (Speed + MoveSpeed);
			
		}
		if(DirectVelocity){
			GetComponent<Rigidbody>().velocity = velocityTarget;
		}else{
			GetComponent<Rigidbody>().velocity = Vector3.Lerp (GetComponent<Rigidbody>().velocity, velocityTarget, Time.fixedDeltaTime * DampingVelocity);
		}
		yaw = Mathf.Lerp (yaw, 0, Time.deltaTime);
		MoveSpeed = Mathf.Lerp (MoveSpeed, Speed, Time.deltaTime);
	}
	
	public void AxisControl (Vector2 axis)
	{
		if (SimpleControl) {
			LimitAxisControl.y = LimitAxisControl.x;	
		}
		roll = Mathf.Lerp (roll, Mathf.Clamp (axis.x, -LimitAxisControl.x, LimitAxisControl.x) * SpeedRoll, Time.deltaTime);
		pitch = Mathf.Lerp (pitch, Mathf.Clamp (axis.y, -LimitAxisControl.y, LimitAxisControl.y) * SpeedPitch, Time.deltaTime);
	}
	public void TurnControl (float turn)
	{
		yaw += turn * Time.deltaTime * SpeedYaw;
	}
	public void SpeedUp (float delta)
	{
		if(delta >= 0)
		MoveSpeed = Mathf.Lerp (MoveSpeed, SpeedMax, Time.deltaTime * (10 * delta));
	}
	public void SpeedUp ()
	{
		MoveSpeed = Mathf.Lerp (MoveSpeed, SpeedMax, Time.deltaTime * 10);
	}


	bool shootTarget (Vector3 targetPos)
	{
		Vector3 dir = (targetPos - transform.position).normalized;
		dot = Vector3.Dot (dir, transform.forward);

		if (TargetDistance <= DistanceAttack) {

			if (dot >= AttackDirection) {
				Spot.SetActive (true);

				attacking = true;
				if (Random.Range (0, 100) <= AttackRate) {
				}
				if (TargetDistance < DistanceAttack ) {
					turn ();	
				}

			} else {


			}
		} else {
			Spot.SetActive (false);

		}
		return true;
	}

	void turn()
	{
		directionTurn = new Vector3(Random.Range(-2,1)+1,Random.Range(-2,1)+1,Random.Range(-2,1)+1);
		AIstate = AIState.Turn;
		timestatetemp = Time.time;
		attacking = false;
	}
}
