using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour {
    // Контроллер сцены (на контроллере)

    public static List<Enemy> enemies;          // Список врагов
    [Header("Enemies prefabs")]
    public GameObject lightEnemyPrefab;
    public GameObject mediumEnemyPrefab;
    public GameObject heavyEnemyPrefab;
    public static List<Transform> respawnPlaces;// Места повяления врагов
    [Space]
    public GameObject spawnPlaces;              // Обьект на сцене с местами появления врагов
    [Header("Text damage")]
    public GameObject textDamagePrefab;         // Префаб 3д-текста
    public GameObject textDamageObjects;        // Родитель для текста с уроном
    private UI ui;
    private GameObject tank;

    // Use this for initialization
    void Start () {
        ui = GameObject.Find("Canvas").GetComponent<UI>();
        tank = GameObject.FindWithTag("Player");
        Initializtion();
        enemies = new List<Enemy>();
        respawnPlaces = new List<Transform>();
        foreach (Transform child in spawnPlaces.transform)
        {
            respawnPlaces.Add(child);
        }
        CheckRespawn();
	}

    // Важен порядок исполнения для усранения ошибок
    private void Initializtion()
    {
        tank.GetComponent<BarrelController>().StartInitialization();
        ui.FillPlayerPanel();
        tank.GetComponent<BarrelController>().EndInitialization();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
    // Уничтожение данного врага
    public void DestroyEnemy(GameObject enemy)
    {
        enemies.Remove(enemy.GetComponent<Enemy>());
        Destroy(enemy);
        CheckRespawn();
    }

    // Проверка на потребность спавна врагов
    private void CheckRespawn()
    {
        while (enemies.Count < 10)
        {
            int randomValue = Random.Range(0, 13);
            int randomPlace = Random.Range(0, 4);
            GameObject respawnEnemy = Instantiate(
                (randomValue < 10 ? lightEnemyPrefab : (randomValue < 12 ? mediumEnemyPrefab : heavyEnemyPrefab)),      // Враги появляються с частотой в отношении 10 : 2 : 1
                respawnPlaces[randomPlace]
                );
            enemies.Add(respawnEnemy.GetComponent<Enemy>());
        }
    }

    // Окончание игры
    public void GameOver()
    {
        SceneManager.LoadScene(0);  // Начинаем заново
    }

    // Создание 3д-текста с уроном
    public void CreateDamageText(Vector3 position, int damage)
    {
        GameObject textDamage = Instantiate(textDamagePrefab, textDamageObjects.transform);
        textDamage.GetComponent<TextDamage>().SetParametrs(damage, position);
    }
}
