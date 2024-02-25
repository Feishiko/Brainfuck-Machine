using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ED : MonoBehaviour
{
    private Text text;
    private string[] words = new string[7];
    private double timer = 0;
    void Start()
    {
        text = GameObject.Find("Text").GetComponent<Text>();

        words[0] = "Brainfuck Company\n\nA game by Feishiko";
        words[1] = "Inspired by Zachtronics";
        words[2] = "Programming\n\nFeishiko";
        words[3] = "Music\n\nFeishiko";
        words[4] = "Game Design\n\nFeishiko";
        words[5] = "Special Thanks\n\nzqh";
        words[6] = "Thanks for your playing!\n\nYou are a very awesome engineer!";
    }

    void Update()
    {
        timer += Time.deltaTime*1000;
        text.text = words[Mathf.Min(6, (int)timer / 4000)];

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LevelSelector");
        }
    }
}
