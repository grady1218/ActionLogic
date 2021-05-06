using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimationScript : MonoBehaviour
{
    Text myText;
    [SerializeField] string[] Colors;
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
