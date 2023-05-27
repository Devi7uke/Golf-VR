using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("MoviendoPelota");
        yield return new  WaitForSeconds(3.0f);
        gameObject.transform.SetParent(pentagrama.transform);
        gameObject.transform.localPosition = new Vector3(-0.5f, 0f, 0f);
        portal.Play();
        yield return new  WaitForSeconds(5.0f);
        portal.Stop();
    }

    private IEnumerator Jumpscare(float waitTime)
    {

        yield return new  WaitForSeconds(3.0f);
        video.SetActive(true);
        xr_p.transform.position = new Vector3(-0.5f, 5.0f, -23.0f);
        xr_p.transform.rotation = Quaternion.Euler(0, 270, 0);
        transform.position = new Vector3(-1.31f, 4.17f, -23.18f);
    }

    private IEnumerator OutOfBounds(float waitTime)
    {
        yield return new  WaitForSeconds(2.0f);
        xr_p.transform.position = new Vector3(-0.5f, 5.0f, -23.0f);
        xr_p.transform.rotation = Quaternion.Euler(0, 270, 0);
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
            coroutine = OutOfBounds(2.0f);
            StartCoroutine(coroutine);
        }

    }
}
