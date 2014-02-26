using UnityEngine;
using System.Collections;

/**
 * This class is just a series of public variables that other classes can reference
 * in order to streamline code
 **/ 

public class ReferenceVar : MonoBehaviour 
{
	public float timer = 0f;//Master game timer

	public float startSpawnRate;//The starting spawn rate (spawn per second)
	public float endSpawnRate;//The ending spawn rate (spawn per second)
	public float spawnRateTime;//The time (in seconds) it takes the startSpawnRate to equal the endSpawnRate

	public Vector3 playerPos;//The player's position (as far as the AI is concerned)


	void Update()
	{
		timer += Time.deltaTime;//Incrament the timer every tick
	}
}
