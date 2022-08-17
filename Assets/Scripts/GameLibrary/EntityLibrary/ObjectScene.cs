using System.Numerics;

namespace GameLibrary.EntityLibrary
{
    /// <summary>
    /// Объект на сцене
    /// </summary>
    public class ObjectScene
    {
        /// <summary>
        /// Границы сцены
        /// </summary>
        public Vector2 Borders { get; set; } = new Vector2(9, 5);
        /// <summary>
        /// Отследить касание границы сцены,для переноса объекта
        /// Возвращает новые координаты если за границей и старые если  
        /// </summary>
        public Vector3 CheckBorder(Vector3 position)
        {
            if (position.X > Borders.X)
                position.X = -Borders.X;
            else if(position.X < -Borders.X)
                position.X = Borders.X;
            if (position.Y > Borders.Y)
                position.Y = -Borders.Y;
            else if(position.Y < -Borders.Y)
                position.Y = Borders.Y;
            return position;
        }
    }
}
