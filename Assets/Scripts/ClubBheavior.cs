using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubBheavior : MonoBehaviour{
	[SerializeField] private GameObject rayCaster;
	[SerializeField] private float maxClubVelocity = 50f;
	[SerializeField] private float maxImpulseForce = 1000f;
	private Vector3 starPosition;
	private Vector3 predictLineDirection;
	private Rigidbody ballRigidbody;
	private Rigidbody rb;
	private GameObject ball;

	private void Start(){
		rb = GetComponent<Rigidbody>();
		ball = GameObject.FindGameObjectWithTag("Ball");
		starPosition = ball.transform.position;
		ballRigidbody = ball.GetComponent<Rigidbody>();
	}
	private void Update(){
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartBallPosition();
		}
		if(Input.GetKeyDown(KeyCode.E)) {
			Vector3 torqueDirection = transform.forward;
			rb.AddTorque(torqueDirection * 100f);
		}
		SpeculteDirection();
	}
	private void SpeculteDirection() {
		RaycastHit hit;
		float maxHitDistance = 1f;
		if(Physics.Raycast(rayCaster.transform.position, rayCaster.transform.forward, out hit, maxHitDistance)) {
			//Debug.DrawRay(rayCaster.transform.position, rayCaster.transform.forward * hit.distance, Color.blue, 0.01f);
			if (hit.collider.CompareTag("Ball")) {
				Debug.Log("Raycast collision");
				predictLineDirection = (transform.GetComponent<Rigidbody>().velocity + transform.right).normalized / 2;
				ball.GetComponent<LineRenderer>().SetPosition(1, new Vector3(predictLineDirection.x, 0, predictLineDirection.z));
			}
		}
		else {
			ball.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
		}
	}
	private void RestartBallPosition(){
		ball.transform.position = starPosition;
		ballRigidbody.velocity = Vector3.zero;
	}
	private void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Ball"){
			float clubVelocity = GetComponent<Rigidbody>().angularVelocity.magnitude;
			float normalizedClubVelocity = Mathf.Clamp01(clubVelocity / maxClubVelocity);
			float impulseForce = normalizedClubVelocity * maxImpulseForce;
			Vector3 impactDirection = transform.right;
			var forceVector = impactDirection * impulseForce;
			ballRigidbody.AddForce(forceVector / 20, ForceMode.Impulse);
			Debug.Log("Collision Entered !!!  " + forceVector);
		}
	}
}