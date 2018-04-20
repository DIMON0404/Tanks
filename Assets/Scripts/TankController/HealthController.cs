using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    // Контроллер здоровья игрока
    private int _health = 100;
    public int Health
    {
        get { return _health; }
    }
    private float _armor = 0.2f;
    public float Armor
    {
        get { return _armor; }
    }
    private UI ui;                  // Контроллер интерфейса
    private Scene scene;            // Контроллер сцены

    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UI>();
        scene = GameObject.Find("Controller").GetComponent<Scene>();
    }

    public void GetDamage(int gotDamage)
    {
        gotDamage = Mathf.RoundToInt(gotDamage * (1f - Armor));
        ChangeHealth(-gotDamage);
        scene.CreateDamageText(transform.position, gotDamage);
    }

    private void ChangeHealth(int deltaHealth)
    {
        _health += deltaHealth;
        if (_health <= 0)
        {
            _health = 0;
            GameObject.Find("Controller").GetComponent<Scene>().GameOver();
        }
        try
        {
            ui.ChangeHealthPlayer();
        }
        catch
        { }
    }
}
