using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    /// <summary>
    /// ゲームシステム
    /// </summary>
    [SerializeField]
    private GameSystem m_gameSystem = null;

    /// <summary>
    /// クールタイム
    /// </summary>
    private TimerComponent m_timer = null;

    private void Awake()
    {
        m_timer = new TimerComponent();
        m_timer.SetTargetTime(6.0f);
    }

    private void Update()
    {
        m_timer.UpdateTime();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (!m_gameSystem) return;
        if (collision.tag != "Player") return;
        if (!m_timer.IsTime()) return;

        var playerExpSystem = collision.GetComponent<PlayerExpSystem>();
        if (!playerExpSystem) return;

        if (!playerExpSystem.IsGetSkill()) return;

        m_gameSystem.StartSkillSelect();
        m_timer.ClearTime();
        playerExpSystem.StatuUp();
    }
}
