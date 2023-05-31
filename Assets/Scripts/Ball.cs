using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Ball;

[RequireComponent (typeof (Rigidbody))]
public class Ball : MonoBehaviour {
	[SerializeField] private float velocityMultiplier = 1.5f;
	[SerializeField] private PhysicMaterial physicMaterial;
	[SerializeField] private List<ParticleSystem> effects;
	[SerializeField] private BallState ballState;
	[SerializeField] private List<AudioClip> audioClips;
	[SerializeField] private AudioSource audioSource;
	private Rigidbody rb;
	private Club club;
	private GameManager gameManager;
	private bool allowMovement = true;
	private bool onCourse = true;
	public Vector3 previousPos;
	public Vector3 initialPos;
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
		previousPos = transform.position;
		initialPos = transform.position;
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		rb = GetComponent<Rigidbody> ();
		//audioSource.GetComponent<AudioSource>();
		club = GameObject.FindGameObjectWithTag("Club").GetComponent<Club>();
    }
	private void OnTriggerEnter(Collider other){
		if(other.tag == "Club" && ballState != BallState.Freezed && allowMovement) {
			StartCoroutine("TriggerCoolDown");
			rb.velocity = club.getVelocity().normalized * velocityMultiplier;
			audioSource.PlayOneShot(audioClips[1]);
			Debug.Log("Trigger Entered!!! : " + club.clubVelocity.normalized * velocityMultiplier);
		}
		if (other.name == "Hole") {
			gameManager.NextLevel();
		}else if (other.tag == "PickupShoot") {
			audioSource.PlayOneShot(audioClips[0]);
		}
		else if (other.tag == "PickupKill") {
			audioSource.PlayOneShot(audioClips[0]);
		}
		else if (other.tag == "PickupSpeed") {
			audioSource.PlayOneShot(audioClips[0]);
			StartCoroutine("SpeedyEffectCoolDown", other.gameObject);
		}
		else if (other.tag == "PickupClean") {
			audioSource.PlayOneShot(audioClips[0]);
			other.gameObject.SetActive(false);
			ballState = BallState.Normal;
		}else if (other.tag == "PickupRandom") {
			int num = Random.Range(0, 6);
			audioSource.PlayOneShot(audioClips[0]);
			Destroy(other.gameObject);
			switch (num) {
				case 0:
					StartCoroutine("PoisonedEffectCoolDown", 10f);
					break;
				case 1:
					StartCoroutine("SlowedEffectCoolDown", 10f);
					break;
				case 2:
					StartCoroutine("OnFireEffectCoolDown", 10f);
					break;
				case 3:
					StartCoroutine("SpeedyEffectCoolDown", other.gameObject);
					break;
				case 4:
					StartCoroutine("BouncyEffectCoolDown", 10f);
					break;
				case 5:
					StartCoroutine("FreezedEffectCoolDown", 10f);
					break;
				default:
					ballState = BallState.Normal;
					break;
			}
			ballState = BallState.Normal;
		}else if (other.tag == "Hole"){
			gameManager.NextLevel();
		}
	}
	private void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag == "Club"){
			rb.velocity = rb.velocity * velocityMultiplier;
			Debug.Log("Ball Velocity: "+ rb.velocity);
		}else if(collision.gameObject.tag == "Course" && onCourse || collision.gameObject.tag == "DownStairs" && onCourse){
			onCourse = false;
			Debug.Log("OutOfTheCourse");
			StartCoroutine("ResetCoolDown");
		}
	}
	private void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Club"){
			previousPos = transform.position;
		}else if(collision.gameObject.tag == "Course" || collision.gameObject.tag == "DownStairs"){
			onCourse = true;
		}
	}
	private void OnCollisionStay(Collision collision){
		if(collision.gameObject.tag == "Course" || collision.gameObject.tag == "DownStairs"){
			onCourse = true;
		}
	}
	public void ApplyState(BallState state) {
		ballState = state;
	}
	IEnumerator TriggerCoolDown() {
		allowMovement = false;
		yield return new WaitForSeconds(1.0f);
		allowMovement = true;
    }
	IEnumerator PoisonedEffectCoolDown(float time) {
		effects[5].Play();
		yield return new WaitForSeconds(time);
		effects[5].Stop();
	}
	IEnumerator SlowedEffectCoolDown(float time) {
		effects[2].Play();
		ballState = BallState.Slowed;
		velocityMultiplier /= 2;
		yield return new WaitForSeconds(10f);
		effects[2].Stop();
		ballState = BallState.Normal;
		velocityMultiplier *= 2;
	}
	IEnumerator OnFireEffectCoolDown(float time) {
		physicMaterial.bounceCombine = PhysicMaterialCombine.Maximum;
		effects[6].Play();
		yield return new WaitForSeconds(time);
		physicMaterial.bounceCombine = PhysicMaterialCombine.Average;
		effects[6].Stop();
	}
	IEnumerator BouncyEffectCoolDown(float time) {
		//physicMaterial.bounceCombine = PhysicMaterialCombine.Maximum;
		physicMaterial.bounciness = 1f;
		yield return new WaitForSeconds(time);
		//physicMaterial.bounceCombine = PhysicMaterialCombine.Average;
		physicMaterial.bounciness = 0.9f;
	}
	IEnumerator FreezedEffectCoolDown(float time) {
		ballState = BallState.Freezed;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.isKinematic = true;
		yield return new WaitForSeconds(time);
		rb.isKinematic = false;
		ballState = BallState.Normal;
	}
	IEnumerator SpeedyEffectCoolDown(GameObject other) {
		other.gameObject.SetActive(false);
		effects[3].Play();
		ballState = BallState.Speedy;
		velocityMultiplier *= 2;
		yield return new WaitForSeconds(10f);
		effects[3].Stop();
		other.gameObject.SetActive(true);
		ballState = BallState.Normal;
		velocityMultiplier /= 2;
	}
	IEnumerator StopWaitTime(){
		yield return new WaitForSeconds(2f);
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}
	IEnumerator ResetCoolDown(){
		bool var1 = false;
		bool var2 = false;
		bool var3 = false;
		yield return new WaitForSeconds(1f);
		var1 = !onCourse;
		yield return new WaitForSeconds(1f);
		var2 = !onCourse;
		yield return new WaitForSeconds(1f);
		var3 = !onCourse;
		yield return new WaitForSeconds(2f);
		if(!onCourse && var1 && var2 && var3){
			gameManager.PreviousBallPosition();
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
	}
}