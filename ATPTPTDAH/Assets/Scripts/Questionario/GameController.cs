using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    private QuestionData[] questionPool;
    private DataController dataController;
    private RoundData jogo;
    private bool rodadaAtiva;
    private static int playerScore;
    private int chances;
    private int questionIndex;
    private int perguntasAcertadas;
    private bool perguntaRespondida;
    private int perguntaAtual;
    public Text textoPergunta;
    public Text textoPontuacao;
    public Text textoScore;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject painelPerguntas;
    public GameObject painelFimJogo;
    public static bool segundoJogo = false;

    List<int> valoresUsados = new List<int>();

    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        jogo = dataController.getCurrentRoundData();
        questionPool = jogo.perguntas;

        playerScore = Player.score;
        chances = Player.colectedStars;
        print(Player.colectedStars);
        perguntasAcertadas = 0;
        questionIndex = 0;
        perguntaRespondida = false;

        if (!segundoJogo)
        {
            showQuestion();
        }
        else
        {
            finalizaJogo();
        }

        rodadaAtiva = true;
    }

    void Update()
    {
        if (playerScore < 0)
        {
            playerScore = 0;
        }

        textoScore.text = "Score: " + playerScore.ToString();
    }

    private void showQuestion()
    {
        removeAnswerButtons();
        perguntaRespondida = false;
        int random = Random.Range(0, questionPool.Length);

        while (valoresUsados.Contains(random))
        {
            random = Random.Range(0, questionPool.Length);
        }

        QuestionData questionData = questionPool[random];
        valoresUsados.Add(random);
        perguntaAtual = random;
        textoPergunta.text = questionData.textoPergunta;

        for (int i = 0; i < questionData.respostas.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObjects.Add(answerButtonGameObject);
            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.setup(questionData.respostas[i]);
        }
    }

    private void repeatQuestion()
    {
        removeAnswerButtons();
        perguntaRespondida = false;

        QuestionData questionData = questionPool[perguntaAtual];
        textoPergunta.text = questionData.textoPergunta;

        for (int i = 0; i < questionData.respostas.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObjects.Add(answerButtonGameObject);
            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.setup(questionData.respostas[i]);
        }
    }

    private void removeAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void answerButtonClicked(bool isCorreto, GameObject clickedButton)
    {
        if (!perguntaRespondida)
        {
            perguntaRespondida = true;

            if (isCorreto)
            {
                playerScore += jogo.pontosPorAcerto;
                chances +=  5;
                perguntasAcertadas++;
                clickedButton.GetComponent<Image>().color = new Vector4(0, 255, 0, 255);
            }
            else
            {
                clickedButton.GetComponent<Image>().color = new Vector4(255, 0, 0, 255);

                if (chances > 0)
                {
                    chances -= 2;
                    print(chances);
                    playerScore -= 200;
                    Invoke("repeatQuestion", 2f);
                    goto pula;
                }
                else
                {
                    playerScore = 100;
                }
            }

            if (Menu.numeroPerguntas > questionIndex + 1)
            {
                questionIndex++;
                Invoke("showQuestion", 2f);
            }
            else
            {
                if (perguntasAcertadas < Menu.numeroPerguntas)
                {
                    Invoke("finalizaJogo", 2f);
                }
                else
                {
                    segundoJogo = true;
                    Player.score = playerScore;
                    Invoke("returnToGame", 2f);
                }
            }

            pula:;
        }
    }

    public void finalizaJogo()
    {
        Player.score = 0;
        rodadaAtiva = false;
        textoPontuacao.text += playerScore.ToString();
        painelPerguntas.SetActive(false);
        painelFimJogo.SetActive(true);
        perguntaRespondida = false;
        segundoJogo = false;
    }

    public void returnToGame()
    {
        SceneManager.LoadScene("TelaJogo");
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
