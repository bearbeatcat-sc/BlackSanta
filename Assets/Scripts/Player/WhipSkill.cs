using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class WhipSkill : Skill, IDamagable
    {
        /// <summary>
        /// 鞭を振るレート
        /// </summary>
        [SerializeField]
        private float m_whipRate = 1.0f;


        /// <summary>
        /// 鞭の当たり判定のオブジェクト
        /// </summary>
        [SerializeField]
        private GameObject m_whipColliderObject = null;

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
                GenerateWhip();
            }
        }

        public override void Initialize()
        {
            m_timerComponent = new TimerComponent();
            m_timerComponent.ClearTime();
            m_timerComponent.SetTargetTime(m_whipRate);
        }

        public override void StatusUp()
        {

        }

        /// <summary>
        /// 鞭の生成
        /// </summary>
        private void GenerateWhip()
        {
            if (!m_whipColliderObject || !m_playerSkill) return;

            var moveVec = m_playerSkill.GetPlayerMoveVec();
            var playerPos = m_playerSkill.GetPlayerPosition();

           var instnace =  GameObject.Instantiate(m_whipColliderObject, playerPos + moveVec, Quaternion.identity);
           var collider = instnace.GetComponent<WhipCollider>();
            collider.SetDamage(GetDamage());
            collider.SetKnockBackPower(GetKnockBackPower());
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
    }
}
