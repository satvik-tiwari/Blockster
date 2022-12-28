using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuBall : MonoBehaviour
{
    //config parameters
    [SerializeField] float ballInitialVelocityX = 600f;
    [SerializeField] float ballInitialVelocityY = 600f;
    [SerializeField] StartMenuPaddle paddle;
    [SerializeField] AudioClip[] sounds;
    [SerializeField] float randomFactor = 0.2f;

    //state
    private bool ballInPlay = false;

    float speed;

    //Cached component References
    AudioSource myAudioSource;
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();
        GetComponent<TrailRenderer>().enabled = false;
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    private void Update()
    {
        if (Time.time>0.3 && !ballInPlay)
        {
            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector2(ballInitialVelocityX, ballInitialVelocityY));
            GetComponent<TrailRenderer>().enabled = true;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
                                (UnityEngine.Random.Range(0f, randomFactor),
                                UnityEngine.Random.Range(0f, randomFactor));

       
        speed = rb.velocity.magnitude;
        // Debug.Log("Before" + rb.velocity.magnitude);
        string name = collision.gameObject.name;
        float angleTweak = 0;
        Vector2 normal = collision.contacts[0].normal;
        // Vector2 vel = rb.velocity;
        //Debug.Log(name);

        float currAngle = Vector2.Angle(rb.velocity, normal);
        //Debug.Log("Angle" + Vector2.Angle(rb.velocity, normal));

        bool angleFuck = false;

        if (currAngle <= 2)
        {
            angleTweak = currAngle + 1.0f;
            angleFuck = true;
        }

        else if (currAngle >= 88)
        {
            angleTweak = currAngle - 1.0f;
            angleFuck = true;

        }
        if (angleFuck)
        {
            //angleTweak = angleTweak;
            Debug.Log("Angle Before : " + currAngle);
            Debug.Log("Velocity Before : " + rb.velocity);
            var change = new Vector2(Mathf.Cos(angleTweak * Mathf.Deg2Rad), Mathf.Sin(angleTweak * Mathf.Deg2Rad));
            Debug.Log("Angle After : " + angleTweak);
            // rb.velocity = speed * change.normalized;
            rb.velocity += change;
            rb.velocity = speed * rb.velocity.normalized;
            Debug.Log("Velocity After : " + rb.velocity);
        }


        if (name.Equals("Start Menu Paddle"))
        {
            // paddleSound.Play();
            AudioClip clip = sounds[1];
            myAudioSource.PlayOneShot(clip);
            var contactPoint = collision.GetContact(0).point.x;
            float paddlePos = paddle.transform.position.x;
            float difference = contactPoint - paddlePos;
            // Debug.Log(difference);
            float angle = map(difference, 0f, 12f, 0f, 20f);//check this one 
                                                            // Debug.Log(angle);
            Vector2 dir = new Vector2(angle, collision.GetContact(0).point.y);
            rb.velocity = dir.normalized * speed;
           // rb.velocity = rb.velocity.magnitude * velocityTweak.normalized;
            // Debug.Log("Speed" + rb.velocity.magnitude);
        }

     

        else
        {
            AudioClip clip = sounds[0];
            myAudioSource.PlayOneShot(clip);
        }
    }
}