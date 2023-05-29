using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosAleatorios : MonoBehaviour
{
    [SerializeField] AudioClip[] sonidos;
    AudioSource fuenteAudio;
    int rand = 0;

    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rand = UnityEngine.Random.Range(0,10);
        if (rand%2 == 0)
            Sonidos();
    }

    void Sonidos(){
        AudioClip clip = sonidos[UnityEngine.Random.Range(0,sonidos.Length)];
        fuenteAudio.PlayOneShot(clip);
    }
}
