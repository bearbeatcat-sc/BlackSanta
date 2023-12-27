using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    /// <summary>
    /// �o���l�X���C�_�[
    /// </summary>
    [SerializeField]
    private Slider m_expSlider = null;

    /// <summary>
    /// �A�j���[�^�[
    /// </summary>
    [SerializeField]
    private Animator m_presentBoxAnimator = null;

    /// <summary>
    /// �o���l�X���C�_�[�̊�����ύX
    /// </summary>
    /// <param name="ratio">����</param>
    public void ChangeExpRatio(float ratio)
    {
        if (!m_expSlider) return;

        m_expSlider.value = ratio;
    }

    /// <summary>
    /// EXP�̒ǉ�
    /// </summary>
    public void AddExp()
    {
        if (m_presentBoxAnimator)
        {
            m_presentBoxAnimator.Play("GetPresentAnimation");
        }
    }
}
