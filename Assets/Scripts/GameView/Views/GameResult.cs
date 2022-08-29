using System;
using CoreEngine.Core;
using UnityEngine;
using UnityEngine.UI;

namespace GameView.Views
{
    public class GameResult : MonoBehaviour, IGameResultView
    {
        [SerializeField] private GameCore core;
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject endGamePanel;

        private void OnPlayerDead(object sender)
        {
            endGamePanel.SetActive(true);
        }

        private void ScoreUpdate(int score)
        {
            scoreText.text = "Score: " + score;
        }

        public void Restart()
        {
            ScoreUpdate(default);
            core.Restart();
            endGamePanel.SetActive(false);
        }

        public void Subscribe(IGameProcess player)
        {
            player.Destroyed += Unsubscribe;
            player.ScoreAdded += ScoreUpdate;
            player.Destroyed += OnPlayerDead;
        }

        private void Unsubscribe(object obj)
        {
            var player = obj as IGameProcess ?? throw new ArgumentException();
            player.ScoreAdded -= ScoreUpdate;
            player.Destroyed -= OnPlayerDead;
            player.Destroyed -= Unsubscribe;
        }
    }
}