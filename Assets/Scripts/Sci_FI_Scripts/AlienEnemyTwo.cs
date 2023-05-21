using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemyTwo : MonoBehaviour {
	private Animator animator;
	private GameObject target;
	private bool allowAttack = false;
	private AudioSource audioSource;
	[SerializeField] private float interval = 10.0f;
	[SerializeField] private float initialWait = 10.0f;
	[SerializeField] private List<GameObject> particles;
	[SerializeField] private List<AudioClip> spiderSounds;
	// Start is called before the first frame update
	void Start() {
		animator = GetComponent<Animator>();
		target = GameObject.FindGameObjectWithTag("Ball");
		audioSource = GetComponent<AudioSource>();
		StartCoroutine("InitialDelay");
	}

	// Update is called once per frame
	void Update() {
		transform.LookAt(target.transform.position);
		if (Vector3.Distance(transform.position, target.transform.position) < 9 && allowAttack) {
			allowAttack = false;
			StartCoroutine("CoolDown");
		}
	}
	IEnumerator InitialDelay() {
		yield return new WaitForSeconds(initialWait);
		allowAttack = true;
	}
	IEnumerator CoolDown() {
		animator.SetInteger("Attack", Random.Range(1, 6));
		particles[Random.Range(0, particles.Count)].GetComponent<ParticleSystem>().Play();
		audioSource.PlayOneShot(spiderSounds[Random.Range(0, spiderSounds.Count)]);
		yield return new WaitForSeconds(1);
		animator.SetInteger("Attack", 0);
		yield return new WaitForSeconds(interval);
		allowAttack = true;
		animator.SetInteger("Attack", 0);
	}
}