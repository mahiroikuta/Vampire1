using UnityEngine;
using UnityEngine.UI;

public class EnemyComponent : MonoBehaviour
{
    public float speed;
    public float maxHp;
    public float hp;
    public float attack;
    public float coolDownTimer;
    public float coolTime;
    public GameObject emptyObj;
    public Slider hpBar;
}
