using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Menu : MonoBehaviour
{
    private string[] mediaExtentions = { ".avi", ".mp4", ".divx", ".wmv" };
    public Slider sliderPerguntas;
    public Slider sliderTimer;
    public Slider sliderVideo;
    public Button botao;
    public InputField espaco;
    public Text textoErro;
    public static int numeroPerguntas;
    public static float timer;
    public static float tempoVideo;
    public static string videoURL;
    
    private void Start()
    {
        botao.onClick.AddListener(clicaBotao);
        espaco.onEndEdit.AddListener(SubmitName);
        textoErro.color = Color.red;
        textoErro.text = "";
    }

    private void Update()
    {
        numeroPerguntas = (int) sliderPerguntas.value;
        timer = sliderTimer.value;
        tempoVideo = sliderVideo.value;
    }

    private void SubmitName(string arg0)
    {
        videoURL = arg0.ToString();
    }

    private bool isMediaFile(string path)
    {
        return -1 != System.Array.IndexOf(mediaExtentions, Path.GetExtension(path).ToLowerInvariant());
    }

    private void clicaBotao()
    {
        if (tempoVideo > 0 && (videoURL == null || !File.Exists(videoURL) || !isMediaFile(videoURL)))
        {
            //MessageBox.Show("URL inválida", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            textoErro.text = "URL inválida";
        }
        else
        {
            SceneManager.LoadScene("TelaJogo");
        }
    }
}
