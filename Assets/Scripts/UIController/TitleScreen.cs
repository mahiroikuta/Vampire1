using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : BaseScreen
{
    GameState _gameState;
    GameEvent _gameEvent;
    [SerializeField] private Button startButton;
    public override void Init(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _gameEvent.setTitleScreen += OnShow;
        _gameEvent.startGame += OnHide;

        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _gameState.gameStatus = GameStatus.IsPlaying;
        _gameEvent.startGame?.Invoke();
    }

    public override void OnShow()
    {
        _gameState.gameStatus = GameStatus.Ready;
        this.gameObject.SetActive(true);
    }
}
