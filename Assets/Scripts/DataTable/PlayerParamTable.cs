using UnityEngine;

[CreateAssetMenu(menuName = "BlackSatan/Create PlayerParamTable", fileName = "PlayerParamTable")]

public class PlayerParamTable : ScriptableObject
{
    /// <summary>
    /// �A�Z�b�g�̃p�X
    /// </summary>
    private static readonly string RESOURCE_PATH = "DataTables/Player/PlayerParamTable";

    /// <summary>
    /// �f�[�^�e�[�u���̃C���X�^���X
    /// </summary>
    private static PlayerParamTable s_instance = null;

    /// <summary>
    /// �C���X�^���X�̎擾
    /// </summary>
    public static PlayerParamTable Instance
    {
        get
        {
            if (!s_instance)
            {
                var asset = Resources.Load(RESOURCE_PATH) as PlayerParamTable;
                if (!asset)
                {
                    Debug.AssertFormat(false, "Missing Resource", RESOURCE_PATH);
                    asset = CreateInstance<PlayerParamTable>();
                }

                s_instance = asset;
            }

            return s_instance;
        }
    }

    /// <summary>
    /// �v���C���[�J�����p�����[�^
    /// </summary>
    [Header("�v���C���[�J�����p�����[�^")]
    public PlayerCameraParams m_playerCameraParams;

    /// <summary>
    /// �v���C���[�ړ��̃p�����[�^
    /// </summary>
    [Header("�v���C���[�ړ��p�����[�^")]
    public PlayerMovementParams m_playerMovementParams;
}

/// <summary>
/// �v���C���[�J�����̃p�����[�^
/// </summary>
[System.Serializable]
public class PlayerCameraParams
{
    /// <summary>
    /// �⊮�W��
    /// </summary>
    [Header("�⊮�W��")]
    [Range(0.0f, 1.0f)]
    public float m_lerpAmount = 0.7f;
}

/// <summary>
/// �v���C���[�ړ��̃p�����[�^
/// </summary>
[System.Serializable]
public class PlayerMovementParams
{
    /// <summary>
    /// �ړ����x
    /// </summary>
    [Header("�ړ����x")]
    [Range(1.0f, 100.0f)]
    public float m_moveSpeed = 10.0f;

    /// <summary>
    /// �⊮�W��
    /// </summary>
    [Header("�⊮�W��")]
    [Range(1.0f, 100.0f)]
    public float m_lerpAmount = 0.7f;
}
