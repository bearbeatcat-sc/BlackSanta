using UnityEngine;

namespace Assets.Scripts.Player.Skills
{

    public class WhipCollider : MonoBehaviour, IDamagable
    {
        /// <summary>
        /// �����蔻��̑��ݎ���
        /// </summary>
        [SerializeField]
        private float m_destroyTime = 0.2f;

        /// <summary>
        /// �^�C�}�[
        /// </summary>
        private TimerComponent m_timer = null;

        /// <summary>
        /// �_���[�W��
        /// </summary>
        private int m_damage = 0;

        /// <summary>
        /// �m�b�N�o�b�N��
        /// </summary>
        private float m_knockBackPower = 0.0f;


        /// <summary>
        /// �_���[�W�̎擾
        /// </summary>
        /// <returns>�_���[�W</returns>
        public int GetDamage()
        {
            return m_damage;
        }

        /// <summary>
        /// �m�b�N�o�b�N�͂̎擾
        /// </summary>
        /// <returns>�m�b�N�o�b�N��</returns>
        public float GetKnockBackPower()
        {
            return m_knockBackPower;
        }

        /// <summary>
        /// �_���[�W�̐ݒ�
        /// </summary>
        /// <param name="damage">�Z�b�g����_���[�W</param>
        public void SetDamage(int damage)
        {
            m_damage = damage;
        }

        /// <summary>
        /// �m�b�N�o�b�N�͂̐ݒ�
        /// </summary>
        /// <param name="power">�Z�b�g����m�b�N�o�b�N��</param>
        public void SetKnockBackPower(float power)
        {
            m_knockBackPower = power;
        }

        // Start is called before the first frame update
        void Start()
        {
            m_timer = new TimerComponent();
            m_timer.ClearTime();
            m_timer.SetTargetTime(m_destroyTime);
        }

        // Update is called once per frame
        void Update()
        {
            m_timer.UpdateTime();
            if (m_timer.IsTime())
            {
                Destroy(gameObject);
            }
        }
    }
}