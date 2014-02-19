using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
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
			hit.text = hitInfo.collider.tag.ToString();
	}
}
