using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {
	public static LoadScene Instance;
    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image progressBar;
	private float target;
	private void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}

	public async void Load_Scene(string sceneName) {
		target = 0;
		progressBar.fillAmount = 0;

		var scene = SceneManager.LoadSceneAsync(sceneName);
		scene.allowSceneActivation = false;
		loaderCanvas.SetActive(true);
		do {
			await Task.Delay(100);
			//progressBar.fillAmount = scene.progress;
			target = scene.progress;
		}while(scene.progress < 0.9f);

		await Task.Delay(1000);

		scene.allowSceneActivation = true;
		loaderCanvas.SetActive(false);
	}

	private void Update() {
		progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 3 * Time.deltaTime);
	}

}
