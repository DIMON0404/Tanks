using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Barrel : MonoBehaviour
{
    // Абстрактный класс для наследования разных видов пушек
    public float Cooldown
    {
        get { return _cooldown; }
    }
    protected float _cooldown;
    protected float lastShot = float.NegativeInfinity;
    public int Damage
    {
        get { return _damage; }
    }
    protected int _damage;
    [HideInInspector]
    public GameObject tank;     // Игрок

    protected void Start()
    {
        tank = GameObject.FindWithTag("Player");
    }

    public BarrelController.BARREL barrel;

    // Попытка выстрела
    public void CheckShot()
    {
        if (Time.time - lastShot > Cooldown)    // Проверка на кулдаун
        {
            lastShot = Time.time;
            Shot();
        }
    }
    
    protected abstract void Shot();

    // При активации данной пушки если она откулдаунена, то делаем задержку в полсекунды
    public void ActiveBarrel()
    {
        if (Time.time - Cooldown > 0.5f)
        {
            lastShot = Time.time + 0.5f - Cooldown;
        }
    }
}
