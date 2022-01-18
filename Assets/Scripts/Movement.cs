using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody my_rigidbody;
    AudioSource my_audiosource;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;

    // Start is called before the first frame update
    void Start()
    {
        my_rigidbody = GetComponent<Rigidbody>();
        my_audiosource = GetComponent<AudioSource>();
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
            my_rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            
            if (!my_audiosource.isPlaying)
            {
                my_audiosource.Play();
            }
        }
        else
        {
            my_audiosource.Stop();
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
        Quaternion deltaRotation = Quaternion.Euler(rotationVector * Time.deltaTime * rotationThrust);
        my_rigidbody.freezeRotation = true; // freezing rotation from the physics system so we can rotate manually
        my_rigidbody.MoveRotation(my_rigidbody.rotation * deltaRotation);
        my_rigidbody.freezeRotation = false; // physics system can now take over
    }
}
