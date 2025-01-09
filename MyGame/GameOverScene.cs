using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class GameOverScene : Scene
    {
        public GameOverScene(int score)
        {
            GameOverText gameOverText = new GameOverText(score);
            AddGameObject(gameOverText);
        }
    }
}
