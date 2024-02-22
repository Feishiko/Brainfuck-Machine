using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    public Resolution resolution;
    public LevelSelector levelSelector;
    public ILevel level;
    public int levelIndex;
    void Start()
    {
        levelSelector = GameObject.Find("Canvas").GetComponent<LevelSelector>();
    }

    void Update()
    {
        if (levelSelector != null)
        {
            if (levelSelector.GetButtonSelect() == this)
            {
                GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                GetComponent<Image>().color = resolution.clear ? Color.green : Color.white;
            }
        }
    }
}
