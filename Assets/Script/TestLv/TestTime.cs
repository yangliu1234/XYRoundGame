using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTime : MonoBehaviour
{
    public bool press =false;
    public int[] list;
    private float timeDel;

    public static List<int> testStateList = new List<int>();


    private void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (press)
        {
            press = false;
            foreach (int num in list)
            {
                TestTime.testStateList.Add(num);
            }

            PrintStateVlaue();
        }
    }

    private void PrintStateVlaue()
    {
        foreach (int num in TestTime.testStateList)
        {
            Debug.Log("=================num:" + num);
        }
    }
}
