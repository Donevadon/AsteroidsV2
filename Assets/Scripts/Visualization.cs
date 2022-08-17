using GameLibrary;
using UnityEngine;

public class Visualization : MonoBehaviour
{
    public static bool is3D = true;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    /// <summary>
    /// Получить направление движения
    /// </summary>
    /// <returns>Направление, необходимое для выбранной визуализации</returns>
    public static Vector3 GetDirection()
    {
        return is3D ? Vector3.forward : Vector3.up;
    }
    /// <summary>
    /// Получить случайное направление движения
    /// </summary>
    /// <returns>Случайное направление, необходимое для выбранной визуализации</returns>
    public static Vector3 GetRandomEuler()
    {
        return is3D ? new Vector3(Random.Range(0, 360), 90, -90) : new Vector3(0, 0, Random.Range(0, 360));
    }
    /// <summary>
    /// Получить вектор вращения
    /// </summary>
    /// <returns>Вектор вращения, необходимый для выбранной визуализации</returns>
    public static Vector3 GetRotateVector()
    {
        return is3D ? -Vector3.left : -Vector3.forward;
    }
    /// <summary>
    /// Получить измененные углы эйлера
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns>Измененные угры эйлера с одной визуализации на другую</returns>
    public static Vector3 GetEuler(Vector3 rotation)
    {
        return is3D ? new Vector3((rotation.z + 90) * -1, 90, -90) : new Vector3(0,0,(rotation.x +90) * -1);
    }
    /// <summary>
    /// Изменить визуализацию
    /// </summary>
    public void ChangeVisualization()
    {
        is3D = !is3D;
        gameManager.ReloadEntity();
    }
}

