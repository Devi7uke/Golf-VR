using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTurtle : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator turtle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        turtle.SetBool("alert", true);
    }

    private void OnTriggerExit(Collider other){
        turtle.SetBool("atack", true);
    }
}
