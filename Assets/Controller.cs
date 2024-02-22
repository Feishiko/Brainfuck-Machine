using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Controller
{
    private static Controller uniqueController;
    public LevelSelector levelSelector;
    public int levelSequence = 0;
    public int levelInfoIndex = 0;
    public string levelString = "";
    public ILevel level;
    public Resolution[] resolutions = new Resolution[20];
    public ButtonLevel buttonSelect;
    public int levelIndex;

    public Resolution ResolutionFind(string levelName)
    {
        for (var iter = 0; iter < 20; iter++)
        {
            if (resolutions[iter] != null)
            {
                if (resolutions[iter].levelName == levelName)
                {
                    return resolutions[iter];
                }
            }
        }
        return null;
    }
    private Controller()
    {
    }
    public static Controller GetInstance()
    {
        if (uniqueController == null)
        {
            uniqueController = new Controller();
        }
        return uniqueController;
    }

    public string SaveToString()
    {
        for (var iter = 0; iter < 20; iter++)
        {
            if (levelSelector.buttonLevels[iter] != null)
            {
                resolutions[iter] = levelSelector.buttonLevels[iter].resolution;
            }
        }
        return JsonUtility.ToJson(this);
    }
    public Controller LoadFromString(string jsonString)
    {
        return JsonUtility.FromJson<Controller>(jsonString);
    }
    public void GameSave()
    {
        // if (!File.Exists(Application.persistentDataPath + "Json/save.json"))
        // {
        //     File.Create(Application.persistentDataPath + "Json/save.json");
        // }
        File.WriteAllText(Application.persistentDataPath + "/save.json", SaveToString());
    }

    public void GameLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/save.json"))
        {
            var controller = LoadFromString(File.ReadAllText(Application.persistentDataPath + "/save.json"));
            levelSequence = controller.levelSequence;
            resolutions = controller.resolutions;
        }
    }
}
