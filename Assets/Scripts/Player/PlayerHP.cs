using Assets.Scripts;
using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    /// <summary>
    /// �m�b�N�o�b�N
    /// </summary>
    [SerializeField]
    private PlayerKnockBackComponent m_knockBackComponent = null;

    /// <summary>
    /// �ő�HP
    /// </summary>
    private int m_maxHP = 200;

    /// <summary>
    /// HP
    /// </summary>
    private int m_hp = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_hp = m_maxHP;
    }

    /// <summary>
    /// �_���[�W
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="knockBackPower">�m�b�N�o�b�N�̕���</param>
    /// <param name="knockBackVec">�m�b�N�o�b�N��</param>
    public void Damage(int damage, Vector2 knockBackVec, float knockBackPower)
    {
        m_hp -= damage;
        Debug.Log($"{nameof(PlayerHP)}.{nameof(Damage)} Damage : {damage}");

        if(m_knockBackComponent)
        {
            m_knockBackComponent.SetKnockBack(knockBackVec, knockBackPower);
        }
    }


}
