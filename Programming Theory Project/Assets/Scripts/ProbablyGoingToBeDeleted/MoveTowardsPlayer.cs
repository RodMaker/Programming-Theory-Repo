using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float speed = 40.0f;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("ChampionCharacter").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
    }
}
