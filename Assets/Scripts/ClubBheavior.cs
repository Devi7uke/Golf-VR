using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class ClubBheavior : MonoBehaviour{
	[SerializeField] private GameObject rayCaster;
	[SerializeField] private float maxClubVelocity = 50f;
	[SerializeField] private float maxImpulseForce = 1000f;
	private Vector3 starPosition;
	private Vector3 predictLineDirection;
	private Vector3 prevPosition;
	private Quaternion prevRotation;
	private Rigidbody ballRigidbody;
	private Rigidbody rb;
	private GameObject ball;
	private Vector3 velocity;
	public float toruqe = 200f;
	
	private void Start(){
		rb = GetComponent<Rigidbody>();
		ball = GameObject.FindGameObjectWithTag("Ball");
		starPosition = ball.transform.position;
		ballRigidbody = ball.GetComponent<Rigidbody>();
		prevPosition = transform.position;
		prevRotation = transform.rotation;
	}
	private void Update(){
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartBallPosition();
		}
		if(Input.GetKeyDown(KeyCode.E)) {
			Vector3 torqueDirection = transform.forward;
			rb.AddTorque(torqueDirection * toruqe);
		}
		SpeculteDirection();
		velocity = TotalVelocity();
	}
	private Vector3 TotalVelocity() {
		Vector3 ballPosition = ball.transform.position;
		Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(prevRotation);

		float angleInDegrees;
		Vector3 rotationAxis;
		deltaRotation.ToAngleAxis(out angleInDegrees, out rotationAxis);
		float angularSpeed = angleInDegrees / Time.deltaTime;

		Vector3 linearVelocity = (transform.position - prevPosition) / Time.deltaTime;
		float linearSpeed = linearVelocity.magnitude;

		Vector3 direction = linearVelocity.normalized;

		//float totalVelocity = Mathf.Sqrt(Mathf.Pow(angularSpeed, 2) + Mathf.Pow(linearSpeed, 2));
		Vector3 totalVelocity = direction * linearSpeed + transform.up * angularSpeed;

		prevPosition = transform.position;
		prevRotation = transform.rotation;

		return totalVelocity;
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
			//float clubVelocity = GetComponent<Rigidbody>().angularVelocity.magnitude;
			//float normalizedClubVelocity = Mathf.Clamp01(velocity / maxClubVelocity);
			float normalizedClubVelocity = Mathf.Clamp01(rb.angularVelocity.magnitude / maxClubVelocity);
			float impulseForce = normalizedClubVelocity * maxImpulseForce;
			Vector3 impactDirection = transform.TransformDirection(rayCaster.transform.right);
			var forceVector = impactDirection * impulseForce;
			ballRigidbody.AddForce(forceVector / 30, ForceMode.Impulse);
			Debug.Log("Collision Entered !!!  " + forceVector);
		}
	}
}