using Assets.Scripts.Player.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectUI : MonoBehaviour
{
    /// <summary>
    /// スキルリストUI
    /// </summary>
    [SerializeField]
    private SkillIcon[] m_skillIcons = null;

    /// <summary>
    /// アニメーター
    /// </summary>
    [SerializeField]
    private Animator m_animator = null;

    /// <summary>
    /// 現在選択しているスキル
    /// </summary>
    private int m_currentSelectSkill = -1;

    /// <summary>
    /// UIの表示
    /// </summary>
    public void Show()
    {
        if (m_animator)
        {
            m_animator.Play("SkillSelect_FadeIn");
        }
    }

    /// <summary>
    /// UIの非表示
    /// </summary>
    public void Hide()
    {
        if (m_animator)
        {
            m_animator.Play("SkillSelect_FadeOut");
        }
    }

    /// <summary>
    /// スキルＵＩの設定
    /// </summary>
    /// <param name="skillTypes">スキルの種類リスト</param>
    public void SetSkillUI(PlayerSkill.SkillType[] skillTypes)
    {
        if (m_skillIcons == null) return;
       
        for(int i = 0; i < m_skillIcons.Length; i++)
        {
            var skillIcon = m_skillIcons[i];
            if(!skillIcon) continue;

            if(i >= skillTypes.Length) continue;

            var skillType = skillTypes[i];;
            skillIcon.SetSkillIcon(skillType);
        }
    }

    /// <summary>
    /// スキルの選択
    /// </summary>
    /// <param name="index">選択したインデックス</param>
    public void SelectSkill(int index)
    {
        if(m_currentSelectSkill == index) return;

        if(index >= 0 && index < m_skillIcons.Length ) 
        {
            var skillIcon = m_skillIcons[index];
            skillIcon.Select();
        }

        var previousIndex = m_currentSelectSkill;
        if (previousIndex >= 0 && previousIndex < m_skillIcons.Length)
        {
            var skillIcon = m_skillIcons[previousIndex];
            skillIcon.Deselect();
        }

        m_currentSelectSkill = index;
    }

    /// <summary>
    /// スキル選択の決定処理
    /// </summary>
    /// <param name="index">選択したインデックス</param>
    public void SelectEnterSkill(int index)
    {
        if (index >= 0 && index < m_skillIcons.Length)
        {
            var skillIcon = m_skillIcons[index];
            skillIcon.Enter();
        }
    }
}
