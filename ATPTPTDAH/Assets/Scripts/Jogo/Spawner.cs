using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public Transform followObject;
    private LoadSceneMode mode;
    private GameObject item;
    private Vector3 pos;
    private int starOrBomb;
    // variavel correspondente à distância do objeto em que bombas podem surgir
    private readonly float MaxDistance = 21.02f;
    // variaveis correspondentes ao range minimo e máximo para spawn de bombas
    private float xMin;
    private float xMax;
    // variável correspondente à posição no eixo x de cada bomba
    private float xItem;
    // variavel responsável pela velocidade de spawn das bombas
    private float TimeBetweenItems = 2f; //2
    private float DropRatioTime = 8f;

    void Start()
    {
        InvokeRepeating("CreateItem", 1f, 0.5f);
        /*InvokeRepeating("CreateItem", DropRatioTime, TimeBetweenItems);
        InvokeRepeating("CreateItem", DropRatioTime * 2, TimeBetweenItems * 1.16f);
        InvokeRepeating("CreateItem", DropRatioTime * 3, TimeBetweenItems * 1.17f);
        InvokeRepeating("CreateItem", DropRatioTime * 4, TimeBetweenItems * 1.18f);
        InvokeRepeating("CreateItem", DropRatioTime * 5, TimeBetweenItems * 1.18f);
        InvokeRepeating("CreateItem", DropRatioTime * 6, TimeBetweenItems * 1.18f);
        InvokeRepeating("CreateItem", DropRatioTime * 7, TimeBetweenItems * 1.18f);
        InvokeRepeating("CreateItem", DropRatioTime * 8, TimeBetweenItems * 1.18f);
        InvokeRepeating("CreateItem", DropRatioTime * 9, TimeBetweenItems * 1.18f);
        InvokeRepeating("CreateItem", DropRatioTime * 10, TimeBetweenItems * 1.18f);
        InvokeRepeating("CreateItem", DropRatioTime * 11, TimeBetweenItems * 1.18f);
        InvokeRepeating("CreateItem", DropRatioTime * 12, TimeBetweenItems * 1.18f);*/
    }

    void Update()
    {
        if (followObject == null)
        {
            Invoke("trocaCena", 4);
        }
        
        // O gameObject do spawner vai seguir o personagem
        pos = new Vector3(followObject.position.x, transform.position.y, transform.position.z);
        transform.position = pos;


        TimeBetweenItems *= 0.9999999f;
        InvokeRepeating("CreateItem", DropRatioTime, TimeBetweenItems);
        DropRatioTime += (DropRatioTime / 3); //3
        
    }

    private void trocaCena()
    {
        SceneManager.LoadScene("Video");
    }

    private void CreateItem() {
        // Vamos setar as o range de alcance do spawner e as posições das bombas
        xMin = pos.x - MaxDistance;
        xMax = pos.x + MaxDistance;
        xItem = Random.Range(xMin, xMax);

        starOrBomb = (int)Random.Range(1, 6);
        if (starOrBomb == 1)
        {
            item = (GameObject)Instantiate(Resources.Load("Star"), new Vector3(xItem, transform.position.y, transform.position.z), Quaternion.identity);
        }
        else
        {
            item = (GameObject)Instantiate(Resources.Load("Bomb"), new Vector3(xItem, transform.position.y, transform.position.z), Quaternion.identity);
        }
        
        //print(starOrBomb);
    }
}
