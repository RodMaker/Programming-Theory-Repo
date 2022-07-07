using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Subclass of Enemy
// INHERITANCE
public class CasterMinion : Enemy
{
    private ChampionCharacter championCharacter;

    [Header("Rockets")]
    public GameObject rocketPrefab;
    private GameObject tmpRocket;

    [Header("Health")]
    public UIEnemyHealthBar uiEnemyHealthBar; 

    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public bool isDead;

    [Header("Gold")]
    public int goldAwardedOnDeath = 50;

    // Start is called before the first frame update
    void Start()
    {
        championCharacter = GameObject.Find("ChampionCharacter").GetComponent<ChampionCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleAttack();

        if (isDead)
        {
            //award gold on death
            //play death animation
            //destroy game object
            AwardGoldOnDeath();
            Destroy(gameObject);
        }
    }

    public override void Move()
    {
        speed = 5;

        if (!isMoving)
        {
            speed = 0;
            return;
        }

        if (isMoving)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            if (championCharacter.transform.position.z - transform.position.z <= 0)
            {
                speed = 0;
                isMoving = false;
            }
        }
    }

    public void HandleAttack()
    {
        if (championCharacter.transform.position.z - transform.position.z <= 0)
        {   
            /* OLD CODE with object pooler
            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();

            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true); // activate it
                pooledProjectile.transform.position = transform.position; // position it at the mouse cursor
            }
            */

            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(championCharacter.transform);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth = Mathf.RoundToInt(currentHealth - damage);
        uiEnemyHealthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void AwardGoldOnDeath()
    {
        // Scan for every player in the scene, award them gold
        // for the multiplayer set it on a "for" loop
        GoldCountBar goldCountBar = FindObjectOfType<GoldCountBar>();

        if (championCharacter != null)
        {
            championCharacter.AddGold(goldAwardedOnDeath);

            if (goldCountBar != null)
            {
                goldCountBar.SetGoldCountText(championCharacter.goldCount);
            }
        }
    }
}
