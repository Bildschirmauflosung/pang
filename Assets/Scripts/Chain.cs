using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        head = GameObject.FindGameObjectWithTag("Hook");
    }

    public void Crash() ///when something crashes into the chain
    {
        head.GetComponent<Hook>().Crash();
    }

    // Update is called once per frame
    void Update()
    {
        if(head == null)
        {
            head = GameObject.FindGameObjectWithTag("Hook");
        }
    }
}
