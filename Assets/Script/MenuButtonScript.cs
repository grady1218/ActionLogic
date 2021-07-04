using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
	[SerializeField] private Animator[] Animators;
	[SerializeField] private AudioClip MenuSE;
	[SerializeField] private AudioClip SelectSE;
	public bool IsClear { get; set; } = false;
	public bool IsMenuOpen { get; set; } = false;
	private AudioSource _SE;
	private GameObject _arrow;
	private GameObject[] _texts;
	private RectTransform _rectTransform;
	private int _selectCount = 0;

	private void Start()
	{
		Debug.Log("aaaa");
		_rectTransform = GetComponent<RectTransform>();
		_SE = transform.GetComponent<AudioSource>();
		_texts = GetChildrenArray(gameObject).Where(obj => obj.name != "Arrow").ToArray();
		_arrow = transform.GetChild(_texts.Length).gameObject;
	}
	private void Update()
	{
		if (IsClear) return;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SetMenu(!IsMenuOpen);
		}

		if (IsMenuOpen)
		{
			int count = _selectCount;
			if (Input.GetKeyDown(KeyCode.W) && _selectCount > 0) _selectCount--;
			else if (Input.GetKeyDown(KeyCode.S) && _selectCount < _texts.Length - 1) _selectCount++;
			_arrow.transform.position = new Vector3(_arrow.transform.position.x, _texts[_selectCount].transform.position.y);

			if (count != _selectCount) _SE.PlayOneShot(SelectSE);
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) SelectMode(_texts[_selectCount].name)();
		}
	}

	private Action SelectMode(string name) => name switch
	{
		"Return" => () => SetMenu(!IsMenuOpen),
		"Retry" => () => SceneManager.LoadScene(SceneManager.GetActiveScene().name),
		"Exit" => () => SceneManager.LoadScene("SelectStageScene"),
		"Quit" => Quit,
		_ => () => { },
	};

	private void Quit()
	{
		#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_STANDALONE
				Application.Quit();
		#endif
	}

	private GameObject[] GetChildrenArray(GameObject parent)
	{
		GameObject[] array = new GameObject[parent.transform.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = parent.transform.GetChild(i).gameObject;
		}
		return array;
	}
	public void SetMenu(bool Pushed)
	{
		IsMenuOpen = Pushed;
		foreach (var anim in Animators)
		{
			anim.SetBool("IsPushMenuButton", Pushed);
		}
		_SE.PlayOneShot(MenuSE);
		GameObject.FindWithTag("Player").GetComponent<PlayerController>().IsMenuOpen = Pushed;
	}
}
