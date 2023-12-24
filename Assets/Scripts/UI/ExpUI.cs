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
    /// �o���l�X���C�_�[�̊�����ύX
    /// </summary>
    /// <param name="ratio">����</param>
    public void ChangeExpRatio(float ratio)
    {
        if (!m_expSlider) return;

        m_expSlider.value = ratio;
    }
}
