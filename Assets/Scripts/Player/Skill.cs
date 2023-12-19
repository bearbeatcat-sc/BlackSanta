using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// スキルの抽象クラス
    /// </summary>
    public abstract class Skill : MonoBehaviour
    {
        /// <summary>
        /// 最大レベル
        /// </summary>
        [SerializeField]
        private int m_MaxLevel = 5;

        /// <summary>
        /// 現在のレベル
        /// </summary>
        private int m_currentLevel = 0;

        /// <summary>
        /// 攻撃
        /// </summary>
        public abstract void Attack();

        /// <summary>
        /// 初期化処理
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// ステータスの上昇
        /// </summary>
        public abstract void StatusUp();

        /// <summary>
        /// レベルアップ
        /// </summary>
        public void LevelUp()
        {
            m_currentLevel = Mathf.Clamp(m_currentLevel + 1, 1, m_MaxLevel);
            StatusUp();
        }

        // Start is called before the first frame update
        private void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        private void Update()
        {
            Attack();
        }
    }
}
