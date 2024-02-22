using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resolution
{
    public string levelName;
    public int resolutionNumber = 0;
    public bool clear = false;
    public string[] resolutionTitles = new string[5];
    public string[] resolutions = new string[5];
}
