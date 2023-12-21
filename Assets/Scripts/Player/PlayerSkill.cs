using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    /// <summary>
    ///  スキルタイプ
    /// </summary>
    public enum SkillType
    {
        Bell,
        CandyCane,
    }

    /// <summary>
    /// メインスキル
    /// </summary>
    [SerializeField]
    private Skill m_mainSkill = null;

    /// <summary>
    /// プレイヤー移動コンポーネント
    /// </summary>
    [SerializeField]
    private PlayerMovement m_playerMovement = null;

    /// <summary>
    /// スキルリスト
    /// </summary>
    [SerializeField]
    private Skill[] m_skill = null;

    /// <summary>
    /// 移動方向のベクトルの取得
    /// </summary>
    /// <returns>移動方向のベクトル</returns>
    public Vector2 GetPlayerMoveVec()
    {
        if (!m_playerMovement) return Vector2.zero;

        return m_playerMovement.GetMoveVec();
    }

    /// <summary>
    /// プレイヤーの位置の取得
    /// </summary>
    /// <returns>プレイヤーの位置</returns>
    public Vector2 GetPlayerPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// スキルの追加
    /// </summary>
    /// <param name="skillType">スキルの種類</param>
    public void AddSkill(SkillType skillType)
    {
        var skillIndex = (int)skillType;
        var skill = m_skill[skillIndex];

        skill.Initialize();
        skill.LevelUp();
        skill.SetPlayerSkill(this);
    }

    private void Start()
    {
        m_mainSkill.SetPlayerSkill(this);
        m_mainSkill.Initialize();
        m_mainSkill.LevelUp();

        AddSkill(SkillType.Bell);
        AddSkill(SkillType.CandyCane);
    }

    // Update is called once per frame
    private void Update()
    {
        m_mainSkill.Attack();

        for(int i = 0; i < m_skill.Length; i++) 
        { 
            var skill = m_skill[i];
            if (!skill) continue;

            skill.Attack();
        }
    }

}
