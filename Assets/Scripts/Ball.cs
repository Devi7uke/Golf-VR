using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof(LineRenderer))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private float MaxForce;
    [SerializeField]
    private float forceModifier = 0.5f;
    [SerializeField]
    private float force;
    private Rigidbody rb;
    private LineRenderer lineRenderer;
    private Vector3 start, end;
    private bool allowShoot = false;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity == Vector3.zero)
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    //Hereeeeeeeeeeeeeeeeeeeeeeeeeeee
    private void ResetPosition()
    {

    }
    private void NextLevel()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Limits") {
            ResetPosition();
        }
        else if (other.name == "Hole")
        {
            NextLevel();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Club" && allowShoot)
        {
            allowShoot = false;
            direction = start - end;
            rb.AddForce(direction * force, ForceMode.Impulse);
            force = 0;
            start = end = Vector3.zero;
        }
    }
}