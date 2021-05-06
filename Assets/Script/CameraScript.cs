using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float CameraSpeed = 2;
    [SerializeField] Vector3 AddCameraPos;
    [SerializeField] Vector3 Center;
    [SerializeField] Vector3 Rect;
    
    Vector3 cameraGoalPoint;
    bool isSearching = false;

    private void Start()
    {
        Player ??= GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        //  カメラを移動する
        if (!CameraMove() && transform.position != Player.transform.position && !isSearching)
        {
            cameraGoalPoint = Player.transform.position - new Vector3(0, 0, 1) + AddCameraPos;
        }

        // ポータルクリックした時、移動先に視点移動する
        foreach (var g in Physics2D.RaycastAll(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), Vector2.zero))
        {
            if (g.transform.CompareTag("WarpPoint"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    cameraGoalPoint = g.transform.GetComponent<TeleScript>().TeleObj.transform.position - new Vector3(0, 0, 1);
                    isSearching = true;
                    return;
                }
            }
        }

        if (isSearching && Input.GetMouseButtonDown(0)) isSearching = false;

        Player.GetComponent<PlayerController>().IsSearching = isSearching || Input.GetKey(KeyCode.LeftShift);
        transform.position = Vector3.Lerp(transform.position, cameraGoalPoint, .1f);
    }

    private bool CameraMove()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 移動制限
            if (Rect.x / 2 <= Mathf.Abs((cameraGoalPoint - Center).x)) cameraGoalPoint.x = Rect.x / 2 * GetDirection(cameraGoalPoint.x - Center.x) + Center.x;
            if (Rect.y / 2 <= Mathf.Abs((cameraGoalPoint - Center).y)) cameraGoalPoint.y = Rect.y / 2 * GetDirection(cameraGoalPoint.y - Center.y) + Center.y;

            if (isSearching)
            {
                isSearching = false;
            }
            cameraGoalPoint += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * CameraSpeed, Input.GetAxisRaw("Vertical") * Time.deltaTime * CameraSpeed);

            return true;
        }

        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Center, new Vector3(Rect.x, Rect.y));
    }

    public int GetDirection(float a)
    {
        return a > 0 ? 1 : -1;
    }
}
