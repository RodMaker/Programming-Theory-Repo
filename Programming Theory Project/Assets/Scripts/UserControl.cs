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
            /*
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true); // activate it
                pooledProjectile.transform.position = transform.position; // position it at player
            }
            */
        }
    }

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
        } else if (m_Selected != null && Input.GetKeyDown(KeyCode.Q))
        {
            HandleAttack();
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
}
