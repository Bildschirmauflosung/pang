using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public GameObject chain;
    public GameObject hak;
    bool pause;

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
        GameManager.instance.hookDeployed = true;
        InvokeRepeating("leaveTrack", 0.0001f, 0.15f);
    }

    public void pauseHook()
    {
        CancelInvoke("leaveTrack");
        pause = true;
    }

    public void unpauseHook()
    {
        InvokeRepeating("leaveTrack", 0.0001f, 0.15f);
        pause = false;
    }

    public void Crash() ///when hook crashes into something
    {
        GameObject[] chains = GameObject.FindGameObjectsWithTag("Chain");
        foreach (GameObject chain in chains)
        {    
            Destroy(chain); 
        }
        GameManager.instance.hookDeployed = false;
        Destroy(hak);
    }

    private void leaveTrack() ///leaves chains after shot
    {
        GameObject pref1 = Instantiate(chain, transform.position - new Vector3(-0.02f, 0.22f), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bound") || collision.CompareTag("Platform"))
        {
            Crash();
        }
        else if (collision.CompareTag("BreakPlatform"))
        {
            Crash();
            GameManager.instance.stagePoints += 50;
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!pause)
            transform.position += new Vector3(0f, 6f*Time.deltaTime, 0f); //moves hook up
    }
}
