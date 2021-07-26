using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
