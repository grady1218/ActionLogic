using UnityEngine;

public class WindScript : MonoBehaviour
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		collision.transform.position += transform.rotation * Vector3.up * 0.1f;
		collision.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		collision.transform.GetComponent<Rigidbody2D>().gravityScale = 1;
	}

	private void OnDrawGizmos()
	{
		//Gizmos.DrawWireCube(transform.rotation * transform.position, new Vector3(1, 5));
	}
}
