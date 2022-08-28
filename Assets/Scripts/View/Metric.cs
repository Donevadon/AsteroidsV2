using System;
using CoreEngine.Core;
using CoreEngine.Entities.Objects.ControlledObjects.Player;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = System.Numerics.Vector2;

namespace View
{
    public class Metric : MonoBehaviour, IMetricView
    {
        [SerializeField] private Text positionText;
        [SerializeField] private Text angleText;
        [SerializeField] private Text speedText;
        [SerializeField] private Text laserTimeText;
        [SerializeField] private Text laserCountText;
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject endGame;


        public void OnUpdatePosition(Vector2 position)
        {
            positionText.text = $"Position: " + position;
        }

        public void OnUpdateAngle(float angle)
        {
            angleText.text = "Angle: " + angle;
        }

        public void OnUpdateSpeed(float speed)
        {
            speedText.text = "Speed: " + speed;
        }

        public void OnUpdateLaserCount(int count)
        {
            laserCountText.text = "Laser count: " + count;
        }

        public void OnLaserRollbackTime(TimeSpan time)
        {
            laserTimeText.text = "Laser time: " + time.Seconds + " sec";
        }

        public void OnPlayerDead(object sender)
        {
            endGame.SetActive(true);
        }

        public void ScoreUpdate(int score)
        {
            scoreText.text = "Score: " + score;
        }
    }
}