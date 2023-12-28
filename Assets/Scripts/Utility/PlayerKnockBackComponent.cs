using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class PlayerKnockBackComponent : KnockBackComponent
    {
        /// <summary>
        /// プレイヤー移動
        /// </summary>
        [SerializeField]
        private PlayerMovement m_playerMovement = null;

        protected override void KnockBack()
        {
            if (!m_playerMovement) return;

            var pos = transform.position + m_knockBackVelocity * m_knockBackPower * Time.deltaTime;            
            m_knockBackPower -= m_knockBackResistance * Time.deltaTime;

            if (m_knockBackPower <= 0.0f)
            {
                m_isKnockBack = false;
            }

            transform.position = pos;
            m_playerMovement.SetTargetPosition(pos);
        }
    }
}
