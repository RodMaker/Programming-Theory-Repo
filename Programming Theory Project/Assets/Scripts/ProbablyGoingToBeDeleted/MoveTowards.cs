using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public float speed = 40.0f;

    //public Camera GameCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        transform.position = Vector3.MoveTowards(transform.position, Input.mousePosition, Time.deltaTime * speed);

        //transform.Translate(Vector3.back * Time.deltaTime * speed);
    }
}
