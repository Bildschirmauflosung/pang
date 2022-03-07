using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class Part2Manager : MonoBehaviour
{
    public static Part2Manager instance;
    private GameObject[] objectList;
    public bool loaded = false;
    public Text points;
    int overallPoints;
    public Text endPoints;
    public GameObject saveChoice;
    public GameObject confirmChoice;
    public int saveNumer;


    // class for saving player data in save file
    [System.Serializable]
    public class PlayerData
    {
        public float posX;
        public float posY;
        public float velocityX;
        public float velocityY;
        public int lives;
        public bool climb;
        public bool iframe;
        public float frameTimer;
        public bool hook;
        public int allScore;
        public int stageScore;

        public PlayerData(GameObject Player, int allPoints)
        {
            Transform position = Player.transform;
            Rigidbody2D velocity = Player.GetComponent<Rigidbody2D>();
            posX = position.position.x;
            posY = position.position.y;
            hook = GameManager.instance.hookDeployed;
            lives = GameManager.instance.PlayerLives;
            velocityX = velocity.velocity.x;
            velocityY = velocity.velocity.y;
            iframe = GameManager.instance.iframeActive;
            frameTimer = GameManager.instance.iframeTime;
            climb = Player.GetComponent<PlayerController>().climb;
            allScore = allPoints;
            stageScore = GameManager.instance.stagePoints;
        }
    }
    // class for saving player data in save file
    [System.Serializable]
    public class ballsData
    {
        public float posX;
        public float posY;
        public float velocityX;
        public float velocityY;
        public int size;

        public ballsData(GameObject ball)
        {
            Transform position = ball.transform;
            Rigidbody2D velocity = ball.GetComponent<Rigidbody2D>();
            posX = position.position.x;
            posY = position.position.y;
            velocityX = velocity.velocity.x;
            velocityY = velocity.velocity.y;
            size = ball.GetComponent<BallControler>().size;
        }
    }

    // chain storing
    [System.Serializable]
    public class chain
    {
        public float posX;
        public float posY;
        public chain(GameObject chain)
        {
            Transform position = chain.transform;
            posX = position.position.x;
            posY = position.position.y;
        }
    }

    [System.Serializable]
    public class Save
    {
        public PlayerData gracz;
        public List<ballsData> balls = new List<ballsData>();
        public List<chain> chains = new List<chain>();
        public int stage;

        public Save(PlayerData player, List<ballsData> pilki)
        {
            gracz = player;
            balls = pilki;
            stage = GameManager.instance.stageNum;
        }

        public Save(PlayerData player, List<ballsData> pilki, List<chain> lancuchy)
        {
            gracz = player;
            balls = pilki;
            chains = lancuchy;
            stage = GameManager.instance.stageNum;
        }
    }

    public void chooseSave(int saveNumber)
    {
        string saveName = "/save" + saveNumber + ".save";
        if (File.Exists(Application.persistentDataPath + saveName))
        {
            saveNumer = saveNumber;
            saveChoice.SetActive(false);
            confirmChoice.SetActive(true);
        }
        else
        {
            saveGame(saveName);
        }
    }

    public void confirmedChoice()
    {
        string saveName = "/save" + saveNumer + ".save";
        saveGame(saveName);
    }

    public void saveGame(string saveName)
    {
        //save players position
        PlayerData gracz = new PlayerData(GameManager.instance.Player, overallPoints);
        //save balls and their positions
        objectList = GameObject.FindGameObjectsWithTag("Ball");
        List<ballsData> ballsList = new List<ballsData>();
        foreach(GameObject ball in objectList)
        {
            ballsList.Add(new ballsData(ball));
        }


        BinaryFormatter plik = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + saveName);
        if (gracz.hook)
        {
            objectList = GameObject.FindGameObjectsWithTag("Chain");
            List<chain> chain = new List<chain>();
            foreach (GameObject chains in objectList)
            {
                chain.Add(new chain(chains));
            }
            plik.Serialize(file, new Save(gracz, ballsList, chain));
        } else
        {
            plik.Serialize(file, new Save(gracz, ballsList));
        }
        file.Close();
        SceneManager.LoadScene("etap2poziomy");
    }

    public void backToEtapy()
    {
        SceneManager.LoadScene("wyborEtapu");
    }

    public void loadGame() 
    {
        string name = "/save" + saveNumer + ".save";
        BinaryFormatter plik = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + name, FileMode.Open);
        Save save = (Save)plik.Deserialize(file);
        file.Close();
        //load player
        GameManager.instance.Player.transform.position = new Vector2(save.gracz.posX, save.gracz.posY);
        GameManager.instance.Player.GetComponent<Rigidbody2D>().velocity = new Vector2(save.gracz.velocityX, save.gracz.velocityY);
        GameManager.instance.Player.GetComponent<PlayerController>().climb = save.gracz.climb;
        GameManager.instance.PlayerLives = save.gracz.lives;
        GameManager.instance.zycia.text = "Lives: " + save.gracz.lives;
        GameManager.instance.stagePoints = save.gracz.stageScore;
        overallPoints = save.gracz.allScore;
        if (save.gracz.iframe)
        {
            GameManager.instance.Player.GetComponent<PlayerController>().unpausePlayer(save.gracz.frameTimer);
        }
        //load balls
        foreach (ballsData ball in save.balls)
        {
            GameObject pref1 = Instantiate((GameObject)Resources.Load("Ball"), new Vector2(ball.posX, ball.posY), Quaternion.identity);
            Rigidbody2D aaa = pref1.GetComponent<Rigidbody2D>();
            pref1.GetComponent<BallControler>().loaded = true;
            aaa.velocity = new Vector2(ball.velocityX, ball.velocityY);
            pref1.GetComponent<BallControler>().size = ball.size;
        }
        //chains
        if(save.chains.Count != 0)
        {
            GameManager.instance.hookDeployed = true;
            Instantiate((GameObject)Resources.Load("Hook"), new Vector2(save.chains[save.chains.Count-1].posX, save.chains[save.chains.Count - 1].posY), Quaternion.identity);
            foreach (chain chain in save.chains)
            {
                Instantiate((GameObject)Resources.Load("Chain"), new Vector2(chain.posX+0.02f, chain.posY), Quaternion.identity);
                Debug.Log(chain.posY);
            }
        }

    }
    
    public void endScore()
    {
        overallPoints += GameManager.instance.stagePoints;
        Debug.Log(overallPoints);
        endPoints.text = "Score: " + overallPoints;
        List<int> scores = new List<int>();
        List<string> scoresDates = new List<string>();
        bool fall = false;
        List<int> wynik = new List<int>();
        List<string> wynikDates = new List<string>();
        Debug.Log(System.DateTime.Now.ToString("dd/mm/yyyy"));
        for (int x = 1; x <= 10; x++)
        {
            if (PlayerPrefs.HasKey("top" + x))
            {
                Debug.Log("top" + x + "   " + PlayerPrefs.GetInt("top" + x));
                scores.Add(PlayerPrefs.GetInt("top" + x));
                scoresDates.Add(PlayerPrefs.GetString("dateTop" + x));
            }
        }
        if (scores.Count == 10)
        {
            for (int x = 0; x < 10; x++)
            {
                //Debug.Log("Dodaje2");
                if (scores[x] < overallPoints && fall)
                {
                    wynik.Add(overallPoints);
                    wynikDates.Add(System.DateTime.Now.ToString("dd/mm/yyyy"));
                    x--;
                    fall = true;
                }
                else
                {
                    wynik.Add(scores[x]);
                    wynikDates.Add(scoresDates[x]);
                }
            }
        }
        else
        {
            //Debug.Log("Dodaje3");
            for (int x = 0; x < scores.Count; x++)
            {
                if (scores[x] < overallPoints && fall)
                {
                    wynik.Add(overallPoints);
                    wynikDates.Add(System.DateTime.Now.ToString("dd/mm/yyyy"));
                    x--;
                    fall = true;
                }
                else
                {
                    wynik.Add(scores[x]);
                    wynikDates.Add(scoresDates[x]);
                }
            }
            if (!fall)
            {
                wynik.Add(overallPoints);
                wynikDates.Add(System.DateTime.Now.ToString("dd/mm/yyyy"));
            }
            else
            {
                wynik.Add(scores[scores.Count]);
                wynikDates.Add(scoresDates[scoresDates.Count]);
            }
        }
        //save it in prefs
        for (int i = 1; i <= wynik.Count; i++)
        {
            PlayerPrefs.SetInt("top"+i, wynik[i-1]);
            PlayerPrefs.SetString("top" + i, wynikDates[i - 1]);
        }



    }

    public void nextLevel()
    {
        PlayerPrefs.SetInt("punktyOver", overallPoints+GameManager.instance.stagePoints);
        PlayerPrefs.SetInt("zycia", GameManager.instance.PlayerLives);
        SceneManager.LoadScene("et2poz" + ++GameManager.instance.stageNum);
    }

    public void chooseSaveFile()
    {
        confirmChoice.SetActive(false);
        GameManager.instance.PauseGui.SetActive(false);
        saveChoice.SetActive(true);
    }

    public void backToPause()
    {
        GameManager.instance.PauseGui.SetActive(true);
        saveChoice.SetActive(false);
    }

    public void confirmSaveChoice()
    {
        confirmChoice.SetActive(true);
        saveChoice.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("punktyOver"))
        {
            overallPoints = PlayerPrefs.GetInt("punktyOver");
        }
        if (PlayerPrefs.HasKey("zycia"))
        {
            GameManager.instance.PlayerLives = PlayerPrefs.GetInt("zycia");
        }
        instance = this;
        if (loaded)
        {
            saveNumer = Menu2Manager.instance.saveFile;
            Destroy(GameObject.FindGameObjectWithTag("Menu"));
            objectList = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject ball in objectList)
            {
                Destroy(ball);
            }
            Invoke("loadGame", 0.0001f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        points.text = "Points:\n" + GameManager.instance.stagePoints;
    }
}
