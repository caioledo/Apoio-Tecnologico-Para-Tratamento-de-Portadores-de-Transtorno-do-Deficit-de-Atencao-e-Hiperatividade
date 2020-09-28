using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    private AnswerData dadosResposta;
    private GameController gameController;
    public Text textoResposta;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        
    }

    public void setup(AnswerData data)
    {
        dadosResposta = data;
        textoResposta.text = dadosResposta.textoResposta;
    }

    public void handleClick()
    {
        gameController.answerButtonClicked(dadosResposta.isCorreta, gameObject);
    }
}
