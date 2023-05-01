using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof(LineRenderer))]
public class Ball : MonoBehaviour {
	[SerializeField]
	private float hitForce = 1000f;
	private Rigidbody rb;
	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody> ();
	}
	void Update(){

	}
	private void ResetPosition(){

	}
	private void NextLevel(){

	}
	private void OnTriggerEnter(Collider other){
		if (other.name == "Limits") {
			ResetPosition();
		} else if (other.name == "Hole") {
			NextLevel();
		}
	}
	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Club") {
			ContactPoint contact = collision.contacts[0];
			Vector3 direction = contact.point - transform.position;
			direction = direction.normalized;
			rb.AddForce(direction * hitForce);
		}
	}
}