using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Subclass of CharacterHandler
// INHERITANCE
public class ChampionCharacter : CharacterHandler
{
    // Start() and Update() methods deleted - we don't need them right now (probably will need Update for keypresses and stuff, unless it is gonna be done on a separate script)

    private Tower m_CurrentAttackingTarget;

    private Tower tower;

    public GameObject rocketPrefab;
    private GameObject tmpRocket;

    public bool isDead;

    // HEALTH variables
    public HealthBar healthBar; 

    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public float healthRegenerationAmount = 1;
    public float healthRegenTimer = 0;


    // MANA variables
    public ManaBar manaBar;

    public int manaLevel = 10;
    public int maxMana;
    public int currentMana;

    public float manaRegenerationAmount = 1;
    public float manaRegenTimer = 0;

    // GOLD variables
    public int gold;
    public int goldAwardedOnDeath = 50;

    public void Start()
    {
        tower = GameObject.Find("Tower").GetComponent<Tower>();
        healthBar = FindObjectOfType<HealthBar>();
        manaBar = FindObjectOfType<ManaBar>();

        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetCurrentHealth(currentHealth);

        maxMana = SetMaxManaFromManaLevel();
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
        manaBar.SetCurrentMana(currentMana);
    }

    // POLYMORPHISM (4 Pillars of OOP)
    public override void GoTo(Vector3 position)
    {
        base.GoTo(position);
        m_CurrentAttackingTarget = null;
    }

    // POLYMORPHISM (4 Pillars of OOP)
    protected override void TowerInRange()
    {
        //we arrive at the tower, attack!
        GoTo(m_CurrentAttackingTarget);
        //m_Target.Attack(); // Attack method needs to be created
        tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
        tmpRocket.GetComponent<RocketBehaviour>().Fire(tower.transform);

        //throw new System.NotImplementedException();
    }

    public void OnTriggerEnter(Collider tower)
    {
        tmpRocket = Instantiate(rocketPrefab, tower.transform.position + Vector3.up, Quaternion.identity);
        tmpRocket.GetComponent<RocketBehaviour>().Fire(transform);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth = Mathf.RoundToInt(currentHealth - damage);
        healthBar.SetCurrentHealth(currentHealth);

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

    public void RegenerateHealth()
    {
        healthRegenTimer += Time.deltaTime;

        if (currentHealth < maxHealth && healthRegenTimer > 1f)
        {
            currentHealth += Mathf.RoundToInt(healthRegenerationAmount * Time.deltaTime);
            healthBar.SetCurrentHealth(Mathf.RoundToInt(currentHealth));
        }
    }

    public void TakeManaDamage(int damage)
    {
        currentMana = currentMana - damage;
        manaBar.SetCurrentMana(currentMana);
    }

    private int SetMaxManaFromManaLevel()
    {
        maxMana = manaLevel * 10;
        return maxMana;
    }

    public void RegenerateStamina()
    {
        manaRegenTimer += Time.deltaTime;

        if (currentMana < maxMana && manaRegenTimer > 1f)
        {
            currentMana += Mathf.RoundToInt(manaRegenerationAmount * Time.deltaTime);
            manaBar.SetCurrentMana(Mathf.RoundToInt(currentMana));
        }
    }
}
