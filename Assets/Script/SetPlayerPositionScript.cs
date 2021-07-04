using UnityEngine;

public class SetPlayerPositionScript : MonoBehaviour
{
	public static Vector3 PlayerWorldPosition { get; set; }
	private void Start()
	{
		if (PlayerWorldPosition != Vector3.zero)
		{
			GameObject.FindGameObjectWithTag("Player").transform.position = PlayerWorldPosition;
		}
	}
}
