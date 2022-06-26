using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This script handles all the control code, so detecting when the users click on a minion or tower and selecting those
// (That would be the ideal situation, but for now it will rest as click on the champion and then activate his movement)
public class UserControl : MonoBehaviour
{
    public Camera GameCamera;
    public float PanSpeed = 10.0f;
    public GameObject Marker;

    private CharacterHandler m_Selected = null;

    public AttackType currentAttack = AttackType.None;

    private Rigidbody championRB;

    // Pushback
    public float pushbackStrength =  15.0f;

    // Rockets
    public GameObject rocketPrefab;
    private GameObject tmpRocket;

    // Smash
    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;

    bool smashing = false;
    float floorY;

    // Start is called before the first frame update
    private void Start()
    {
        Marker.SetActive(false);
    }

    public void HandleSelection()
    {
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                var unit = hit.collider.GetComponentInParent<CharacterHandler>();
                m_Selected = unit;
            }
        }
    }

    public void HandleAction()
    {
        if (m_Selected != null && Input.GetMouseButtonDown(1))
        {
            var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var tower = hit.collider.GetComponentInParent<Tower>();

                if (tower != null)
                {
                    m_Selected.GoTo(tower);
                }
                else
                {
                    m_Selected.GoTo(hit.point);
                }
            }
        }
    }

    /* 
    OLD CODE with object pooler
    public void HandleAttack()
    {
        if ((m_Selected != null && Input.GetKeyDown(KeyCode.Q)))
        {
            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
            var ray = GameCamera.ScreenPointToRay(Input.mousePosition); //added
            RaycastHit hit; // added

            if (pooledProjectile != null && Physics.Raycast(ray, out hit))
            {
                pooledProjectile.SetActive(true); // activate it
                pooledProjectile.transform.position = m_Selected.transform.position; // position it at the mouse cursor
            }
        }
    }
    */

    // Update is called once per frame
    private void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        GameCamera.transform.position = GameCamera.transform.position + new Vector3(move.y, 0, -move.x) * PanSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
        else if (m_Selected != null && Input.GetMouseButtonDown(1))
        {
            HandleAction();
        } 
        else if (m_Selected != null && Input.GetKeyDown(KeyCode.E))
        {
            currentAttack = AttackType.Rockets;
            HandleProjectileAttack();
        } 
        else if (m_Selected != null && Input.GetKeyDown(KeyCode.T))
        {
            //HandleJumping(); // or dash
        } 
        else if (m_Selected != null && Input.GetKeyDown(KeyCode.R))
        {
            currentAttack = AttackType.Smash;
            smashing = true;
            StartCoroutine(Smash());
        }
        
        MarkerHandling();
    }

    // Handle displaying the marker above the unit that is currently selected (or hiding it if none is selected)
    void MarkerHandling()
    {
        if (m_Selected == null && Marker.activeInHierarchy)
        {
            Marker.SetActive(false);
            Marker.transform.SetParent(null);
        }
        else if (m_Selected != null && Marker.transform.parent != m_Selected.transform)
        {
            Marker.SetActive(true);
            Marker.transform.SetParent(m_Selected.transform, false);
            Marker.transform.localPosition = new Vector3(0,10,0);
        }
    }

    public void HandleProjectileAttack()
    {
        foreach(var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, m_Selected.transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && Input.GetKeyDown(KeyCode.Q))
        {
            currentAttack = AttackType.Pushback;
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRB.AddForce(awayFromPlayer * pushbackStrength, ForceMode.Impulse);
            Debug.Log("Player collided with " + collision.gameObject.name + " with current attack set to " + currentAttack.ToString());
        }
    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();

        // Store the y position before taking off
        floorY = transform.position.y;

        // Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            // move the player up while still keeping their x velocity
            championRB.velocity = new Vector2(championRB.velocity.x, smashSpeed);
            yield return null;
        }

        // Now moce the player down
        while (transform.position.y > floorY)
        {
            championRB.velocity = new Vector2(championRB.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        // Cycle through all enemies
        for (int i=0; i < enemies.Length; i++)
        {
            // Apply an explosion force that originates from our position
            if (enemies[i] != null)
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
        }

        // We are no longer smashing, so set the boolean to false
        smashing = false;
    }
}
