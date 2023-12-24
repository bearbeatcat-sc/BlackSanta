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
