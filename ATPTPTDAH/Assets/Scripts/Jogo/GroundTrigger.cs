using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    private float pos;
    private Vector3 rightGround = new Vector3(0, -4.3599f, 0);
    private Vector3 leftGround = new Vector3(0, -4.3599f, 0);
    private bool inside = false;
    private bool saiu = false;
    private bool entrouDireita = false;
    private bool entrouEsquerda = false;
    private Quaternion r;
    private GameObject clone;
    public GameObject ground;

    void Start()
    {
        rightGround.x = (ground.transform.position.x + 63.06f);
        leftGround.x = (ground.transform.position.x - 42.04f);
    }

    void Update()
    {
        pos = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        
        if (pos < transform.position.x && inside)
        {
            if (clone != null)
            {

            }
            else
            {
                clone = (GameObject) Instantiate(GameObject.FindGameObjectWithTag("Ground"), rightGround, r);
            }

            destroyObjectAtLocation(leftGround, 1);
            inside = false;
            saiu = false;
            entrouEsquerda = true;
            print("loop 1");
        }
        else if (pos > transform.position.x && inside)
        {
            if (clone != null)
            {

            }
            else
            {
                clone = (GameObject)Instantiate(GameObject.FindGameObjectWithTag("Ground"), leftGround, r);
            }

            destroyObjectAtLocation(rightGround, 1);
            inside = false;
            saiu = false;
            entrouDireita = true;
            print("loop 2");
        }

        if (pos > transform.position.x && saiu && entrouDireita)
        {
            if (clone != null)
            {

            }
            else
            {
                clone = (GameObject)Instantiate(GameObject.FindGameObjectWithTag("Ground"), rightGround, r);
            }

            destroyObjectAtLocation(leftGround, 1);
            inside = false;
            saiu = false;
            entrouEsquerda = false;
            print("loop 1");
        }
        else if (pos < transform.position.x && saiu && entrouEsquerda)
        {
            if (clone != null)
            {

            }
            else
            {
                clone = (GameObject)Instantiate(GameObject.FindGameObjectWithTag("Ground"), leftGround, r);
            }

            destroyObjectAtLocation(rightGround, 1);
            inside = false;
            saiu = false;
            entrouDireita = false;
            print("loop 2");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "GroundCheck")
        {
            inside = true;
            saiu = false;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "GroundCheck")
        {
            inside = false;
            saiu = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "GroundCheck")
        {
            inside = false;
            saiu = true;
        }
    }


    private void destroyObjectAtLocation(Vector3 tmpLocation, float minDist)
    {
        Transform[] tiles = GameObject.FindObjectsOfType<Transform>();

        for (int i = 0; i < tiles.Length; i++)
        {
            if (Vector3.Distance(tiles[i].position, tmpLocation) <= minDist)
            {
                Destroy(tiles[i].gameObject);
            }
        }
    }
}
