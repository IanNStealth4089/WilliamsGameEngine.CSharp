using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Ship : GameObject
    {
        private static float xMomentum = 0;
        private static float yMomentum = 0;
        private const float accelFactor = 0.0003F;
        private const float decelFactor = 0.01F;
        private const int FireDelay = 20;
        private int _fireTimer;
        private readonly Sprite _sprite = new Sprite();
        // Creates our ship.
        public Ship()
        {
            _sprite.Texture = Game.GetTexture("Resources/ship.png");
            _sprite.Position = new Vector2f(50, 50);
        }
        // functions overridden from GameObject:
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            Vector2f pos = _sprite.Position; // had to add something

            FloatRect bounds = _sprite.GetGlobalBounds();
            float x = pos.X;
            float y = pos.Y;
            int msElapsed = elapsed.AsMilliseconds();
            int windowWidth = MyGame.WindowWidth;
            int windowHeight = MyGame.WindowHeight;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) { yMomentum -= accelFactor * msElapsed; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down)) { yMomentum += accelFactor * msElapsed; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) { xMomentum -= accelFactor * msElapsed; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) { xMomentum += accelFactor * msElapsed; }
            yMomentum *= (1 - decelFactor);
            xMomentum *= (1 - decelFactor);
            y += yMomentum * msElapsed;
            x += xMomentum * msElapsed;
            _sprite.Position = new Vector2f((x+windowWidth)%windowWidth, (y+windowHeight+bounds.Height/2.0f)%windowHeight-bounds.Height/2.0f);

            if (_fireTimer > 0) { _fireTimer -= msElapsed; }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && _fireTimer <= 0) {
                _fireTimer = FireDelay;
                float laserX = x + bounds.Width;
                float laserY = y + bounds.Height/2.0F;
                Laser topLaser = new Laser(new Vector2f(laserX, laserY));
                laserX = x + bounds.Width * 3F / 4F;
                laserY = y + bounds.Height * 3F / 4F;
                Laser middleLaser = new Laser(new Vector2f(laserX, laserY));
                laserY = y + bounds.Height / 4F;
                Laser lowLaser = new Laser(new Vector2f(laserX, laserY));
                Game.CurrentScene.AddGameObject(topLaser);
                Game.CurrentScene.AddGameObject(middleLaser);
                Game.CurrentScene.AddGameObject(lowLaser);
            }
        }
    }
}
