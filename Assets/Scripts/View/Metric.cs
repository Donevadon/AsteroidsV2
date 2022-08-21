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

        public void UpdatePosition(Vector2 position)
        {
            this.position.text = $"Position: " + position;
        }

        public void UpdateAngle(float angle)
        {
            this.angle.text = "Angle: " + angle;
        }

        public void UpdateSpeed(float speed)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateLaserCount(int count)
        {
            throw new System.NotImplementedException();
        }

        public void LaserRollbackTime(int time)
        {
            throw new System.NotImplementedException();
        }
    }
}