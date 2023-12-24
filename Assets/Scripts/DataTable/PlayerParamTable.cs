using UnityEngine;

[CreateAssetMenu(menuName = "BlackSatan/Create PlayerParamTable", fileName = "PlayerParamTable")]

public class PlayerParamTable : ScriptableObject
{
    /// <summary>
    /// アセットのパス
    /// </summary>
    private static readonly string RESOURCE_PATH = "DataTables/Player/PlayerParamTable";

    /// <summary>
    /// データテーブルのインスタンス
    /// </summary>
    private static PlayerParamTable s_instance = null;

    /// <summary>
    /// インスタンスの取得
    /// </summary>
    public static PlayerParamTable Instance
    {
        get
        {
            if (!s_instance)
            {
                var asset = Resources.Load(RESOURCE_PATH) as PlayerParamTable;
                if (!asset)
                {
                    Debug.AssertFormat(false, "Missing Resource", RESOURCE_PATH);
                    asset = CreateInstance<PlayerParamTable>();
                }

                s_instance = asset;
            }

            return s_instance;
        }
    }

    /// <summary>
    /// プレイヤーカメラパラメータ
    /// </summary>
    [Header("プレイヤーカメラパラメータ")]
    public PlayerCameraParams m_playerCameraParams;

    /// <summary>
    /// プレイヤー移動のパラメータ
    /// </summary>
    [Header("プレイヤー移動パラメータ")]
    public PlayerMovementParams m_playerMovementParams;
}

/// <summary>
/// プレイヤーカメラのパラメータ
/// </summary>
[System.Serializable]
public class PlayerCameraParams
{
    /// <summary>
    /// 補完係数
    /// </summary>
    [Header("補完係数")]
    [Range(0.0f, 1.0f)]
    public float m_lerpAmount = 0.7f;
}

/// <summary>
/// プレイヤー移動のパラメータ
/// </summary>
[System.Serializable]
public class PlayerMovementParams
{
    /// <summary>
    /// 移動速度
    /// </summary>
    [Header("移動速度")]
    [Range(1.0f, 100.0f)]
    public float m_moveSpeed = 10.0f;

    /// <summary>
    /// 補完係数
    /// </summary>
    [Header("補完係数")]
    [Range(1.0f, 100.0f)]
    public float m_lerpAmount = 0.7f;
}
