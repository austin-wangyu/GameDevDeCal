using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeAttack : Attack {
    new public int damage = 1;

    protected override void OnTriggerEnter2D(Collider2D collision) {
        Health other = collision.gameObject.GetComponent<Health>();
        PlayerMovement speed = collision.gameObject.GetComponent<PlayerMovement>();

        if(other != null)
        {
            other.takeDamage(damage);
            speed.slow();
            Destroy(gameObject);
        }
    }
}
