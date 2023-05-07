using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets.DynamicMoveProvider;

public class SpaceShipMovement : MonoBehaviour{
	public GameObject targetZero;
	[SerializeField]
	private float speed = 1f;
	[SerializeField]
	private float rotationSpeed = 0.1f;
	private Transform target;
	public bool allowMovemnet = false;
	public bool zeroTarget = false;
	// Start is called before the first frame update
	void Start(){
		target = GameObject.FindGameObjectWithTag("Target").transform;
	}

	// Update is called once per frame
	void Update() {
		if (allowMovemnet) {
			if (!zeroTarget) {
				Vector3 direction = targetZero.transform.position - transform.position;
				direction.Normalize();
				transform.position += direction * 1f * Time.deltaTime;
			} else {
				Vector3 direction = target.position - transform.position;
				direction.Normalize();
				Quaternion targetRotation = Quaternion.LookRotation(direction);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
				transform.position += direction * speed * Time.deltaTime;
			}
		}
	}
	private void OnTriggerEnter(Collider other) {
		if(other.tag == "Target") {
			Debug.Log("Hole in: ");
			allowMovemnet = false;
		}else if(other.tag == "TargetZero") {
			zeroTarget = true;
		}
	}
}