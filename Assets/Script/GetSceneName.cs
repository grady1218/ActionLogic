using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetSceneName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = SceneManager.GetActiveScene().name;
    }
}
