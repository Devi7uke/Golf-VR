using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private GameObject ball;
	private Rigidbody rb;
	private Rigidbody ballRB;
	[SerializeField]
	private float force = 10.0f;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		ballRB = ball.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
    Vector3 GetClubFaceNormal(GameObject club, Vector3 pointOfImpact)
    {
        Mesh clubMesh = club.GetComponent<MeshFilter>().mesh;
        int[] triangles = clubMesh.triangles;
        Vector3 closestFaceNormal = Vector3.zero;
        float closestDistance = float.MaxValue;

        // Iterate over each triangle face in the mesh
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v1 = clubMesh.vertices[triangles[i]];
            Vector3 v2 = clubMesh.vertices[triangles[i + 1]];
            Vector3 v3 = clubMesh.vertices[triangles[i + 2]];

            // Calculate the normal of the face
            Vector3 faceNormal = Vector3.Cross(v2 - v1, v3 - v1).normalized;

            // Calculate the distance from the face to the point of impact
            float distance = Vector3.Distance(pointOfImpact, v1);

            // If this face is closer to the point of impact than the previous closest face, set it as the new closest face
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestFaceNormal = faceNormal;
            }
        }
        return closestFaceNormal;
    }
    float CalculateForceMagnitude(float angle)
    {
        // Clamp the angle to a maximum of 45 degrees
        angle = Mathf.Clamp(angle, 0.0f, 45.0f);

        // Map the angle to a force magnitude between 0.0 and 1.0
        float normalizedAngle = angle / 45.0f;
        float forceMagnitude = Mathf.Lerp(0.0f, 1.0f, normalizedAngle);

        // Scale the force magnitude by the desired maximum force
        forceMagnitude *= force;

        return forceMagnitude;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
		{
			Vector3 forceDirection = ball.transform.position - transform.position;
			forceDirection = forceDirection.normalized;
            Vector3 clubFaceNormal = GetClubFaceNormal(gameObject, collision.contacts[0].point);
            float angle = Vector3.Angle(clubFaceNormal, -forceDirection);
            float forceMangnitude = CalculateForceMagnitude(angle);
            Vector3 forceVector = forceMangnitude * clubFaceNormal;
            Quaternion rotation = Quaternion.Euler(0, 90, 0);
            Vector3 rotatedForceVector = rotation * forceVector;
            ballRB.AddForce(rotatedForceVector, ForceMode.Impulse);
            Debug.Log("Collision Entered!!");

            Debug.DrawRay(ball.transform.position, rotatedForceVector, Color.red, 1.0f);
        }
    }
}
