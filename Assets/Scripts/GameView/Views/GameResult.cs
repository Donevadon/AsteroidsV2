using CoreEngine.Entities.Objects.ControlledObjects.Player;
using UnityEngine;
using UnityEngine.UI;

namespace GameView.Views
{
    public class GameResult : MonoBehaviour, IGameResultView
    {
        [SerializeField] private GameCore core;
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject endGamePanel;
        
        void IGameResultView.OnPlayerDead(object sender)
        {
            endGamePanel.SetActive(true);
        }

        void IGameResultView.ScoreUpdate(int score)
        {
            scoreText.text = "Score: " + score;
        }

        public void Restart()
        {
            core.Restart();
            endGamePanel.SetActive(false);
        }
    }
}