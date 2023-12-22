using UnityEngine;

namespace Assets.Scripts.Player.Skills
{
    public class BellSkill : Skill, IDamagable
    {
        /// <summary>
        /// 回転速度
        /// </summary>
        [SerializeField]
        private float m_rotateSpeed = 30.0f;

        /// <summary>
        /// ベルの回転幅
        /// </summary>
        [SerializeField]
        private float m_range = 3.0f;

        /// <summary>
        /// ベルのオブジェクト
        /// </summary>
        [SerializeField]
        private GameObject m_bellObject = null;

        /// <summary>
        /// ベルオブジェクトのスクリプト
        /// </summary>
        private BellObject m_bellObjectScript = null;

        /// <summary>
        /// プレイヤーの位置取得
        /// </summary>
        /// <returns>プレイヤーの位置</returns>
        public Vector2 GetPlayerPosition()
        {
            if(!m_playerSkill) return Vector2.zero;

            return m_playerSkill.GetPlayerPosition();
        }

        public override void Attack()
        {
            if (!m_bellObjectScript) return;
            if (m_currentLevel == 0) return;

            m_bellObjectScript.Attack();
        }

        public int GetDamage()
        {
            return m_damage;
        }

        public float GetKnockBackPower()
        {
            return m_knockBackPower;
        }

        public override void Initialize()
        {
            var instance = GameObject.Instantiate(m_bellObject);
            m_bellObjectScript = instance.GetComponent<BellObject>();

            m_bellObjectScript.SetDamage(GetDamage());
            m_bellObjectScript.SetKnockBackPower(GetKnockBackPower());
            m_bellObjectScript.Initialize(this, m_rotateSpeed, m_range);
        }

        public override void StatusUp()
        {

        }


    }
}
