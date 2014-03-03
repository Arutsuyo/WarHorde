using UnityEngine;
using System.Collections;

public class SuperSimpleAI : MonoBehaviour 
{
	public float walkSpeed = 3.5f;//The speed we move when walking/patrolling (not engaged)
	public float runSpeed = 5f;//The speed we move when running (engaged)
	public float moveBuffer = 3f;//How far away until the enemy will stop moving towards a target

	private ReferenceVar referenceVar;//Reference to our reference variables
	private NavMeshAgent nav;//Reference to our navigation mesh agent
	private Transform playerTrans;//Reference to the player's transform
	private HashIDs hashIDs;//Reference to the hash IDs
	private Animator anim;//Reference to our animator
	private Rigidbody rigid;//Reference to our regidbody

	private Vector3 lastPlayerPos;//The last player position that the enemy read
	private bool closeToPlayer = false;//If we are close to the player (we don't move when we are)

	void Awake()
	{
		//Set up our references
		referenceVar = GameObject.FindGameObjectWithTag("GameController").GetComponent<ReferenceVar>(); 
		nav = GetComponent<NavMeshAgent>();
		playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		hashIDs = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();

	}

	void Start()
	{
		//Establish the starting player position, this is done in Start so that it's done after our referenceVar is set up
		lastPlayerPos = referenceVar.playerPos;

		//Spawn the enemy with the NavMeshAgent turned off so that they can drop
		nav.enabled = false;
	}

	void Update()
	{
		//If the player has moved far enough to change their reference position, change the destination and uptate the local position
		if (lastPlayerPos != referenceVar.playerPos && !closeToPlayer && nav.enabled)
		{
			lastPlayerPos = referenceVar.playerPos;
			nav.SetDestination(lastPlayerPos);
		}

		//Get our speed
		float speed = Vector3.Project(nav.velocity, transform.forward).magnitude;

		//Give the animator our speed
		anim.SetFloat(hashIDs.speedFloat, speed);

		//If the enemy is closer than their movement buffer to the player, then stop moving and start attacking
		if (Vector3.Distance(playerTrans.position, transform.position) <= moveBuffer)
		{
			if (nav.enabled)
				nav.Stop();

			anim.SetBool(hashIDs.attackingBool, true);

			//Look at the player
			transform.LookAt(new Vector3(playerTrans.position.x, transform.position.y, playerTrans.position.z));

			closeToPlayer = true;
		}
		else//If we are not too close to the player, set our bools
		{
			closeToPlayer = false;

			anim.SetBool(hashIDs.attackingBool, false);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		//If we hit the terrain (The Floor) then activate the NavMeshAgent and begin moving to the player
		if (other.gameObject.tag == "Terrain")
		{
			nav.enabled = true;

			//Also set this position as the target position
			nav.SetDestination(lastPlayerPos);

			//Destroy the Rigidbody
			Destroy(rigid);
		}
	}
}





























