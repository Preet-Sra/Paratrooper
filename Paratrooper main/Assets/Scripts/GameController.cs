using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public int TropersInRightSide,TrooperInLeftSide;
    bool gameOver = false;
    private int ScoreCount;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] GameObject StartScreen;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] TMP_Text HighScoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        HighScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
        Application.targetFrameRate = 60;
    }

   public void StartGame()
    {
        GetComponent<AirTrafficController>().enabled = true;
        FindObjectOfType<CannonController>().enabled = true;
        StartScreen.SetActive(false);
    }

    public void IncreaseTrooper(bool RightSide)
    {
        if (RightSide)
        {
            TropersInRightSide++;
        }
        else
        {
            TrooperInLeftSide++;
        }
    }


    private void Update()
    {
        if (TropersInRightSide >= 3|| TrooperInLeftSide >= 3)
        {
            GetComponent<AirTrafficController>().enabled = false;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameEnd()
    {
        if (!gameOver)
        {
            gameOver = true;
            GameObject canon = GameObject.FindGameObjectWithTag("Canon");
            
            canon.GetComponent<CannonController>().enabled=false;
            canon.GetComponent<SpriteRenderer>().enabled = false;
            canon.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(ShowGameOverPanel());
        }
        
    }

    public void IncreaseScore(int amt)
    {
        ScoreCount += amt;
        ScoreText.text = ScoreCount.ToString();

        if (ScoreCount > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", ScoreCount);
        }
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(0.5f);
        GameOverPanel.SetActive(true);
        SoundManager.instance.PlaySmallBlastSound();
        yield return new WaitForSeconds(0.3f);
        SoundManager.instance.PlayGameOverSound();
        yield return new WaitForSeconds(3f);
        GameOverPanel.GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, 1f).SetEase(Ease.Linear);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
