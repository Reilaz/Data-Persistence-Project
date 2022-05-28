using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    //public static MainManager Instance;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScore;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    public GameObject restart;
    public string name;

    // Start is called before the first frame update
    void Start()
    {
        name = Data.Instance.name;
        Debug.Log(name + " variable local");
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        BestScore.text = "Best Score: " + Data.Instance.namePlayer + " : " + Data.Instance.maxScore;
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        //Data.Instance.maxScore = m_Points;
        //Debug.Log(Data.Instance.maxScore);
        ScoreText.text = $"Score : {m_Points}";
        if (m_Points > Data.Instance.maxScore)
        {
            Debug.Log("points es mayo al guardado");
            Data.Instance.namePlayer = name;
            Data.Instance.maxScore = m_Points;
            Data.Instance.SaveMaxScore();
        }
        
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        restart.SetActive(true);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
