using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int PlayerLives;
    public Text zycia;
    public GameObject Player;
    public GameObject GameOver;
    public GameObject GameOverGui;
    public GameObject PauseGui;
    public GameObject WinGui;
    private GameObject[] balls;
    float[] iframes = new float[2];
    public int stagePoints;
    public int stageNum;

    public bool paused;
    bool end;
    public bool iframeActive;
    public float iframeTime;
    public bool hookDeployed;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        stagePoints = 0;
        paused = false;
        end = false;
        zycia.text = "Lives: " + PlayerLives.ToString();
        GameOver.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.5f);
        GameOver.SetActive(false);
        PauseGui.SetActive(false);
        GameOverGui.SetActive(false);
        WinGui.SetActive(false);
    }

    public void Damage()
    {
        if (PlayerLives > 1)
        {
            PlayerLives--;
        }
        else
        {
            pauseGame();
            GameOverGui.SetActive(true);
            balls = GameObject.FindGameObjectsWithTag("Hook");
            if (balls.Length > 0)
            {
                balls[0].GetComponent<Hook>().Crash();
            }
            if (GameObject.Find("Part2Manager") != null)
            {
                GameObject.Find("Part2Manager").GetComponent<Part2Manager>().endScore();
            }
            end = true;
        }
        zycia.text = "Lives: " + PlayerLives.ToString();
    }

    public void pauseGame()
    {
        paused = true;
        Physics2D.simulationMode = SimulationMode2D.Script;
        Player.GetComponent<PlayerController>().enabled = false;
        GameOver.SetActive(true);
        if (hookDeployed)
        {
            Player.GetComponent<PlayerController>().hak.GetComponent<Hook>().pauseHook();
        }
    }

    public void unpauseGame()
    {
        paused = false;
        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        Player.GetComponent<PlayerController>().enabled = true;
        GameOver.SetActive(false);
        PauseGui.SetActive(false);
        if (hookDeployed)
        {
            Player.GetComponent<PlayerController>().hak.GetComponent<Hook>().unpauseHook();
        }
    }

    public void checkWin()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length == 1)
        {
            pauseGame();
            WinGui.SetActive(true);
            end = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape") && !end)
        {
            if (!paused)
            {
                iframes = Player.GetComponent<PlayerController>().passIframes();
                if (iframes[0] == 1f)
                {
                    iframeActive = true;
                    iframeTime = iframes[1];
                    Player.GetComponent<PlayerController>().cancelIframe();
                    Player.GetComponent<PlayerController>().CancelInvoke("startTimer");
                }
                pauseGame();
                PauseGui.SetActive(true);
                
            }
            else
            {
                unpauseGame();
                if (iframeActive)
                {
                    Player.GetComponent<PlayerController>().unpausePlayer(iframes[1]);
                }
                PauseGui.SetActive(false);
            }
        }
    }
}
