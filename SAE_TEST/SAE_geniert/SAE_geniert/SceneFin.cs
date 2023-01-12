using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;

using System.Windows;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
namespace SAE_geniert
{
    internal class SceneFin : GameScreen
    {
        private Game1 _myGame;
        private Texture2D _textExplication;

        public SceneFin(Game1 game) : base(game)
        {
            //game = Game;
            _myGame = game;
        }
        public override void LoadContent()
        {
            _textExplication = Content.Load<Texture2D>("story_de_fin");
            base.LoadContent();
        }



        public override void Update(GameTime gameTime)
        {
          
        }
        public override void Draw(GameTime gameTime)
        {
            /* GraphicsDevice.Clear(Color.Black);
             _myGame.SpriteBatch.Begin();

             game.SpriteBatch.End();

             //throw new NotImplementedException()*/
            GraphicsDevice.Clear(Color.Orange);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textExplication, new Vector2(0, 0), Color.White);
            _myGame.SpriteBatch.End();
        }
    }
}
