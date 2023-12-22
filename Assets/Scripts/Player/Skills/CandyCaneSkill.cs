using Assets.Scripts;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Skills;
using UnityEngine;

namespace Assets.Scripts.Player.Skills
{
    public class CandyCaneSkill : Skill
    {
        /// <summary>
        /// �ړ��̎��
        /// </summary>
        public enum MoveType
        {
            Straight,
            Drop,
        }

        /// <summary>
        /// �ړ��̎��
        /// </summary>
        [SerializeField]
        private MoveType m_moveType = MoveType.Drop;

        /// <summary>
        /// �L�����f�B�[�P�C���I�u�W�F�N�g
        /// </summary>
        [SerializeField]
        private GameObject m_candyCaneObject = null;

        /// <summary>
        /// �������[�g
        /// </summary>
        [SerializeField]
        private float m_generateRate = 1.0f;

        /// <summary>
        /// �����鎞�̏㏸���x
        /// </summary>
        [SerializeField]
        private float m_upSpeed = 6.0f;

        /// <summary>
        /// �������x
        /// </summary>
        [SerializeField]
        private float m_fallSpeed = 0.01f;

        /// <summary>
        /// �^�C�}�[
        /// </summary>
        private TimerComponent m_timerComponent = null;

        public override void Attack()
        {
            if (m_currentLevel == 0) return;

            m_timerComponent.UpdateTime();

            if (IsCanAttack())
            {
                ResetCoolTime();
                GenerateCane();
            }
        }

        public override void Initialize()
        {
            m_timerComponent = new TimerComponent();
            m_timerComponent.ClearTime();
            m_timerComponent.SetTargetTime(m_generateRate);
        }

        /// <summary>
        /// �P�C���̐���
        /// </summary>
        private void GenerateCane()
        {
            if (!m_candyCaneObject || !m_playerSkill) return;

            var moveVec = m_playerSkill.GetPlayerMoveVec();
            var playerPos = m_playerSkill.GetPlayerPosition();

            var instnace = GameObject.Instantiate(m_candyCaneObject, playerPos + moveVec, Quaternion.identity);
            var candyCaneObject = instnace.GetComponent<CandyCaneObject>();
            candyCaneObject.Initialize(m_moveType, moveVec, m_moveType == MoveType.Drop ? m_upSpeed : 0.0f, m_fallSpeed, GetDamage(), GetKnockBackPower());
        }

        /// <summary>
        /// �N�[���^�C���̃��Z�b�g
        /// </summary>
        private void ResetCoolTime()
        {
            m_timerComponent.ClearTime();
        }

        /// <summary>
        /// �U���ł��邩�H
        /// </summary>
        /// <returns>
        /// <para>true �U���ł���</para>
        /// <para>false �U���ł��Ȃ�</para>
        /// </returns>
        private bool IsCanAttack()
        {
            return m_timerComponent.IsTime();
        }

        public int GetDamage()
        {
            return m_damage;
        }

        public float GetKnockBackPower()
        {
            return m_knockBackPower;
        }

        public override void StatusUp()
        {

        }
    }
}