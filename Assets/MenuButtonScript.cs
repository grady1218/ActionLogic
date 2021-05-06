using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
    [SerializeField] Animator[] Animators;
    GameObject _arrow;
    GameObject[] _texts;
    RectTransform RectTransform;
    int SelectCount = 0;
    public bool IsClear {get; set;} = false;
    bool isMenuOpen = false;

    private void Start()
    {
        RectTransform = GetComponent<RectTransform>();
        _texts = GetChildrenArray( gameObject ).Where( obj => obj.name != "Arrow" ).ToArray();
        _arrow = transform.GetChild( _texts.Length ).gameObject;
    }
    private void Update()
    {
        if( IsClear ) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetMenu(!isMenuOpen);
        }

        if( isMenuOpen ) {
            if (Input.GetKeyDown(KeyCode.W) && SelectCount > 0) SelectCount--;
            else if (Input.GetKeyDown(KeyCode.S) && SelectCount < _texts.Length - 1) SelectCount++;
            _arrow.transform.position = new Vector3( _arrow.transform.position.x, _texts[SelectCount].transform.position.y );
            if( Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) ) SelectMode( _texts[SelectCount].name )();
        }
    }

    private Action SelectMode( string name ) => name switch {
        "Return" => () => SetMenu(!isMenuOpen),
        "Retry" => () => SceneManager.LoadScene(SceneManager.GetActiveScene().name),
        "Exit" => () => SceneManager.LoadScene("SelectStageScene"),
        _ => () => {},
    };
    private GameObject[] GetChildrenArray( GameObject parent ){
        GameObject[] array = new GameObject[parent.transform.childCount];
        for( int i = 0; i < array.Length; i++ ){
            array[i] = parent.transform.GetChild( i ).gameObject;
        }
        return array;
    }
    public void SetMenu(bool Pushed)
    {
        isMenuOpen = Pushed;
        foreach(var anim in Animators)
        {
            anim.SetBool("IsPushMenuButton", Pushed);
        }
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().IsMenuOpen = Pushed;
    }
}
