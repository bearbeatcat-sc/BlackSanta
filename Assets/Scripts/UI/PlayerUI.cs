using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    /// <summary>
    /// �o���l�t�h
    /// </summary>
    [SerializeField]
    private ExpUI m_expUI = null;

    /// <summary>
    /// �o���l�X���C�_�[�̊�����ύX
    /// </summary>
    /// <param name="ratio">����</param>
    public void ChangeExpRatio(float ratio)
    {
        if (!m_expUI) return;

        m_expUI.ChangeExpRatio(ratio);
    }

    /// <summary>
    /// EXP�̒ǉ�
    /// </summary>
    public void AddExp()
    {
        if(!m_expUI) return;
        m_expUI.AddExp();
    }
}
