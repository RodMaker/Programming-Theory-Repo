using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Base class for all characters. It will handle movement order given through the UserControl script
// It requires a NavMeshAgent to navigate the scene
[RequireComponent(typeof(NavMeshAgent))]
public class CharacterHandler : MonoBehaviour
{
    public float Speed = 3;

    protected NavMeshAgent m_Agent;
    
    //protected Tower m_Target; // Need to investigate if I will classify it as a Transform, or as another script for example Tower / Enemy
    //protected Enemy m_EnemyTarget;

    protected void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = Speed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (MainManager.Instance != null)
        {
            SetColor(MainManager.Instance.PlayerColor); // Will need to think about this a little harder since I only want the player and his allies to change color, and the enemies have another, maybe through a boolean isAlly or something
        }
    }

    void SetColor(Color color)
    {
        var colorHandler = GetComponentInChildren<ColorHandler>();
        if (colorHandler != null)
        {
            colorHandler.SetColor(color);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_Target != null)
        {
            float distance = Vector3.Distance(m_Target.transform.position, transform.position);
            if (distance < 2.0f)
            {
                m_Agent.isStopped = true;
                //TowerInRange();
                //EnemyInRange();
            }
        }
    }

    /*
    public virtual void GoTo(Tower target)
    {
        m_Target = target;

        if (m_Target != null)
        {
            m_Agent.SetDestination(m_Target.transform.position);
            m_Agent.isStopped = false;
        }
    }

    public virtual void GoTo(Enemy enemyTarget)
    {
        m_EnemyTarget = enemyTarget;

        if (m_EnemyTarget != null)
        {
            m_Agent.SetDestination(m_EnemyTarget.transform.position);
            m_Agent.isStopped = false;
        }
    }
    */

    public virtual void GoTo(Vector3 position)
    {
        // if we order to go to a random point, we don't have a target anymore
        m_Target = null;
        m_Agent.SetDestination(position);
        m_Agent.isStopped = false;
    }

    /*
    protected abstract void TowerInRange();

    protected abstract void EnemyInRange();
    */
}
