using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameover = false;

    public Text scoreText;
    public GameObject gameoverUI;
    public static GameManager instance; //�̱���

    private int combo = 0;
    private int score = 0;
    private Text gameoverText;
    private Text bestScoreText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameoverText = gameoverUI.transform.GetChild(0).GetComponent<Text>(); //gameoverUI�� 2��° �ڽ��� bestScoreText
        bestScoreText = gameoverUI.transform.GetChild(2).GetComponent<Text>(); //gameoverUI�� 2��° �ڽ��� bestScoreText
    }

    void Update()
    {
        if(isGameover && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore()
    {
        if(!isGameover)
        {
            combo++;
            score += combo;
            scoreText.text = "Score : " + score;
        }
    }

    public int GetCombo()
    {
        print(combo);
        return combo;
    }

    public void ResetCombo()
    {
        combo = 0;
    }

    public void OnPlayerDead()
    {
        isGameover = true;
        int bestScore = PlayerPrefs.GetInt("BestScore"); //BestScore PlayerPrefs�� ��������

        if(bestScore < score) //bestScore�� �����ߴٸ�
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);

            gameoverText.text = "New BestScore!";
        }

        bestScoreText.text = "BestScore : " + bestScore;
        gameoverUI.SetActive(true);
    }

}
