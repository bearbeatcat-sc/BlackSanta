using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Skills
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
        /// 与えるダメージ
        /// </summary>
        [SerializeField]
        protected int m_damage = 1;

        /// <summary>
        /// ノックバック力
        /// </summary>
        [SerializeField]
        protected float m_knockBackPower = 1.0f;

        /// <summary>
        /// 現在のレベル
        /// </summary>
        protected int m_currentLevel = 0;

        /// <summary>
        /// プレイヤースキル
        /// </summary>
        protected PlayerSkill m_playerSkill = null;

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
            Debug.Log($"{nameof(Skill)}.{nameof(LevelUp)}. LevelUP! : [{nameof(m_currentLevel)} : {m_currentLevel}]");
            StatusUp();
        }

        /// <summary>
        /// プレイヤースキルのセット
        /// </summary>
        /// <param name="playerSkill">プレイヤースキル</param>
        public void SetPlayerSkill(PlayerSkill playerSkill)
        {
            m_playerSkill = playerSkill;
        }

    }
}
