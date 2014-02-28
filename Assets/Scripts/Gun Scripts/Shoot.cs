using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public float damage = 10f;
    public bool automatic = false;
	public GameObject bullet = null;
	public GameObject pos = null;
	public float force = 200f;
	public ParticleSystem flash = null;

	public float shotTime = 0.25f;
	public float flashIntensity = 3f;
	public float fadeSpeed = 10f;

	private LineRenderer gunShotLine;
	private Light gunShotLight;
	private float timer = 0f;
	private bool isShooting;


	void Awake()
	{
		//Get our references
		gunShotLine = GetComponentInChildren<LineRenderer>();
		gunShotLight = gunShotLine.gameObject.light;

		gunShotLine.enabled = false;
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

		if (isShooting == true)
		{
			timer += Time.deltaTime;

			if (timer >= shotTime)
			{
				gunShotLine.enabled = false;
			}
		}

		gunShotLight.intensity = Mathf.Lerp(gunShotLight.intensity, 0f, fadeSpeed * Time.deltaTime);
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
			gunShotLine.enabled = true;
			gunShotLine.SetPosition(0, gunShotLine.transform.position);
			gunShotLine.SetPosition(1, hitInfo.point);
			gunShotLight.intensity = flashIntensity;
			
			isShooting = true;
			timer = 0f;

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
