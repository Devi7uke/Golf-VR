using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.Assertions;

public class EntrarCasa : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator coroutine;
    private GameObject xr_p;
    public GameObject penta;
    ParticleSystem portal;
    public GameObject video;
    private GameObject escalera;
    private GameObject pentagrama;
    private bool isContact = false;
    private bool auxiliar = false;

    void Start()
    {
        portal = penta.GetComponent<ParticleSystem>();
        escalera = GameObject.FindGameObjectWithTag("Escalera");
        pentagrama = GameObject.FindGameObjectWithTag("Pentagrama");
        xr_p = GameObject.FindGameObjectWithTag("XROrigin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MoverJugador(float waitTime)
    {
        Debug.Log("MoviendoJugador");
        yield return new  WaitForSeconds(waitTime);
        xr_p.transform.SetParent(escalera.transform);
        xr_p.transform.localPosition = new Vector3(0f, 0f, 0f);
        Debug.Log(xr_p.GetComponent<XROrigin>().MoveCameraToWorldLocation(escalera.transform.position));
        Debug.Log("MoviendoPelota");
        yield return new  WaitForSeconds(3.0f);
        gameObject.transform.SetParent(pentagrama.transform);
        gameObject.transform.localPosition = new Vector3(0f, 1f, 1f);
        gameObject.GetComponent<Ball>().previousPos = escalera.transform.position;
        portal.Play();
        yield return new  WaitForSeconds(5.0f);
        portal.Stop();
    }

    private IEnumerator Jumpscare(float waitTime)
    {

        yield return new  WaitForSeconds(3.0f);
        video.SetActive(true);
        xr_p.transform.SetParent(escalera.transform);
        xr_p.transform.localPosition = new Vector3(0f, 0f, 0f);
        Debug.Log(xr_p.GetComponent<XROrigin>().MoveCameraToWorldLocation(escalera.transform.position));
        transform.position = new Vector3(-1.31f, 4.17f, -23.18f);
    }

    private IEnumerator OutOfBounds(float waitTime)
    {
        yield return new  WaitForSeconds(2.0f);
        xr_p.transform.SetParent(escalera.transform);
        xr_p.transform.localPosition = new Vector3(0f, 0f, 0f);
        Debug.Log(xr_p.GetComponent<XROrigin>().MoveCameraToWorldLocation(escalera.transform.position));
        gameObject.transform.position = new Vector3(-1.31f, 4.17f, -23.18f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("MoviendoJugador");
        if (other.tag == "Fuente"){
            Debug.Log("Salto");
            coroutine = MoverJugador(2.0f);
            StartCoroutine(coroutine);
        }else if(other.tag == "Jumpscare"){
            Debug.Log("Susto");
            coroutine = Jumpscare(2.0f);
            StartCoroutine(coroutine);
        }else if(other.tag == "Piso"){
            Debug.Log("Salio");
            //coroutine = OutOfBounds(2.0f);   
        }

    }
    private void OnCollisionStay(Collision collision){
        if (collision.gameObject.tag == "DownStairs"){
            isContact = true;
        }
    }
    private void OnCollisionExit(Collision collision){
        if (collision.gameObject.tag == "DownStairs"){
            isContact = false;
        }
    }
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "DownStairs" && !auxiliar){
            StartCoroutine("IsInContact");
            isContact = true;
        }
    }
    private IEnumerator IsInContact(){
        yield return new WaitForSeconds(5f);
        if (isContact){
            auxiliar = true;
            Debug.Log("MoviendoJugador");
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Ball>().previousPos = GameObject.FindGameObjectWithTag("stairs2").transform.position;
            Debug.Log(xr_p.GetComponent<XROrigin>().MoveCameraToWorldLocation(GameObject.FindGameObjectWithTag("stairs2").transform.position));
        }else{
            coroutine = OutOfBounds(2.0f);
            StartCoroutine(coroutine);
        }
    }
}