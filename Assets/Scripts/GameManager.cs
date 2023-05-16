using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
	[SerializeField] private List<GameObject> panels;
	[SerializeField] private List <AudioClip> music;

	private Camera mainCamera;
	private int level = 1;
	private string sceneName;
	private GameObject ball;
	private Vector3 startBallPosition;
	private Vector3 previousBallPosition;
	private bool intro = true;
	private void Awake() {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		if (SceneManager.GetActiveScene().name == "Scenario_3" ) {
			if (level == 1) {
				mainCamera.GetComponent<AudioSource>().clip = music[0];
			}else if (level == 2) {
				mainCamera.GetComponent<AudioSource>().clip = music[1];
			}else if (intro) {
				intro = false;
				mainCamera.GetComponent<AudioSource>().clip = music[2];
			}
		}
		if (SceneManager.GetActiveScene().name == "MainMenu") {
			panels[Random.Range(0, panels.Count)].SetActive(true);
		}
	}
	private void Start() {
		if (SceneManager.GetActiveScene().name != "MainMenu") {
			ball = GameObject.FindGameObjectWithTag("Ball");
			startBallPosition = ball.transform.position;
			sceneName = SceneManager.GetActiveScene().name;
		}
	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.F)) {
			ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.Impulse);
		}
		else if (Input.GetKeyDown(KeyCode.R)) {
			ResetPosition();
		}
	}
	public void Play() {
		SceneManager.LoadScene("Scenario_1");
	}
	public void Quit() {
		#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
		#else
				Application.Quit();
		#endif
	}
	public void Settings() {

	}
	public void SelectLevel() {

	}
	public void NextLevel() {
		if (sceneName == "Scenario_1") { 
			if (level == 1) {
				Samurai_Level2();
				level = 2;
			} else if (level == 2) {
				Horror_Scene();
			}
		} else  if (sceneName == "Scenario_2") {
			if (level == 1) {
				Horror_Level2();
				level = 2;
			}
			else if (level == 2) {
				Sci_Fi_Scene();
			}
		} else if (sceneName == "Scenario_3") {
			if (level == 1) {
				Sci_Fi_Level2();
				level = 2;
			}
			else if (level == 2) {
				Nature_Scene();
			}
		} else if (sceneName == "Scenario_4") {
			if (level == 1) {
				Nature_Level2();
				level = 2;
			}
			else if (level == 2) {
				Samuarai_Scene();
			}
		}
	}
	public void SetPreviousBallPosition() {
		previousBallPosition = ball.transform.position;
	}
	public void ResetPosition() {
		ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
		ball.transform.position = startBallPosition;
	}
	public void Samuarai_Scene() {
		SceneManager.LoadScene("Scenario_1");
	}
	public void Horror_Scene() {
		SceneManager.LoadScene("Scenario_2");
	}
	public void Sci_Fi_Scene() {
		SceneManager.LoadScene("Scenario_3");
	}
	public void Nature_Scene() {
		SceneManager.LoadScene("Scenario_4");
	}
	public void Sci_Fi_Level1() {

	}
	public void Sci_Fi_Level2() {

	}
	public void Horror_Level1() {

	}
	public void Horror_Level2() {

	}
	public void Samurai_Level1() {

	}
	public void Samurai_Level2() {

	}
	public void Nature_Level1() {

	}
	public void Nature_Level2() {

	}
}