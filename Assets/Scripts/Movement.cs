using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngineAudio;

    Rigidbody my_rigidbody;
    AudioSource my_audiosource;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartRightRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartLeftRotation();
        }
        else
        {
            StopRotation();
        }
    }

    void StartThrusting()
    {
        my_rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);

        if (!my_audiosource.isPlaying)
        {
            my_audiosource.PlayOneShot(mainEngineAudio);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        my_audiosource.Stop();
        mainEngineParticles.Stop();
    }

    void StartRightRotation()
    {
        ApplyRotation(Vector3.forward);
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    void StartLeftRotation()
    {
        ApplyRotation(Vector3.back);
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    void StopRotation()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }

    void ApplyRotation(Vector3 rotationVector)
    {
        Quaternion deltaRotation = Quaternion.Euler(rotationVector * Time.deltaTime * rotationThrust);
        my_rigidbody.freezeRotation = true; // freezing rotation from the physics system so we can rotate manually
        my_rigidbody.MoveRotation(my_rigidbody.rotation * deltaRotation);
        my_rigidbody.freezeRotation = false; // physics system can now take over
    }
}
