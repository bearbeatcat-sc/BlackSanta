using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpSystem : MonoBehaviour
{
    /// <summary>
    /// プレイヤーUI
    /// </summary>
    private PlayerUI m_playerUI = null;

    /// <summary>
    /// 取得した経験値
    /// </summary>
    private int m_exp = 0;

    /// <summary>
    /// 次のレベルアップに必要な経験値
    /// </summary>
    private int m_levelUpExp = 0;

    /// <summary>
    /// プレイヤーレベルアップテーブル
    /// </summary>
    private int[] m_playerLevelUpTable = null;

    /// <summary>
    /// 最大レベル
    /// </summary>
    private int m_maxLevel = 0;

    /// <summary>
    /// 現在のレベル
    /// </summary>
    private int m_currentLevel = 0;

    /// <summary>
    /// 経験値
    /// </summary>
    public int Exp
    {
        get { return m_exp; }
    }

    /// <summary>
    /// 初期化
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
    /// スキルの取得が可能か？
    /// </summary>
    /// <returns>
    /// <para>true 可能</para>
    /// <para>false 不可能</para>
    /// </returns>
    public bool IsGetSkill()
    {
        if (m_currentLevel >= m_maxLevel - 1) return false;

        return m_exp >= m_levelUpExp;
    }
    
    /// <summary>
    /// ステータスの上昇
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
    /// 経験値の加算
    /// </summary>
    /// <param name="exp">経験値</param>
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
