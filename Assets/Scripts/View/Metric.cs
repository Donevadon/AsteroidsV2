using System;
using CoreEngine.Core;
using CoreEngine.Entities.Objects.Factory;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = System.Numerics.Vector2;

namespace View
{
    public class Metric : MonoBehaviour, IMetricView
    {
        [SerializeField] private Text position;
        [SerializeField] private Text angle;
        [SerializeField] private Text speed;
        [SerializeField] private Text laserTime;
        [SerializeField] private Text laserCount;
        [SerializeField] private Text score;
        [SerializeField] private GameObject endGame;


        public void OnUpdatePosition(Vector2 position)
        {
            this.position.text = $"Position: " + position;
        }

        public void OnUpdateAngle(float angle)
        {
            this.angle.text = "Angle: " + angle;
        }

        public void OnUpdateSpeed(float speed)
        {
            this.speed.text = "Speed: " + speed;
        }

        public void OnUpdateLaserCount(int count)
        {
            laserCount.text = "Laser count: " + count;
        }

        public void OnLaserRollbackTime(TimeSpan time)
        {
            laserTime.text = "Laser time: " + time.Seconds + " sec";
        }

        public void OnPlayerDead(IObject sender)
        {
            endGame.SetActive(true);
        }

        public void ScoreUpdate(int score)
        {
            this.score.text = "Score: " + score;
        }
    }
}