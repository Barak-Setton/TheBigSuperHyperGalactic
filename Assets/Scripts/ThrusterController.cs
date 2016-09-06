using UnityEngine;
using System.Collections;

public class ThrusterController : MonoBehaviour {

    public bool toHeigh;

    private float rotationX = 0f;

    public float drag;

    public float acceleration;
    public float rotationRate;

    public float turnRotationAngle;
    public float turnRotationSeekSpeed;

    private float rotationVelocity;
    private float groundAngleVelocity;

    private Rigidbody carRigidbody;



    public void Move(float Vertical, float Horizontal, float brake)
    {
        // check if we are touching the ground:
        if (Physics.Raycast(transform.position, transform.up * -1, 3f))
        {
            toHeigh = false;

            // we are on the ground; enable the accelerator and increase drag:
            carRigidbody.drag = drag;

            // clamp if foward vecter to height
            rotationX = Mathf.Clamp(rotationX, -45, 45);
            transform.localEulerAngles = new Vector3(-rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);

            // calculate forward force:
            Vector3 forwardForce = transform.forward * acceleration * Vertical;


            // correct force for deltatime and vehicle mass:
            forwardForce = forwardForce * Time.deltaTime * carRigidbody.mass;
            carRigidbody.AddForce(forwardForce);
        }
        else
        {
            toHeigh = true;
            // we aren't on the ground and dont want to just halt in mid-air: reduce drag:
            carRigidbody.drag = 0f; // NEED TO FIGURE THIS OUT SO IT DOESNT DRIFT FOR REALLY LONG
        }

        // You can turn in the air or no the ground:
        Vector3 turnTorque = Vector3.up * rotationRate * Horizontal;

        // Correct force for deltatime and vehicle mass:
        turnTorque = turnTorque * Time.deltaTime * carRigidbody.mass;
        carRigidbody.AddTorque(turnTorque);

        // "FAke" rotate the car when you are turning:
        Vector3 newRotation = transform.eulerAngles;
        newRotation.z = Mathf.SmoothDampAngle(newRotation.z, Horizontal * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
        transform.eulerAngles = newRotation;
    }

	// Use this for initialization
	void Start () {
        carRigidbody = GetComponent<Rigidbody>();
	}
	
}
