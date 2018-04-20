using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BarrelController : MonoBehaviour {
    // Контроллер пушек

    public GameObject barrelBase;                       // Родитель для пушки
    [Header("Barrels: lazer, sniper, hard")]
    public List<GameObject> barrels;                    // Пушки
    public enum BARREL { LAZER, SNIPER, HARD };         
    public static BARREL currentBarrel = BARREL.LAZER;  // Текущая пушка (тип - перечисление)
    public static GameObject activeBarrel;              // Активная пушка на сцене (аналог currentBarrel)
    private UI ui;                                      // Контроллер интерфейса


	// Use this for initialization
	void Start () {
        ui = GameObject.Find("Canvas").GetComponent<UI>();
	}

    public void StartInitialization()
    {
        // Активирем все пушки для инициализации данных в ui
        foreach (GameObject barrel in barrels)
        {
            barrel.SetActive(true);
        }
    }

    // Оставляем только активную
    public void EndInitialization()
    {
        foreach (Transform child in barrelBase.transform)
        {
            if (child.GetComponent<Barrel>().barrel == currentBarrel)
            {
                activeBarrel = child.gameObject;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Пролистывание пушек вправо-влево
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if ((int)currentBarrel == 0)
            {
                currentBarrel = (BARREL)Enum.GetValues(typeof(BARREL)).Length - 1;
            }
            else
            {
                currentBarrel--;
            }
            SwapBurrel();
        }
		if (Input.GetKeyDown(KeyCode.E))
        {
            if ((int)currentBarrel == Enum.GetValues(typeof(BARREL)).Length - 1)
            {
                currentBarrel = (BARREL)0;
            }
            else
            {
                currentBarrel++;
            }
            SwapBurrel();
        }

        // Выстрел
        if (Input.GetKey(KeyCode.X))
        {
            try
            {
                Shot();
            }
            catch
            {
                // Если ошибка (бывает при удержании клавишы выстрела и смене пушки), пробуем еще один раз
                StartCoroutine(CheckShotAgain());
            }
        }
    }

    // Смена пушки
    public void SwapBurrel()
    {
        activeBarrel.SetActive(false);
        activeBarrel = barrels[(int)currentBarrel];
        activeBarrel.SetActive(true);
        activeBarrel.GetComponent<Barrel>().ActiveBarrel();
        ui.ChangeActiveBarrel();
    }

    private IEnumerator CheckShotAgain()
    {
        yield return new WaitForSeconds(0.1f);
        Shot();
    }

    private void Shot()
    {
        activeBarrel.GetComponent<Barrel>().CheckShot();
    }
}
