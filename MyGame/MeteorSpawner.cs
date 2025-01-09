using GameEngine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class MeteorSpawner : GameObject
    {
        private const int SpawnDelay = 1000;
        private int _timer;
        public override void Update(Time elapsed)
        {
            int msElapsed = elapsed.AsMilliseconds();
            _timer -= msElapsed;
            if (_timer <= 0)
            {
                _timer = SpawnDelay;
                Vector2u size = Game.RenderWindow.Size;
                float meteorX = size.X + 100;
                float meteorY = Game.Random.Next() % size.Y;
                float meteorM = (Game.Random.Next() % 10F) / 20F - 0.25F;
                Meteor meteor = new Meteor(new Vector2f(meteorX, meteorY), meteorM);
                Game.CurrentScene.AddGameObject(meteor);
            }
        }
    }
}
