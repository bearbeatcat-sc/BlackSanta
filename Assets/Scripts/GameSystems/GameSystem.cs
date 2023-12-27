using Assets.Scripts.GameSystems;
using Assets.Scripts.Player.Skills;
using System;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameSystem : MonoBehaviour
{
    /// <summary>
    /// �Q�[���̏��
    /// </summary>
    public enum GameStateType
    {
        Wait,
        Play,
        GameOver,
        Max,
    }   

    /// <summary>
    /// �v���C���[
    /// </summary>
    [SerializeField]
    private GameObject m_player = null;

    /// <summary>
    /// �v���C���[UI
    /// </summary>
    [SerializeField]
    private PlayerUI m_playerUI = null;  

    /// <summary>
    /// �X�L���I���V�X�e��
    /// </summary>
    [SerializeField]
    private SkillSelectSystem m_skillSelectSystem = null;

    /// <summary>
    /// �v���C���[�J����
    /// </summary>
    [SerializeField]
    private PlayerCamera m_playerCamera = null;

    /// <summary>
    /// �v���C���[�o���l�V�X�e��
    /// </summary>
    private PlayerExpSystem m_playerExpSystem = null;

    /// <summary>
    /// �v���C���[�X�L���V�X�e��
    /// </summary>
    private PlayerSkill m_playerSkillSystem = null;

    /// <summary>
    /// �Q�[���X�e�[�g�}�V��
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
    /// �X�L���I���̊J�n
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
    /// �Q�[���O�ҋ@�����X�e�[�g
    /// </summary>
    public class WaitGameState : GameState<GameSystem>
    {

        /// <summary>
        /// �R���X�g���N�^
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
    /// �Q�[�����X�e�[�g
    /// </summary>
    public class GamePlayState : GameState<GameSystem>
    {
        /// <summary>
        /// �Q�[���v���C���̃X�e�[�g
        /// </summary>
        public enum GamePlayStateType
        {
            Start,
            Playing,
            SkillSelect,
            Max,
        }

        /// <summary>
        /// �Q�[���X�e�[�g�}�V��
        /// </summary>
        private GameStateMachine<GamePlayState> m_gameStateMachine = null;


        /// <summary>
        /// �v���C���[�̃C���X�^���X
        /// </summary>
        private GameObject m_playerInstance = null;

        /// <summary>
        /// �R���X�g���N�^
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
        /// �X�L���I���̊J�n
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
        /// �v���C���X�e�[�g
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
        /// �X�L���I���X�e�[�g
        /// </summary>
        private class SkillSelect : GameState<GamePlayState>
        {
            /// <summary>
            /// �R���X�g���N�^
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
    /// �Q�[���I�[�o�[�X�e�[�g
    /// </summary>
    public class GameOverState : GameState<GameSystem>
    {
        /// <summary>
        /// �R���X�g���N�^
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
/// �Q�[���X�e�[�g�̑���N���X
/// </summary>
public class GameStateMachine<Type>
{
    /// <summary>
    /// �Q�[���X�e�[�g���X�g
    /// </summary>
    private GameState<Type>[] m_gameStates = null;

    /// <summary>
    /// ���݂̃Q�[���X�e�[�g
    /// </summary>
    private int m_currentGameState = -1;

    public GameStateMachine(int gameStateCount)
    {
        m_gameStates = new GameState<Type>[gameStateCount];
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    public void Update()
    {
        if (m_currentGameState < 0 || m_currentGameState >= m_gameStates.Length) return;

        var currentState = m_gameStates[m_currentGameState];
        currentState.Update();
    }

    /// <summary>
    /// ���݂̃X�e�[�g�̎擾
    /// </summary>
    /// <returns>���݂̃X�e�[�g</returns>
    public GameState<Type> GetCurrentState()
    {
        if (m_currentGameState < 0) return null;

        return m_gameStates[m_currentGameState];
    }

    /// <summary>
    /// ���݂̃X�e�[�g�̃C���f�b�N�X���擾
    /// </summary>
    /// <returns>���݂̃X�e�[�g�̃C���f�b�N�X</returns>
    public int GetCurrentStateIndex()
    {
        return m_currentGameState;
    }

    /// <summary>
    /// �Q�[���X�e�[�g�̒ǉ�
    /// </summary>
    /// <param name="gameStateIndex">�ǉ�����C���f�b�N�X</param>
    /// <param name="state">�ǉ�����X�e�[�g</param>
    public void AddGameState(int gameStateIndex, GameState<Type> state)
    {
        if(gameStateIndex >= m_gameStates.Length)
        {
            return;
        }

        m_gameStates[gameStateIndex] = state;
    }

    /// <summary>
    /// �X�e�[�g�̕ύX
    /// </summary>
    /// <param name="gameStateIndex">�ύX��̃C���f�b�N�X</param>
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
/// �Q�[���̏�Ԃ̒��ۃN���X
/// </summary>
public abstract class GameState<Type>
{
    /// <summary>
    /// �R���e�L�X�g
    /// </summary>
    protected Type m_context;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="context">�R���e�L�X�g</param>
    public GameState(Type context)
    {
        m_context = context;
    }

    /// <summary>
    /// �J�n����
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// �X�V����
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// �I������
    /// </summary>
    public abstract void Exit();
}
