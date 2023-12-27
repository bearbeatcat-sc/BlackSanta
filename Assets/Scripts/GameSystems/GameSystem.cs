using Assets.Scripts.GameSystems;
using Assets.Scripts.Player.Skills;
using System;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameSystem : MonoBehaviour
{
    /// <summary>
    /// ゲームの状態
    /// </summary>
    public enum GameStateType
    {
        Wait,
        Play,
        GameOver,
        Max,
    }   

    /// <summary>
    /// プレイヤー
    /// </summary>
    [SerializeField]
    private GameObject m_player = null;

    /// <summary>
    /// プレイヤーUI
    /// </summary>
    [SerializeField]
    private PlayerUI m_playerUI = null;  

    /// <summary>
    /// スキル選択システム
    /// </summary>
    [SerializeField]
    private SkillSelectSystem m_skillSelectSystem = null;

    /// <summary>
    /// プレイヤーカメラ
    /// </summary>
    [SerializeField]
    private PlayerCamera m_playerCamera = null;

    /// <summary>
    /// プレイヤー経験値システム
    /// </summary>
    private PlayerExpSystem m_playerExpSystem = null;

    /// <summary>
    /// プレイヤースキルシステム
    /// </summary>
    private PlayerSkill m_playerSkillSystem = null;

    /// <summary>
    /// ゲームステートマシン
    /// </summary>
    private GameStateMachine<GameSystem> m_gameStateMachine = null;

    // Start is called before the first frame update
    void Start()
    {
        m_gameStateMachine = new GameStateMachine<GameSystem>((int)GameStateType.Max);
        var waitGameState = new WaitGameState(this);
        var gamePlayState = new GamePlayState(this);
        var gameOverState = new GameOverState(this);

        m_gameStateMachine.AddGameState((int)GameStateType.Wait, waitGameState);
        m_gameStateMachine.AddGameState((int)GameStateType.Play, gamePlayState);
        m_gameStateMachine.AddGameState((int)GameStateType.GameOver, gameOverState);
        m_gameStateMachine.ChangeState((int)GameStateType.Play);
    }

    // Update is called once per frame
    void Update()
    {
        m_gameStateMachine.Update();
    }

    /// <summary>
    /// スキル選択の開始
    /// </summary>
    public void StartSkillSelect()
    {
        if(m_gameStateMachine.GetCurrentStateIndex() == (int)GameStateType.Play)
        {
            var state = m_gameStateMachine.GetCurrentState();
            if(state is GamePlayState gamePlayState)
            {
                gamePlayState.StartSkillSelect();
            }
        }
    }

    /// <summary>
    /// ゲーム前待機処理ステート
    /// </summary>
    public class WaitGameState : GameState<GameSystem>
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WaitGameState(GameSystem gameSystem)
            :base(gameSystem)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }

    /// <summary>
    /// ゲーム中ステート
    /// </summary>
    public class GamePlayState : GameState<GameSystem>
    {
        /// <summary>
        /// ゲームプレイ中のステート
        /// </summary>
        public enum GamePlayStateType
        {
            Start,
            Playing,
            SkillSelect,
            Max,
        }

        /// <summary>
        /// ゲームステートマシン
        /// </summary>
        private GameStateMachine<GamePlayState> m_gameStateMachine = null;


        /// <summary>
        /// プレイヤーのインスタンス
        /// </summary>
        private GameObject m_playerInstance = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GamePlayState(GameSystem context)
            :base(context)
        {
            m_gameStateMachine = new GameStateMachine<GamePlayState>((int)GamePlayStateType.Max);

            var startState = new StartState(this);
            var playingState = new PlayingState(this);
            var skillSelectState = new SkillSelect(this);

            m_gameStateMachine.AddGameState((int)GamePlayStateType.Start, startState);
            m_gameStateMachine.AddGameState((int)GamePlayStateType.Playing, playingState);
            m_gameStateMachine.AddGameState((int)GamePlayStateType.SkillSelect, skillSelectState);
        }

        /// <summary>
        /// スキル選択の開始
        /// </summary>
        public void StartSkillSelect()
        {
            m_gameStateMachine.ChangeState((int)GamePlayStateType.SkillSelect);
        }

        public override void Enter()
        {
            m_gameStateMachine.ChangeState((int)GamePlayStateType.Start);
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            m_gameStateMachine.Update();
        }

        private class StartState : GameState<GamePlayState>
        {
            public StartState(GamePlayState context)
                : base(context)
            {

            }

            public override void Enter()
            {
                var playerObject = m_context.m_context.m_player;

                if (!playerObject) return;

                var playerInstance = GameObject.Instantiate(playerObject);
                m_context.m_playerInstance = playerInstance;

                var playerExpSystem =  playerInstance.GetComponent<PlayerExpSystem>();
                if (!playerExpSystem) return;

                var playerSkillSystem = playerInstance.GetComponent<PlayerSkill>();
                if(!playerSkillSystem) return;

                m_context.m_context.m_playerExpSystem = playerExpSystem;
                m_context.m_context.m_playerSkillSystem = playerSkillSystem; 
                m_context.m_context.m_playerCamera.SetPlayer(playerInstance);
                m_context.m_context.m_skillSelectSystem.SetPlayerSkill(playerSkillSystem);
                playerExpSystem.Initialize(m_context.m_context.m_playerUI);
            }

            public override void Exit()
            {
            }

            public override void Update()
            {
                m_context.m_gameStateMachine.ChangeState((int)GamePlayStateType.Playing);
            }
        }

        /// <summary>
        /// プレイ中ステート
        /// </summary>
        private class PlayingState : GameState<GamePlayState>
        {
            public PlayingState(GamePlayState context)
                :base(context)
            {
            }

            public override void Enter()
            {
                
            }

            public override void Exit()
            {
            }

            public override void Update()
            {
            }
        }

        /// <summary>
        /// スキル選択ステート
        /// </summary>
        private class SkillSelect : GameState<GamePlayState>
        {
            /// <summary>
            /// コンストラクタ
            /// </summary>
            public SkillSelect(GamePlayState context)
                :base(context)
            {
            }

            public override void Enter()
            {
                m_context.m_context.m_skillSelectSystem.StartSelectSkill();
            }

            public override void Exit()
            {
                m_context.m_context.m_skillSelectSystem.EndSelectSkill();
            }

            public override void Update()
            {
                if (m_context.m_context.m_skillSelectSystem.UpdateSkillSelect())
                {
                    m_context.m_gameStateMachine.ChangeState((int)GamePlayStateType.Playing);
                }
            }
        }
    }

    /// <summary>
    /// ゲームオーバーステート
    /// </summary>
    public class GameOverState : GameState<GameSystem>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameOverState(GameSystem context)
            :base(context)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }

}

