using UnityEngine;
using System.Collections;

public class EnemyMeleeTrigger : MonoBehaviour 
{
	private Animator anim;//Reference to our animator
	private EnemyMeleeAttack enemyMeleeAttack;//Reference to the attack script, so we can actually hit the player
	private HashIDs hash;//Reference to our hash ID's

	void Awake()
	{
		//Get our references
		anim = transform.parent.parent.GetComponent<Animator>();
		enemyMeleeAttack = anim.gameObject.GetComponent<EnemyMeleeAttack>();
		hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
	}

	void OnTriggerEnter(Collider other)
	{
		//If we hit the player and are in the attacking state, send a message to the attack script
		if (anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.attackingState && other.tag == "Player")
		{
			enemyMeleeAttack.DoDamage(other.gameObject);
		}
	}
}
