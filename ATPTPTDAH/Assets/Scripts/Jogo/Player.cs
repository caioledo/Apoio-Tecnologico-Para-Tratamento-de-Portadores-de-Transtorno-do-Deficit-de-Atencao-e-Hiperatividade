using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector2 entrada;
    private Rigidbody2D rb2d;
    private Animator animator;
    private Text text;
    private Text timerText;
    private bool highest = false;
    private bool up;
    private int hp = 2;
    private float timer = 0;
    private float timeLeft = 0;
    private bool invencible;
    public float velocidade = 0.228f;
    public float jumpPower = 200f;
    public bool grounded = true;
    public RectTransform painelGameOver;
    public Text gameOverText;
    public static int score = 0;
    public static int colectedStars = 0;

    void Start()
    {
        invencible = false;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        GameObject canvas;

        if (Menu.timer > 0)
        {
            timer = Menu.timer/2;
            timeLeft = (timer * 60) + 1;
        }
        else
        {
            canvas = GameObject.Find("TimerText");
            timerText = canvas.GetComponent<Text>();
            timerText.enabled = false;
        }

        canvas = GameObject.Find("Text");
        text = canvas.GetComponent<Text>();

        canvas = GameObject.Find("TimerText");
        timerText = canvas.GetComponent<Text>();
    }

    void Update()
    {
        if (Menu.timer > 0)
        {
            timeLeft -= Time.deltaTime;
            int timeInt = (int) timeLeft;
            System.TimeSpan ts = System.TimeSpan.FromSeconds(timeInt);
            timerText.text = "Time: " + ts.ToString(@"mm\:ss");

            if (timeLeft <= 0)
            {
                invencible = true;
                timeUp();
            }
        }

        animator.SetBool("grounded", grounded);

        if (Input.GetAxis("Horizontal") < -0.1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
        pulo();

        text.text = "Score: " + score.ToString();

        if (animator.IsInTransition(0) && !(animator.GetNextAnimatorStateInfo(0).IsName("Hurt")))
        {
            animator.SetBool("hurt", false);
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        entrada.x = h;
        mover(h);
    }

    private void mover(float h)
    {
        if (entrada != Vector2.zero)
        {
            animator.SetBool("correndo", true);
        }
        else
        {
            animator.SetBool("correndo", false);
        }

        rb2d.MovePosition(rb2d.position + (entrada * velocidade));
    }

    private void pulo()
    {
        if ((up || (Input.GetKeyDown("up") &&  grounded)) && !highest)
        {
            rb2d.gravityScale = -30;
            up = false;
        }
        else if (!Input.GetKeyDown("up") && !grounded && !highest)
        {
            rb2d.gravityScale -= 30;

            if (rb2d.gravityScale <= -138)
            {
                highest = true;
            }
        }
        else if (!grounded && highest)
        {
            rb2d.gravityScale += 10;

            if (Input.GetKeyDown("up") && rb2d.gravityScale >= 80)
            {
                up = true;
            }
        }
        else if (grounded)
        {
            rb2d.gravityScale = 65;
            highest = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Star")
        {
            score += 100;
            colectedStars++;
            print(score);
        }
    }

    public void dano(int dano)
    {
        if (!invencible)
        {
            hp--;

            if (hp > 0)
            {
                animator.SetBool("hurt", true);
                print("Você tem " + hp + " de hp restante");
            }
            else
            {
                animator.SetBool("dead", true);
                Destroy(gameObject, 0.8f);
                painelGameOver.gameObject.SetActive(true);
                gameOverText.gameObject.SetActive(true);
            }
        }
    }

    private void timeUp()
    {
        painelGameOver.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        Invoke("endGame", 3f);
    }

    private void endGame()
    {
        SceneManager.LoadScene("Video");
    }
}
