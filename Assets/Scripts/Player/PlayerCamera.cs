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

    /// <summary>
    /// プレイヤーオブジェクトの設定
    /// </summary>
    /// <param name="player">プレイヤー</param>
    public void SetPlayer(GameObject player)
    {
        m_playerObject = player;
    }

    // Start is called before the first frame update
    void Start()
    {
        var playerParamTable = PlayerParamTable.Instance;
        Debug.Assert(playerParamTable != null, "paramTable is null.");

        var playerCameraParams  = playerParamTable.m_playerCameraParams;
        if (playerCameraParams == null)
        {
            return;
        }

        m_lerpAmount = playerCameraParams.m_lerpAmount;
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
