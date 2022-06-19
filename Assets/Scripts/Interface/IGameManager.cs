using System;
public interface IGameManager
{ 
    IGameBoard GameBoard { get;}
    Action GameOver { get; set; }
    Action StartGame { get; set; }
    Action HitMade { get; set; }
    Action<float> UpdateHealth { get; set; }
}