using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public ButtonLevel buttonLevel;
    public ButtonLevel[] buttonLevels = new ButtonLevel[20];
    public LevelInfoButton levelInfoButton;
    private LevelInfoButton[] levelInfoButtons = new LevelInfoButton[5];
    public ButtonLevel buttonSelect;
    public LevelInfoButton levelInfoButtonSelect;
    private Controller controller = Controller.GetInstance();
    private int levelCreated = 0;
    private GameObject content;
    void Start()
    {
        controller.levelSelector = this;
        controller.GameLoad();
        controller.GameSave();
        controller.GameLoad();
        NewLevelCreate(new SmallTest(), 0);
        NewLevelCreate(new SwapTest(), 1);
        NewLevelCreate(new SimpleLoopTest(), 2);
        NewLevelCreate(new LevelTest(), 3);
        NewLevelCreate(new PlusTest(), 3);
        NewLevelCreate(new Plus2Test(), 4);
        NewLevelCreate(new AndAndAndGateTest(), 5);
        NewLevelCreate(new TFTest(), 6);
        NewLevelCreate(new TimesTest(), 7);
        NewLevelCreate(new BiggerTest(), 8);
        NewLevelCreate(new AbsTest(), 8);
        NewLevelCreate(new BitTest(), 9);
        if (buttonLevels[11] != null)
        {
            if (buttonLevels[11].resolution.clear)
            {
                NewLevelCreate(new EDTest(), 1);
                buttonLevels[12].GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("ED"));
            }
        }
        content = GameObject.Find("Content");
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, levelCreated*55);

        // Level Info Buttons
        var levelInfoPanel = GameObject.Find("LevelInfoPanel");
        for (var iter = 0; iter < 5; iter++)
        {
            var infoButton = Instantiate<LevelInfoButton>(levelInfoButton, levelInfoPanel.transform);
            infoButton.levelInfoIndex = iter;
            levelInfoButtons[iter] = infoButton;
            levelInfoButtons[iter].GetComponent<Button>().onClick.AddListener(() => LevelInfoButtonSelect(infoButton));
        }

        // Level Button Selected
        if (controller.buttonSelect != null)
        {
            buttonSelect = controller.buttonSelect;
        }
        controller.GameSave();
    }

    void Update()
    {
        // Level Button Info
        for (var iter = 0; iter < 5; iter++)
        {
            if (buttonSelect == null)
            {
                levelInfoButtons[iter].gameObject.SetActive(false);
            }
            else
            {
                if (buttonSelect.resolution.resolutionTitles[iter] == "")
                {
                    buttonSelect.resolution.resolutionTitles[iter] = null;
                }
                if (buttonSelect.resolution.resolutionTitles[iter] != null)
                {
                    levelInfoButtons[iter].gameObject.SetActive(true);
                    levelInfoButtons[iter].GetComponentInChildren<Text>().text = buttonSelect.resolution.resolutionTitles[iter];
                }
            }
        }

        // Tool Button Interactive
        GameObject.Find("New").GetComponent<Button>().interactable = levelInfoButtonSelect != null;
        GameObject.Find("Delete").GetComponent<Button>().interactable = levelInfoButtonSelect != null;
        GameObject.Find("Rename").GetComponent<Button>().interactable = levelInfoButtonSelect != null;
        GameObject.Find("Play").GetComponent<Button>().interactable = levelInfoButtonSelect != null;

        // Story
        if (buttonSelect != null)
        {
            GameObject.Find("Story").GetComponent<Text>().text = buttonSelect.level.story;
        }

        // Quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void NewLevelCreate(ILevel level, int levelSequence)
    {
        if (controller.levelSequence >= levelSequence)
        {
            var content = GameObject.Find("Content");
            var button = Instantiate<ButtonLevel>(buttonLevel, content.transform);
            button.GetComponentInChildren<Text>().text = level.name;
            button.GetComponent<Button>().onClick.AddListener(() => ButtonSelect(button));
            button.level = level;
            button.levelIndex = levelCreated;
            levelCreated += 1;
            var resolution = controller.ResolutionFind(level.name);
            if (resolution != null)
            {
                button.resolution = resolution;
                // Debug.Log(resolution.clear);
            }
            else
            {
                button.resolution = new Resolution
                {
                    levelName = level.name
                };
                button.resolution.resolutionTitles[0] = "New Solution";
                // button.resolution.resolutionNumber += 1;
            }
            controller.GameSave();
            for (var iter = 0; iter < 20; iter++)
            {
                if (buttonLevels[iter] == null)
                {
                    buttonLevels[iter] = button;
                    break;
                }
            }
        }
    }

    public void NewResolution()
    {
        if (buttonSelect.resolution.resolutionNumber < 4)
        {
            buttonSelect.resolution.resolutionTitles[buttonSelect.resolution.resolutionNumber + 1] = "New Solution";

            buttonSelect.resolution.resolutionNumber += 1;

        }
        controller.GameSave();
    }

    public void EditResolutionName()
    {
        levelInfoButtonSelect.Rename();
    }

    public void PlayResolution()
    {
        controller.levelInfoIndex = levelInfoButtonSelect.levelInfoIndex;
        controller.levelString = buttonSelect.resolution.resolutions[levelInfoButtonSelect.levelInfoIndex];
        controller.level = buttonSelect.level;
        controller.levelIndex = buttonSelect.levelIndex;
        controller.buttonSelect = buttonSelect;
        SceneManager.LoadScene("Game");
    }

    public void DeleteResolution()
    {
        if (levelInfoButtonSelect != null)
        {
            if (buttonSelect.resolution.resolutionNumber > 0)
            {
                var deleteIndex = levelInfoButtonSelect.levelInfoIndex;
                for (var iter = 0; iter < 5 - 1; iter++)
                {
                    if (iter >= deleteIndex)
                    {
                        buttonSelect.resolution.resolutionTitles[iter] = buttonSelect.resolution.resolutionTitles[iter + 1];
                        buttonSelect.resolution.resolutions[iter] = buttonSelect.resolution.resolutions[iter + 1];
                    }
                }
                buttonSelect.resolution.resolutionTitles[buttonSelect.resolution.resolutionNumber] = null;
                buttonSelect.resolution.resolutions[buttonSelect.resolution.resolutionNumber] = null;
                levelInfoButtons[buttonSelect.resolution.resolutionNumber].gameObject.SetActive(false);
                buttonSelect.resolution.resolutionNumber -= 1;
                levelInfoButtonSelect = null;
                controller.GameSave();
            }
        }

    }

    public void ButtonSelect(ButtonLevel buttonLevel)
    {
        for (var i = 0; i < 5; i++)
        {
            levelInfoButtons[i].gameObject.SetActive(false);
        }
        buttonSelect = buttonLevel;
        buttonLevel.levelSelector = this;
    }

    public void LevelInfoButtonSelect(LevelInfoButton levelInfoButton)
    {
        levelInfoButtonSelect = levelInfoButton;
        levelInfoButton.levelSelector = this;
    }

    public ButtonLevel GetButtonSelect()
    {
        return buttonSelect;
    }

    public LevelInfoButton GetLevelInfoButtonSelect()
    {
        return levelInfoButtonSelect;
    }
}
