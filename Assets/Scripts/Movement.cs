using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// comment lines explains one down code line

public class Movement : MonoBehaviour
{
    // variables
    Rigidbody rb;
    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float rotateForce = 200f;

    // audio
    AudioSource audioSource;
    [SerializeField] AudioClip mainRocketEngineVoice;

    // particle effrcts for the movement
    [SerializeField] ParticleSystem particleLeftThrust;
    [SerializeField] ParticleSystem particleRightThrust;
    [SerializeField] ParticleSystem particleMainThrust;

    private void ApplyThrust(float rotationThisFrame)
    {
        // freezing rotation so we can manually rotate
        rb.freezeRotation = true; 
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        // unfreezing rotation so the physics system can take over
        rb.freezeRotation = false; 
    }

    // this method is to get informations from keyboard. According to the incoming info,
    // some relative force is applied to rocket
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            // one and two under line equally works
            //rb.AddRelativeForce(0, 1, 0);
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);

            // If player presses the space key, let the rocket sound play if it's not already playing
            // If player doesn't press the space key, stop playing rocket sound
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainRocketEngineVoice);
            }

            if(!particleMainThrust.isPlaying)
                particleMainThrust.Play();
        }
        else
        {
            audioSource.Stop();
            // If sound stops, there is no need to particle system. Stop playing it too.
            particleMainThrust.Stop();
        }
    }

    void ProcessRotate()
    {
        // Apply relative thrust to needed way and play some vfx.
        if (Input.GetKey(KeyCode.A))
        {
            ApplyThrust(rotateForce);

            if(!particleLeftThrust.isPlaying)
            {
                particleLeftThrust.Play();
                particleRightThrust.Stop();
            }
                
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyThrust(-rotateForce);

            // Play right, pause others
            if (!particleRightThrust.isPlaying)
            {
                particleRightThrust.Play();
                particleLeftThrust.Stop();
            }
        }
        else
        {
            particleRightThrust.Stop();
            particleLeftThrust.Stop();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }
}
