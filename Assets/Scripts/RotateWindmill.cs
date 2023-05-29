using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWindmill : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotateVelocity = -0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right*rotateVelocity, Space.World);
    }
}
