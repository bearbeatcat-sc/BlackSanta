using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpSystem : MonoBehaviour
{
    /// <summary>
    /// �v���C���[UI
    /// </summary>
    private PlayerUI m_playerUI = null;

    /// <summary>
    /// �擾�����o���l
    /// </summary>
    private int m_exp = 0;

    /// <summary>
    /// ���̃��x���A�b�v�ɕK�v�Ȍo���l
    /// </summary>
    private int m_levelUpExp = 0;

    /// <summary>
    /// �v���C���[���x���A�b�v�e�[�u��
    /// </summary>
    private int[] m_playerLevelUpTable = null;

    /// <summary>
    /// �ő僌�x��
    /// </summary>
    private int m_maxLevel = 0;

    /// <summary>
    /// ���݂̃��x��
    /// </summary>
    private int m_currentLevel = 0;

    /// <summary>
    /// �o���l
    /// </summary>
    public int Exp
    {
        get { return m_exp; }
    }

    /// <summary>
    /// ������
    /// </summary>
    public void Initialize(PlayerUI playerUI)
    {
        m_exp = 0;
        m_playerUI = playerUI;

        var playerParmaTable = PlayerParamTable.Instance;
        if (!playerParmaTable) return;

        var playerLevelUpParams = playerParmaTable.m_playerLevelUpParams;
        if (playerLevelUpParams == null) return;

        m_playerLevelUpTable = playerLevelUpParams.m_levelUPTable;
        m_maxLevel = m_playerLevelUpTable.Length;
    }

    /// <summary>
    /// �X�L���̎擾���\���H
    /// </summary>
    /// <returns>
    /// <para>true �\</para>
    /// <para>false �s�\</para>
    /// </returns>
    public bool IsGetSkill()
    {
        if (m_currentLevel >= m_maxLevel - 1) return false;

        return m_exp >= m_levelUpExp;
    }
    
    /// <summary>
    /// �X�e�[�^�X�̏㏸
    /// </summary>
    public void StatuUp()
    {
        if (m_currentLevel >= m_maxLevel - 1) return;

        m_currentLevel++;
        m_levelUpExp = m_playerLevelUpTable[m_currentLevel];
        Debug.Log($"{nameof(PlayerExpSystem)}.{nameof(StatuUp)} PlayerLevelUP! [PlayerLevel:{m_currentLevel}]");
        Debug.Log($"{nameof(PlayerExpSystem)}.{nameof(StatuUp)} Next [Need Exp:{m_levelUpExp}]");
        ChangeExpRatio();
    }

    /// <summary>
    /// �o���l�̉��Z
    /// </summary>
    /// <param name="exp">�o���l</param>
    public void AddExp(int exp)
    {
        m_exp += exp;
        ChangeExpRatio();

        if(m_playerUI)
        {
            m_playerUI.AddExp();
        }
    }

    private void ChangeExpRatio()
    {
        if (m_playerUI)
        {
            m_playerUI.ChangeExpRatio((float)m_exp / m_levelUpExp);
        }
    }
}
