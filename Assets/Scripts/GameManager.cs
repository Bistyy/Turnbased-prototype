using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public enum BattleState
    {
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    }

    public BattleState currentState;

    void Start()
    {
        currentState = BattleState.PlayerTurn;
        Debug.Log($"Current state = {currentState}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
