using UnityEngine;
using StateMachine;
using TMPro;

public class GameManager : GamePlayBehaviour
{
    public static System.Action OnTakingCoins;

    [SerializeField] GameObject _gameOverObj;

    private void Start()
    {
        OnNextGameState(GamePlayStates.INITIALIZING);
    }

    private void Update()
    {
        StateBehaviour(GamePlayCurrentState);

        UpdateState();
    }

    void StateBehaviour(GamePlayStates state)
    {
        switch(state)
        {
            case GamePlayStates.INITIALIZING:
                {
                    _gameOverObj.SetActive(false);

                    CameraBehaviour.OnSearchingPlayer?.Invoke();

                    break;
                }
            case GamePlayStates.START:
                {

                    OnNextGameState.Invoke(GamePlayStates.GAMEPLAY);

                    break;
                }
            case GamePlayStates.GAMEPLAY:
                {
                    Time.timeScale = 1;

                    PauseGame();

                    break;
                }
            case GamePlayStates.PAUSE:
                {
                    Time.timeScale = 0;

                    PauseGame();

                    break;
                }
            case GamePlayStates.GAMEOVER:
                {
                    Time.timeScale = 0;

                    _gameOverObj.SetActive(true);

                    break;
                }
        }
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GetCurrentGameState() != GamePlayStates.PAUSE)
            {
                OnNextGameState?.Invoke(GamePlayStates.PAUSE);
            }
            else
            {
                OnNextGameState?.Invoke(GamePlayStates.GAMEPLAY);
            }
        }
    }
}
