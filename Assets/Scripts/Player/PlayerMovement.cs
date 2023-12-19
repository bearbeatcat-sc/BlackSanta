using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// �ړ����x
    /// </summary>
    [SerializeField]
    private float m_MoveSpeed = 10.0f;

    /// <summary>
    /// �⊮�W��
    /// </summary>
    [SerializeField]
    private float m_lerpAmount = 0.7f;

    /// <summary>
    /// �A�j���[�^�[
    /// </summary>
    [SerializeField]
    private Animator m_animator = null;

    /// <summary>
    /// �O�t���[���̃}�E�X�ʒu
    /// </summary>
    private Vector2 m_previousMousePosition = Vector2.zero;

    /// <summary>
    /// �ΏۂƂȂ�ʒu
    /// </summary>
    private Vector2 m_targetPosition = Vector2.zero;

    /// <summary>
    /// �����̃X�P�[��
    /// </summary>
    private Vector3 m_initScale = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_initScale = transform.localScale;
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
        else if (sKey.isPressed)
        {
            value.y -= m_MoveSpeed;
        }

        if (aKey.isPressed)
        {
            value.x -= m_MoveSpeed;
            Flip(true);
        }
        else if (dKey.isPressed)
        {
            value.x += m_MoveSpeed;
            Flip(false);
        }

        var currentPosition = m_targetPosition;
        currentPosition.x += value.x * Time.deltaTime;
        currentPosition.y += value.y * Time.deltaTime;

        m_targetPosition = currentPosition;
        PlayMoveAnimation(value.magnitude > 0.0f);
    }

    /// <summary>
    /// ���]
    /// </summary>
    private void Flip(bool isLeft)
    {
        var scale = transform.localScale;
        scale.x = isLeft ? m_initScale.x * -1 : m_initScale.x;
        transform.localScale = scale;
    }

    /// <summary>
    /// �ړ��A�j���[�V�����̍Đ�
    /// </summary>
    private void PlayMoveAnimation(bool isMove)
    {
        if (!m_animator) return;
        m_animator.SetBool("IsMove", isMove);
    }
}
