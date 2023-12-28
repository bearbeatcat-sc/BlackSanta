using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class KnockBackComponent : MonoBehaviour
    {
        [SerializeField]
        protected float m_knockBackResistance = 1.0f;

        /// <summary>
        /// ノックバックする力
        /// </summary>
        protected float m_knockBackPower = 1.0f;

        /// <summary>
        /// ノックバックのベクトル
        /// </summary>
        protected Vector3 m_knockBackVelocity = Vector2.zero;

        /// <summary>
        /// ノックバックしているか
        /// </summary>
        protected bool m_isKnockBack = false;

        /// <summary>
        /// ノックバックの設定
        /// </summary>
        /// <param name="vec">ノックバックするベクトル</param>
        /// <param name="power">ノックバックする力</param>
        public void SetKnockBack(Vector2 vec, float power)
        {
            m_knockBackPower = power;
            m_knockBackVelocity += new Vector3(vec.x, vec.y, 0);
            m_knockBackVelocity.Normalize();
            m_isKnockBack = true;
        }

        private void Update()
        {
            if (!m_isKnockBack) return;
            KnockBack();
        }

        /// <summary>
        /// ノックバック
        /// </summary>
        protected virtual void KnockBack()
        {
            transform.position += m_knockBackVelocity * m_knockBackPower * Time.deltaTime;
            m_knockBackPower -= m_knockBackResistance * Time.deltaTime;

            if(m_knockBackPower <= 0.0f)
            {
                m_isKnockBack = false;
            }
        }
    }
}
