using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
	[SerializeField] private List<GameObject> panels;
	[SerializeField] private List <AudioClip> music;
	[SerializeField] private GameObject start;
	private Camera mainCamera;
	private int level = 1;
	private string sceneName;
	private GameObject ball;
	private GameObject menu, levels;

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
			menu = GameObject.FindGameObjectWithTag("Menu");
			levels = GameObject.FindGameObjectWithTag("Levels");
			menu.SetActive(true);
			levels.SetActive(false);
		}
	}
	private void Start() {
		if (SceneManager.GetActiveScene().name != "MainMenu") {
			ball = GameObject.FindGameObjectWithTag("Ball");
			startBallPosition = ball.transform.position;
			sceneName = SceneManager.GetActiveScene().name;
		}
		if(SceneManager.GetActiveScene().name == "Scenario_3" || SceneManager.GetActiveScene().name == "Scenario_1" || SceneManager.GetActiveScene().name == "Scenario_2") {
			GameObject.FindGameObjectWithTag("XROrigin").transform.parent = start.transform;
			GameObject.FindGameObjectWithTag("XROrigin").transform.localPosition = new Vector3(0, 0, 0);
		}
	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.F)) {
			ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.Impulse);
		}
		else if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().name == "Scenario_3") {
			ResetPosition();
		}
	}
	public void Play() {
		LoadScene.Instance.Load_Scene("Scenario_3");
		//SceneManager.LoadScene("Scenario_1");
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
	public void NextLevel() {
		if (sceneName == "Scenario_1") { 
				Horror_Scene();
		} else  if (sceneName == "Scenario_2") {
				Sci_Fi_Scene();
		} else if (sceneName == "Scenario_3") {
				MainMenu_Scene();
		}
	}
	public void PreviousBallPosition() {
		ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
		ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		ball.transform.position = ball.GetComponent<Ball>().previousPos;
	}
	public void ResetPosition() {
		ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
		ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		ball.transform.position = startBallPosition;
	}
	public void Samuarai_Scene() {
		LoadScene.Instance.Load_Scene("Scenario_1");
	}
	public void Horror_Scene() {
		LoadScene.Instance.Load_Scene("Scenario_2");
	}
	public void Sci_Fi_Scene() {
		LoadScene.Instance.Load_Scene("Scenario_3");
	}
	public void MainMenu_Scene() {
		LoadScene.Instance.Load_Scene("MainMenu");
	}
	public void viewLevels(){
		menu.SetActive(false);
		levels.SetActive(true);
	}
	public void hideLevels(){
		menu.SetActive(true);
		levels.SetActive(false);
	}
}