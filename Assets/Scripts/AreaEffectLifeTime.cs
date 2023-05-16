using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectLifeTime : MonoBehaviour{
	private BoxCollider boxCollider;
	private GameObject ball;
	[SerializeField] private List<AudioClip> audioClips;
	private AudioSource audioSource;
	void Start(){
		ball = GameObject.FindGameObjectWithTag("Ball");
		boxCollider = GetComponent<BoxCollider>();
		audioSource = GetComponent<AudioSource>();
		audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
		StartCoroutine("LifeTime");
	}
	private void Update() {
		//float size = Mathf.Sin(Time.time) + 1f;
		//boxCollider.size = new Vector3(size, size, size);
		//boxCollider.center = new Vector3(Mathf.Cos(Time.time), 0f, 0f);
	}
	IEnumerator LifeTime() {
		yield return new WaitForSeconds(7.0f);
		Destroy(gameObject);
	}
	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Ball") {
			ball.GetComponent<Rigidbody>().AddForce(2 * new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), ForceMode.Impulse);
		}
	}
}