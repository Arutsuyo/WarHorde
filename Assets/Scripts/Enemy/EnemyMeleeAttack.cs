using UnityEngine;
using System.Collections;

public class EnemyMeleeAttack : MonoBehaviour 
{
	public float damage = 10f;//How much damage the enemy does
	public float damageVariance = 0f;//How much the enemies damage varies from hit to hit

	public void DoDamage(GameObject obj)
	{
		obj.GetComponent<PlayerHealth>().TakeDamage(Random.Range(damage - damageVariance, damage + damageVariance));
	}
}
