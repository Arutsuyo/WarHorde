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
	private Vector3 lastPlayerPos;//The last player position that the enemy read
	private bool closeToPlayer = false;//If we are close to the player (we don't move when we are)

	void Awake()
	{
		//Set up our references
		referenceVar = GameObject.FindGameObjectWithTag("GameController").GetComponent<ReferenceVar>(); 
		nav = GetComponent<NavMeshAgent>();
		playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

	}

	void Start()
	{
		//Establish the starting player position, this is done in Start so that it's done after our referenceVar is set up
		lastPlayerPos = referenceVar.playerPos;

		//Also set this position as the target position
		nav.SetDestination(lastPlayerPos);
	}

	void Update()
	{
		//If the player has moved far enough to change their reference position, change the destination and uptate the local position
		if (lastPlayerPos != referenceVar.playerPos && !closeToPlayer)
		{
			lastPlayerPos = referenceVar.playerPos;
			nav.SetDestination(lastPlayerPos);
		}

		//If the enemy is closer than their movement buffer to the player, then stop moving
		if (Vector3.Distance(playerTrans.position, transform.position) <= moveBuffer)
		{
			nav.Stop();
			closeToPlayer = true;
		}
		else//If we are not too close to the player, set our bool
		{
			closeToPlayer = false;
		}
	}
}





























