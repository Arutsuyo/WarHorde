using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
	public float health = 100f;//The player's current health
	
	public void TakeDamage(float d)
	{
		health -= d;//Take the d
		
		//If we are dead, restart the level
		if (health <= 0)
		{
			Application.LoadLevel(0);
		}
	}
}
