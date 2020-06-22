using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] zombies;
    private bool isRising = false;
    private bool isFalling = false;
    public int RiseSpeed = 1;

    private int score;
    private int lives;
    private bool gameOver;
    private int level;

    private int ActiveZombieIndex = 0;
    private Vector2 StartPosition;

    public Image[] heart = new Image[3];

    public Text scoreText;

    public Button restart;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        score = 0;
        lives = 3;
        RiseSpeed = 1;
        level = 5;
        scoreText.text = score.ToString();
        restart.gameObject.SetActive(false);
        pickNewZombie();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            if (isRising)
            {
                if (zombies[ActiveZombieIndex].transform.position.y - StartPosition.y >= 3)
                {
                    isRising = false;
                    isFalling = true;
                }
                else
                {
                    zombies[ActiveZombieIndex].transform.Translate(Vector2.up * Time.deltaTime * RiseSpeed);
                }
            }
            else if (isFalling)
            {
                if (zombies[ActiveZombieIndex].transform.position.y - StartPosition.y <= 0)
                {
                    isFalling = false;
                    isRising = false;
                    lives--;
                    UpdateLife();
                    if (lives == 0)
                    {
                        gameOver = true;
                        restart.gameObject.SetActive(true);
                    }
                }
                else
                {
                    zombies[ActiveZombieIndex].transform.Translate(Vector2.down * Time.deltaTime * RiseSpeed);
                }
            }
            else
            {
                pickNewZombie();
            }
        }
        
    }

    private void UpdateLife()
    {
        heart[lives].gameObject.SetActive(false);
    }

    private void pickNewZombie()
    {
        isRising = true;
        isFalling = false;
        ActiveZombieIndex = UnityEngine.Random.Range(0, zombies.Length);
        StartPosition = zombies[ActiveZombieIndex].transform.position;
    }

    public void KillEnemy()
    {
        zombies[ActiveZombieIndex].transform.position = StartPosition;
        score++;
        if(score>=level)
        {
            RiseSpeed++;
            level *= 2;
        }
        scoreText.text = score.ToString(); 
        pickNewZombie();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
