using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectYN : MonoBehaviour
{
	[SerializeField] AudioClip MenuSE;
	[SerializeField] AudioClip SelectSE;
	AudioSource SE;
	[SerializeField] GameObject _arrow;
	[SerializeField] GameObject[] _texts;
	int SelectCount = 0;

	private void Start()
	{
		SE = transform.GetComponent<AudioSource>();
	}
	private void Update()
	{
		int count = SelectCount;
		if (Input.GetKeyDown(KeyCode.A) && SelectCount > 0) SelectCount--;
		else if (Input.GetKeyDown(KeyCode.D) && SelectCount < _texts.Length - 1) SelectCount++;
		_arrow.transform.position = _texts[SelectCount].transform.position;

		if( count != SelectCount ) SE.PlayOneShot( SelectSE );
		if( Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) ) SelectMode( _texts[SelectCount].name )();
	}

	private Action SelectMode( string name ) => name switch {
		"Yes" => () => SetAnim("Tutorial"),
		"No" => () => SetAnim("SelectStageScene"),
		_ => () => {},
	};

	private void SetAnim( string SceneName ) {
		var anim = transform.GetComponent<Animator>();
		anim.GetBehaviour<SceneChanger>().SceneName = SceneName;
		anim.SetTrigger( "IsSelectedTrigger" );
	}


	private GameObject[] GetChildrenArray( GameObject parent ){
		GameObject[] array = new GameObject[parent.transform.childCount];
		for( int i = 0; i < array.Length; i++ ){
			array[i] = parent.transform.GetChild( i ).gameObject;
		}
		return array;
	}
}
