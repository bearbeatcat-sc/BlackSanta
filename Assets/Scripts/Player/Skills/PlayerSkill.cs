using UnityEngine;

namespace Assets.Scripts.Player.Skills
{
    public class PlayerSkill : MonoBehaviour
    {
        /// <summary>
        ///  �X�L���^�C�v
        /// </summary>
        public enum SkillType
        {
            Bell,
            CandyCane,
            SnowBall,
        }

        /// <summary>
        /// ���C���X�L��
        /// </summary>
        [SerializeField]
        private Skill m_mainSkill = null;

        /// <summary>
        /// �v���C���[�ړ��R���|�[�l���g
        /// </summary>
        [SerializeField]
        private PlayerMovement m_playerMovement = null;

        /// <summary>
        /// �X�L�����X�g
        /// </summary>
        [SerializeField]
        private Skill[] m_skill = null;

        /// <summary>
        /// �ړ������̃x�N�g���̎擾
        /// </summary>
        /// <returns>�ړ������̃x�N�g��</returns>
        public Vector2 GetPlayerMoveVec()
        {
            if (!m_playerMovement) return Vector2.zero;

            return m_playerMovement.GetMoveVec();
        }

        /// <summary>
        /// �v���C���[�̈ʒu�̎擾
        /// </summary>
        /// <returns>�v���C���[�̈ʒu</returns>
        public Vector2 GetPlayerPosition()
        {
            return transform.position;
        }

        /// <summary>
        /// �X�L���̒ǉ�
        /// </summary>
        /// <param name="skillType">�X�L���̎��</param>
        public void AddSkill(SkillType skillType)
        {
            var skillIndex = (int)skillType;
            var skill = m_skill[skillIndex];

            skill.LevelUp();
            skill.Initialize();
            skill.SetPlayerSkill(this);
        }

        private void Start()
        {
            m_mainSkill.SetPlayerSkill(this);
            m_mainSkill.LevelUp();
            m_mainSkill.Initialize();

            AddSkill(SkillType.Bell);
            AddSkill(SkillType.CandyCane);
            AddSkill(SkillType.SnowBall);
        }

        // Update is called once per frame
        private void Update()
        {
            m_mainSkill.Attack();

            for (int i = 0; i < m_skill.Length; i++)
            {
                var skill = m_skill[i];
                if (!skill) continue;

                skill.Attack();
            }
        }
    }
}
