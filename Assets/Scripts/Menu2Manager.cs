using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Menu2Manager : MonoBehaviour
{
    public Dropdown dr;
    public Button load1;
    public Button load2;
    public Button load3;
    public GameObject Default;
    public GameObject chooseSavefile;
    public int saveFile;
    public static Menu2Manager instance;

    public void backToEtapy()
    {
        SceneManager.LoadScene("wyborEtapu");
    }

    public void play2()
    {
        SceneManager.LoadScene("et2poz" + (dr.value+1));
    }

    public void loadGame() 
    {
        Default.SetActive(false);
        chooseSavefile.SetActive(true);
    }

    public void ranking()
    {
        
    }

    public void loadSave(int saveNum)
    {
        saveFile = saveNum;
        string saveName = "/save" + saveNum + ".save";
        BinaryFormatter plik = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + saveName, FileMode.Open);
        Part2Manager.Save save = (Part2Manager.Save)plik.Deserialize(file);
        file.Close();
        SceneManager.LoadScene("defaultScene" + save.stage);
    }


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        if (!File.Exists(Application.persistentDataPath+ "/save1.save"))
        {
            load1.enabled = false;
            load1.image.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            load1.enabled = true;
        }
        if (!File.Exists(Application.persistentDataPath + "/save2.save"))
        {
            load2.enabled = false;
            load2.image.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            load2.enabled = true;
        }
        if (!File.Exists(Application.persistentDataPath + "/save3.save"))
        {
            load3.enabled = false;
            load3.image.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            load3.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
