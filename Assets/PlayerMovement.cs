using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    private float m_MoveSpeed = 10.0f;

    /// <summary>
    /// 補完係数
    /// </summary>
    [SerializeField]
    private float m_lerpAmount = 0.7f;

    /// <summary>
    /// 前フレームのマウス位置
    /// </summary>
    private Vector2 m_previousMousePosition = Vector2.zero;

    /// <summary>
    /// 対象となる位置
    /// </summary>
    private Vector2 m_targetPosition = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        transform.position = Vector3.Lerp(transform.position, new Vector3(m_targetPosition.x, m_targetPosition.y, 0.0f), m_lerpAmount);
    }

    private void Move()
    {
        var keyBoard = Keyboard.current;

        if (keyBoard == null) return;

        var value = Vector2.zero;

        var wKey = keyBoard.wKey;
        var aKey = keyBoard.aKey;
        var sKey = keyBoard.sKey;
        var dKey = keyBoard.dKey;

        if(wKey.isPressed)
        {
            value.y += m_MoveSpeed;
        }
        if(aKey.isPressed)
        {
            value.x -= m_MoveSpeed;
        }
        if(sKey.isPressed)
        {
            value.y -= m_MoveSpeed;
        }
        if (dKey.isPressed)
        {
            value.x += m_MoveSpeed;
        }

        var currentPosition = m_targetPosition;
        currentPosition.x += value.x * Time.deltaTime;
        currentPosition.y += value.y * Time.deltaTime;

        m_targetPosition = currentPosition;
    }
}
