using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof(LineRenderer))]
public class Ball : MonoBehaviour {
	[SerializeField]
	private float forceMultiplier = 5f;
	private Rigidbody rb;
	private Club club;
	private Vector3 startPosition;
	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody> ();
		club = GameObject.FindGameObjectWithTag("Club").GetComponent<Club> ();
		startPosition = transform.position;
    }
	void Update(){

	}
	private void ResetPosition(){
		rb.velocity = Vector3.zero;
		transform.position = startPosition;
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
			rb.AddForce(direction * club.clubVelocity.magnitude * forceMultiplier, ForceMode.Acceleration);
			Debug.Log(direction * club.clubVelocity.magnitude * forceMultiplier);
		}
	}
}