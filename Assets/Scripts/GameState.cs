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
    public GameObject camera;
    public GameObject enemyPrefab;
    public List<EnemyComponent> enemies;
    public GameObject bulletPrefab;
    public List<BulletComponent> bullets;
    public float enemyDuration;

    [System.NonSerialized]
    public GameStatus gameStatus;
}
