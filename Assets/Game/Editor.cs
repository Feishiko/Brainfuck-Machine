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
    private int inputPos = 0;
    public OutputCell outputCell;
    public OutputCell[] outputCells = new OutputCell[6];
    private int outputPos = 0;
    private int[] outputValue = new int[] { -1, -1, -1, -1, -1, -1 };
    public TargetCell targetCell;
    public TargetCell[] targetCells = new TargetCell[6];
    public Cell cell;
    public Cell[] cells = new Cell[6];
    private int pointerPosition = 0;
    private int loopIter = 0;
    private int loopPassIter = 0;
    private int[] loopStartPos = new int[50];
    private string commands;
    private int commandPosition = 0;
    private bool isActive = false;
    private bool isRunning = false;
    private int testPassed = 0; // Should be 100
    private int currentTest = 0; // Maybe only show on text
    private bool checker = true;
    private float runTimer = 0;
    private bool levelComplete = false;
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

        // Clear
        GameObject.Find("Clear").GetComponent<Text>().text = levelComplete ? "Clear!" : "";
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

        runTimer += Time.deltaTime * 1000;
        if (isActive && runTimer > 10)
        {
            ProgramRun();
            runTimer = 0;
        }

        // Checker
        GameObject.Find("Step").GetComponent<Button>().interactable = checker;
        GameObject.Find("Run").GetComponent<Button>().interactable = checker;
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
        var length = commands.Length;
        if (commandPosition < length)
        {
            var command = commands.Substring(commandPosition, 1);
            Debug.Log(command);
            switch (command)
            {
                case "+": Plus(); break;
                case "-": Minus(); break;
                case "<": CellPositionMoveLeft(); break;
                case ">": CellPositionMoveRight(); break;
                case "[": LoopStart(); break;
                case "]": LoopEnd(); break;
                case ",": Input(); break;
                case ".": Output(); break;
            }
            commandPosition += 1;
        }
        else
        {
            // Check
            for (var iter = 0; iter < 6; iter++)
            {
                if (outputValue[iter] != level.outputs[iter, testPassed])
                {
                    checker = false;
                    isActive = false;
                    Debug.Log("Result illegal!");
                }
            }
            if (checker)
            {
                Debug.Log("TestPassed!");
                testPassed += 1;
                currentTest += 1;
                commandPosition = 0;
                inputPos = 0;
                outputPos = 0;
                outputValue = new int[] { -1, -1, -1, -1, -1, -1 };
                pointerPosition = 0;
                loopIter = 0;
                loopPassIter = 0;
                loopStartPos = new int[50];
                for (var iter = 0; iter < 6; iter++)
                {
                    cells[iter].value = 0;
                    outputCells[iter].value = -1;
                    outputCells[iter].gameObject.SetActive(false);
                    inputCells[iter].GetComponent<Text>().text = level.inputs[iter, currentTest].ToString();
                    targetCells[iter].GetComponent<Text>().text = level.outputs[iter, currentTest].ToString();
                }
                GameObject.Find("TestPercent").GetComponent<Text>().text = $"{currentTest + 1}/100";
                if (testPassed >= 99)
                {
                    levelComplete = true;
                    GameObject.Find("Clear").GetComponent<Text>().text = levelComplete ? "Clear!" : "";
                    BrainfuckStop();
                }
            }
        }
    }

    public void BrainfuckStop()
    {
        commandPosition = 0;
        isRunning = false;
        isActive = false;
        inputPos = 0;
        outputPos = 0;
        outputValue = new int[] { -1, -1, -1, -1, -1, -1 };
        pointerPosition = 0;
        loopIter = 0;
        loopPassIter = 0;
        loopStartPos = new int[50];
        testPassed = 0;
        currentTest = 0;
        for (var iter = 0; iter < 6; iter++)
        {
            cells[iter].value = 0;
        }
    }

    public void EmitLength()
    {
        // Check whether the loop is legal
        var loop = 0;
        checker = true;
        foreach (var item in editor.text.ToCharArray())
        {
            if (item == '[')
            {
                loop += 1;
            }
            if (item == ']')
            {
                loop -= 1;
            }
            if (loop < 0)
            {
                // Can't Run
                checker = false;
            }
        }
        if (loop != 0)
        {
            checker = false;
        }
        GameObject.Find("InputSize").GetComponent<Text>().text = $"Size: {editor.text.Length}/100";
    }

    private void Plus()
    {
        cells[pointerPosition].value = cells[pointerPosition].value < 255 ? cells[pointerPosition].value + 1 : 0;
    }
    private void Minus()
    {
        cells[pointerPosition].value = cells[pointerPosition].value > 0 ? cells[pointerPosition].value - 1 : 255;
    }
    private void CellPositionMoveLeft()
    {
        pointerPosition = pointerPosition > 0 ? pointerPosition - 1 : 0;
    }
    private void CellPositionMoveRight()
    {
        pointerPosition = pointerPosition < 5 ? pointerPosition + 1 : 5;
    }

    private void LoopStart()
    {
        // Passed
        if (cells[pointerPosition].value != 0)
        {
            loopStartPos[loopIter] = commandPosition;
            loopIter += 1;
        }
        else
        {
            // Not Pass
            loopPassIter += 1;
            var tmpPos = 0;
            while (true)
            {
                tmpPos += 1;
                var command = commands.Substring(commandPosition + tmpPos, 1);
                if (command == "[")
                {
                    loopPassIter += 1;
                }
                if (command == "]")
                {
                    if (loopPassIter == 1)
                    {
                        loopPassIter = 0;
                        commandPosition += tmpPos;
                        break;
                    }

                    if (loopPassIter > 1)
                    {
                        loopPassIter -= 1;
                    }
                }
            }
        }
    }

    private void LoopEnd()
    {
        // Go Back
        if (cells[pointerPosition].value != 0)
        {
            commandPosition = loopStartPos[loopIter - 1];
        }
        else
        {
            // Go through
            loopIter -= 1;
        }
    }

    private void Input()
    {
        cells[pointerPosition].value = (inputPos < 5) || (level.inputs[inputPos, testPassed] != -1) ?
        level.inputs[inputPos, testPassed] : cells[pointerPosition].value;
        inputPos += 1;
    }

    private void Output()
    {
        if (outputPos < 5)
        {
            outputValue[outputPos] = cells[pointerPosition].value;
            outputCells[outputPos].gameObject.SetActive(true);
            outputCells[outputPos].value = cells[pointerPosition].value;
            Debug.Log(cells[pointerPosition].value.ToString());
            outputPos += 1;
        }
    }
}
