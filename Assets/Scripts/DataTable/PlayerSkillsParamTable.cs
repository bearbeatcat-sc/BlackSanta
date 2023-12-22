using Assets.Scripts.Player.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlackSatan/Create PlayerSkillsParamTable", fileName = "PlayerSkillsParamTable")]
public class PlayerSkillsParamTable : ScriptableObject
{
    /// <summary>
    /// アセットのパス
    /// </summary>
    private static readonly string RESOURCE_PATH = "DataTables/Player/PlayerSkillsParamTable";

    /// <summary>
    /// データテーブルのインスタンス
    /// </summary>
    private static PlayerSkillsParamTable s_instance = null;

    /// <summary>
    /// インスタンスの取得
    /// </summary>
    public static PlayerSkillsParamTable Instance
    {
        get
        {
            if (!s_instance)
            {
                var asset = Resources.Load(RESOURCE_PATH) as PlayerSkillsParamTable;
                if (!asset)
                {
                    Debug.AssertFormat(false, "Missing Resource", RESOURCE_PATH);
                    asset = CreateInstance<PlayerSkillsParamTable>();
                }

                s_instance = asset;
            }

            return s_instance;
        }
    }

    /// <summary>
    /// 鞭スキルのパラメータ
    /// </summary>
    [Header("鞭スキルのパラメータ")]
    public WhipSkillParams m_whipSkillParams;

    /// <summary>
    /// ベルスキルのパラメータ
    /// </summary>
    [Header("ベルスキルのパラメータ")]
    public BellSkillParams m_bellSkillParams;

    /// <summary>
    /// キャンディケインスキルのパラメータ
    /// </summary>
    [Header("キャンディケインスキルのパラメータ")]
    public CandyCaneSkillParams m_candyCaneSkillParams;

    /// <summary>
    /// 雪玉スキルのパラメータ
    /// </summary>
    [Header("雪玉スキルのパラメータ")]
    public SnowBallSkillParams m_snowBallSkillParams;
}

/// <summary>
/// ベースのスキルのレベル別パラメータ
/// </summary>
[System.Serializable]
public class SkillBaseLevelParam
{
    /// <summary>
    /// ノックバックの強さ
    /// </summary>
    [Header("ノックバックの強さ")]
    [Range(0f, 100f)]
    public float m_knockBackPower = 1.0f;

    /// <summary>
    /// ダメージ
    /// </summary>
    [Header("ダメージ")]
    [Range(1, 100)]
    public int m_Damage = 1;
}

/// <summary>
/// 鞭スキルのパラメータ
/// </summary>
[System.Serializable]
public class WhipSkillParams
{
    /// <summary>
    /// 鞭スキルのレベルパラメータリスト
    /// </summary>
    [Header("鞭スキルのレベル別パラメータリスト")]
    public WhipSkillLevelParam[] m_whipSkillLevelParams;
}

/// <summary>
/// 鞭のレベルごとの強さ
/// </summary>
[System.Serializable]
public class WhipSkillLevelParam : SkillBaseLevelParam
{
    /// <summary>
    /// 鞭の振るレート
    /// </summary>
    [Header("鞭の振るレート")]
    [Range(0f,100f)]
    public float m_whipRate = 1.0f;
}

/// <summary>
/// ベルスキルのパラメータ
/// </summary>
[System.Serializable]
public class BellSkillParams
{
    /// <summary>
    /// ベルスキルのレベルパラメータリスト
    /// </summary>
    [Header("ベルスキルのレベル別パラメータリスト")]
    public BellSkillLevelParam[] m_bellSkillLevelParams;
}

/// <summary>
/// ベルのレベルごとの強さ
/// </summary>
[System.Serializable]
public class BellSkillLevelParam : SkillBaseLevelParam
{
    /// <summary>
    /// 回転速度
    /// </summary>
    [Header("回転速度")]
    [Range(0f, 100f)]
    public float m_rotateSpeed = 30.0f;

    /// <summary>
    /// 回転範囲
    /// </summary>
    [Header("回転範囲")]
    [Range(0f, 100f)]
    public float m_range = 3.0f;
}

/// <summary>
/// キャンディケインスキルのパラメータ
/// </summary>
[System.Serializable]
public class CandyCaneSkillParams
{
    /// <summary>
    /// キャンディケインのレベルパラメータリスト
    /// </summary>
    [Header("キャンディケインのレベル別のパラメータリスト")]
    public CandyCaneSkillLevelParam[] m_bellSkillLevelParams;
}

/// <summary>
/// キャンディケインのレベルごとの強さ
/// </summary>
[System.Serializable]
public class CandyCaneSkillLevelParam : SkillBaseLevelParam
{
    /// <summary>
    /// 移動の種類
    /// </summary>
    [Header("ケインの移動の種類")]
    public CandyCaneSkill.MoveType m_moveType;

    /// <summary>
    /// 生成レート
    /// </summary>
    [Header("生成レート")]
    [Range(0f, 100f)]
    public float m_generateRate = 1.0f;

    /// <summary>
    /// 投げる時の上昇速度
    /// </summary>
    [Header("投げる時の上昇速度")]
    [Range(0f, 100f)]
    public float m_upSpeed = 6.0f;

    /// <summary>
    /// 落下速度
    /// </summary>
    [Header("落下速度")]
    [Range(0f, 100f)]
    public float m_fallSpeed = 0.01f;
}

/// <summary>
/// 雪玉スキルのパラメータ
/// </summary>
[System.Serializable]
public class SnowBallSkillParams
{
    /// <summary>
    /// 雪玉スキルのレベルパラメータリスト
    /// </summary>
    [Header("雪玉スキルのレベル別のパラメータリスト")]
    public SnowBallSkillLevelParam[] m_snowBallSkillLevelParams;
}

/// <summary>
/// 雪玉のレベルごとの強さ
/// </summary>
[System.Serializable]
public class SnowBallSkillLevelParam : SkillBaseLevelParam
{
    /// <summary>
    /// 移動の種類
    /// </summary>
    [Header("雪玉の移動の種類")]
    public SnowBallSkill.MoveType m_moveType;

    /// <summary>
    /// 生成レート
    /// </summary>
    [Header("生成レート")]
    [Range(0f, 100f)]
    public float m_generateRate = 1.0f;

    /// <summary>
    /// エリアの範囲
    /// </summary>
    [Header("水たまりエリアの範囲")]
    [Range(0f, 100f)]
    public float m_areaScale = 1.0f;

    /// <summary>
    /// エリアの生存時間
    /// </summary>
    [Header("エリアの生存時間")]
    [Range(0f, 100f)]
    public float m_areaDestoryTime = 10.0f;

    /// <summary>
    /// 投げる時の上昇速度
    /// </summary>
    [Header("投げる時の上昇速度")]
    [Range(0f, 100f)]
    public float m_upSpeed = 6.0f;

    /// <summary>
    /// 落下速度
    /// </summary>
    [Header("落下速度")]
    [Range(0f, 100f)]
    public float m_fallSpeed = 0.01f;
}