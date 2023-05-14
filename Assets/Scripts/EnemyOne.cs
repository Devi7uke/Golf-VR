using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne: MonoBehaviour {
	[SerializeField] private GameObject LeftGun;
	[SerializeField] private GameObject RightGun;
	[SerializeField] private float time = 10f;
	[SerializeField] private float waitTime = 10f;
	private bool enableShot = false;
	private Animator animator;
	
	private void Start() {
		animator = GetComponent<Animator>();
		StartCoroutine("InitalWaitTime");
	}
	private void Update() {
		if (enableShot) {
			animator.SetInteger("Shot", Random.Range(1, 6));
			StartCoroutine("CoolDown");
		}
	}
	IEnumerator InitalWaitTime() {
		yield return new WaitForSeconds(waitTime);
		enableShot = true;

	}
	IEnumerator CoolDown() {
		enableShot = false;
		LeftGun.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
		yield return new WaitForSeconds(time/5.0f);
		RightGun.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
		animator.SetInteger("Shot", 0);
		yield return new WaitForSeconds(time);
		enableShot = true;
	}
}