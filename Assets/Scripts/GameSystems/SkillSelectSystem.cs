using Assets.Scripts.Player.Skills;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.GameSystems
{
    public class SkillSelectSystem : MonoBehaviour
    {
        /// <summary>
        /// スキルセレクトUI
        /// </summary>
        [SerializeField]
        private SkillSelectUI m_skillSelectUI = null;

        /// <summary>
        /// プレイヤースキルシステム
        /// </summary>
        private PlayerSkill m_playerSkill = null;

        /// <summary>
        /// スキル選択インデックス
        /// </summary>
        private int m_skillSelectIndex = 0;

        /// <summary>
        /// スキルタイプリスト
        /// </summary>
        private PlayerSkill.SkillType[] m_skillTypes = new PlayerSkill.SkillType[3];

        /// <summary>
        /// インプットシステム
        /// </summary>
        private BlackSantaInputSystem m_inputSystem = null;

        private void Awake()
        {
            m_inputSystem = new BlackSantaInputSystem();
            m_inputSystem.Enable();
            m_inputSystem.Player.SkillSelect_Left.performed += OnPressLeftArrow;
            m_inputSystem.Player.SkillSelect_Right.performed += OnPressRightArrow;
        }

        /// <summary>
        /// プレイヤースキルシステムの設定
        /// </summary>
        /// <param name="playerSkill">プレイヤースキルシステム</param>
        public void SetPlayerSkill(PlayerSkill playerSkill)
        {
            m_playerSkill = playerSkill;
        }

        /// <summary>
        /// 矢印左キー押された瞬間のコールバック
        /// </summary>
        /// <param name="context"></param>
        public void OnPressLeftArrow(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            m_skillSelectIndex = Mathf.Clamp(m_skillSelectIndex - 1, 0, 2);
            m_skillSelectUI.SelectSkill(m_skillSelectIndex);
        }

        /// <summary>
        /// 矢印右キー押された瞬間のコールバック
        /// </summary>
        /// <param name="context"></param>
        public void OnPressRightArrow(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            m_skillSelectIndex = Mathf.Clamp(m_skillSelectIndex + 1, 0, 2);
            m_skillSelectUI.SelectSkill(m_skillSelectIndex);
        }

        /// <summary>
        /// スキル選択の開始
        /// </summary>
        public void StartSelectSkill()
        {
            Time.timeScale = 0.0f;
            SetSkills();
            m_skillSelectUI.Show();
            m_skillSelectIndex = 0;
            m_skillSelectUI.SelectSkill(m_skillSelectIndex);
        }

        /// <summary>
        /// スキル選択中の更新処理
        /// </summary>
        /// <returns>
        /// <para>true スキル選択を終えた</para>
        /// <para>false スキル選択を終えていない</para>
        /// </returns>
        public bool UpdateSkillSelect()
        {
            var keyBoard = Keyboard.current;
            if (keyBoard == null) return true;

            var enterKey = keyBoard.enterKey;
            if (enterKey.isPressed)
            {
                var skill = m_skillTypes[m_skillSelectIndex];
                if (m_playerSkill)
                {
                    m_playerSkill.AddSkill(skill);
                    m_skillSelectUI.SelectEnterSkill(m_skillSelectIndex);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// スキル選択の終了
        /// </summary>
        public void EndSelectSkill()
        {
            Time.timeScale = 1.0f;
            m_skillSelectIndex = 0;
            m_skillSelectUI.Hide();
        }

        /// <summary>
        /// スキルリストのセット
        /// </summary>
        public void SetSkills()
        {
            for(int i = 0; i < m_skillTypes.Length; i++)
            {
                var skill = GetRandomSkillType();
                m_skillTypes[i] = skill;
            }

            m_skillSelectUI.SetSkillUI(m_skillTypes);
        }

        /// <summary>
        /// ランダムにスキルタイプを取得
        /// </summary>
        /// <returns>スキルタイプ</returns>
        public PlayerSkill.SkillType GetRandomSkillType()
        {
            var max = (int)PlayerSkill.SkillType.Max;
            var index = Random.Range(0, max - 1);

            return (PlayerSkill.SkillType)index;
        }
    }
}
