using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTr; // プレイヤーのTransformをInspectorから入れる

    [SerializeField] float distance = -10.0f;
    [SerializeField] Vector3 cameraOrgPos = new Vector3(0, 0, -10f); // カメラの初期位置位置 
    [SerializeField] Vector2 camaraMaxPos = new Vector2(10, 10); // カメラの右上限界点
    [SerializeField] Vector2 camaraMinPos = new Vector2(-10, -10); // カメラの左下限界点
    private void LateUpdate()
    {
        Vector3 playerPos = playerTr.position; // プレイヤーの位置
        Vector3 camPos = transform.position; // カメラの位置

        // 滑らかにプレイヤーの場所に追従
        camPos = Vector3.Lerp(transform.position, playerPos + cameraOrgPos, 3.0f * Time.deltaTime);

        // カメラの位置を制限
        camPos.x = Mathf.Clamp(camPos.x, camaraMinPos.x, camaraMaxPos.x);
        camPos.y = Mathf.Clamp(camPos.y, camaraMinPos.y, camaraMaxPos.y);
        camPos.z = -distance;
        transform.position = camPos;
    }

}
