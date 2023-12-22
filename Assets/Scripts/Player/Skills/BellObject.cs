
using Assets.Scripts;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Skills
{
    internal class BellObject : MonoBehaviour, IDamagable
    {
        /// <summary>
        /// タイマー
        /// </summary>
        private TimerComponent m_timer = null;

        /// <summary>
        /// 回転速度
        /// </summary>
        private float m_rotateSpeed = 0.0f;

        /// <summary>
        /// ベルの回転幅
        /// </summary>
        private float m_range = 0.0f;

        /// <summary>
        /// ダメージ量
        /// </summary>
        private int m_damage = 0;

        /// <summary>
        /// ノックバック力
        /// </summary>
        private float m_knockBackPower = 0.0f;

        /// <summary>
        /// ベルスキル
        /// </summary>
        private BellSkill m_bellSkill = null;

        /// <summary>
        /// ダメージの取得
        /// </summary>
        /// <returns>ダメージ</returns>
        public int GetDamage()
        {
            return m_damage;
        }

        /// <summary>
        /// ノックバック力の取得
        /// </summary>
        /// <returns>ノックバック力</returns>
        public float GetKnockBackPower()
        {
            return m_knockBackPower;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="bellSkill"></param>
        /// <param name="rotateSpeed"></param>
        /// <param name="range"></param>
        public void Initialize(BellSkill bellSkill, float rotateSpeed, float range)
        {
            m_bellSkill = bellSkill;
            m_rotateSpeed = rotateSpeed;
            m_range = range;

            if (m_timer == null)
            {
                m_timer = new TimerComponent();
            }

            m_timer.ClearTime();
            m_timer.SetTargetTime(m_rotateSpeed);
        }

        /// <summary>
        /// ダメージの設定
        /// </summary>
        /// <param name="damage">セットするダメージ</param>
        public void SetDamage(int damage)
        {
            m_damage = damage;
        }

        /// <summary>
        /// ノックバック力の設定
        /// </summary>
        /// <param name="power">セットするノックバック力</param>
        public void SetKnockBackPower(float power)
        {
            m_knockBackPower = power;
        }

        public void Attack()
        {
            m_timer.UpdateTime();
            Rotate();

            if (m_timer.IsTime())
            {
                m_timer.ClearTime();
            }
        }

        /// <summary>
        /// 回転処理
        /// </summary>
        private void Rotate()
        {
            var timeRatio = m_timer.GetRatio();
            var playerPos = m_bellSkill.GetPlayerPosition();
            var position = transform.position;
            var x = playerPos.x + m_range * Mathf.Sin(timeRatio * 360.0f);
            var y = playerPos.y + m_range * Mathf.Cos(timeRatio * 360.0f);

            position.x = x;
            position.y = y;
            transform.position = position;

        }
    }
}
