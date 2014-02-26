using UnityEngine;
using System.Collections;

public class AISpawnContol : MonoBehaviour 
{
	public GameObject enemy;//The enemy our spawn point will be spawning
	public float spawnVariance = 0.05f;//How much the spawn time can randomly differ between spawn points

	private ReferenceVar referenceVar;//Reference to our reference variable
	private float localSpawnRateTime;//The local version of our spawnRateTime
	private float spawnRate;//Our current spawn rate
	private float nextSpawn;//What time the timer should read when we spawn the next enemy (we will spawn an enemy out of the gate)
	private bool spawn = false;//If we should spawn this tick

	void Awake()
	{
		//Set up our references
		referenceVar = GameObject.FindGameObjectWithTag("GameController").GetComponent<ReferenceVar>();

		//Set up our localSpawnRateTime based on a randomly selected float
		localSpawnRateTime = referenceVar.spawnRateTime * Random.Range(1f - spawnVariance, 1f + spawnVariance);

		//Match our starting spawn rate to our spawn rate time
		spawnRate = referenceVar.startSpawnRate;

		//Set Randomly set up the time for the first spawn to further offbalance the seperate spawn points
		nextSpawn = Random.Range(0f, spawnRate);
	}

	void Update()
	{
		//Update the spawn rate based on our variables, but only do so if we are above our lower bound spawn rate
		if (spawnRate > referenceVar.endSpawnRate)
			spawnRate = (-(referenceVar.startSpawnRate - referenceVar.endSpawnRate)/localSpawnRateTime)*referenceVar.timer+referenceVar.startSpawnRate;

		if (spawn)//If we are good to spawn, spawn an enemy at the spawn point
			Instantiate(enemy, transform.position, transform.rotation);

		ShouldSpawn();
	}

	void ShouldSpawn()
	{
		spawn = false;

		//If we are overdue for a spawn, set the next spawn time and turn the spawn bool to true
		if (nextSpawn <= referenceVar.timer)
		{
			nextSpawn = referenceVar.timer + spawnRate;
			spawn = true;
		}
	}
}





























