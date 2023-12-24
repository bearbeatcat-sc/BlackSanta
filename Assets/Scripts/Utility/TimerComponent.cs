using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class TimerComponent
    {
        /// <summary>
        /// 目標時間
        /// </summary>
        [SerializeField]
        private float m_targetTime = 1.0f;

        /// <summary>
        /// 現在の時間
        /// </summary>
        private float m_currentTime = 0.0f;

        /// <summary>
        /// 時間の更新
        /// </summary>
        public void UpdateTime()
        {
            m_currentTime += Time.deltaTime;
        }

        /// <summary>
        /// 時間の初期化
        /// </summary>
        public void ClearTime()
        {
            m_currentTime = 0.0f;
        }

        /// <summary>
        /// 割合の取得
        /// </summary>
        /// <returns>割合</returns>
        public float GetRatio()
        {
            return m_currentTime / m_targetTime;
        }

        /// <summary>
        /// 目標時間の設定
        /// </summary>
        /// <param name="targetTime">目標時間</param>
        public void SetTargetTime(float targetTime)
        {
            m_targetTime = targetTime;
        }

        /// <summary>
        /// 目標時間を超えているか
        /// </summary>
        /// <returns>
        /// <para>true 超えている</para>
        /// <para>false 超えていない</para>
        /// </returns>
        public bool IsTime()
        {
            return m_currentTime >= m_targetTime;
        }
    }
}
