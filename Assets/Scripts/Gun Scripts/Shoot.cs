using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public float damage = 10f;
	public GameObject bullet = null;
	public GameObject pos = null;
	public float force = 200f;
	public ParticleSystem flash = null;
	public GUIText hit = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
			Shooting();
	}
	void Shooting()
	{
		GameObject shot = GameObject.Instantiate( bullet, pos.transform.position, pos.transform.rotation ) as GameObject;
		shot.rigidbody.AddForce( transform.right * force);
		
		flash.Play();
		
		RaycastHit hitInfo;
		Ray ray = new Ray( pos.transform.position, pos.transform.TransformDirection( Vector3.back ) );
		if ( Physics.Raycast( ray, out hitInfo ) )
		{
			try
			{
				hit.text = hitInfo.collider.tag.ToString();
			}
			catch (UnassignedReferenceException)
			{
				//If the reference was never assigned, give an error in the log WITHOUT crashing the game horribly
				Debug.LogError("GUIText \"hit\" in class \"Shoot\" is unassigned.");
			}

			//If what we hit is tagged as an ememy, make them take damage
			if (hitInfo.collider.gameObject.tag == "Enemy")
			{
				hitInfo.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
			}
		}
	}
}
