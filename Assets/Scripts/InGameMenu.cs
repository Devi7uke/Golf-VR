using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InGameMenu : MonoBehaviour {
    public Transform cam;
    public float distance = 2;
    public GameObject menu;
    public InputActionProperty showButton;
    // Start is called before the first frame update
    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update() {
        if(showButton.action.WasPressedThisFrame()){
            menu.SetActive(!menu.activeSelf);
            menu.transform.position = cam.position + new Vector3(cam.forward.x, 0, cam.forward.z).normalized * distance;

        }
        menu.transform.LookAt(new Vector3(cam.position.x, menu.transform.position.y, cam.position.z));
        //menu.transform.forward *= -1;
    }
}