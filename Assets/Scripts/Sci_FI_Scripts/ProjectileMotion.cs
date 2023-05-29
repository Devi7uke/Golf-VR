using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ProjectileMotion : MonoBehaviour {
	public GameObject areaEffect;
	private GameObject target;
	private float gravity = 9.81f;
	private float initialSpeed = 15;
	private float distance = 0f;
	private float launchAngle = 0f;
	private AudioSource audioSource;
	private ParticleSystem particles;
	[SerializeField] private AlienEnemyOne alienEnemyOne;
	// Start is called before the first frame update
	void Start() {
		particles = GetComponent<ParticleSystem>();
		target = GameObject.FindGameObjectWithTag("Ball");
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update() {
		distance = Vector3.Distance(target.transform.position, transform.position);
		//float correctionAngle = Mathf.Atan((target.transform.position.y - transform.position.y) / distance) * Mathf.Rad2Deg;
		//launchAngle = (Mathf.Rad2Deg * Mathf.Asin(gravity * distance / Mathf.Pow(initialSpeed, 2)) / 2)+ correctionAngle;
		if(distance < alienEnemyOne.distanceDetection){
			launchAngle = (Mathf.Rad2Deg * Mathf.Asin(gravity * distance / Mathf.Pow(initialSpeed, 2)) / 2);
			transform.LookAt(target.transform.position);
			transform.Rotate(-launchAngle, 0, 0);
		}
	}
	void OnParticleCollision(GameObject other) {
		//Debug.Log("Particle collided with " + other.name);
		List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
		particles.GetCollisionEvents(other, collisionEvents);
		foreach (ParticleCollisionEvent collisionEvent in collisionEvents) {
			Vector3 position = collisionEvent.intersection;
			Instantiate(areaEffect, position, transform.rotation);
		}
	}
}