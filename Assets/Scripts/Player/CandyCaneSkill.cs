using Assets.Scripts;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneSkill : Skill
{
    /// <summary>
    /// �L�����f�B�[�P�C���I�u�W�F�N�g
    /// </summary>
    [SerializeField]
    private GameObject m_candyCaneObject = null;

    /// <summary>
    /// �������[�g
    /// </summary>
    [SerializeField]
    private float m_generateRate = 1.0f;

    /// <summary>
    /// �������x
    /// </summary>
    [SerializeField]
    private float m_fallSpeed = 0.01f;

    /// <summary>
    /// �^�C�}�[
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
    /// �P�C���̐���
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
    /// �N�[���^�C���̃��Z�b�g
    /// </summary>
    private void ResetCoolTime()
    {
        m_timerComponent.ClearTime();
    }

    /// <summary>
    /// �U���ł��邩�H
    /// </summary>
    /// <returns>
    /// <para>true �U���ł���</para>
    /// <para>false �U���ł��Ȃ�</para>
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
