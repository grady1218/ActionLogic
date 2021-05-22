using UnityEngine;
using UnityEngine.SceneManagement;

public class GateScript : MonoBehaviour
{
	[SerializeField] GameObject EnterGateInfoPrefub;
	[SerializeField] string SceneName;
	[SerializeField] MenuButtonScript Menu;
	GameObject infoObj;

	bool _isInclude = false;
	// Start is called before the first frame update
	void Start()
	{
		if (SceneName == null)
		{
			Debug.LogError($"シーンネームを入力してください\nError by {transform.name}");
		}

		//  記録
		else if(PlayerPrefs.HasKey(SceneName))
		{
			if(PlayerPrefs.GetInt(SceneName) == 1)
			{
				transform.parent.GetChild(2).GetComponent<TextMesh>().color = Color.red;
			}
		}
		else
		{
			PlayerPrefs.SetInt(SceneName,0);
		}

		Menu ??= GameObject.Find( "MenuPanel" ).GetComponent<MenuButtonScript>();
	}

	private void Update(){
		if (_isInclude && !Menu.IsMenuOpen && Input.GetKeyDown(KeyCode.W))
		{
			_isInclude = false;
			SetPlayerPositionScript.PlayerWorldPosition = transform.position;
			SceneManager.LoadScene(SceneName);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			_isInclude = true;
			infoObj = Instantiate(EnterGateInfoPrefub);
			infoObj.transform.SetParent(collision.transform);
			infoObj.transform.localPosition = new Vector3(0, 1, 0);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			_isInclude = false;
			Destroy(infoObj);
		}
	}
}
