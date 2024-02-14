using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Editor : MonoBehaviour
{
    private InputField editor;
    public Cell cell;
    public Cell[] cells = new Cell[6];
    private int pointerPosition = 0;
    private int loopIter = 0;
    private string commands;
    private bool isActive = false;
    private bool isRunning = false;
    private int testPassed = 0; // Should be 100
    public ILevel level;
    void Start()
    {
        // Test Level
        level = new LevelTest();
        // editor = GetComponentInChildren<InputField>();
        editor = GameObject.Find("InputField").GetComponent<InputField>();
        // editor.interactable = false;
        for (var iter = 0; iter < 6; iter++)
        {
            cells[iter] = Instantiate<Cell>(cell, this.transform);
            var pos = cells[iter].GetComponent<RectTransform>().position;
            cells[iter].GetComponent<RectTransform>().position = new Vector3(pos.x + iter * 80, pos.y, 0);
        }

        // Level Init
        level.Method();
    }

    void Update()
    {
        // Pointer to cell
        for (var iter = 0; iter < 6; iter++)
        {
            cells[iter].GetComponent<Text>().color = Color.white;
        }
        cells[pointerPosition].GetComponent<Text>().color = Color.yellow;
    }

    public void BrainfuckStep()
    {
        isRunning = true;
    }

    public void BrainfuckStop()
    {
        isRunning = false;
    }
}
