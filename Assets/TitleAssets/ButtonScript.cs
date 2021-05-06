using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] GameObject[] ExitObjs;
    public void OnPressedStartButton()
    {
        foreach(var obj in ExitObjs)
        {
            obj.GetComponent<Animator>().SetTrigger("TitleExitTrigger");
        }
    }
}
