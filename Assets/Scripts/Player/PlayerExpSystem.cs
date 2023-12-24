using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpSystem : MonoBehaviour
{
    /// <summary>
    /// �擾�����o���l
    /// </summary>
    private int m_exp = 0;

    /// <summary>
    /// ���̃��x���A�b�v�ɕK�v�Ȍo���l
    /// </summary>
    private int m_levelUpExp = 10;

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
    public void Initialize()
    {
        m_exp = 0;
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
        return m_exp >= m_levelUpExp;
    }
    
    /// <summary>
    /// �X�e�[�^�X�̏㏸
    /// </summary>
    public void StatuUp()
    {
        // ��Ōo���l�e�[�u����p��
        m_levelUpExp += 10;
    }

    /// <summary>
    /// �o���l�̉��Z
    /// </summary>
    /// <param name="exp">�o���l</param>
    public void AddExp(int exp)
    {
        m_exp += exp;
    }
}
