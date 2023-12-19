using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    /// <summary>
    /// カメラ
    /// </summary>
    [SerializeField]
    private Camera m_Camera = null;

    /// <summary>
    /// 補完係数
    /// </summary>
    [SerializeField]
    private float m_lerpAmount = 0.7f;

    /// <summary>
    /// プレイヤーオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject m_playerObject = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (!m_Camera || !m_playerObject) return;

        var cameraPosition = m_Camera.transform.position;
        var playerPosition = m_playerObject.transform.position;

        cameraPosition = Vector3.Lerp(cameraPosition, playerPosition, Time.deltaTime * m_lerpAmount);
        cameraPosition.z = -1.0f;
        m_Camera.transform.position = cameraPosition;
    }
}
