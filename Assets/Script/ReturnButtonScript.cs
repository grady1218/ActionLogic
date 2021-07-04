using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButtonScript : MonoBehaviour
{
	public void LoadingNewScene()
	{
		SceneManager.LoadScene("SelectStageScene");
	}
}
