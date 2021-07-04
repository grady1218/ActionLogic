using UnityEngine;
using UnityEngine.Events;

public class SwitchScript : MonoBehaviour
{
	[SerializeField] private Sprite HitSprite;
	[SerializeField] private UnityEvent PushingEvent;
	[SerializeField] private UnityEvent PushExitEvent;
	private Sprite _baseSprite;
	// Start is called before the first frame update
	void Start()
	{
		_baseSprite = GetComponent<SpriteRenderer>().sprite;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		GetComponent<SpriteRenderer>().sprite = HitSprite;
		PushingEvent.Invoke();
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		GetComponent<SpriteRenderer>().sprite = _baseSprite;
	}
}
