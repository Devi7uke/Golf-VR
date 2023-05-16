using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardsDetection : MonoBehaviour {
	private ParticleSystem particles;
	private ParticleCollisionEvent collisionEvents;
	private GameObject ball;
	private Ball ballScript;
	// Start is called before the first frame update
	void Start() {
		particles = GetComponent<ParticleSystem>();
		ball = GameObject.FindGameObjectWithTag("Ball");
		ballScript = ball.GetComponent<Ball>();
	}
	// Update is called once per frame
	void Update() {
		
	}
	void OnCollisionEnter(Collision collision) {
		Debug.Log(collision.gameObject.name);
	}
	private void OnParticleCollision(GameObject other) {
		Debug.Log(other.name);
	}
	void OnParticleTrigger() {
		ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, Random.Range(0f, 2f), 0), ForceMode.Impulse);
		if (gameObject.name == "ShardMagic") {
			ballScript.ApplyState(Ball.BallState.Poisoned);
			Debug.Log("ShardMagic");
		}else if (gameObject.name == "ShardVine") {
			ballScript.ApplyState(Ball.BallState.Slowed);
			Debug.Log("ShardVine");
		}else if (gameObject.name == "ShardIce") {
			ballScript.ApplyState(Ball.BallState.Freezed);
			Debug.Log("ShardIce");
		}
	}
}