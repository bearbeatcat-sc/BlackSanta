using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts
{
    public class FlipComponent : MonoBehaviour
    {
        /// <summary>
        /// 初期のスケール
        /// </summary>
        private Vector3 m_initScale = Vector3.zero;

        /// <summary>
        /// 初期スケールの設定
        /// </summary>
        /// <param name="initScale">初期スケール</param>
        public void SetInitScale(Vector3 initScale)
        {
            m_initScale = initScale;
        }

        /// <summary>
        /// 反転
        /// </summary>
        public void Flip(bool isLeft)
        {
            var scale = transform.localScale;
            scale.x = isLeft ? m_initScale.x * -1 : m_initScale.x;
            transform.localScale = scale;
        }
    }
}
