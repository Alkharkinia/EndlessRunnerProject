using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefText : MonoBehaviour
{


    public string name;
    public int score = 0;


    void Start()
    {
        UpdateScoreText();


    }

    void Update()
    {


        UpdateScoreText();


    }

    void UpdateScoreText()
    {
        if (name == "Score")
        {
            GetComponent<Text>().text = score.ToString();
        }
        else
        {
            GetComponent<Text>().text = PlayerPrefs.GetInt(name).ToString();
        }
    }

}
