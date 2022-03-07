using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class RankingContr : MonoBehaviour
{
    public Text rank1;
    public Text rank2;
    public Text rank3;
    public Text rank4;
    public Text rank5;
    public Text rank6;
    public Text rank7;
    public Text rank8;
    public Text rank9;
    public Text rank10;

    // Start is called before the first frame update
    void Start()
    {
        List<int> scores = new List<int>();
        List<string> scoresDates = new List<string>();
        for (int x = 1; x <= 10; x++)
        {
            if (PlayerPrefs.HasKey("top" + x))
            {
                Debug.Log("coœ istnieje");
                scores.Add(PlayerPrefs.GetInt("top" + x));
                scoresDates.Add(PlayerPrefs.GetString("dateTop" + x));
            }
        }
        for (int x = 0; x < 10; x++)
        {
            if(x < scores.Count)
            {
                switch (x)
                {
                    case 0:
                        rank1.text = "" + scores[x] + "   " + scoresDates[x];
                        break;
                    case 1:
                        rank2.text = "" + scores[x] + "   " + scoresDates[x];
                        break;
                    case 2:
                        rank3.text = "" + scores[x] + "   " + scoresDates[x];
                        break;
                    case 3:
                        rank4.text = "" + scores[x] + "   " + scoresDates[x];
                        break;
                    case 4:
                        rank5.text = "" + scores[x] + "   " + scoresDates[x];
                        break;
                    case 5:
                        rank6.text = "" + scores[x] + "   " + scoresDates[x];
                        break;
                    case 6:
                        rank7.text = "" + scores[x] + "   " + scoresDates[x];
                        break;
                    case 7:
                        rank8.text = "" + scores[x] + "   " + scoresDates[x];
                        break;
                    case 8:
                        rank9.text = "" + scores[x] + "   " + scoresDates[x];
                        break;
                    case 9:
                        rank10.text = "" + scores[x] + "   " + scoresDates[x];
                        break;
                }
            }
            else
            {
                switch (x)
                {
                    case 0:
                        rank1.text = "Empty score";
                        break;
                    case 1:
                        rank2.text = "Empty score";
                        break;
                    case 2:
                        rank3.text = "Empty score";
                        break;
                    case 3:
                        rank4.text = "Empty score";
                        break;
                    case 4:
                        rank5.text = "Empty score";
                        break;
                    case 5:
                        rank6.text = "Empty score";
                        break;
                    case 6:
                        rank7.text = "Empty score";
                        break;
                    case 7:
                        rank8.text = "Empty score";
                        break;
                    case 8:
                        rank9.text = "Empty score";
                        break;
                    case 9:
                        rank10.text = "Empty score";
                        break;
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
