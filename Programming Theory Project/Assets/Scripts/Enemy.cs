using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all enemies
public class Enemy : MonoBehaviour
{
    public float speed = 3;
    private ChampionCharacter championCharacter;
    public bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        championCharacter = GameObject.Find("ChampionCharacter").GetComponent<ChampionCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // ABSTRACTION
    }

    public void Move()
    {
        if (championCharacter.transform.position.z - transform.position.z == 0)
        {
            isMoving = false;
        }

        if (!isMoving)
        {
            return;
        }

        if (isMoving)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
