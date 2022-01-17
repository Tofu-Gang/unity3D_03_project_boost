using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);
        }
    }

    void ApplyRotation(Vector3 rotationVector)
    {
        rigidbody.freezeRotation = true; // freezing rotation from the physics system so we can rotate manually
        transform.Rotate(rotationVector * Time.deltaTime * rotationThrust);
        rigidbody.freezeRotation = false; // physics system can now take over
    }
}
