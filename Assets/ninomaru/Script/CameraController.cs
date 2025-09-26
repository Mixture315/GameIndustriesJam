using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("カメラの追尾設定")]

    [SerializeField] Transform playerTr; // プレイヤーのTransformをInspectorから入れる

    private float distance = -10.0f;//カメラのZ値

    [Header("カメラの初期設定")]
    [SerializeField] Vector3 cameraOrgPos = new Vector3(0, 0, -10f); // カメラの初期位置位置 
    [SerializeField] Vector2 camaraMaxPos = new Vector2(10, 10); // カメラの右上限界点
    [SerializeField] Vector2 camaraMinPos = new Vector2(-10, -10); // カメラの左下限界点

    [SerializeField] private float BaseWidth = 1920;
    [SerializeField] private float BaseHeight = 1080;
    [SerializeField] private float BaseSize = 7;
    public void Adjust_OrthographicSize()
    {
        float baseAspect = BaseWidth / BaseHeight;
        float currentAspect = Camera.main.aspect;

        float aspectRate = baseAspect / currentAspect;
        if (Mathf.Approximately(1.0f, aspectRate))
        {
            return;
        }

        Camera.main.orthographicSize = aspectRate * BaseSize;
        Camera.main.ViewportToWorldPoint(Vector2.zero);
        Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    //アップデートの後に処理が始まる
    private void LateUpdate()
    {
        //Adjust_OrthographicSize();

        Vector3 originalPos = transform.position;

        originalPos = Tracking(originalPos);

        originalPos.z = distance;

        transform.position = originalPos;

    }

    public Vector2 Tracking(Vector2 pos)
    {
        Vector3 playerPos = playerTr.position; // プレイヤーの位置
        Vector3 camPos = pos; // カメラの位置

        // 滑らかにプレイヤーの場所に追従
        camPos = Vector3.Lerp(transform.position, playerPos + cameraOrgPos, 3.0f * Time.deltaTime);

        // カメラの位置を制限
        camPos.x = Mathf.Clamp(camPos.x, camaraMinPos.x, camaraMaxPos.x);
        camPos.y = Mathf.Clamp(camPos.y, camaraMinPos.y, camaraMaxPos.y);

        return camPos;
    }


}
