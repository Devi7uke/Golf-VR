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
	private BallState ballState;
	private bool allowMovement = true;
	public enum BallState {
		Normal,
		Poisoned,
		Slowed,
		OnFire,
		Speedy,
		Bouncy,
		Freezed
	}
	void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		rb = GetComponent<Rigidbody> ();
		club = GameObject.FindGameObjectWithTag("Club").GetComponent<Club>();
    }
	private void OnTriggerEnter(Collider other){
		if (other.name == "Hole") {
			gameManager.NextLevel();
		}else if (other.tag == "PickupShoot") {

		}else if (other.tag == "PickupKill") {

		}else if (other.tag == "PickupSpeed") {
			ballState = BallState.Speedy;
		}else if (other.tag == "PickupClean") {
			ballState = BallState.Normal;
		}else if (other.tag == "PickupRandom") {
			int num = Random.Range(0, 6);
			switch (num) {
				case 0:
					ballState = BallState.Poisoned;
					break;
				case 1:
					ballState = BallState.Slowed;
					break;
				case 2:
					ballState = BallState.OnFire;
					break;
				case 3:
					ballState = BallState.Speedy;
					break;
				case 4:
					ballState = BallState.Bouncy;
					break;
				case 5:
					ballState = BallState.Freezed;
					break;
				default:
					ballState = BallState.Normal;
					break;
			}
			ballState = BallState.Normal;
		}
	}
	public void ApplyState(BallState state) {
		ballState = state;
	}
	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Club" && allowMovement) {
			gameManager.SetPreviousBallPosition();
			ContactPoint contact = collision.contacts[0];
			Vector3 direction = contact.point - transform.position;
			direction = direction.normalized;
			rb.AddForce(direction * club.clubVelocity.magnitude * forceMultiplier, ForceMode.Acceleration);
			Debug.Log(direction * club.clubVelocity.magnitude * forceMultiplier);
		}
	}
	private IEnumerator PoisonedEffectCoolDown(float time) {
		yield return new WaitForSeconds(time);
	}
	private IEnumerator SlowedEffectCoolDown(float time) {
		yield return new WaitForSeconds(time);
	}
	private IEnumerator OnFireEffectCoolDown(float time) {
		yield return new WaitForSeconds(time);
	}
	private IEnumerator SpeedyEffectCoolDown(float time) {
		yield return new WaitForSeconds(time);
	}
	private IEnumerator BouncyEffectCoolDown(float time) {
		yield return new WaitForSeconds(time);
	}
	private IEnumerator FreezedEffectCoolDown(float time) {
		yield return new WaitForSeconds(time);
	}
}