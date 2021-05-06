using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hito : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");

        var movement = new Vector3(moveHorizontal, 0, 0);

        rb.velocity = (movement * speed);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Flag"))
        {
            Debug.Log("hata");
            SceneManager.LoadScene("Clear");
        }
    }
}