/// <summary>
/// ゲームステートの操作クラス
/// </summary>
public class GameStateMachine<Type>
{
    /// <summary>
    /// ゲームステートリスト
    /// </summary>
    private GameState<Type>[] m_gameStates = null;

    /// <summary>
    /// 現在のゲームステート
    /// </summary>
    private int m_currentGameState = -1;

    public GameStateMachine(int gameStateCount)
    {
        m_gameStates = new GameState<Type>[gameStateCount];
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Update()
    {
        if (m_currentGameState < 0 || m_currentGameState >= m_gameStates.Length) return;

        var currentState = m_gameStates[m_currentGameState];
        currentState.Update();
    }

    /// <summary>
    /// 現在のステートの取得
    /// </summary>
    /// <returns>現在のステート</returns>
    public GameState<Type> GetCurrentState()
    {
        if (m_currentGameState < 0) return null;

        return m_gameStates[m_currentGameState];
    }

    /// <summary>
    /// 現在のステートのインデックスを取得
    /// </summary>
    /// <returns>現在のステートのインデックス</returns>
    public int GetCurrentStateIndex()
    {
        return m_currentGameState;
    }

    /// <summary>
    /// ゲームステートの追加
    /// </summary>
    /// <param name="gameStateIndex">追加するインデックス</param>
    /// <param name="state">追加するステート</param>
    public void AddGameState(int gameStateIndex, GameState<Type> state)
    {
        if(gameStateIndex >= m_gameStates.Length)
        {
            return;
        }

        m_gameStates[gameStateIndex] = state;
    }

    /// <summary>
    /// ステートの変更
    /// </summary>
    /// <param name="gameStateIndex">変更先のインデックス</param>
    public void ChangeState(int gameStateIndex)
    {
        if(gameStateIndex >= m_gameStates.Length)
        {
            return;
        }

        if(m_currentGameState > 0 && m_currentGameState < m_gameStates.Length)
        {
            var currentState = m_gameStates[m_currentGameState];
            currentState.Exit();
        }

        var nextState = m_gameStates[gameStateIndex];
        nextState.Enter();
        m_currentGameState = gameStateIndex;
    }
}

/// <summary>
/// ゲームの状態の抽象クラス
/// </summary>
public abstract class GameState<Type>
{
    /// <summary>
    /// コンテキスト
    /// </summary>
    protected Type m_context;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">コンテキスト</param>
    public GameState(Type context)
    {
        m_context = context;
    }

    /// <summary>
    /// 開始処理
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// 更新処理
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// 終了処理
    /// </summary>
    public abstract void Exit();
}
