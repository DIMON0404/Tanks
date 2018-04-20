using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Barrel {
    // Пушка-снайпер. Достаточно большой урон с средним временем перезарядки
    // Выстрел происходит моментально
    // Есть лазерный прицел
    private GameObject lazer;       // Лазер (прицел)
    private Vector3 startPosition;  // Точка начала лазера
    private Vector3 endPosition;    // Конечная точка лазера
    public GameObject shotPoint;    // Точка "выстрела"
    private ParticleSystem effect;  // Частици для эффекта
    private RaycastHit hit;         // Луч для проверки попадания


    // Use this for initialization
    new void Start () {
        base.Start();
        barrel = BarrelController.BARREL.SNIPER;
        _cooldown = 3f;
        _damage = 500;
        lazer = transform.Find("Lazer").gameObject;
        effect = transform.Find("Effect").GetComponent<ParticleSystem>();
	}

    private void Update()
    {
        // Все время рисуем лазер до ближайшей точки столкновения
        startPosition = lazer.transform.position;   
        // Если что-то находит на расстоянии 1000, то возвращаем точку
        // Иначе берем точку на расстоянии 500 от танка (для избежания ошибки)
        if (Physics.Raycast(startPosition, transform.TransformDirection(Vector3.forward), out hit, 1000))
        {
            endPosition = hit.point;
        }
        else
        {
            endPosition = tank.transform.forward * 500 + tank.transform.position;
        }
        lazer.transform.localScale = new Vector3(1, 1, Vector3.Distance(startPosition, endPosition) / transform.localScale.z);

    }

    protected override void Shot()
    {
        effect.transform.position = endPosition;    // Эффект появляется на точке попадания
        effect.Play();
        if (hit.collider.gameObject.tag.Equals("Enemy"))
        {
            hit.collider.GetComponent<Enemy>().GetDamage(Damage);
        }

    }
}
