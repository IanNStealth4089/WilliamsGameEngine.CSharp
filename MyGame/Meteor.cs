using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Meteor : GameObject
    {
        private const float Speed = 0.1F;
        float slope = 0;
        Sprite _sprite = new Sprite();
        public Meteor(Vector2f pos, float slope)
        {
            _sprite.Texture = Game.GetTexture("Resources/meteor.png");
            _sprite.Position = pos;
            AssignTag("meteor");
            this.slope = slope;
            SetCollisionCheckEnabled(true);
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if (otherGameObject.HasTag("laser")){
                otherGameObject.MakeDead();
                GameScene gameScene = (GameScene)Game.CurrentScene;
                gameScene.IncreaseScore();
                Vector2f pos = _sprite.Position;
                pos.Y += _sprite.GetGlobalBounds().Height / 2.0F;
                pos.X += _sprite.GetGlobalBounds().Width / 2.0F;
                Explosion explosion = new Explosion(pos);
                Game.CurrentScene.AddGameObject(explosion);
            }
            MakeDead();
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            FloatRect bounds = _sprite.GetGlobalBounds();
            int msElapsed = elapsed.AsMilliseconds();
            Vector2f pos = _sprite.Position;
            pos.Y -= Speed * msElapsed * slope; 
            pos.X -= Speed * msElapsed;
            int windowHeight = MyGame.WindowHeight;
            if(pos.X < bounds.Width*-1)
            {
                GameScene scene = (GameScene)Game.CurrentScene;
                scene.decreaseLives();
                MakeDead();
            } else
            {
                _sprite.Position = new Vector2f(pos.X, (pos.Y + windowHeight + bounds.Height / 2.0F) % windowHeight - bounds.Height / 2.0F);
            }
        }
    }
}
