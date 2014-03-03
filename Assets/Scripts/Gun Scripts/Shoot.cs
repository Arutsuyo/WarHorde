using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public float damage = 10f;
    public bool automatic = false;
	public GameObject bullet = null;
	public GameObject pos = null;
	public float force = 200f;
	public ParticleSystem flash = null;

	void Awake()
	{
		/*Get our references
		gunShotLine = GetComponentInChildren<LineRenderer>();
		gunShotLight = gunShotLine.gameObject.light;

		gunShotLine.enabled = false;
        */      
	}

	// Update is called once per frame
	void Update () {
        if (automatic)
        {
            if (Input.GetButton("Fire1"))
                Shooting();
        } else
        {
            if (Input.GetButtonDown("Fire1"))
                Shooting();
        }

	}
	void Shooting()
	{
		/*GameObject shot = GameObject.Instantiate( bullet, pos.transform.position, pos.transform.rotation ) as GameObject;
		shot.rigidbody.AddForce( transform.right * force);*/
		
		flash.Play();

		RaycastHit hitInfo;
		Ray ray = new Ray( pos.transform.position, pos.transform.TransformDirection( Vector3.back ) );
		if ( Physics.Raycast( ray, out hitInfo ) )
		{

			//If what we hit is tagged as an ememy, make them take damage
			if (hitInfo.collider.tag == "Enemy")
			{
				//Save the transform of what we hit
				Transform target = hitInfo.transform;

				//Reference to the EnemyHealth class of our target
				EnemyHealth targetHealth = null;

				//Go up through the our enemy until we find an object with am EnemyHealth class
				while (targetHealth == null)
				{
					targetHealth = target.GetComponent<EnemyHealth>();
					target = target.parent;
				}

				targetHealth.TakeDamage(damage);
			}
		}
	}
}
