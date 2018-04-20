using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardBullet : MonoBehaviour {
    // Снаряд для тяжелой пушки
    private Hard hard;  // Сама пушка
    // Use this for initialization
    void Start()
    {
        StartCoroutine(DestroyThis());
        hard = transform.parent.parent.GetComponent<Hard>();
        gameObject.GetComponent<Rigidbody>().AddForce(hard.tank.transform.forward * 1000);
        transform.SetParent(GameObject.FindWithTag("BulletsPocket").transform);             // Для абстрагирования от положения танка назначаем нового родителя
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            other.GetComponent<Enemy>().GetDamage(hard.Damage);
        }
        else if (!other.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Через некоторое время секунд если снаряд не врезается никуда, он исчезает
    private IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
