using Assets.Scripts.Player.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    /// <summary>
    /// �X�L���A�C�R��
    /// </summary>
    [SerializeField]
    private Image m_image = null;

    /// <summary>
    /// �X�L���摜�̃��X�g
    /// </summary>
    [SerializeField]
    private Sprite[] m_skillSprites = null;

    /// <summary>
    /// �A�j���[�^�[
    /// </summary>
    [SerializeField]
    private Animator m_animator = null;

    /// <summary>
    /// �I��
    /// </summary>
    public void Select()
    {
        if (m_animator)
        {
            m_animator.Play("SkillIcon_Select");
            Debug.Log($"{nameof(SkillIcon)}.{nameof(Select)}");
        }
    }

    public void Enter()
    {
        if (m_animator)
        {
            m_animator.Play("SkillIcon_Enter");
        }
    }

    /// <summary>
    /// �I������
    /// </summary>
    public void Deselect()
    {
        if(m_animator)
        {
            m_animator.Play("SkillIcon_Idle");
            Debug.Log($"{nameof(SkillIcon)}.{nameof(Deselect)}");
        }
    }

    public void SetSkillIcon(PlayerSkill.SkillType skillType)
    {
        if (!m_image)
        {
            return;
        }

        var skillIndex = (int)skillType;
        if(skillIndex >= m_skillSprites.Length)
        {
            return;
        }

        var sprite = m_skillSprites[skillIndex];
        m_image.sprite = sprite;
    }
}
