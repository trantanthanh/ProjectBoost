using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody myRigidbody;
    [SerializeField] private float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 50f;
    [SerializeField] AudioClip sfxEngine;
    [SerializeField] ParticleSystem thrustEffect;
    [SerializeField] ParticleSystem leftThrustEffect;
    [SerializeField] ParticleSystem rightThrustEffect;
    AudioSource audioSource;

    CollisionHandler collisionHandler;
    public bool lostControl = false;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        collisionHandler = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!lostControl && !collisionHandler.isTransitioningNextLevel && !collisionHandler.isTransitioningReloadLevel)
        {
            ProcessThrust();
            ProcessRotation();
        }
    }
    public void StopThrustEffect()
    {
        thrustEffect.Stop();
        leftThrustEffect.Stop();
        rightThrustEffect.Stop();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sfxEngine);
            }
            if (!thrustEffect.isPlaying)
            {
                thrustEffect.Play();
            }
            myRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            Debug.Log("Thrusting");
        }
        else
        {
            thrustEffect.Stop();
            if (!collisionHandler.isTransitioningNextLevel && !collisionHandler.isTransitioningReloadLevel && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotating Left");
            ApplyRotation(rotationThrust);
            leftThrustEffect.Stop();
            if (!rightThrustEffect.isPlaying)
            {
                rightThrustEffect.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            Debug.Log("Rotating right");
            rightThrustEffect.Stop();
            if (!leftThrustEffect.isPlaying)
            {
                leftThrustEffect.Play();
            }
        }
        else
        {
            leftThrustEffect.Stop();
            rightThrustEffect.Stop();
        }
    }

    private void ApplyRotation(float rotation)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        myRigidbody.freezeRotation = false;
    }
}
