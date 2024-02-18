using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell: MonoBehaviour
{
    public int value = 0;
    void Start()
    {
    }

    void Update()
    {
        GetComponent<Text>().text = value.ToString();
    }
}
