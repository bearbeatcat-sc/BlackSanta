using Assets.Scripts;
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
    /// アニメーター
    /// </summary>
    [SerializeField]
    private Animator m_animator = null;

    /// <summary>
    /// 初期の反転コンポーネント
    /// </summary>
    [SerializeField]
    private FlipComponent m_flipComponent = null;

    /// <summary>
    /// 対象となる位置
    /// </summary>
    private Vector2 m_targetPosition = Vector2.zero;

    /// <summary>
    /// 左向きか
    /// </summary>
    private bool m_isLeft = false;

    /// <summary>
    /// 移動方向のベクトルの取得
    /// </summary>
    /// <returns>移動方向のベクトル</returns>
    public Vector2 GetMoveVec()
    {
        return m_isLeft ? Vector2.left : Vector2.right;
    }
    
    /// <summary>
    /// ターゲット位置の設定
    /// </summary>
    /// <param name="targetPosition">ターゲット位置</param>
    public void SetTargetPosition(Vector2 targetPosition)
    {
        m_targetPosition = targetPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_flipComponent.SetInitScale(transform.localScale);

        var playerParamTable = PlayerParamTable.Instance;
        Debug.Assert(playerParamTable != null, "paramTable is null.");

        var playerMovementParams = playerParamTable.m_playerMovementParams;
        if (playerMovementParams == null)
        {
            return;
        }

        var moveSpeed = playerMovementParams.m_moveSpeed;
        var lerpamount = playerMovementParams.m_lerpAmount;

        m_MoveSpeed = moveSpeed;
        m_lerpAmount = lerpamount;
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
            m_flipComponent.Flip(true);
            m_isLeft = true;
        }
        else if (dKey.isPressed)
        {
            value.x += m_MoveSpeed;
            m_flipComponent.Flip(false);
            m_isLeft = false;
        }

        var currentPosition = m_targetPosition;
        currentPosition.x += value.x * Time.deltaTime;
        currentPosition.y += value.y * Time.deltaTime;

        m_targetPosition = currentPosition;
        PlayMoveAnimation(value.magnitude > 0.0f);
    }

    /// <summary>
    /// 移動アニメーションの再生
    /// </summary>
    private void PlayMoveAnimation(bool isMove)
    {
        if (!m_animator) return;

        m_animator.SetBool("IsMove", isMove);
    }
}
