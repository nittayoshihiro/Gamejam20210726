using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager
{
    /// <summary>
    /// ゲーム中状態
    /// </summary>
    public class IngameState : GameManagerBaseState
    {
        public override void OnEnter(GameManager owner, GameManagerBaseState prevbaseState)
        {
            owner.m_minute = 0.0f;
            owner.m_seconds = 0.0f;
            owner.m_decimal = 0.0f;
            owner.m_oldDecimal = 0.0f;
            Instantiate(owner.m_player, new Vector3(0f, 1f, 0f),Quaternion.identity);
        }

        public override void OnUpdate(GameManager owner)
        {
            owner.m_decimal += Time.deltaTime;
            if (owner.m_decimal >= 60f)
            {
                owner.m_seconds++;
                owner.m_decimal -= 60;
            }
            if (owner.m_seconds >= 60f)
            {
                owner.m_minute++;
                owner.m_seconds -= 60;
            }
            if ((int)owner.m_decimal != (int)owner.m_oldDecimal)
            {
                owner.m_timerText.text = owner.m_minute.ToString("00") + ":" + ((int)owner.m_seconds).ToString("00") + ":" + ((int)owner.m_decimal).ToString("00");
            }
            owner.m_oldDecimal = owner.m_decimal;
        }

        public override void OnExit(GameManager owner, GameManagerBaseState nextState)
        {
            owner.m_resultText.text = "Game Clear\n" + owner.m_timerText.text;
        }
    }
}