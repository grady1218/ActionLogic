using UnityEngine;
using UnityEngine.UI;

public class FlagScript : MonoBehaviour
{
	[SerializeField] private GameObject Clear;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			string sceneName = GameObject.Find("SceneName").GetComponent<Text>().text;
			ClearTime clearTime = null;

			foreach (Transform t in Clear.transform)
			{
				if (t.name == "CurrentRecord") clearTime = t.GetComponent<ClearTime>();
				if (t.name == "OldRecord") t.GetComponent<Text>().text = $"旧 {PlayerPrefs.GetFloat(sceneName + "Time").ToString("f2")}秒";
			}

			GameObject.Find("MenuPanel").GetComponent<MenuButtonScript>().IsClear = true;
			clearTime.IsClear = true;
			PlayerPrefs.SetInt(sceneName, 1);

			if (PlayerPrefs.HasKey(sceneName + "Time"))
			{
				if (PlayerPrefs.GetFloat(sceneName + "Time") > clearTime.Time)
				{
					PlayerPrefs.SetFloat(sceneName + "Time", clearTime.Time);
				}
			}
			else
			{
				PlayerPrefs.SetFloat(sceneName + "Time", clearTime.Time);
			}


			var psc = collision.GetComponent<PlayerController>();
			psc.IsClear = true;
			psc.AudioSource.Stop();
			collision.GetComponent<Animator>().SetFloat("MoveCount", 0);
			Clear.GetComponent<Animator>().SetTrigger("Clear");
		}
	}
}



/*if (collision.transform.CompareTag("Player"))
{
    Clear.GetComponent<Animator>().SetTrigger("Clear");
}*/