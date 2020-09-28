using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    public RoundData[] rodadas;
    private int indexRodada;
    private int playerHighScore;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //SceneManager.LoadScene("Questionario");
    }

    void Update()
    {
        
    }

    public void setRoundData(int round)
    {
        indexRodada = round;
    }

    public RoundData getCurrentRoundData()
    {
        return rodadas[indexRodada];
    }
}
