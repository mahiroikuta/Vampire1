using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    Ready,
    IsPlaying,
    GetItem,
    GameOver,
    GameClear,
}

[System.Serializable]
public class GameState
{
    public GameObject player;
    public GameObject emptySphere;
    public GameObject camera;
    public GameObject enemyPrefab;

    [System.NonSerialized]
    public GameStatus gameStatus;

    public List<EnemyComponent> enemies;
}
