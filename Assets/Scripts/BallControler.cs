using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControler : MonoBehaviour
{
    //create neccessary variables
    public int size;
    float speedX, speedY;
    bool right = true;

    //create components
    public GameObject ball;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //assign scale
        ball.GetComponent<Transform>().localScale = new Vector3(size * 0.4f, size * 0.4f, 1f);
        //assign components
        rb = GetComponent<Rigidbody2D>();

        //assign speeds
        switch (size)
        {
            case 1:
                speedX = 2.5f;
                speedY = 6.8f;
                break;
            case 2:
                speedX = 3.7f;
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

        if (right)
        {
            rb.velocity = new Vector2(speedX, speedY);
        }
        else
        {
            rb.velocity = new Vector2(-speedX, speedY);  //moving ball 
        }


    }



    private void Burst()
    {
        Vector3 asd1 = transform.position + new Vector3(0.5f, 1f, 0f);
        Vector3 asd2 = transform.position + new Vector3(-0.5f, 1f, 0f);
        GameObject pref1 = Instantiate(ball, asd1, Quaternion.identity);
        pref1.GetComponent<BallControler>().size = size - 1;
        GameObject pref2 = Instantiate(ball, asd2, Quaternion.identity);
        pref2.GetComponent<BallControler>().size = size - 1;
        Destroy(ball);
        pref2.GetComponent<BallControler>().right = false;
    }

    private void Bounce()
    {
        
            if (rb.velocity.x > 0)
            {
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(speedX, speedY);  //moving ball according to size
            }
            else
            {
                rb.AddForce(new Vector2(-rb.velocity.x, -rb.velocity.y));
                rb.velocity = new Vector2(-speedX, speedY);  //moving ball according to size
            }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (size > 1)
                Burst();
            else
                Destroy(ball);
        }
        else if (collision.CompareTag("Floor"))
        {
            Bounce();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(3f, 3f) * (rb.velocity.normalized);
    }
}
