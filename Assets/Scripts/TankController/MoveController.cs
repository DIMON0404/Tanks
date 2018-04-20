using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {
    // Контроллер перемещения игрока
    public static float kMovementSpeed = 1;     // Коефициент скорости перемещения
    public static float kRotateSpeed = 1;       // Коефициент скорости поворота
    public int movementSpeed = 5;               // Скорость перемещения
    public int rotateSpeed = 2;                 // Скорость поворота
    [Header("Limit line: l, r, t, b")]
    public GameObject[] lines;                  // Граници сцены

	// Update is called once per frame
	void Update () {
        // Перемещение вперед
		if (Input.GetKey(KeyCode.UpArrow))
        {
            GoForward();
        }
        // Перемещение назад
        if (Input.GetKey(KeyCode.DownArrow))
        {
            GoBack();
            // Если одновременно с поворотом, то поворот в другую сторону
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotateRight();
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                RotateLeft();
            }
        }
        // Повороты. Сработает только если не едем назад (там отдельно)
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotateLeft();
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                RotateRight();
            }
        }

        // При нажатии шифта, уменьшаем скорость поворота
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            kRotateSpeed /= 5;
        }
        // При отпускании возвращаем обратно
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            kRotateSpeed *= 5;
        }
    }

    // Вперед
    protected void GoForward()
    {
        transform.Translate(transform.forward * movementSpeed * kMovementSpeed / 50, Space.World);
        CheckLimit();
    }

    // Назад
    protected void GoBack()
    {
        transform.Translate(-transform.forward * movementSpeed * kMovementSpeed * 0.8f / 50, Space.World);
        CheckLimit();
    }

    // Влево
    protected void RotateLeft()
    {
        transform.Rotate(0, kRotateSpeed * -rotateSpeed, 0);
    }

    // Вправо
    protected void RotateRight()
    {
        transform.Rotate(0, kRotateSpeed * rotateSpeed, 0);
    }

    // Проверка выезда за границу
    protected void CheckLimit()
    {
        if (transform.position.x < lines[0].transform.position.x)
        {
            transform.position = new Vector3(lines[0].transform.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > lines[1].transform.position.x)
        {
            transform.position = new Vector3(lines[1].transform.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.z > lines[2].transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, lines[2].transform.position.z);
        }
        if (transform.position.z < lines[3].transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, lines[3].transform.position.z);
        }
    }
}
