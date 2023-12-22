using UnityEngine;

[CreateAssetMenu(menuName = "BlackSatan/Create EnemyParamsTable", fileName = "EnemyParamsTable")]
public class EnemyParamsTable : ScriptableObject
{
    /// <summary>
    /// アセットのパス
    /// </summary>
    private static readonly string RESOURCE_PATH = "DataTables/Enemys/EnemyParamsTable";

    /// <summary>
    /// データテーブルのインスタンス
    /// </summary>
    private static EnemyParamsTable s_instance = null;

    /// <summary>
    /// インスタンスの取得
    /// </summary>
    public static EnemyParamsTable Instance
    {
        get
        {
            if (!s_instance)
            {
                var asset = Resources.Load(RESOURCE_PATH) as EnemyParamsTable;
                if (!asset)
                {
                    Debug.AssertFormat(false, "Missing Resource", RESOURCE_PATH);
                    asset = CreateInstance<EnemyParamsTable>();
                }

                s_instance = asset;
            }

            return s_instance;
        }
    }

    /// <summary>
    /// ロボットエネミーのパラメータ
    /// </summary>
    [Header("ロボットエネミーのパラメータ")]
    public RoboEnemyParams m_roboEnemyParams;
}

/// <summary>
/// ロボットエネミーのパラメータ
/// </summary>
[System.Serializable]
public class RoboEnemyParams
{
    /// <summary>
    /// 移動速度
    /// </summary>
    [Header("移動速度")]
    [Range(1.0f, 100.0f)]
    public float m_moveSpeed = 1.0f;

    /// <summary>
    /// 攻撃力
    /// </summary>
    [Header("攻撃力")]
    [Range(1, 100)]
    public int m_attack = 10;

    /// <summary>
    /// ノックバックの強さ
    /// </summary>
    [Header("ノックバックの強さ")]
    [Range(1.0f, 100.0f)]
    public float m_knockBackPower = 1.0f;

    /// <summary>
    /// 最大HP
    /// </summary>
    [Header("最大HP")]
    [Range(1, 100)]
    public int m_maxHP = 10;
}
