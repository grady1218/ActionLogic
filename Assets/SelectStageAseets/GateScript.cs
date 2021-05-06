using UnityEngine;
using UnityEngine.SceneManagement;

public class GateScript : MonoBehaviour
{
    [SerializeField] GameObject EnterGateInfoPrefub;
    [SerializeField] string SceneName;
    GameObject infoObj;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneName == null)
        {
            Debug.LogError($"シーンネームを入力してください\nError by {transform.name}");
        }

        //  記録
        else if(PlayerPrefs.HasKey(SceneName))
        {
            if(PlayerPrefs.GetInt(SceneName) == 1)
            {
                transform.parent.GetChild(2).GetComponent<TextMesh>().color = Color.red;
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneName,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            infoObj = Instantiate(EnterGateInfoPrefub);
            infoObj.transform.SetParent(collision.transform);
            infoObj.transform.localPosition = new Vector3(0, 1, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SetPlayerPositionScript.PlayerWorldPosition = transform.position;
                SceneManager.LoadScene(SceneName);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(infoObj);
        }
    }
}
