using System;

namespace NewScript.Player
{
    public interface IPlayerController
    {
        event Action<float> Move;
        event Action<float> Rotate;
    }
}