using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchScript : MonoBehaviour
{
    [SerializeField] Sprite HitSprite;
    [SerializeField] UnityEvent PushingEvent;
    [SerializeField] UnityEvent PushExitEvent;
    Sprite baseSprite;
    // Start is called before the first frame update
    void Start()
    {
        baseSprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().sprite = HitSprite;
        PushingEvent.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().sprite = baseSprite;
    }
}
