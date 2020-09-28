using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //private Vector2 position;
    public Transform followObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(followObject.position.x, transform.position.y, transform.position.z);
        transform.position = pos;
        //position.y = -0.6f;
        //transform.localPosition = -0.6f;
        //transform.localRotation.y = -0.6;
    }
}
