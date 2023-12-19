using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    /// <summary>
    /// �ړ����x
    /// </summary>
    [SerializeField]
    protected float m_moveSpeed = 1.0f;

    /// <summary>
    /// �ő�HP
    /// </summary>
    [SerializeField]
    private int m_maxHp = 10;

    /// <summary>
    /// ���݂�HP
    /// </summary>
    private int m_hp = 10;

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
        m_findPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Attack();
    }
}
