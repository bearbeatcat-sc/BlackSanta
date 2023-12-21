using Assets.Scripts;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneSkill : Skill
{
    /// <summary>
    /// キャンディーケインオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject m_candyCaneObject = null;

    /// <summary>
    /// 生成レート
    /// </summary>
    [SerializeField]
    private float m_generateRate = 1.0f;

    /// <summary>
    /// 落下速度
    /// </summary>
    [SerializeField]
    private float m_fallSpeed = 0.01f;

    /// <summary>
    /// タイマー
    /// </summary>
    private TimerComponent m_timerComponent = null;

    public override void Attack()
    {
        if (m_currentLevel == 0) return;

        m_timerComponent.UpdateTime();

        if (IsCanAttack())
        {
            ResetCoolTime();
            GenerateCane();
        }
    }

    public override void Initialize()
    {
        m_timerComponent = new TimerComponent();
        m_timerComponent.ClearTime();
        m_timerComponent.SetTargetTime(m_generateRate);
    }

    /// <summary>
    /// ケインの生成
    /// </summary>
    private void GenerateCane()
    {
        if (!m_candyCaneObject || !m_playerSkill) return;

        var moveVec = m_playerSkill.GetPlayerMoveVec();
        var playerPos = m_playerSkill.GetPlayerPosition();

        var instnace = GameObject.Instantiate(m_candyCaneObject, playerPos + moveVec, Quaternion.identity);
        var candyCaneObject = instnace.GetComponent<CandyCaneObject>();
        candyCaneObject.Initialize(m_fallSpeed, GetDamage(), GetKnockBackPower());
    }

    /// <summary>
    /// クールタイムのリセット
    /// </summary>
    private void ResetCoolTime()
    {
        m_timerComponent.ClearTime();
    }

    /// <summary>
    /// 攻撃できるか？
    /// </summary>
    /// <returns>
    /// <para>true 攻撃できる</para>
    /// <para>false 攻撃できない</para>
    /// </returns>
    private bool IsCanAttack()
    {
        return m_timerComponent.IsTime();
    }

    public int GetDamage()
    {
        return m_damage;
    }

    public float GetKnockBackPower()
    {
        return m_knockBackPower;
    }

    public override void StatusUp()
    {

    }
}
