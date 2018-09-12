using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    /// <summary>
    /// Projectile prefab for shooting
    /// </summary>
    public Transform shotPrefab;
    private LineRenderer laserLine;
    /// <summary>
    /// Cooldown in seconds between two shots
    /// </summary>
    public float bulletRate = 0.25f;
    public float laserRate = 1.0f;
    private WaitForSeconds laserDuration = new WaitForSeconds(.07f);

    //--------------------------------
    // 2 - Cooldown
    //--------------------------------

    private float bulletCooldown = 0f;
    private float laserCooldown = 0f;


    void Start()
    {
        laserLine = GetComponent<LineRenderer>();

        if (gameObject.name != "Player") {
            bulletCooldown = UnityEngine.Random.value;
        }
    }

    void Update()
    {
        if (bulletCooldown > 0)
        {
            bulletCooldown -= Time.deltaTime;
        }

        if (laserCooldown > 0)
        {
            bulletCooldown -= Time.deltaTime;
        }
    }

    //--------------------------------
    // 3 - Shooting from another script
    //--------------------------------

    /// <summary>
    /// Create a new projectile if possible
    /// </summary>
    public void Attack(bool isEnemy)
    {
        if (CanBullet)
        {
            bulletCooldown = bulletRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            // The is enemy property
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right; // towards in 2D space is the right of the sprite
            }
        }

        if (CanLaser)
        {
            StartCoroutine(ShotEffect());
            
            laserLine.SetPosition(0, gameObject.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(0, 1));

            if (hit)
            {
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                laserLine.SetPosition(1, gameObject.transform.position + new Vector3(0, 0, 50));
            }
        }
    }

    private void StartCoroutine(IEnumerable enumerable) //yo wtf is this, it was auto created b/c it couldn't call shoteffect();
    {
        enumerable();
    }

    private IEnumerable ShotEffect()
    {
        laserLine.enabled = true;
        yield return laserDuration;
        laserLine.enabled = false;
    }
    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanBullet
    {
        get
        {
            return bulletCooldown <= 0f;
        }
    }

    public bool CanLaser
    {
        get
        {
            return laserCooldown <= 0f;
        }
    }
}
