using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    /// <summary>
    /// 経験値スライダー
    /// </summary>
    [SerializeField]
    private Slider m_expSlider = null;

    /// <summary>
    /// アニメーター
    /// </summary>
    [SerializeField]
    private Animator m_presentBoxAnimator = null;

    /// <summary>
    /// 経験値スライダーの割合を変更
    /// </summary>
    /// <param name="ratio">割合</param>
    public void ChangeExpRatio(float ratio)
    {
        if (!m_expSlider) return;

        m_expSlider.value = ratio;
    }

    /// <summary>
    /// EXPの追加
    /// </summary>
    public void AddExp()
    {
        if (m_presentBoxAnimator)
        {
            m_presentBoxAnimator.Play("GetPresentAnimation");
        }
    }
}
