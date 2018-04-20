using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plazm : MonoBehaviour {
    // Снаряд для лазарной пушки
    Lazer lazer;    // Сама пушка
	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyThis());
        lazer = transform.parent.parent.parent.GetComponent<Lazer>();
        gameObject.GetComponent<Rigidbody>().AddForce(lazer.tank.transform.forward * 5000);
        transform.SetParent(GameObject.FindWithTag("BulletsPocket").transform);
	}

    private void OnTriggerEnter(Collider other)
    {
        // Уничтожить снаряд только если столкновение не с врагом и не с игроком (мало ли)
        if (other.tag.Equals("Enemy"))
        {
            other.GetComponent<Enemy>().GetDamage(lazer.Damage);
        }
        else if (!other.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Через некоторое время секунд если снаряд не врезается никуда, он исчезает
    private IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
