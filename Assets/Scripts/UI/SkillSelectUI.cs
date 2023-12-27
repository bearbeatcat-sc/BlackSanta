using Assets.Scripts.Player.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectUI : MonoBehaviour
{
    /// <summary>
    /// �X�L�����X�gUI
    /// </summary>
    [SerializeField]
    private SkillIcon[] m_skillIcons = null;

    /// <summary>
    /// �A�j���[�^�[
    /// </summary>
    [SerializeField]
    private Animator m_animator = null;

    /// <summary>
    /// ���ݑI�����Ă���X�L��
    /// </summary>
    private int m_currentSelectSkill = -1;

    /// <summary>
    /// UI�̕\��
    /// </summary>
    public void Show()
    {
        if (m_animator)
        {
            m_animator.Play("SkillSelect_FadeIn");
        }
    }

    /// <summary>
    /// UI�̔�\��
    /// </summary>
    public void Hide()
    {
        if (m_animator)
        {
            m_animator.Play("SkillSelect_FadeOut");
        }
    }

    /// <summary>
    /// �X�L���t�h�̐ݒ�
    /// </summary>
    /// <param name="skillTypes">�X�L���̎�ރ��X�g</param>
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
    /// �X�L���̑I��
    /// </summary>
    /// <param name="index">�I�������C���f�b�N�X</param>
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
    /// �X�L���I���̌��菈��
    /// </summary>
    /// <param name="index">�I�������C���f�b�N�X</param>
    public void SelectEnterSkill(int index)
    {
        if (index >= 0 && index < m_skillIcons.Length)
        {
            var skillIcon = m_skillIcons[index];
            skillIcon.Enter();
        }
    }
}
