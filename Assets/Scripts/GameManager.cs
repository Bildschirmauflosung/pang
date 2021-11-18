using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool hookDeployed;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
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
}
