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
    internal class SceneExplication : GameScreen
    {
        private Game1 _myGame;
        private Texture2D _textExplication;

        public SceneExplication(Game game) : base(game)
        {
            game = Game; 
        }
        public override void LoadContent()
        {
            _textExplication = Content.Load<Texture2D>("story");
            base.LoadContent();
        }



        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        public override void Draw(GameTime gameTime)
        {
           /* GraphicsDevice.Clear(Color.Black);
            _myGame.SpriteBatch.Begin();
            
            game.SpriteBatch.End();
            
            //throw new NotImplementedException()*/
        }
    }
}
