using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    //На Canvas
    [Header("BarrelsPanels: lazer, sniper, hard")]
    public GameObject[] barrelsPanels;              // Панель пушек
    [Header("Barrels: lazer, sniper, hard")]
    public Barrel[] barrels;                        // Пушки танка
    [HideInInspector]
    public enum ColorAlpha { enable, disable};      
    public static Dictionary<ColorAlpha, float> alphaChannel = new Dictionary<ColorAlpha, float>(); // Словарь со значением величины альфа-канала для разниых состояний панели
    private static bool isInitializated = false;    // Проверка на инициализацию (для статики)
    private GameObject tank = null;                 // Игрок
    [Header("Player parametrs")]
    public GameObject healthPlayerPanel;
    public GameObject armorPlayerPanel;
    [Header("Enemy parametrs")]
    public GameObject healthEnemyPanel;
    public GameObject armorEnemyPanel;
    public GameObject enemyNameText;

    // Изменение активной пушки
    public void ChangeActiveBarrel()
    {
        for (int i = 0; i < barrelsPanels.Length; i++)
        {
            Color currentColor = barrelsPanels[i].GetComponent<Image>().color;
            barrelsPanels[i].GetComponent<Image>().color = 
                new Color(currentColor.r, currentColor.g, currentColor.b,
                barrels[i].barrel == BarrelController.currentBarrel ? alphaChannel[ColorAlpha.enable] : alphaChannel[ColorAlpha.disable]);
        }
    }

    // Изменение количества жизней у игрока
    public void ChangeHealthPlayer()
    {
        healthPlayerPanel.transform.Find("HealthText").GetComponent<Text>().text = tank.GetComponent<HealthController>().Health + "%";
    }

    // Изменение количества жизней у врага (и брони)
    public void ChangeHealthEnemy(Enemy enemy)
    {
        healthEnemyPanel.GetComponent<Image>().fillAmount = enemy.Health > 0 ? (float)enemy.Health / enemy.MaxHealth : 0;
        enemyNameText.GetComponent<Text>().text = enemy.nameEnemy;
        armorEnemyPanel.transform.Find("ArmorText").GetComponent<Text>().text = Mathf.RoundToInt(enemy.Armor * 100) + "%";
    }

    // Инициализация полей интерфейса
    public void FillPlayerPanel()
    {
        if (!isInitializated)
        {
            alphaChannel.Add(ColorAlpha.enable, 1f);
            alphaChannel.Add(ColorAlpha.disable, 0.125f);
            isInitializated = true;
        }
        if (tank == null)
        {
            tank = GameObject.FindWithTag("Player");
        }
        for (int i = 0; i < barrelsPanels.Length; i++)
        {
            barrelsPanels[i].transform.Find("DamageText").GetComponent<Text>().text = barrels[i].Damage + barrelsPanels[i].transform.Find("DamageText").GetComponent<Text>().text;
            barrelsPanels[i].transform.Find("CooldownText").GetComponent<Text>().text = barrels[i].Cooldown + "";
        }
        ChangeActiveBarrel();
        healthPlayerPanel.transform.Find("HealthText").GetComponent<Text>().text = tank.GetComponent<HealthController>().Health + "%";
        armorPlayerPanel.transform.Find("ArmorText").GetComponent<Text>().text = tank.GetComponent<HealthController>().Armor * 100 + "%";
    }
}
