using UnityEngine;

namespace Assets.Scripts.Player.Skills
{

    public class WhipCollider : MonoBehaviour, IDamagable
    {
        /// <summary>
        /// 当たり判定の存在時間
        /// </summary>
        [SerializeField]
        private float m_destroyTime = 0.2f;

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