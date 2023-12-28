using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    /// <summary>
    /// ��������G�l�~�[�̃��X�g
    /// </summary>
    [SerializeField]
    private GameObject[] m_generateEnemys = new GameObject[0];

    /// <summary>
    /// �������[�g
    /// </summary>
    [SerializeField]
    private float m_generateRate = 1.0f;

    /// <summary>
    /// �����͈�
    /// </summary>
    [SerializeField]
    private float m_generateRange = 1.0f;

    /// <summary>
    /// ��������G�l�~�[�̎��
    /// </summary>
    private BaseEnemy.EnemyType m_generateEnemyType = BaseEnemy.EnemyType.Robot;

    /// <summary>
    /// �����^�C�}�[
    /// </summary>
    private TimerComponent m_timer = null;

    private void Awake()
    {
        m_timer = new TimerComponent();
        m_timer.ClearTime();
        m_timer.SetTargetTime(m_generateRate);
    }

    private void Update()
    {
        m_timer.UpdateTime();
        if (m_timer.IsTime())
        {
            m_timer.ClearTime();
            Generate();
        }
    }

    private void Generate()
    {
        if((int)m_generateEnemyType >= m_generateEnemys.Length)
        {
            return;
        }

        var spawanerPos = transform.position;
        float x = Random.Range(-m_generateRate, m_generateRate);
        float y = Random.Range(-m_generateRate, m_generateRate);

        spawanerPos.x += x;
        spawanerPos.y += y;

        GameObject.Instantiate(m_generateEnemys[(int)m_generateEnemyType], spawanerPos, Quaternion.identity);
    }
}
