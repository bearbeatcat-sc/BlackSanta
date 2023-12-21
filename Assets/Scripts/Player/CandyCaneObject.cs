using Assets.Scripts;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class CandyCaneObject : MonoBehaviour, IDamagable
    {
        /// <summary>
        /// 存在時間
        /// </summary>
        [SerializeField]
        private float m_destroyTime = 0.2f;

        /// <summary>
        /// 生成範囲X
        /// </summary>
        [SerializeField]
        private float m_fallRangeX = 4.0f;

        /// <summary>
        /// 投げる時の上昇速度
        /// </summary>
        [SerializeField]
        private float m_upSpeed = 6.0f;

        /// <summary>
        /// フリップコンポーネント
        /// </summary>
        [SerializeField]
        private FlipComponent m_flipComponent = null;

        /// <summary>
        /// タイマー
        /// </summary>
        private TimerComponent m_timer = null;

        /// <summary>
        /// ダメージ量
        /// </summary>
        private int m_damage = 0;

        /// <summary>
        /// 落下速度
        /// </summary>
        private float m_fallSpeed = 0.0f;

        /// <summary>
        /// ノックバック力
        /// </summary>
        private float m_knockBackPower = 0.0f;

        /// <summary>
        /// 移動ベクトル
        /// </summary>
        private Vector2 m_moveVec = Vector2.zero;

        /// <summary>
        /// ダメージの取得
        /// </summary>
        /// <returns></returns>
        public int GetDamage()
        {
            return m_damage;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="fallSpeed">落下速度</param>
        /// <param name="damage">ダメージ力</param>
        /// <param name="knockBackPower">ノックバック力</param>
        public void Initialize(float fallSpeed, int damage, float knockBackPower)
        {
            m_damage = damage;
            m_fallSpeed = fallSpeed;
            m_knockBackPower = knockBackPower;
            m_moveVec = new Vector2(Random.Range(-m_fallRangeX, m_fallRangeX), m_upSpeed);

            m_flipComponent.SetInitScale(transform.localScale);
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
        /// 攻撃
        /// </summary>
        public void Attack()
        {
            m_timer.UpdateTime();
            if (m_timer.IsTime())
            {
                Destroy(gameObject);
            }

            Fall();
            m_flipComponent.Flip(m_moveVec.x < 0.0f);
        }

        void Start()
        {
            m_timer = new TimerComponent();
            m_timer.ClearTime();
            m_timer.SetTargetTime(m_destroyTime);
        }

        void Update()
        {
            Attack();
        }

        /// <summary>
        /// 落下
        /// </summary>
        private void Fall()
        {
            m_moveVec += new Vector2(0, -1.0f) * m_fallSpeed * Time.deltaTime;
            var pos = transform.position;
            pos.x += m_moveVec.x * Time.deltaTime;
            pos.y += m_moveVec.y * Time.deltaTime;

            transform.position = pos;
        }

    }
}
