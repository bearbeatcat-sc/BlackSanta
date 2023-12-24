using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpSystem : MonoBehaviour
{
    /// <summary>
    /// 取得した経験値
    /// </summary>
    private int m_exp = 0;

    /// <summary>
    /// 次のレベルアップに必要な経験値
    /// </summary>
    private int m_levelUpExp = 10;

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
    public void Initialize()
    {
        m_exp = 0;
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
        return m_exp >= m_levelUpExp;
    }
    
    /// <summary>
    /// ステータスの上昇
    /// </summary>
    public void StatuUp()
    {
        // 後で経験値テーブルを用意
        m_levelUpExp += 10;
    }

    /// <summary>
    /// 経験値の加算
    /// </summary>
    /// <param name="exp">経験値</param>
    public void AddExp(int exp)
    {
        m_exp += exp;
    }
}
