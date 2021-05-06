using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Select : MonoBehaviour
{
    public void LoadingNewScene()
    {
        Debug.Log("push");
        SceneManager.LoadScene("SelectStageScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
