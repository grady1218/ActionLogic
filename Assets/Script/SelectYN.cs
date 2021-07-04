using System;
using UnityEngine;

public class SelectYN : MonoBehaviour
{
	[SerializeField] private AudioClip MenuSE;
	[SerializeField] private AudioClip SelectSE;
	[SerializeField] private GameObject Arrow;
	[SerializeField] private GameObject[] Texts;
	private AudioSource _SE;
	private int SelectCount = 0;

	private void Start()
	{
		_SE = transform.GetComponent<AudioSource>();
	}
	private void Update()
	{
		int count = SelectCount;
		if (Input.GetKeyDown(KeyCode.A) && SelectCount > 0) SelectCount--;
		else if (Input.GetKeyDown(KeyCode.D) && SelectCount < Texts.Length - 1) SelectCount++;
		Arrow.transform.position = Texts[SelectCount].transform.position;

		if (count != SelectCount) _SE.PlayOneShot(SelectSE);
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) SelectMode(Texts[SelectCount].name)();
	}

	private Action SelectMode(string name) => name switch
	{
		"Yes" => () => SetAnim("Tutorial"),
		"No" => () => SetAnim("SelectStageScene"),
		_ => () => { }
		,
	};

	private void SetAnim(string SceneName)
	{
		var anim = transform.GetComponent<Animator>();
		anim.GetBehaviour<SceneChanger>().SceneName = SceneName;
		anim.SetTrigger("IsSelectedTrigger");
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
}
