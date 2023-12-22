using Assets.Scripts;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Skills
{
    internal class SnowArea : MonoBehaviour, IDamagable
    {
        /// <summary>
        /// 当たり判定の存在時間
        /// </summary>
        private float m_destroyTime = 10.0f;

        /// <summary>
        /// 範囲の大きさ
        /// </summary>
        private float m_scale = 2.0f;

        /// <summary>
        /// タイマー
        /// </summary>
        private TimerComponent m_timer = null;

        /// <summary>
        /// ダメージ量
        /// </summary>
        private int m_damage = 0;

        /// <summary>
        /// ノックバック力
        /// </summary>
        private float m_knockBackPower = 0.0f;

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
        /// <param name="damage">ダメージ</param>
        /// <param name="knockBackPower">ノックバック力</param>
        public void Initialize(int damage, float knockBackPower, float destoryTime, float scale)
        {
            m_damage = damage;
            m_knockBackPower = knockBackPower;
            m_destroyTime = destoryTime;
            m_scale = scale;

            transform.localScale = transform.localScale * scale;
        }

        // Start is called before the first frame update
        void Start()
        {
            m_timer = new TimerComponent();
            m_timer.ClearTime();
            m_timer.SetTargetTime(m_destroyTime);
        }

        // Update is called once per frame
        void Update()
        {
            m_timer.UpdateTime();
            if (m_timer.IsTime())
            {
                Destroy(gameObject);
            }
        }
    }
}
