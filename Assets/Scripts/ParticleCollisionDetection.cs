using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionDetection : MonoBehaviour {
	private ParticleSystem particles;  

	private void Start() {
		particles = GetComponent<ParticleSystem>();
	}
	void OnParticleCollision(GameObject other) {
		Debug.Log("Particle collided with " + other.name);
		//List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
		//particles.GetCollisionEvents(other, collisionEvents);

		/*foreach (ParticleCollisionEvent collisionEvent in collisionEvents) {
			Vector3 position = collisionEvent.intersection;
			Vector3 normal = collisionEvent.normal;

			Debug.Log("Collision position: " + position);
			Debug.Log("Collision normal: " + normal);
		}*/
	}
}