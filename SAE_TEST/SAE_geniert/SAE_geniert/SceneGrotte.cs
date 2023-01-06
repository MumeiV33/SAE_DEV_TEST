using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using System;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
namespace SAE_geniert
{
    public class SceneGrotte : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public SceneGrotte(Game1 game) : base(game) { }


        public override void Initialize()
        {
            base.Initialize();

            Console.WriteLine("grotte");

        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {

        }

    }
}
