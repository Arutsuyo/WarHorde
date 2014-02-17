using UnityEngine;
using System.Collections;

public class simpleAI : MonoBehaviour 
{
	public float fieldOfViewAngle = 110f;//The angle we can see the player
	public float walkSpeed = 3.5f;//The speed we move when walking/patrolling (not engaged)
	public float runSpeed = 5f;//The speed we move when running (engaged)
	public float moveBuffer = 3f;//How far away until the enemy will stop moving towards a target
	public float waitTime = 3f;//How long the enemy will wait until they start moving toward another patrol point
	public int numWayPoints;//The number of waypoints on the map

	private GameObject player;//Reference to the player
	private NavMeshAgent nav;//Reference to our nav mesh agent
	public Vector3 lastPlayerSighting;//The last place the player was seen
	private GameObject[] patrolPoints;//An array of all our possible patrol points
	private int currentPointIndex = -1;//The index of the point we are currently moving towards (-1 means we don't have a waypoint)
	public bool ischasing = false;//If we are currently chasing the player
	private bool isPatrolling = false;//If we are currently patrolling
	private float timer;//Timer for waiting

	void Awake()
	{
		//Get our references
		player = GameObject.FindGameObjectWithTag("Player");
		nav = GetComponent<NavMeshAgent>();

		//Now we have to find our patrol points
		patrolPoints = new GameObject[numWayPoints];
		patrolPoints = GameObject.FindGameObjectsWithTag("Waypoint");
	}

	void Update()
	{
		//If we detect the player, chase, activate chase mode
		if (DetectPlayer())
		{
			ischasing = true;
		}

		if (ischasing)
			Chase();
		else
			Patrol ();//Go into patrol mode if we arent chasing
	}

	void Chase()
	{

		nav.SetDestination(lastPlayerSighting);//Set our destination to the player's postion

		Debug.Log(nav.destination.ToString());

		isPatrolling = false;//We are no longer patrolling
		nav.speed = runSpeed;//We are also running

		if (Vector3.Distance(transform.position, player.transform.position) <= moveBuffer)
		{
			//If we are close enough to attack our player, stop moving
			nav.Stop();
			transform.LookAt(player.transform.position);//Also look at the player
		}
		else if (Vector3.Distance(transform.position, nav.destination) <= moveBuffer)
		{
			//Otherwise if we reach our destination and the player isn't there, that means we lost them.  Stop chasing
			ischasing = false;
		}
	}

	bool DetectPlayer()
	{
		//Get the player's postion relative to the enemy's
		Vector3 direction = player.transform.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);//Get the angle between the enemy and player
		//Detect if the player is within the enemy's field of view
		if (angle < fieldOfViewAngle * 0.5f)
		{
			//If it is, do a raycast to make sure we can actually see the player
			RaycastHit hit;
			if (Physics.Raycast(transform.position, direction.normalized, out hit))
			{
				//If our raycast hits somthing...
				if (hit.collider.gameObject == player)
				{
					Debug.Log("Player Detected");
					lastPlayerSighting = player.transform.position;
					//And the thing it hit is the player, set us we can detect the player
					return true;
				}
			}
		}
		//If any of these checks failed, we cannot detect the player
		return false;
	}

	void Patrol()
	{
		nav.speed = walkSpeed;//Since we are patrolling, set our movement speed to walking

		if (!isPatrolling)//If we are not currently moving towards somthing
		{

			timer += Time.deltaTime;//Incrament the timer

			if (timer >= waitTime)//If we are done waiting, set our next movement point
			{
				isPatrolling = true;//We are going to move before this statement is over
				SetPoint();//Start moving toward something
				timer = 0f;//Reset the timer
			}
		}
		else
		{
			//If we are "close enough" to our destination...
			if (Vector3.Distance(transform.position, nav.destination) < moveBuffer)
			{
				isPatrolling = false;//Stop moving
				nav.Stop();
			}
		}
	}

	void SetPoint()
	{
		int wayPointChoice;//The index of our randomly chosen waypoint
		if (currentPointIndex == -1)//If we dont have a selected waypoint, chose between all of them
			wayPointChoice = Random.Range(0, numWayPoints - 1);//Find a random waypoint to move towards

		else if (currentPointIndex == 0)//If our selection is waypoint 0, chose from the others
			wayPointChoice = Random.Range(1, numWayPoints - 1);

		else if (currentPointIndex == numWayPoints - 1)//If our selection is the last waypoint, chose from the others
			wayPointChoice = Random.Range(0, numWayPoints - 2);

		else//If our waypoint choice in in the middle of the possible selections, split the random selection
		{
			if (Random.Range(0, 1) == 0)//If we pick 0, chose a point below our current selection on the list
				wayPointChoice = Random.Range(0, currentPointIndex - 1);
			else//Otherwise chose a point above our current selection on the List
				wayPointChoice = Random.Range(currentPointIndex + 1, numWayPoints - 1);
		}

	 	//Once we chose our next waypoint, start moving towards it
		nav.SetDestination(patrolPoints[wayPointChoice].transform.position);
	}
}
























