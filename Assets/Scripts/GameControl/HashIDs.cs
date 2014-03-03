using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour 
{
	public int attackingState;
	public int speedFloat;
	public int attackingBool;
	public int deadBool;

	void Awake()
	{
		attackingState = Animator.StringToHash("Base Layer.Attacking");
		speedFloat = Animator.StringToHash("Speed");
		attackingBool = Animator.StringToHash("Attacking");
		deadBool = Animator.StringToHash("Dead");

	}
}
