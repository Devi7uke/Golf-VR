using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrap : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotateVelocity = -0.7f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up*rotateVelocity, Space.World);
    }
}
