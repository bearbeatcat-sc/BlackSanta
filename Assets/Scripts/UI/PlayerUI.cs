using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    /// <summary>
    /// 経験値ＵＩ
    /// </summary>
    [SerializeField]
    private ExpUI m_expUI = null;

    /// <summary>
    /// 経験値スライダーの割合を変更
    /// </summary>
    /// <param name="ratio">割合</param>
    public void ChangeExpRatio(float ratio)
    {
        if (!m_expUI) return;

        m_expUI.ChangeExpRatio(ratio);
    }

    /// <summary>
    /// EXPの追加
    /// </summary>
    public void AddExp()
    {
        if(!m_expUI) return;
        m_expUI.AddExp();
    }
}
