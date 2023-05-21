using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGoal : MonoBehaviour{
    private GameObject ship;
	private GameObject player;
    private GameObject ball;
	private SpaceShipMovement spaceShipMovement;
	// Start is called before the first frame update
	private void Start() {
		ball = GameObject.FindGameObjectWithTag("Ball");
		ship = GameObject.FindGameObjectWithTag("Ship");
		player = GameObject.FindGameObjectWithTag("XROrigin");
		spaceShipMovement = ship.GetComponent<SpaceShipMovement>();

	}
	private void OnTriggerEnter(Collider other) {
		if(other.tag == "Ball") {
			player.transform.SetParent(ship.transform);
			player.transform.localPosition = new Vector3(0, 2, 1);
			ball.GetComponent<Rigidbody>().useGravity = false;
			ball.transform.SetParent(ship.transform);
            ball.transform.localPosition = new Vector3(0, 2, 1);
			spaceShipMovement.allowMovemnet = true;
			
			Debug.Log("Trigger Entered!!!");
		}
	}
}