using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipOrbit : MonoBehaviour {
	private float speed = 1f;
	private float radius = 700f;
	private bool allowMovemnet = false;
	private Transform pivot;
	private Transform player;
	private Vector3 axis = new Vector3(0f, 1f, 0f);

	public Transform parentP;
	void Start() {
		pivot = GameObject.FindGameObjectWithTag("Pivot").transform;
		player = GameObject.FindGameObjectWithTag("XROrigin").transform;
	}

	void Update() {
		if (allowMovemnet) {
			Vector3 newPosition = pivot.position + (Quaternion.AngleAxis(speed * Time.time, axis) * (Vector3.right * radius));
			transform.LookAt(pivot);
			transform.Rotate(Vector3.up, -90f);
			transform.position = newPosition;
		}
		if(Input.GetKeyDown(KeyCode.Space)) {
			startMovement();
		}
	}
	public void startMovement() {
		player.SetParent(parentP);
		player.localPosition = new Vector3(0, 0, 0);
		allowMovemnet = true;
	}
}