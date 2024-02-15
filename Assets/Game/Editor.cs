using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Editor : MonoBehaviour
{
    private InputField editor;
    public InputCell inputCell;
    public InputCell[] inputCells = new InputCell[6];
    public OutputCell outputCell;
    public OutputCell[] outputCells = new OutputCell[6];
    private int[] outputValue = new int[] { -1, -1, -1, -1, -1, -1 };
    public TargetCell targetCell;
    public TargetCell[] targetCells = new TargetCell[6];
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
        // Level Init
        GameObject.Find("Title").GetComponent<Text>().text = level.name;
        GameObject.Find("Rule").GetComponent<Text>().text = level.rule;
        level.Method();
        for (var iter = 0; iter < 6; iter++)
        {
            // MemoryCell
            cells[iter] = Instantiate<Cell>(cell, this.transform);
            var pos = cells[iter].GetComponent<RectTransform>().position;
            cells[iter].GetComponent<RectTransform>().position = new Vector3(pos.x + iter * 80, pos.y, 0);

            // InputCell
            inputCells[iter] = Instantiate<InputCell>(inputCell, this.transform);
            var inputPos = inputCells[iter].GetComponent<RectTransform>().position;
            inputCells[iter].GetComponent<RectTransform>().position = new Vector3(inputPos.x + iter * 80, inputPos.y, 0);
            inputCells[iter].GetComponent<Text>().text = level.inputs[iter, 0].ToString();

            // OutputCell
            outputCells[iter] = Instantiate<OutputCell>(outputCell, this.transform);
            var outputPos = outputCells[iter].GetComponent<RectTransform>().position;
            outputCells[iter].GetComponent<RectTransform>().position = new Vector3(outputPos.x + iter * 80, outputPos.y, 0);
            outputCells[iter].GetComponent<Text>().text = "-1";

            // TargetCell
            targetCells[iter] = Instantiate<TargetCell>(targetCell, this.transform);
            var targetPos = targetCells[iter].GetComponent<RectTransform>().position;
            targetCells[iter].GetComponent<RectTransform>().position = new Vector3(targetPos.x + iter * 80, targetPos.y, 0);
            targetCells[iter].GetComponent<Text>().text = level.outputs[iter, 0].ToString();
        }
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

    public void EmitLength()
    {
        GameObject.Find("InputSize").GetComponent<Text>().text = $"Size: {editor.text.Length}/100";
    }
}
