using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    public class ResultState: GameManagerBaseState
    {
        public override void OnEnter(GameManager owner, GameManagerBaseState prevbaseState)
        {
            owner.m_exitButton.SetActive(true);
        }

        public override void OnUpdate(GameManager owner)
        {
            
        }

        public override void OnExit(GameManager owner, GameManagerBaseState nextState)
        {
            owner.m_exitButton.SetActive(false);
        }
    }
}
