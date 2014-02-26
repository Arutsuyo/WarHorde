using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour 
{
	public float startingHealth = 30f;//How much health the enemy starts with (outside variance)
	public float healthVariance = 5f;//How much enemy health percent can vary from enemy to enemy
	public float health;//How much health the enemy has
	

	void Awake()
	{
		//Set our starting health using our variance
		health = Random.Range(startingHealth - healthVariance, startingHealth + healthVariance);
	}

	//Called by other objects (the player) to cause damage
	public void TakeDamage(float d)
	{
		health -= d;//Take the d

		//If we are dead, do animation stuff and delete this object
		if (health <= 0)
		{
			//ANIMATION STUFF

			Destroy(this.gameObject);
		}
	}
}
