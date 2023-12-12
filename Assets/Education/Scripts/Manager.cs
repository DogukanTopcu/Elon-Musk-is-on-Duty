using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public bool isGameOver;
    public bool isTakeSatellite;
    public int totalScore;

    private TextMeshProUGUI point;

    void Awake()
    {
        isGameOver = false;
        isTakeSatellite = false;
        totalScore = 0;

        point = FindInActiveObjectByName("Point").
            GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        point.SetText(totalScore.ToString());
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("Basıldı");

    }

    public void Exit()
    {
        Application.Quit();
    }


    public GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

}
