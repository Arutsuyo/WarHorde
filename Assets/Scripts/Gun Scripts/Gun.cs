using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour {
    private Queue<Clip> clips = new Queue<Clip>();
    private Clip currentClip = null;
	public int ammo = 0;
	public GameObject bullet = null;
	public GameObject pos = null;
	public float force = 200f;
	public ParticleSystem flash = null;
    public int pickup = 0;
    public GUIText hit = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (pickup > 0)
            newClip();
		if(Input.GetButtonDown("Fire1"))
			Shooting();
		if (currentClip.empty == true || Input.GetButtonDown("Reload")) 
            if (reload())
            {
                //Queue Reload animation
            }
    }

	void newClip () {
        clips.Enqueue(new Clip(ammo));
        pickup--;
		}

	bool reload(){
		if (clips.Count > 0)
        {
            currentClip = clips.Dequeue();
            return true;
        } else
            return false;
	}

	bool fire(){
        return currentClip.shoot();
		}

	void Shooting()
	{
		if (fire())
        {
            GameObject shot = GameObject.Instantiate(bullet, 
                                                     pos.transform.position, 
                                                     pos.transform.rotation) 
                as GameObject;
            shot.rigidbody.AddForce(transform.right * force);
		
            flash.Play();
		
            RaycastHit hitInfo;
            Ray ray = new Ray(pos.transform.position, pos.transform.TransformDirection(Vector3.back));
            if (Physics.Raycast(ray, out hitInfo))
                if(hit != null)
                hit.text = hitInfo.collider.tag.ToString();
        }
	}
}
