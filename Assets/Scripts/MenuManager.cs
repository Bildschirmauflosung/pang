using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    public void PlayButton()
    {
        SceneManager.LoadScene("wyborEtapu");
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void backToEtapy()
    {
        SceneManager.LoadScene("wyborEtapu");
    }

    public void etap1Wybor()
    {
        SceneManager.LoadScene("etap1Trudnosc");
    }

    //gdy wybrano etap latwy
    public void play1Easy()
    {
        string poziom = "et1easy";
        poziom += Random.Range(1, 6);
        SceneManager.LoadScene(poziom);
    }
    //gdy sredni
    public void play1Medium()
    {
        string poziom = "et1medium";
        poziom += Random.Range(1, 6);
        SceneManager.LoadScene(poziom);
    }

    //gdy trudny
    public void play1Hard()
    {
        string poziom = "et1hard";
        poziom += Random.Range(1, 6);
        SceneManager.LoadScene(poziom);
    }

    public void etap2Wybor()
    {
        SceneManager.LoadScene("etap2poziomy");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(Menu2Manager.instance);
        Screen.SetResolution(1048, 532, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
