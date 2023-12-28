using Assets.Scripts;
using Assets.Scripts.Player.Skills;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    /// <summary>
    /// �G�l�~�[�̎��
    /// </summary>
    public enum EnemyType
    {
        Robot,
    }

    /// <summary>
    /// �ړ����x
    /// </summary>
    [SerializeField]
    protected float m_moveSpeed = 1.0f;

    /// <summary>
    /// �ő�HP
    /// </summary>
    [SerializeField]
    protected int m_maxHp = 10;

    /// <summary>
    /// �m�b�N�o�b�N
    /// </summary>
    [SerializeField]
    protected KnockBackComponent knockBack = null;

    /// <summary>
    /// ���݂�HP
    /// </summary>
    private int m_hp = 10;

    /// <summary>
    /// �U����
    /// </summary>
    protected int m_attack = 10;

    /// <summary>
    /// �^����m�b�N�o�b�N�̋���
    /// </summary>
    protected float m_knockBackPower = 1.0f;

    /// <summary>
    /// �������v���C���[�̏��
    /// </summary>
    private GameObject m_findPlayer = null;

    /// <summary>
    /// ���S����
    /// </summary>
    public abstract void Death();

    /// <summary>
    /// �ړ�����
    /// </summary>
    public abstract void Move();

    /// <summary>
    /// ������
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// �U������
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// �_���[�W����
    /// </summary>
    /// <param name="attack">�U����</param>
    public void Damage(int attack)
    {
        m_hp -= attack;
        Debug.Log($"{nameof(BaseEnemy)}.{nameof(Damage)} HP: " + m_hp);

        if(m_hp <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// �v���C���[�̈ʒu���擾
    /// </summary>
    /// <returns></returns>
    protected Vector2 GetPlayerPosition()
    {
        if (!m_findPlayer) return Vector2.zero;

        return m_findPlayer.transform.position;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
        m_findPlayer = null;
        m_hp = m_maxHp;
    }

    // Update is called once per frame
    private void Update()
    {
        if(m_findPlayer == null)
        {
            m_findPlayer = GameObject.FindGameObjectWithTag("Player");
        }

        Move();
        Attack();
    }

    /// <summary>
    /// �m�b�N�o�b�N����
    /// </summary>
    /// <param name="vec">�x�N�g��</param>
    /// <param name="power">��</param>
    private void KnockBack(Vector2 vec, float power)
    {
        if (!knockBack) return;

        knockBack.SetKnockBack(vec, power);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            var playerHP = collision.gameObject.GetComponent<PlayerHP>();
            if(!playerHP) return;

            var currentPos = transform.position;
            var playerPos = collision.gameObject.transform.position;
            var vec = playerPos - currentPos;
            vec.z = 0;
            vec.Normalize();

            playerHP.Damage(m_attack, vec, 10.0f);
            return;
        }

        // �U������
        if(collision.tag == "Attack")
        {
            var gameObject = collision.gameObject;
            var skill = gameObject.GetComponent<IDamagable>();

            if (skill == null) return;
            var damege = skill.GetDamage();
            var knockBackPower = skill.GetKnockBackPower();

            Damage(damege);
            var currentPos = transform.position;
            var skillPos = gameObject.transform.position;
            var vec = currentPos - skillPos;
            vec.z = 0;
            vec.Normalize();
            KnockBack(vec, knockBackPower);
        }
    }
}
