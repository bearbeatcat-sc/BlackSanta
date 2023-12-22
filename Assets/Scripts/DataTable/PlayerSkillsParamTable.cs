using Assets.Scripts.Player.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlackSatan/Create PlayerSkillsParamTable", fileName = "PlayerSkillsParamTable")]
public class PlayerSkillsParamTable : ScriptableObject
{
    /// <summary>
    /// �A�Z�b�g�̃p�X
    /// </summary>
    private static readonly string RESOURCE_PATH = "DataTables/Player/PlayerSkillsParamTable";

    /// <summary>
    /// �f�[�^�e�[�u���̃C���X�^���X
    /// </summary>
    private static PlayerSkillsParamTable s_instance = null;

    /// <summary>
    /// �C���X�^���X�̎擾
    /// </summary>
    public static PlayerSkillsParamTable Instance
    {
        get
        {
            if (!s_instance)
            {
                var asset = Resources.Load(RESOURCE_PATH) as PlayerSkillsParamTable;
                if (!asset)
                {
                    Debug.AssertFormat(false, "Missing Resource", RESOURCE_PATH);
                    asset = CreateInstance<PlayerSkillsParamTable>();
                }

                s_instance = asset;
            }

            return s_instance;
        }
    }

    /// <summary>
    /// �ڃX�L���̃p�����[�^
    /// </summary>
    [Header("�ڃX�L���̃p�����[�^")]
    public WhipSkillParams m_whipSkillParams;

    /// <summary>
    /// �x���X�L���̃p�����[�^
    /// </summary>
    [Header("�x���X�L���̃p�����[�^")]
    public BellSkillParams m_bellSkillParams;

    /// <summary>
    /// �L�����f�B�P�C���X�L���̃p�����[�^
    /// </summary>
    [Header("�L�����f�B�P�C���X�L���̃p�����[�^")]
    public CandyCaneSkillParams m_candyCaneSkillParams;

    /// <summary>
    /// ��ʃX�L���̃p�����[�^
    /// </summary>
    [Header("��ʃX�L���̃p�����[�^")]
    public SnowBallSkillParams m_snowBallSkillParams;
}

/// <summary>
/// �x�[�X�̃X�L���̃��x���ʃp�����[�^
/// </summary>
[System.Serializable]
public class SkillBaseLevelParam
{
    /// <summary>
    /// �m�b�N�o�b�N�̋���
    /// </summary>
    [Header("�m�b�N�o�b�N�̋���")]
    [Range(0f, 100f)]
    public float m_knockBackPower = 1.0f;

    /// <summary>
    /// �_���[�W
    /// </summary>
    [Header("�_���[�W")]
    [Range(1, 100)]
    public int m_Damage = 1;
}

/// <summary>
/// �ڃX�L���̃p�����[�^
/// </summary>
[System.Serializable]
public class WhipSkillParams
{
    /// <summary>
    /// �ڃX�L���̃��x���p�����[�^���X�g
    /// </summary>
    [Header("�ڃX�L���̃��x���ʃp�����[�^���X�g")]
    public WhipSkillLevelParam[] m_whipSkillLevelParams;
}

/// <summary>
/// �ڂ̃��x�����Ƃ̋���
/// </summary>
[System.Serializable]
public class WhipSkillLevelParam : SkillBaseLevelParam
{
    /// <summary>
    /// �ڂ̐U�郌�[�g
    /// </summary>
    [Header("�ڂ̐U�郌�[�g")]
    [Range(0f,100f)]
    public float m_whipRate = 1.0f;
}

/// <summary>
/// �x���X�L���̃p�����[�^
/// </summary>
[System.Serializable]
public class BellSkillParams
{
    /// <summary>
    /// �x���X�L���̃��x���p�����[�^���X�g
    /// </summary>
    [Header("�x���X�L���̃��x���ʃp�����[�^���X�g")]
    public BellSkillLevelParam[] m_bellSkillLevelParams;
}

/// <summary>
/// �x���̃��x�����Ƃ̋���
/// </summary>
[System.Serializable]
public class BellSkillLevelParam : SkillBaseLevelParam
{
    /// <summary>
    /// ��]���x
    /// </summary>
    [Header("��]���x")]
    [Range(0f, 100f)]
    public float m_rotateSpeed = 30.0f;

    /// <summary>
    /// ��]�͈�
    /// </summary>
    [Header("��]�͈�")]
    [Range(0f, 100f)]
    public float m_range = 3.0f;
}

/// <summary>
/// �L�����f�B�P�C���X�L���̃p�����[�^
/// </summary>
[System.Serializable]
public class CandyCaneSkillParams
{
    /// <summary>
    /// �L�����f�B�P�C���̃��x���p�����[�^���X�g
    /// </summary>
    [Header("�L�����f�B�P�C���̃��x���ʂ̃p�����[�^���X�g")]
    public CandyCaneSkillLevelParam[] m_bellSkillLevelParams;
}

/// <summary>
/// �L�����f�B�P�C���̃��x�����Ƃ̋���
/// </summary>
[System.Serializable]
public class CandyCaneSkillLevelParam : SkillBaseLevelParam
{
    /// <summary>
    /// �ړ��̎��
    /// </summary>
    [Header("�P�C���̈ړ��̎��")]
    public CandyCaneSkill.MoveType m_moveType;

    /// <summary>
    /// �������[�g
    /// </summary>
    [Header("�������[�g")]
    [Range(0f, 100f)]
    public float m_generateRate = 1.0f;

    /// <summary>
    /// �����鎞�̏㏸���x
    /// </summary>
    [Header("�����鎞�̏㏸���x")]
    [Range(0f, 100f)]
    public float m_upSpeed = 6.0f;

    /// <summary>
    /// �������x
    /// </summary>
    [Header("�������x")]
    [Range(0f, 100f)]
    public float m_fallSpeed = 0.01f;
}

/// <summary>
/// ��ʃX�L���̃p�����[�^
/// </summary>
[System.Serializable]
public class SnowBallSkillParams
{
    /// <summary>
    /// ��ʃX�L���̃��x���p�����[�^���X�g
    /// </summary>
    [Header("��ʃX�L���̃��x���ʂ̃p�����[�^���X�g")]
    public SnowBallSkillLevelParam[] m_snowBallSkillLevelParams;
}

/// <summary>
/// ��ʂ̃��x�����Ƃ̋���
/// </summary>
[System.Serializable]
public class SnowBallSkillLevelParam : SkillBaseLevelParam
{
    /// <summary>
    /// �ړ��̎��
    /// </summary>
    [Header("��ʂ̈ړ��̎��")]
    public SnowBallSkill.MoveType m_moveType;

    /// <summary>
    /// �������[�g
    /// </summary>
    [Header("�������[�g")]
    [Range(0f, 100f)]
    public float m_generateRate = 1.0f;

    /// <summary>
    /// �G���A�͈̔�
    /// </summary>
    [Header("�����܂�G���A�͈̔�")]
    [Range(0f, 100f)]
    public float m_areaScale = 1.0f;

    /// <summary>
    /// �G���A�̐�������
    /// </summary>
    [Header("�G���A�̐�������")]
    [Range(0f, 100f)]
    public float m_areaDestoryTime = 10.0f;

    /// <summary>
    /// �����鎞�̏㏸���x
    /// </summary>
    [Header("�����鎞�̏㏸���x")]
    [Range(0f, 100f)]
    public float m_upSpeed = 6.0f;

    /// <summary>
    /// �������x
    /// </summary>
    [Header("�������x")]
    [Range(0f, 100f)]
    public float m_fallSpeed = 0.01f;
}