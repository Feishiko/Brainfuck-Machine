using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetCell : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        gameObject.SetActive(GetComponent<Text>().text != "-1");
    }
}
