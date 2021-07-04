using UnityEngine;

public class BoxScript : MonoBehaviour
{
	private Vector3 _prePos;
	private GameObject _particle;

	private void Start()
	{
		_particle = transform.GetChild(0).gameObject;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player") && Mathf.Abs(collision.transform.position.y - transform.position.y) <= .2f)
		{
			collision.transform.GetComponent<Animator>().SetBool("IsPush", true);
			_prePos = transform.position;
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			var e = _particle.GetComponent<ParticleSystem>().emission;
			e.enabled = _prePos != transform.position;
			_prePos = transform.position;

			if (Mathf.Abs(collision.transform.position.y - transform.position.y) <= .2f && !collision.transform.GetComponent<Animator>().GetBool("IsPush"))
			{
				collision.transform.GetComponent<Animator>().SetBool("IsPush", true);
			}
			else if (Mathf.Abs(collision.transform.position.y - transform.position.y) > .2f && collision.transform.GetComponent<Animator>().GetBool("IsPush"))
			{
				collision.transform.GetComponent<Animator>().SetBool("IsPush", false);
			}

		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			var e = _particle.GetComponent<ParticleSystem>().emission;
			e.enabled = false;
			collision.transform.GetComponent<Animator>().SetBool("IsPush", false);
		}
	}

}
