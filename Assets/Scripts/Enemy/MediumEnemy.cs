using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemy : Enemy {
    // Средний враг
    // Особенность - повышение брони с каждым попаданием
	// Use this for initialization
	new void Start () {
        movementSpeed = 5;
        _health = 200;
        base.Start();
        _armor = 0f;
        damage = 20;
        nameEnemy = "Stacker";
    }
	
    public override void GetDamage(int gotDamage)
    {
        base.GetDamage(gotDamage);
        _armor += (0.9f - Armor) / 10;
    }
}