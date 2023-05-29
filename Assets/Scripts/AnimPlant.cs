using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlant : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator plant;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        plant.SetBool("alert", true);
    }

    private void OnTriggerExit(Collider other){
        plant.SetBool("atack", true);
    }
}
