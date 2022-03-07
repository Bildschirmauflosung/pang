using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControler : MonoBehaviour
{
    //create neccessary variables
    float vectY;
    public int size;
    float speedX, speedY;
    bool right = true;
    public bool loaded = false;

    //create components
    public GameObject ball;
    private Vector2 speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //assign scale
        ball.GetComponent<Transform>().localScale = new Vector3(size * 0.6f, size * 0.6f, 1f);
        //assign components
        rb = GetComponent<Rigidbody2D>();

        //assign speeds
        switch (size)
        {
            case 1:
                speedX = 2.1f;
                speedY = 6f;
                break;
            case 2:
                speedX = 3f;
                speedY = 8f;
                break;
            case 3:
                speedX = 4f;
                speedY = 10f;
                break;
            default:
                speedX = 1f;
                speedY = 1f;
                break;
        }
        if (!loaded) 
        { 
            if (right)
            {
                rb.velocity = new Vector2(speedX/2, speedY/2);
            }
            else
            {
                rb.velocity = new Vector2(-speedX/2, speedY/2);  //moving ball 
            }
        }
        else
        {
            loaded = false;
        }

    }



    private void Burst() ///split the ball
    {
        Vector3 asd1 = transform.position + new Vector3(0.3f, 0f, 0f);
        Vector3 asd2 = transform.position + new Vector3(-0.3f, 0f, 0f);
        GameObject pref1 = Instantiate(ball, asd1, Quaternion.identity);
        pref1.GetComponent<BallControler>().size = size - 1;
        GameObject pref2 = Instantiate(ball, asd2, Quaternion.identity);
        pref2.GetComponent<BallControler>().size = size - 1;
        Destroy(ball);
        pref2.GetComponent<BallControler>().right = false;
    }

    private void Bounce() ///bounce ball from floor
    {
            if (rb.velocity.x > 0)
            {
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(speedX, speedY);  //bouncing ball right
            }
            else
            {
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(-speedX, speedY);  //bouncing ball left
        }

    }

    private void ballShot() ///when ball is shot
    {
        GameManager.instance.stagePoints += 100;
        if (size > 1)
            Burst();
        else
        {
            Destroy(ball);
            GameManager.instance.checkWin();
        }

        //remove inactive balls from scene
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            if (ball.activeInHierarchy && !ball.GetComponent<CircleCollider2D>().enabled)
            {
                Destroy(ball);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hook"))
        {
            collision.gameObject.GetComponent<Hook>().Crash();
            ballShot();
        }
        else if (collision.CompareTag("Chain"))
        {
            collision.gameObject.GetComponent<Chain>().Crash();
            ballShot();
        }
        else if (collision.CompareTag("Floor"))
        {
            Bounce();
        }
        else if (collision.CompareTag("Platform"))
        {
            if (rb.velocity.x > 0)
            {
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(speedX, rb.velocity.y);
            }
            else
            {
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(-speedX, rb.velocity.y);
            }
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        // make sure x speed stays within range (necessary for corners of platforms)
        if (size == 3)
        {
            if (rb.velocity.x > 3.93 || (rb.velocity.x > 0 && rb.velocity.x < 3.9))
            {
                vectY = rb.velocity.y;
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(3.92327f, vectY);
            }
            if (rb.velocity.x < -3.93 || (rb.velocity.x < 0 && rb.velocity.x > -3.9))
            {
                vectY = rb.velocity.y;
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(-3.92327f, vectY);
            }
        }
        else if (size == 2)
        {
            if (rb.velocity.x > 3 || (rb.velocity.x > 0 && rb.velocity.x < 2.9))
            {
                vectY = rb.velocity.y;
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(2.95f, vectY);
            }
            if (rb.velocity.x < -3 || (rb.velocity.x < 0 && rb.velocity.x > -2.9))
            {
                vectY = rb.velocity.y;
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(-2.95f, vectY);
            }
        }
        else if (size == 1)
        {
            if (rb.velocity.x > 2.1 || (rb.velocity.x > 0 && rb.velocity.x < 2))
            {
                vectY = rb.velocity.y;
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(2.05f, vectY);
            }
            if (rb.velocity.x < -2.1 || (rb.velocity.x < 0 && rb.velocity.x > -2))
            {
                vectY = rb.velocity.y;
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(-2.05f, vectY);
            }
        }

        //rb.velocity = new Vector2(3f, 3f) * (rb.velocity.normalized);
    }
}
