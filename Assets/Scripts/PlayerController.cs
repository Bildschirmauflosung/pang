using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public GameObject hook;
    public GameObject hak;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    public float speed = 10f;
    public bool climb = false;
    bool iframe = false;
    float Itimer;


    //get access to the private variables to save
    public float[] passIframes()
    {
        float[] data = new float[2];
        if (!iframe)
        {
            data[0] = 0f;
            data[1] = 0f;
            return data;
        }
        else
        {
            data[0] = 1f;
            data[1] = Itimer;
            return data;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hak == null)
        {
            hak = GameObject.FindGameObjectWithTag("Hook");
        }
        if (Input.GetKeyDown("space"))
        {
            if (GameManager.instance.hookDeployed)
            {
                hak.GetComponent<Hook>().Crash();
            }
            hak = Instantiate((GameObject)Resources.Load("Hook"), transform.position - new Vector3(0f, 0f), Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
        if ((Input.GetKey("w") || Input.GetKey("up")) && climb)
        {
            transform.position += new Vector3(0f, 4 * Time.deltaTime, 0f);
        }
        if ((Input.GetKey("s") || Input.GetKey("down")) && climb)
        {
            transform.position += new Vector3(0f, -4 * Time.deltaTime, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            climb = true;
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
        }
        if (collision.CompareTag("BallTrigger") && !iframe)
        {
            Itimer = 3f;
            iframe = true;
            GameManager.instance.Damage();
            Invoke("cancelIframe", 1.5f);
            InvokeRepeating("startTimer", 0f, 0.1f);
            InvokeRepeating("shade", 0f, 0.05f);
        }
    }

    //timer to pass in case of save or pause
    private void startTimer()
    {
        Itimer -= 0.1f;
    }

    public void cancelIframe()
    {
        iframe = false;
        CancelInvoke("shade");
        CancelInvoke("startTimer");
        sprite.color = new Color(1f, 1f, 1f, 1f);
    }

    public void unpausePlayer(float time)
    {
        iframe = true;
        Itimer = time;
        Invoke("cancelIframe", time);
        InvokeRepeating("startTimer", 0f, 0.1f);
        InvokeRepeating("shade", 0f, 0.05f);
    }

    private void shade()
    {
        if(sprite.color.a == 1)
        {
            sprite.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            sprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            climb = false;
            rigid.gravityScale = 1;
        }
    }
}
