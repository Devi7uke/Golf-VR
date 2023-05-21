using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemyOne: MonoBehaviour {
	[SerializeField] private GameObject LeftGun;
	[SerializeField] private GameObject RightGun;
	[SerializeField] private float time = 10f;
	[SerializeField] private float waitTime = 10f;
	[SerializeField] private float distanceDetection = 15f;
	[SerializeField] private List<AudioClip> roars;
	[SerializeField] private List<AudioClip> launch;
	private AudioSource audioSource;
	private bool enableShot = false;
	private Animator animator;
	private GameObject ball;
	
	private void Start() {
		animator = GetComponent<Animator>();
		ball = GameObject.FindGameObjectWithTag("Ball");
		audioSource = GetComponent<AudioSource>();
		StartCoroutine("InitalWaitTime");
	}
	private void Update() {
		if (enableShot && Vector3.Distance(transform.position, ball.transform.position) < distanceDetection) {
			animator.SetInteger("Shot", Random.Range(1, 6));
			StartCoroutine("CoolDown");
		}
		transform.LookAt(ball.transform);
		RotateGuns();
	}
	private void RotateGuns() {
		LeftGun.transform.LookAt(ball.transform);
		RightGun.transform.LookAt(ball.transform);
	}
	IEnumerator InitalWaitTime() {
		yield return new WaitForSeconds(waitTime);
		enableShot = true;

	}
	IEnumerator CoolDown() {
		enableShot = false;
		LeftGun.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
		LeftGun.transform.GetChild(0).GetComponent<AudioSource>().pitch = 2;
		LeftGun.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(launch[Random.Range(0, launch.Count)]);
		audioSource.PlayOneShot(roars[Random.Range(0, roars.Count)]);
		yield return new WaitForSeconds(time / 5.0f);
		if (Vector3.Distance(transform.position, ball.transform.position) < distanceDetection) {
			RightGun.transform.GetChild(0).GetComponent<AudioSource>().pitch = 2;
			RightGun.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
			RightGun.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(launch[Random.Range(0, launch.Count)]);
		}
		animator.SetInteger("Shot", 0);
		yield return new WaitForSeconds(time);
		enableShot = true;
	}
}