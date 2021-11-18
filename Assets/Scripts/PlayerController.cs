using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public GameObject hook;
    private GameObject hak;
    Rigidbody2D rigid;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (GameManager.instance.hookDeployed)
            {
                hak.GetComponent<Hook>().Crash();
            }
            hak = Instantiate(hook, transform.position - new Vector3(0f, 0.5f), Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("a"))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
        
    }

    
}
