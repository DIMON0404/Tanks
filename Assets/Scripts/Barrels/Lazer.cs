using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Barrel {
    // Легкая пушка с небольшой перезарядкой и уроном
    // Простреливает врагов насквозь
    public List<GameObject> shotPoints; // Точка появления снаряда
    private GameObject bullet;          // Снаряд

    // Use this for initialization
    new void Start () {
        base.Start();
        barrel = BarrelController.BARREL.LAZER;
        _cooldown = 0.1f;
        _damage = 10;
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Plazm");
    }

    protected override void Shot()
    {
        foreach (GameObject point in shotPoints)
        {
            Instantiate(bullet, point.transform);
        }
    }
}
