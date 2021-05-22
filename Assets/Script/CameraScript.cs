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
    MenuButtonScript _clear;
    bool _isSearching = false;

    private void Start()
    {
        Player ??= GameObject.FindGameObjectWithTag("Player");
        _clear = GameObject.Find("MenuPanel").GetComponent<MenuButtonScript>();
    }
    void Update()
    {
        //  カメラを移動する
        if (IsCameraMove() && !_isSearching && transform.position != Player.transform.position)
        {
            cameraGoalPoint = Player.transform.position - new Vector3(0, 0, 1) + AddCameraPos;
        }
        else
        {
            cameraGoalPoint += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * CameraSpeed, Input.GetAxisRaw("Vertical") * Time.deltaTime * CameraSpeed);
        }

        // ポータルクリックした時、移動先に視点移動する
        foreach (var g in Physics2D.RaycastAll(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), Vector2.zero))
        {
            if (g.transform.CompareTag("WarpPoint"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    cameraGoalPoint = g.transform.GetComponent<TeleScript>().TeleObj.transform.position - new Vector3(0, 0, 1);
                    _isSearching = true;
                    return;
                }
            }
        }

        if (_isSearching && Input.GetMouseButtonDown(0)) _isSearching = false;

        Player.GetComponent<PlayerController>().IsSearching = _isSearching || Input.GetKey(KeyCode.LeftShift);
        transform.position = Vector3.Lerp(transform.position, cameraGoalPoint, .1f);
    }

    private bool IsCameraMove()
    {
        if (!_clear.IsMenuOpen && !_clear.IsClear && Input.GetKey(KeyCode.LeftShift))
        {
            // 移動制限
            if (Rect.x / 2 <= Mathf.Abs((cameraGoalPoint - Center).x)) cameraGoalPoint.x = Rect.x / 2 * GetDirection(cameraGoalPoint.x - Center.x) + Center.x;
            if (Rect.y / 2 <= Mathf.Abs((cameraGoalPoint - Center).y)) cameraGoalPoint.y = Rect.y / 2 * GetDirection(cameraGoalPoint.y - Center.y) + Center.y;

            if (_isSearching) _isSearching = false;
            return false;
        }
        return true;
    }

    private void OnDrawGizmos() => Gizmos.DrawWireCube(Center, new Vector3(Rect.x, Rect.y));
    
    public int GetDirection(float a) => a > 0 ? 1 : -1;
}
