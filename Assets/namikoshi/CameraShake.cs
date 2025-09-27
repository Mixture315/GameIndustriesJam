using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraShake : MonoBehaviour
{
    [Header("カメラシェイク設定")]
    [SerializeField] private float duration = 0.3f;  // 揺れる時間
    [SerializeField] private float magnitude = 0.2f; // 揺れの強さ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // スペースキーが押されたらシェイク開始
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shake(duration, magnitude));
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position; // Zは固定

        float elapsed = 0.0f;


        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;


            transform.position = new Vector3(
                originalPos.x + offsetX,
                originalPos.y + offsetY,
                originalPos.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 揺れ終わったら元の位置に戻す
        transform.position = originalPos;
    }
}
