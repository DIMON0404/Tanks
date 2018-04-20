using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy {
    // Тяжелый враг

	// Use this for initialization
	void Start () {
        movementSpeed = 2;
        _health = 3500;
        base.Start();
        _armor = 0.4f;
        damage = 50;
        nameEnemy = "Taytan";
    }
}
