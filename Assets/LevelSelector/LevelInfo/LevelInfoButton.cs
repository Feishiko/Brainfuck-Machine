using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoButton : MonoBehaviour
{
    public LevelSelector levelSelector;
    public int levelInfoIndex;
    private GameObject inputField;
    private Controller controller = Controller.GetInstance();
    void Start()
    {
        inputField = GameObject.Find("InputField");
        inputField.SetActive(false);

    }

    void Update()
    {
        if (levelSelector != null)
        {
            if (levelSelector.GetLevelInfoButtonSelect() == this)
            {
                GetComponent<Image>().color = Color.blue;
            }
            else
            {
                GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void Submit()
    {
        levelSelector.buttonSelect.resolution.resolutionTitles[levelSelector.levelInfoButtonSelect.levelInfoIndex] = inputField.GetComponent<InputField>().text;
        controller.GameSave();
        inputField.SetActive(false);
    }

    public void Rename()
    {
        inputField.SetActive(true);
    }
}
