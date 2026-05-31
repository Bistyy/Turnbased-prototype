using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
   
    public enum BattleState
    {
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    }

    public event Action<BattleState> OnStateChanged;
    private BattleState _currentState;

    public BattleState CurrentState
    {
        get { return _currentState; }
        set 
        { 
            _currentState = value;
            OnStateChanged?.Invoke(value);
        }
    }
    void Start()
    {
        CurrentState = BattleState.PlayerTurn;
        Debug.Log($"Current state = {CurrentState}");
    }
}
