using UnityEngine;

public class TeleScript : MonoBehaviour
{

	enum DirectionMode
	{
		Up,
		Down,
		Right,
		Left
	}

	public GameObject TeleObj;
	[SerializeField] private DirectionMode Direction;
	[SerializeField] private bool IsOneWay = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		Warp(other.gameObject);
	}

	private void Warp(GameObject obj)
	{
		if (obj.GetComponent<WarpSpanScript>() != null)
		{
			if (obj.GetComponent<WarpSpanScript>().IsInPortal || IsOneWay) return;
			else obj.GetComponent<WarpSpanScript>().IsInPortal = true;

		}

		Vector2 v = new Vector2();
		var t = obj.transform.GetComponent<Rigidbody2D>().velocity;

		switch (Direction)
		{
			case DirectionMode.Up: v = new Vector2(t.x, -t.y); break;
			case DirectionMode.Down: v = new Vector2(t.x, t.y); break;
			case DirectionMode.Right: v = new Vector2(t.y, t.x); break;
			case DirectionMode.Left: v = new Vector2(-t.y, t.x); break;
		}

		obj.transform.position = TeleObj.transform.position; //- (Vector3)v * .2f;  //  .2fは補正
		obj.transform.GetComponent<Rigidbody2D>().velocity = v;


	}

}
