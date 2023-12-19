using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    /// <summary>
    /// �X�L���̍ő吔
    /// </summary>
    [SerializeField]
    private int m_maxSkilCount = 3;

    /// <summary>
    /// �X�L�����X�g
    /// </summary>
    private Skill[] m_skill = null;

    /// <summary>
    /// �擾���Ă���X�L����
    /// </summary>
    private int m_skillCount = 0;

    /// <summary>
    /// �X�L���̒ǉ�
    /// </summary>
    /// <param name="skill">�ǉ�����X�L��</param>
    public void AddSkill(Skill skill)
    {
        if(m_skillCount >= m_maxSkilCount - 1)
        {            
            return;
        }

        var levelUpSkill = GetCanLevelUpSkill(skill);

        if(levelUpSkill)
        {
            levelUpSkill.LevelUp();
            return;
        }

        m_skill[m_skillCount] = skill;
        m_skillCount++;
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_skill = new Skill[m_maxSkilCount];
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    /// <summary>
    /// ���x���A�b�v�ł���X�L���̎擾
    /// </summary>
    /// <param name="setSkill">�ݒ肷��X�L��</param>
    /// <returns>���x���A�b�v�ł���X�L��</returns>
    private Skill GetCanLevelUpSkill(Skill setSkill)
    {
        for(int i = 0; i < m_skill.Length; i++)
        {
            var skill = m_skill[i];
           
            if(skill.GetType() == setSkill.GetType())
            {
                return skill;
            }
        }

        return null;
    }

    /// <summary>
    /// �X�L���̏�����
    /// </summary>
    private void InitializeSkill()
    {
        if (m_skill == null) return;

        for(int i = 0; m_skill.Length > 0; i++)
        {
            m_skill[i] = null;
        }
    }
}
