using System.Collections;
using System.Collections.Generic;
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

	/*private float ClubVelocity() {
		Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);
		float angleInDegrees;
		Vector3 rotationAxis;
		deltaRotation.ToAngleAxis(out angleInDegrees, out rotationAxis);
		float angularSpeed = angleInDegrees / Time.deltaTime;
		Vector3 linearVelocity = (transform.position - previousPosition) / Time.deltaTime;
		float linearSpeed = linearVelocity.magnitude;
		Vector3 direction = linearVelocity.normalized;
		float totalVelocity = Mathf.Sqrt(Mathf.Pow(angularSpeed, 2) + Mathf.Pow(linearSpeed, 2));
		previousPosition = transform.position;
		previousRotation = transform.rotation;
		return totalVelocity;
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Ball" && allowStroke)
		{
			allowStroke = false;
			Vector3 forceDirection = ball.transform.position - transform.position;
			forceDirection = forceDirection.normalized;
			Vector3 clubFaceNormal = GetClubFaceNormal(gameObject, collision.contacts[0].point);
			float angle = Vector3.Angle(clubFaceNormal, -forceDirection);
			float forceMangnitude = CalculateForceMagnitude(angle);
			Vector3 rotatedForceVector = Quaternion.Euler(0, 90, 0) * (forceMangnitude * clubFaceNormal);
			gameObject.GetComponent<BoxCollider>().enabled = false;
			gameObject.GetComponent<MeshCollider>().enabled = false;
			Vector3 forceVector = new Vector3(Mathf.Abs(rotatedForceVector.x), (rotatedForceVector.y < 0) ? 0 : rotatedForceVector.y, Mathf.Abs(rotatedForceVector.z)) *  angularSpeed / 10;
			ballRB.AddForce(forceVector, ForceMode.Acceleration);
			Debug.Log(forceVector + " " + angularSpeed);
			Debug.DrawRay(ball.transform.position, forceVector, Color.cyan, 1.0f);
		}
	}
	float CalculateForceMagnitude(float angle)
	{
		// Clamp the angle to a maximum of 45 degrees
		angle = Mathf.Clamp(angle, 0.0f, 45.0f);

		// Map the angle to a force magnitude between 0.0 and 1.0
		float normalizedAngle = angle / 45.0f;
		float forceMagnitude = Mathf.Lerp(0.0f, 1.0f, normalizedAngle);

		// Scale the force magnitude by the desired maximum force
		forceMagnitude *= force;

		return forceMagnitude;
	}
	private void RestartBallPosition()
	{
		ball.transform.position = starPosition;
		ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
	Vector3 GetClubFaceNormal(GameObject club, Vector3 pointOfImpact)
	{
		Mesh clubMesh = club.GetComponent<MeshFilter>().mesh;
		int[] triangles = clubMesh.triangles;
		Vector3 closestFaceNormal = Vector3.zero;
		float closestDistance = float.MaxValue;

		// Iterate over each triangle face in the mesh
		for (int i = 0; i < triangles.Length; i += 3)
		{
			Vector3 v1 = clubMesh.vertices[triangles[i]];
			Vector3 v2 = clubMesh.vertices[triangles[i + 1]];
			Vector3 v3 = clubMesh.vertices[triangles[i + 2]];

			// Calculate the normal of the face
			Vector3 faceNormal = Vector3.Cross(v2 - v1, v3 - v1).normalized;

			// Calculate the distance from the face to the point of impact
			float distance = Vector3.Distance(pointOfImpact, v1);

			// If this face is closer to the point of impact than the previous closest face, set it as the new closest face
			if (distance < closestDistance)
			{
				closestDistance = distance;
				closestFaceNormal = faceNormal;
			}
		}
		return closestFaceNormal;
	}
	private float AngularRotationSpeed()
	{
		Vector3 ballPosition = ball.transform.position;
		Quaternion rotationChange = transform.rotation * Quaternion.Inverse(prevRotation);

		// Calculate the angular velocity based on the rotation change
		float angleInDegrees;
		Vector3 rotationAxis;
		rotationChange.ToAngleAxis(out angleInDegrees, out rotationAxis);
		float angularSpeed = angleInDegrees / Time.deltaTime;

		// Calculate the direction of the rotation based on the dot product of the rotation axis and the vector from the club face to the ball
		Vector3 clubFaceNormal = GetClubFaceNormal(gameObject, ballPosition);
		float dotProduct = Vector3.Dot(rotationAxis, clubFaceNormal);
		float direction = Mathf.Sign(dotProduct);

		// Update the previous rotation for the next frame
		prevRotation = transform.rotation;

		return angularSpeed * direction;
	}
	public class Player : MonoBehaviour
{
	[SerializeField]
	private GameObject ball;
	private Rigidbody rb;
	private Rigidbody ballRB;
	[SerializeField]
	private float force = 10.0f;
	private bool allowStroke = true;
	private Vector3 starPosition;
	private Quaternion prevRotation;
	private float angularSpeed;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		ballRB = ball.GetComponent<Rigidbody>();
		starPosition = ball.transform.position;
		prevRotation = transform.rotation;

	}

	// Update is called once per frame
	void Update()
	{

		if (ball.GetComponent<Rigidbody>().velocity == Vector3.zero)
		{
			allowStroke = true;
			ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			gameObject.GetComponent<BoxCollider>().enabled = true;
			gameObject.GetComponent<MeshCollider>().enabled = true;

		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			RestartBallPosition();
		}
		angularSpeed = AngularRotationSpeed();
	}
}
	 
	 */

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