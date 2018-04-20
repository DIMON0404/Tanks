using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy : Enemy {
    // Легкий враг
	// Use this for initialization
	void Start () {
        movementSpeed = 8;
        _health = 25;
        base.Start();
        _armor = 0.1f;
        damage = 10;
        nameEnemy = "Jumkemon";
        transform.GetChild(0).GetComponent<Animator>().speed = Random.Range(0.9f, 1.1f);
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}
}
