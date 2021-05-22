using UnityEngine;
using UnityEngine.UI;

public class ClearTime : MonoBehaviour
{
    public float time;
    public bool IsClear = false;
    Text TimeText;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        TimeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsClear)
        {
            time += Time.deltaTime;
        }
        TimeText.GetComponent<Text>().text = $"新 {time.ToString("f2")}秒";
    }
}
