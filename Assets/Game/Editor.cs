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
    private int commandPosition = 0;
    private bool isActive = false;
    private bool isRunning = false;
    private int testPassed = 0; // Should be 100
    private int currentTest = 0; // Maybe only show on text
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
            cells[iter].GetComponent<RectTransform>().position = new Vector3(pos.x + iter * 160, pos.y, 0);

            // InputCell
            inputCells[iter] = Instantiate<InputCell>(inputCell, this.transform);
            var inputPos = inputCells[iter].GetComponent<RectTransform>().position;
            inputCells[iter].GetComponent<RectTransform>().position = new Vector3(inputPos.x + iter * 160, inputPos.y, 0);
            inputCells[iter].GetComponent<Text>().text = level.inputs[iter, 0].ToString();

            // OutputCell
            outputCells[iter] = Instantiate<OutputCell>(outputCell, this.transform);
            var outputPos = outputCells[iter].GetComponent<RectTransform>().position;
            outputCells[iter].GetComponent<RectTransform>().position = new Vector3(outputPos.x + iter * 160, outputPos.y, 0);
            outputCells[iter].GetComponent<Text>().text = "-1";

            // TargetCell
            targetCells[iter] = Instantiate<TargetCell>(targetCell, this.transform);
            var targetPos = targetCells[iter].GetComponent<RectTransform>().position;
            targetCells[iter].GetComponent<RectTransform>().position = new Vector3(targetPos.x + iter * 160, targetPos.y, 0);
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

        editor.interactable = !isRunning;

        if (isActive)
        {
            ProgramRun();
        }
    }

    public void PreviewNext()
    {
        if (!isRunning)
        {
            if (currentTest < 99)
            {
                currentTest += 1;
            }
            for (var iter = 0; iter < 6; iter++)
            {
                inputCells[iter].GetComponent<Text>().text = level.inputs[iter, currentTest].ToString();
                targetCells[iter].GetComponent<Text>().text = level.outputs[iter, currentTest].ToString();
            }
        }
        GameObject.Find("TestPercent").GetComponent<Text>().text = $"{currentTest + 1}/100";
    }

    public void PreviewPrevious()
    {
        if (!isRunning)
        {
            if (currentTest > 0)
            {
                currentTest -= 1;
            }
            for (var iter = 0; iter < 6; iter++)
            {
                inputCells[iter].GetComponent<Text>().text = level.inputs[iter, currentTest].ToString();
                targetCells[iter].GetComponent<Text>().text = level.outputs[iter, currentTest].ToString();
            }
        }
        GameObject.Find("TestPercent").GetComponent<Text>().text = $"{currentTest + 1}/100";
    }

    public void BrainfuckStep()
    {
        if (!isRunning)
        {
            ProgramLoad();
            currentTest = 0;
            for (var iter = 0; iter < 6; iter++)
            {
                inputCells[iter].GetComponent<Text>().text = level.inputs[iter, currentTest].ToString();
                targetCells[iter].GetComponent<Text>().text = level.outputs[iter, currentTest].ToString();
            }
            GameObject.Find("TestPercent").GetComponent<Text>().text = $"{currentTest + 1}/100";
        }
        isRunning = true;
        isActive = false;
        ProgramRun();
    }

    public void BrainfuckRun()
    {
        if (!isRunning)
        {
            ProgramLoad();
            currentTest = 0;
            for (var iter = 0; iter < 6; iter++)
            {
                inputCells[iter].GetComponent<Text>().text = level.inputs[iter, currentTest].ToString();
                targetCells[iter].GetComponent<Text>().text = level.outputs[iter, currentTest].ToString();
            }
            GameObject.Find("TestPercent").GetComponent<Text>().text = $"{currentTest + 1}/100";
        }
        isRunning = true;
        isActive = true;
    }

    public void ProgramLoad()
    {
        commands = "";
        loopIter = 0;
        var commandsArray = editor.text.ToCharArray();
        foreach (var item in commandsArray)
        {
            if (item == '+' || item == '-' || item == '<' || item == '>' || item == '[' || item == ']' || item == ',' || item == '.')
            {
                commands += item;
            }
        }
    }

    public void ProgramRun()
    {
        Debug.Log(commands.Substring(commandPosition, 1));
        commandPosition += 1;
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
