using UnityEngine;
using UnityEngine.UI;

public class EnemyComponent : MonoBehaviour
{
    public float speed;
    public int maxHp;
    public int hp;
    public int attack;
    public float coolDownTimer;
    public float coolTime;
    public GameObject emptyObj;
    public Slider hpBar;
}
