using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {
	private TimedObjectDestructor des;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy( this.gameObject, 5f );
	}

	void OnCollisionEnter(Collision collision)
	{
		DestroyImmediate( this.gameObject );
	}
}
