using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    // Object properties
    [SerializeField] PaddleLogic paddle;
    [SerializeField] float xPush = 0f;
    [SerializeField] float yPush = 10f;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] AudioClip[] ballSounds;

    // Component cache
    AudioSource ballAudioSource;
    Rigidbody2D rigidBody;


    // Operation variables
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;

        ballAudioSource = GetComponent<AudioSource>();

        rigidBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
    }

    private void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0)) {
            hasStarted = true;
            rigidBody.velocity = new Vector2(xPush, yPush); 
        }

    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);


        rigidBody.velocity = new Vector2(0, 0); 
        transform.position = paddleToBallVector + paddlePos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted) {
            var random = new System.Random();
            AudioClip clip = ballSounds[random.Next(ballSounds.Length)];    

            ballAudioSource.PlayOneShot(clip);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x + 
                UnityEngine.Random.Range(-randomFactor,randomFactor), rigidBody.velocity.y + 
                UnityEngine.Random.Range(-randomFactor,randomFactor));
        }
    }

}
