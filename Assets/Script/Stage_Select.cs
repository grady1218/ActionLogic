﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Select : MonoBehaviour
{
    public void LoadNewScene()
    {
        Debug.Log("push");
        SceneManager.LoadScene("SelectStageScene");
    }
}
