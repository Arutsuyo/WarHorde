using UnityEngine;
using System.Collections;

public class PlayerWatch : MonoBehaviour 
{
	public float updateRange = 1.5f;//How far the player has to move for their position to update

	private ReferenceVar referenceVar;//Reference to our reference variables
	private Transform playerTrans;//Reference to the player's transform
	private SuperSimpleAI superSimpleAI;//Reference to our enemie's brain

	void Awake()
	{
		//Set up our references
		referenceVar = GetComponent<ReferenceVar>();
		playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

		//Intantiate our starting player position
		referenceVar.playerPos = playerTrans.position;
	}

	void Update()
	{
		//Simply check if the player has moved a certain distance from their last position, once they do update the position
		if (Vector3.Distance(referenceVar.playerPos, playerTrans.position) >= updateRange)
		{
			referenceVar.playerPos = playerTrans.position;
		}
	}
}
