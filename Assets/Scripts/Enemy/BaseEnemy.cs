using Assets.Scripts;
using Assets.Scripts.Player.Skills;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    /// <summary>
    /// エネミーの種類
    /// </summary>
    public enum EnemyType
    {
        Robot,
    }

    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    protected float m_moveSpeed = 1.0f;

    /// <summary>
    /// 最大HP
    /// </summary>
    [SerializeField]
    protected int m_maxHp = 10;

    /// <summary>
    /// ノックバック
    /// </summary>
    [SerializeField]
    protected KnockBackComponent knockBack = null;

    /// <summary>
    /// 現在のHP
    /// </summary>
    private int m_hp = 10;

    /// <summary>
    /// 攻撃力
    /// </summary>
    protected int m_attack = 10;

    /// <summary>
    /// 与えるノックバックの強さ
    /// </summary>
    protected float m_knockBackPower = 1.0f;

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
        Debug.Log($"{nameof(BaseEnemy)}.{nameof(Damage)} HP: " + m_hp);

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
        m_hp = m_maxHp;
    }

    // Update is called once per frame
    private void Update()
    {
        if(m_findPlayer == null)
        {
            m_findPlayer = GameObject.FindGameObjectWithTag("Player");
        }

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
        if(collision.tag == "Player")
        {
            var playerHP = collision.gameObject.GetComponent<PlayerHP>();
            if(!playerHP) return;

            var currentPos = transform.position;
            var playerPos = collision.gameObject.transform.position;
            var vec = playerPos - currentPos;
            vec.z = 0;
            vec.Normalize();

            playerHP.Damage(m_attack, vec, 10.0f);
            return;
        }

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
