using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class ClearTime : MonoBehaviour
{
	public float Time;
	public bool IsClear = false;
	private Text _timeText;
	// Start is called before the first frame update
	void Start()
	{
		Time = 0.0f;
		_timeText = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!IsClear)
		{
			Time += UnityEngine.Time.deltaTime;
		}
		_timeText.GetComponent<Text>().text = $"新 {Time.ToString("f2")}秒";
	}
}
