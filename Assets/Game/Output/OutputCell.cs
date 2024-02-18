using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputCell : MonoBehaviour
{
    public int value = -1;
    void Start()
    {
    }

    void Update()
    {
        gameObject.SetActive(value != -1);
        GetComponent<Text>().text = value.ToString();
    }
}
