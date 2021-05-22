using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTutorial : MonoBehaviour
{
    [SerializeField] GameObject DisplayObject;
    private void Start() {
        DisplayObject ??= transform.GetChild( 0 ).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other){
        if( other.tag != "Player" ) return;
        DisplayObject.SetActive( true );
        DisplayObject.GetComponent<Animator>().SetBool( "IsIncludeArea", true );
    } 
    private void OnTriggerExit2D(Collider2D other){
        DisplayObject.GetComponent<Animator>().SetBool( "IsIncludeArea", false );
    }
    
}
