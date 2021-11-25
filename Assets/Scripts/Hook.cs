using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public GameObject chain;
    public GameObject hak;
    private List<GameObject> ChainList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.hookDeployed = true;
        ChainList.Add(hak);
        InvokeRepeating("leaveTrack", 0.0001f, 0.1f);
    }

        public void Crash() ///when hook crashes into something
    {

        foreach(GameObject chain in ChainList)
        {
            Destroy(chain);
        }
        GameManager.instance.hookDeployed = false;
    }

    private void leaveTrack() ///leaves chains after shot
    {
        GameObject pref1 = Instantiate(chain, transform.position - new Vector3(-0.02f, 0.5f), Quaternion.identity);
        pref1.GetComponent<Chain>().head = hak;
        ChainList.Add(pref1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bound"))
        {
            Crash();
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0f, 6f*Time.deltaTime, 0f); //moves hook up
    }
}
