using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager : SingletonMonoBehaviour<GameManager>
{
    /// <summary>ゲーム状況</summary>
    GameManagerBaseState m_currentGameManagerBaseState;
    /// <summary>初期化ステート</summary>
    InitializedState m_initializedState = new InitializedState();
    /// <summary>ゲーム中ステート</summary>
    IngameState m_ingameState = new IngameState();
    /// <summary>一時停止ステート</summary>
    PauseState m_pauseState = new PauseState();
    /// <summary>リザルトステート</summary>
    ResultState m_resultState = new ResultState();
    [SerializeField] GameObject m_startButton;
    [SerializeField] GameObject m_exitButton;
    [SerializeField] Text m_timerText;
    [SerializeField] Text m_resultText;
    [SerializeField] GameObject m_player;
    private float m_minute;
    private float m_seconds;
    private float m_decimal;
    private float m_oldDecimal;

    private void Awake()
    {
        //1つ以上あったらゲームオブジェクトを削除
        if (this != Instance)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        m_initializedState.SetStateID(GameManagerID.Initialized);
        m_ingameState.SetStateID(GameManagerID.Ingame);
        m_pauseState.SetStateID(GameManagerID.Pause);

        m_currentGameManagerBaseState = m_initializedState;
    }

    public void Update()
    {
        m_currentGameManagerBaseState.OnUpdate(this);
    }

    /// <summary>
    /// ゲームのステータスを変更します
    /// </summary>
    /// <param name="nextGameState">変更するステート</param>
    public void ChangeGameState(GameManagerBaseState nextGameState)
    {
        m_currentGameManagerBaseState.OnExit(this,nextGameState);
        m_currentGameManagerBaseState = nextGameState;
        m_currentGameManagerBaseState.OnEnter(this, m_currentGameManagerBaseState);
    }

    public void StartButton()
    {
        ChangeGameState(m_ingameState);
    }

    public void ExitButton()
    {
        ChangeGameState(m_initializedState);
    }
}
