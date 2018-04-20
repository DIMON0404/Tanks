using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {
    // Абстрактный клас для наследования разных типов врагов
    protected int _health;      // Здоровье
    public int Health
    {
        get { return _health; }
    }
    protected int _maxHealth;   // Максимальное здоровье
    public int MaxHealth
    {
        get { return _maxHealth;}
    }
    protected float _armor;     // Броня
    public float Armor      
    {
        get { return _armor; }
    }
    protected int damage;       // Урон
    protected int movementSpeed;// Скорость перемещения
    public string nameEnemy;    // Имя врага
    protected NavMeshAgent agent;   // Агент для навигации
    protected GameObject target;    // Цель куда илти
    private Scene scene;            // Контроллер сцены
    private UI ui;                  // Контроллер интерфейса

	// Use this for initialization
	protected void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        agent.speed = movementSpeed;
        transform.GetChild(0).GetComponent<Animator>().speed = Random.Range(0.9f, 1.1f);
        scene = GameObject.Find("Controller").GetComponent<Scene>();
        ui = GameObject.Find("Canvas").GetComponent<UI>();
        _maxHealth = _health;
    }
	
	// Update is called once per frame
	protected void Update () {
        agent.SetDestination(target.transform.position);
	}

    public virtual void GetDamage(int gotDamage)
    {
        gotDamage = Mathf.RoundToInt(gotDamage * (1f - Armor));
        _health -= gotDamage;
        scene.CreateDamageText(transform.position, gotDamage);
        ui.ChangeHealthEnemy(this);
        if (_health <= 0)
        {
            DestroyThis();
        }
    }

    // Уничтожение себя
    protected void DestroyThis()
    {
        GameObject.Find("Controller").gameObject.GetComponent<Scene>().DestroyEnemy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<HealthController>().GetDamage(damage);
            DestroyThis();
        }
    }

}
