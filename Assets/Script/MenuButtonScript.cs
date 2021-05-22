using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
    [SerializeField] Animator[] Animators;
    [SerializeField] AudioClip MenuSE;
    [SerializeField] AudioClip SelectSE;
    AudioSource SE;
    GameObject _arrow;
    GameObject[] _texts;
    RectTransform RectTransform;
    int SelectCount = 0;
    public bool IsClear {get; set;} = false;
    public bool IsMenuOpen{get; set;} = false;

    private void Start()
    {
        Debug.Log("aaaa");
        RectTransform = GetComponent<RectTransform>();
        SE = transform.GetComponent<AudioSource>();
        _texts = GetChildrenArray( gameObject ).Where( obj => obj.name != "Arrow" ).ToArray();
        _arrow = transform.GetChild( _texts.Length ).gameObject;
    }
    private void Update()
    {
        if( IsClear ) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetMenu(!IsMenuOpen);
        }

        if( IsMenuOpen ) {
            int count = SelectCount;
            if (Input.GetKeyDown(KeyCode.W) && SelectCount > 0) SelectCount--;
            else if (Input.GetKeyDown(KeyCode.S) && SelectCount < _texts.Length - 1) SelectCount++;
            _arrow.transform.position = new Vector3( _arrow.transform.position.x, _texts[SelectCount].transform.position.y );

            if( count != SelectCount ) SE.PlayOneShot( SelectSE );
            if( Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) ) SelectMode( _texts[SelectCount].name )();
        }
    }

    private Action SelectMode( string name ) => name switch {
        "Return" => () => SetMenu(!IsMenuOpen),
        "Retry" => () => SceneManager.LoadScene(SceneManager.GetActiveScene().name),
        "Exit" => () => SceneManager.LoadScene("SelectStageScene"),
        "Quit" => Quit,
        _ => () => {},
    };

    private void Quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        Application.Quit();
        #endif
    }

    private GameObject[] GetChildrenArray( GameObject parent ){
        GameObject[] array = new GameObject[parent.transform.childCount];
        for( int i = 0; i < array.Length; i++ ){
            array[i] = parent.transform.GetChild( i ).gameObject;
        }
        return array;
    }
    public void SetMenu(bool Pushed)
    {
        IsMenuOpen = Pushed;
        foreach(var anim in Animators)
        {
            anim.SetBool("IsPushMenuButton", Pushed);
        }
        SE.PlayOneShot( MenuSE );
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().IsMenuOpen = Pushed;
    }
}
