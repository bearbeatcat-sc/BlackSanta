using Assets.Scripts;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Skills
{
    public class SnowBallSkill: Skill
    {
        /// <summary>
        /// 移動の種類
        /// </summary>
        public enum MoveType
        {
            Straight,
            Drop,
        }

        /// <summary>
        /// 移動の種類
        /// </summary>
        [SerializeField]
        private MoveType m_moveType = MoveType.Drop;

        /// <summary>
        /// スノーボールのオブジェクト
        /// </summary>
        [SerializeField]
        private GameObject m_snowBallObject = null;

        /// <summary>
        /// 生成レート
        /// </summary>
        [SerializeField]
        private float m_generateRate = 1.0f;

        /// <summary>
        /// 投げる時の上昇速度
        /// </summary>
        [SerializeField]
        private float m_upSpeed = 6.0f;

        /// <summary>
        /// 落下速度
        /// </summary>
        [SerializeField]
        private float m_fallSpeed = 0.01f;

        /// <summary>
        /// エリアの範囲
        /// </summary>
        [SerializeField]
        private float m_areaScale = 1.0f;

        /// <summary>
        /// エリアの生存時間
        /// </summary>
        [SerializeField]
        private float m_areaDestroyTime = 10.0f;

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
                GenerateSnowBall();
            }
        }

        public override void Initialize()
        {
            m_timerComponent = new TimerComponent();
            m_timerComponent.ClearTime();
            m_timerComponent.SetTargetTime(m_generateRate);
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

        /// <summary>
        /// 雪玉の生成
        /// </summary>
        private void GenerateSnowBall()
        {
            if (!m_snowBallObject || !m_playerSkill) return;

            var moveVec = m_playerSkill.GetPlayerMoveVec();
            var playerPos = m_playerSkill.GetPlayerPosition();

            var instnace = GameObject.Instantiate(m_snowBallObject, playerPos + moveVec, Quaternion.identity);
            var snowBallObject = instnace.GetComponent<SnowBall>();
            snowBallObject.Initialize(m_moveType, moveVec, m_moveType == MoveType.Drop ? m_upSpeed : 0.0f, m_fallSpeed, GetDamage(), GetKnockBackPower(), m_areaDestroyTime, m_areaScale);
        }

        public float GetKnockBackPower()
        {
            return m_knockBackPower;
        }

        public override void StatusUp()
        {
            var playerSkillParamTable = PlayerSkillsParamTable.Instance;
            if (!playerSkillParamTable) return;

            var snowSkillParams = playerSkillParamTable.m_snowBallSkillParams;
            if (snowSkillParams == null) return;

            var snowBallSkilLevelParams = snowSkillParams.m_snowBallSkillLevelParams;
            if (snowBallSkilLevelParams.Length <= 0 || snowBallSkilLevelParams == null) return;

            var levelParam = snowBallSkilLevelParams[m_currentLevel - 1];
            if (levelParam == null) return;

            m_knockBackPower = levelParam.m_knockBackPower;
            m_damage = levelParam.m_Damage;
            m_areaScale = levelParam.m_areaScale;
            m_generateRate = levelParam.m_generateRate;
            m_moveType = levelParam.m_moveType;
            m_upSpeed = levelParam.m_upSpeed;
            m_fallSpeed = levelParam.m_fallSpeed;
            m_areaDestroyTime = levelParam.m_areaDestoryTime;
        }
    }
}
