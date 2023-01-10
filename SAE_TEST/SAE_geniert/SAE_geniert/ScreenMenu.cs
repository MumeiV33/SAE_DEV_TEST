using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using SAE_geniert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_geniert
{


    public class ScreenMenu : GameScreen
    {
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1
        private new Game1 _myGame;

        // texture du menu avec 3 boutons
        private Texture2D _textBoutons;

        // contient les rectangles : position et taille des 3 boutons présents dans la texture 
        private Rectangle[] lesBoutons;



        public ScreenMenu(Game1 game) : base(game)
        {
            _myGame = game;
            lesBoutons = new Rectangle[3];
            lesBoutons[0] = new Rectangle(700, 365, 190, 70);
            lesBoutons[1] = new Rectangle(300, 265, 190, 70);//oui bouton start
            lesBoutons[2] = new Rectangle(300, 365, 190, 70); // oui exit

        }
        public override void LoadContent()
        {
            _textBoutons = Content.Load<Texture2D>("Menu_Avec_Nom_STart");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {

            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < lesBoutons.Length; i++)
                {
                    // si le clic correspond à un des 3 boutons
                    if (lesBoutons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        // on change l'état défini dans Game1 en fonction du bouton cliqué
                        if (i == 0)
                            _myGame.Etat = Game1.Etats.Controls;
                        else if (i == 1)
                            _myGame.Etat = Game1.Etats.Play;
                        else
                            _myGame.Etat = Game1.Etats.Quit;
                        break;
                    }

                }
            }




        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textBoutons, new Vector2(0, 0), Color.White);
            _myGame.SpriteBatch.End();

        }
    }

}
