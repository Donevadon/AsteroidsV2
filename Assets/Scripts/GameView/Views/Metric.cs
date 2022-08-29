using System;
using CoreEngine.Core;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = System.Numerics.Vector2;

namespace GameView.Views
{
    public class Metric : MonoBehaviour, IMetricView
    {
        [SerializeField] private Text positionText;
        [SerializeField] private Text angleText;
        [SerializeField] private Text speedText;
        [SerializeField] private Text laserTimeText;
        [SerializeField] private Text laserCountText;

        private void OnUpdatePosition(Vector2 position)
        {
            positionText.text = $"Position: " + position;
        }

        private void OnUpdateAngle(float angle)
        {
            angleText.text = "Angle: " + angle;
        }

        private void OnUpdateSpeed(float speed)
        {
            speedText.text = "Speed: " + speed;
        }

        private void OnUpdateLaserCount(int count)
        {
            laserCountText.text = "Laser count: " + count;
        }

        private void OnLaserRollbackTime(TimeSpan time)
        {
            laserTimeText.text = "Laser time: " + time.Seconds + " sec";
        }

        public void Subscribe(IMetricSource source)
        {
            source.LaserReloaded += OnUpdateLaserCount;
            source.PositionChanged += OnUpdatePosition;
            source.RotationChanged += OnUpdateAngle;
            source.SpeedChanged += OnUpdateSpeed;
            source.LaserTimeUpdated += OnLaserRollbackTime;
            source.Destroyed += Unsubscribe;
            ResetValues(source);
        }

        private void Unsubscribe(object obj)
        {
            var source = obj as IMetricSource ?? throw new ArgumentException();
            source.LaserReloaded -= OnUpdateLaserCount;
            source.PositionChanged -= OnUpdatePosition;
            source.RotationChanged -= OnUpdateAngle;
            source.SpeedChanged -= OnUpdateSpeed;
            source.LaserTimeUpdated -= OnLaserRollbackTime;
            source.Destroyed -= Unsubscribe;
        }

        private void ResetValues(IMetricSource source)
        {
            OnUpdateAngle(source.Angle);
            OnUpdatePosition(source.Position);
            OnUpdateSpeed(default);
            OnLaserRollbackTime(default);
            OnUpdateLaserCount(default);
        }
    }
}