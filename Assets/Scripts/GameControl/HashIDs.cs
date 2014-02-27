using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour 
{
	public int speedFloat;
	public int attackingBool;
	public int deadBool;

	void Awake()
	{
		speedFloat = Animator.StringToHash("Speed");
		attackingBool = Animator.StringToHash("Attacking");
		deadBool = Animator.StringToHash("Dead");
	}
}
