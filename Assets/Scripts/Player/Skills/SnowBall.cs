using Assets.Scripts;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Skills
{
    internal class SnowBall : MonoBehaviour, IDamagable
    {
        /// <summary>
        /// 雪エリアのゲームオブジェクト
        /// </summary>
        [SerializeField]
        private GameObject m_snowArea = null;

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
        /// 投げる時の上昇速度
        /// </summary>
        private float m_upSpeed = 6.0f;

        /// <summary>
        /// 落下速度
        /// </summary>
        private float m_fallSpeed = 0.0f;

        /// <summary>
        /// ノックバック力
        /// </summary>
        private float m_knockBackPower = 0.0f;

        /// <summary>
        /// エリアの存在時間
        /// </summary>
        private float m_areaDestroyTime = 10.0f;

        /// <summary>
        /// エリアのスケール
        /// </summary>
        private float m_areaScale = 1.0f;

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
        /// <param name="moveType">移動の種類</param>
        /// <param name="playerVec">プレイヤーの移動ベクトル</param>
        /// <param name="fallSpeed">落下速度</param>
        /// <param name="damage">ダメージ力</param>
        /// <param name="knockBackPower">ノックバック力</param>
        /// <param name="areaDestoryTime">エリアの生存時間</param>
        /// <param name="areaScale">エリアの大きさ</param>
        /// <param name="upSpeed">上昇速度</param>
        public void Initialize(SnowBallSkill.MoveType moveType, Vector2 playerVec, float upSpeed, float fallSpeed, int damage, float knockBackPower, float areaDestoryTime, float areaScale)
        {
            m_damage = damage;
            m_fallSpeed = fallSpeed;
            m_knockBackPower = knockBackPower;
            m_upSpeed = upSpeed;
            m_areaDestroyTime = areaDestoryTime;
            m_areaScale = areaScale;

            if (moveType == SnowBallSkill.MoveType.Drop)
            {
                m_moveVec = new Vector2(Random.Range(-m_fallRangeX, m_fallRangeX), m_upSpeed);
            }
            else
            {
                m_moveVec = new Vector2(playerVec.x * 20.0f, m_upSpeed);
            }

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
                CreateGenerateSnowArea();
            }

            m_flipComponent.Flip(m_moveVec.x < 0.0f);
            Fall();
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

        /// <summary>
        /// 雪エリアの生成
        /// </summary>
        private void CreateGenerateSnowArea()
        {
            if (!m_snowArea) return;

            var instnace = GameObject.Instantiate(m_snowArea, transform.position, Quaternion.identity);
            var snowArea = instnace.GetComponent<SnowArea>();

            if(!snowArea)
            {
                snowArea.Initialize(m_damage, m_knockBackPower, m_destroyTime, m_areaScale);
            }
        }
    }
}
