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
	private GameManager gameManager;
	// Start is called before the first frame update
	void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		rb = GetComponent<Rigidbody> ();
		club = GameObject.FindGameObjectWithTag("Club").GetComponent<Club> ();
    }
	private void OnTriggerEnter(Collider other){
		if (other.name == "Hole") {
			gameManager.NextLevel();
		}
	}
	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Club") {
			gameManager.SetPreviousBallPosition();
			ContactPoint contact = collision.contacts[0];
			Vector3 direction = contact.point - transform.position;
			direction = direction.normalized;
			rb.AddForce(direction * club.clubVelocity.magnitude * forceMultiplier, ForceMode.Acceleration);
			Debug.Log(direction * club.clubVelocity.magnitude * forceMultiplier);
		}
	}
}