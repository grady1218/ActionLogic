using UnityEngine;

public class AdminScript : MonoBehaviour
{
	[SerializeField] GameObject Box;
	// Start is called before the first frame update
	public void OnPushingSwitch()
	{
		Box.GetComponent<Rigidbody2D>().gravityScale = 1f;
	}
}
