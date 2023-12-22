using UnityEngine;

[CreateAssetMenu(menuName = "BlackSatan/Create EnemyParamsTable", fileName = "EnemyParamsTable")]
public class EnemyParamsTable : ScriptableObject
{
    /// <summary>
    /// �A�Z�b�g�̃p�X
    /// </summary>
    private static readonly string RESOURCE_PATH = "DataTables/Enemys/EnemyParamsTable";

    /// <summary>
    /// �f�[�^�e�[�u���̃C���X�^���X
    /// </summary>
    private static EnemyParamsTable s_instance = null;

    /// <summary>
    /// �C���X�^���X�̎擾
    /// </summary>
    public static EnemyParamsTable Instance
    {
        get
        {
            if (!s_instance)
            {
                var asset = Resources.Load(RESOURCE_PATH) as EnemyParamsTable;
                if (!asset)
                {
                    Debug.AssertFormat(false, "Missing Resource", RESOURCE_PATH);
                    asset = CreateInstance<EnemyParamsTable>();
                }

                s_instance = asset;
            }

            return s_instance;
        }
    }

    /// <summary>
    /// ���{�b�g�G�l�~�[�̃p�����[�^
    /// </summary>
    [Header("���{�b�g�G�l�~�[�̃p�����[�^")]
    public RoboEnemyParams m_roboEnemyParams;
}

/// <summary>
/// ���{�b�g�G�l�~�[�̃p�����[�^
/// </summary>
[System.Serializable]
public class RoboEnemyParams
{
    /// <summary>
    /// �ړ����x
    /// </summary>
    [Header("�ړ����x")]
    [Range(1.0f, 100.0f)]
    public float m_moveSpeed = 1.0f;

    /// <summary>
    /// �U����
    /// </summary>
    [Header("�U����")]
    [Range(1, 100)]
    public int m_attack = 10;

    /// <summary>
    /// �m�b�N�o�b�N�̋���
    /// </summary>
    [Header("�m�b�N�o�b�N�̋���")]
    [Range(1.0f, 100.0f)]
    public float m_knockBackPower = 1.0f;

    /// <summary>
    /// �ő�HP
    /// </summary>
    [Header("�ő�HP")]
    [Range(1, 100)]
    public int m_maxHP = 10;
}
