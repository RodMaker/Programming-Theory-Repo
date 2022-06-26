using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Subclass of Enemy
// INHERITANCE
public class CasterMinion : Enemy
{
    private ChampionCharacter championCharacter;

    // Rockets
    public GameObject rocketPrefab;
    private GameObject tmpRocket;

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
}
