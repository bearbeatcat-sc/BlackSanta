using Assets.Scripts;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    protected float m_moveSpeed = 1.0f;

    /// <summary>
    /// 最大HP
    /// </summary>
    [SerializeField]
    private int m_maxHp = 10;

    /// <summary>
    /// ノックバック
    /// </summary>
    [SerializeField]
    private KnockBackComponent knockBack = null;

    /// <summary>
    /// 現在のHP
    /// </summary>
    private int m_hp = 10;

    /// <summary>
    /// 見つけたプレイヤーの情報
    /// </summary>
    private GameObject m_findPlayer = null;

    /// <summary>
    /// 死亡処理
    /// </summary>
    public abstract void Death();

    /// <summary>
    /// 移動処理
    /// </summary>
    public abstract void Move();

    /// <summary>
    /// 初期化
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// 攻撃処理
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="attack">攻撃力</param>
    public void Damage(int attack)
    {
        m_hp -= attack;

        if(m_hp <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// プレイヤーの位置を取得
    /// </summary>
    /// <returns></returns>
    protected Vector2 GetPlayerPosition()
    {
        if (!m_findPlayer) return Vector2.zero;

        return m_findPlayer.transform.position;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
        m_findPlayer = null;
        m_findPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Attack();
    }

    /// <summary>
    /// ノックバック処理
    /// </summary>
    /// <param name="vec">ベクトル</param>
    /// <param name="power">力</param>
    private void KnockBack(Vector2 vec, float power)
    {
        if (!knockBack) return;

        knockBack.SetKnockBack(vec, power);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 攻撃判定
        if(collision.tag == "Attack")
        {
            var gameObject = collision.gameObject;
            var skill = gameObject.GetComponent<IDamagable>();

            if (skill == null) return;
            var damege = skill.GetDamage();
            var knockBackPower = skill.GetKnockBackPower();

            Damage(damege);
            var currentPos = transform.position;
            var skillPos = gameObject.transform.position;
            var vec = currentPos - skillPos;
            vec.z = 0;
            vec.Normalize();
            KnockBack(vec, knockBackPower);
        }
    }
}
