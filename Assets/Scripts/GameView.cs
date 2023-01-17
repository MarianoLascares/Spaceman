using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text coinsText, scoreText, maxScoreText;
    private PlayerController controler;
    // Start is called before the first frame update
    void Start()
    {
        controler = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.currentGameState == GameState.inGame)
        {
            int coins = GameManager.instance.collectedObject;
            float score = controler.GetTravelledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxscore", score);

            coinsText.text = coins.ToString();
            scoreText.text = "Score: "+score.ToString("f1");
            maxScoreText.text = "MaxScore: "+maxScore.ToString("f1");
        }
        if (GameManager.instance.currentGameState == GameState.gameOver)
        {
            int coins = GameManager.instance.collectedObject;
            float score = controler.GetTravelledDistance();

            coinsText.text = "Gold:" + coins.ToString();
            scoreText.text = "Score: \n" + score.ToString("f1");
        }
    }
}
