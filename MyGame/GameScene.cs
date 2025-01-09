using GameEngine;

namespace MyGame
{
    class GameScene : Scene
    {
        private int _score;
        private int _lives = 3;
        public GameScene()
        {
            Ship ship = new Ship();
            AddGameObject(ship);
            MeteorSpawner meteorSpawner = new MeteorSpawner();
            AddGameObject(meteorSpawner);
            Score score = new Score(new SFML.System.Vector2f(10.0f,10.0f));
            AddGameObject(score);

        }
        public int getScore() { return _score; }
        public void IncreaseScore()
        {
            _score++;
        }
        public int getLives() { return _lives; }
        public void decreaseLives()
        {
            --_lives;
            if(_lives == 0)
            {
                GameOverScene gameOverScene = new GameOverScene(_score);
                Game.SetScene(gameOverScene);
            }
        }
    }
}