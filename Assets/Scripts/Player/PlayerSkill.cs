using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    /// <summary>
    /// スキルの最大数
    /// </summary>
    [SerializeField]
    private int m_maxSkilCount = 3;

    /// <summary>
    /// スキルリスト
    /// </summary>
    private Skill[] m_skill = null;

    /// <summary>
    /// 取得しているスキル数
    /// </summary>
    private int m_skillCount = 0;

    /// <summary>
    /// スキルの追加
    /// </summary>
    /// <param name="skill">追加するスキル</param>
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
    /// レベルアップできるスキルの取得
    /// </summary>
    /// <param name="setSkill">設定するスキル</param>
    /// <returns>レベルアップできるスキル</returns>
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
    /// スキルの初期化
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
