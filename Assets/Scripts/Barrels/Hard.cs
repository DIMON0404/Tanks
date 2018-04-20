using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hard : Barrel {
    // Тяжелая пушка с большим кулдауном и уроном
    public GameObject shotPoint;    // Точка появления снаряда
    private GameObject bullet;      // Снаряд

    // Use this for initialization
    new void Start () {
        base.Start();
        barrel = BarrelController.BARREL.HARD;
        _cooldown = 7;
        _damage = 5000;
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/HardBullet");
    }

    protected override void Shot()
    {
        Instantiate(bullet, shotPoint.transform);
    }
}
