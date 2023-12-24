using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    /// <summary>
    /// �J����
    /// </summary>
    [SerializeField]
    private Camera m_Camera = null;

    /// <summary>
    /// �⊮�W��
    /// </summary>
    [SerializeField]
    private float m_lerpAmount = 0.7f;

    /// <summary>
    /// �v���C���[�I�u�W�F�N�g
    /// </summary>
    [SerializeField]
    private GameObject m_playerObject = null;

    /// <summary>
    /// �v���C���[�I�u�W�F�N�g�̐ݒ�
    /// </summary>
    /// <param name="player">�v���C���[</param>
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
