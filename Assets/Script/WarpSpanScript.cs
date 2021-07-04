using UnityEngine;

public class WarpSpanScript : MonoBehaviour
{
	public bool IsInPortal = false;
	[SerializeField] private float SpanTime = .1f;
	private float _count = 0;
	void Update()
	{
		if (IsInPortal) _count = _count < SpanTime ? _count += Time.deltaTime : 0;
		if (_count == 0) IsInPortal = false;
	}
}
