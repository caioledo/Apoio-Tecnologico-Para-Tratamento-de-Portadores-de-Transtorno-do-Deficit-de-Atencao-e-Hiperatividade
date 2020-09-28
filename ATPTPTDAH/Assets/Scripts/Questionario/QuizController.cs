using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{
    private DataController dados;

    void Start()
    {
        dados = FindObjectOfType<DataController>();
    }

    void startGame()
    {
        //dados.setRoundData(round);
        SceneManager.LoadScene("Questionario");
    }
}
