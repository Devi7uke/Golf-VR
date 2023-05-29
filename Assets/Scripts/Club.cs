using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.OpenXR.Input;

public class Club : MonoBehaviour {
	[SerializeField] private GameObject rayCaster;
	private GameObject ball;
	[SerializeField]
	private float maxVelocity = 20f;
	private Vector3 startPosition;
	private Vector3 previousPosition;
	private Vector3 predictLineDirection;
	private Quaternion previousRotation;
	private Rigidbody rb;

	public Vector3 clubVelocity;
	public float torque = 100f;

	// Start is called before the first frame update
	void Start() {
		previousPosition = transform.position;
		previousRotation = transform.rotation;
		ball = GameObject.FindGameObjectWithTag("Ball");
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update() {
        clubVelocity = ClubVelocity();
        //clubVelocity = rb.angularVelocity;
		if(Input.GetKeyDown(KeyCode.U)) {
			rb.AddTorque(Vector3.right * torque);
		}
	}

    private Vector3 ClubVelocity() {
		Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);
		float angleInDegrees;
		Vector3 rotationAxis;
		deltaRotation.ToAngleAxis(out angleInDegrees, out rotationAxis);
		float angularVelocity = angleInDegrees / Time.deltaTime;
		Vector3 linearVelocity = (transform.position - previousPosition) / Time.deltaTime;
		previousPosition = transform.position;
		previousRotation = transform.rotation;
		return new Vector3(linearVelocity.x + angularVelocity, linearVelocity.y, linearVelocity.z + angularVelocity);
	}

	public Vector3 getVelocity() {
		//Calculate velocity
		Vector3 velocity = (transform.position - previousPosition) / Time.deltaTime;
		if (velocity.magnitude > maxVelocity) {
			velocity = velocity * (maxVelocity / velocity.magnitude);
		}
		return new Vector3(velocity.x, 0, velocity.z);
	}
}