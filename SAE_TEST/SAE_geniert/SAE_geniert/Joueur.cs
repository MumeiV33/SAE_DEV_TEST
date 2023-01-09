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
    public class Joueur
    {

        private GraphicsDeviceManager _graphics;
        private TiledMap _tiledMap;
        private TiledMap _tiledMapTest;
        private TiledMap _tiledMapTestFin;
        private TiledMap _tiledMapRendererTestFin;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer mapLayer;
        private TiledMapTileLayer mapLayerTest;

        public Vector2 _positionPerso;
        public AnimatedSprite _perso;
        public int _sensPerso;
        public int _vitessePerso;

        //-----> Autres
        private KeyboardState _keyboardState;
        private SpriteBatch _spriteBatch;

        public void LoadContentPerso()
        {
            _vitessePerso = 100;

        }

        public void DeplacementsPerso(float deltaSeconds)
        {
            // TODO: Add your update logic here

            //-----------------Déplacements-------------------------------------------------------------------
            
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();
            _perso.Update(deltaSeconds); // time écoulé

            _keyboardState = Keyboard.GetState();
            //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^

            //-=-=-=-=-=-=-=-=-=-DROITE-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------------------------------------------------------------
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.8);
                //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^



                if (!IsCollision(tx, ty))
                    _positionPerso.X += walkSpeed;
                _perso.Play("walkEast");


            }
            //-=-=-=-=-=-=-=-=-=-GAUCHE-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------------------------------------------------------------
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.5);
                //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^

                if (!IsCollision(tx, ty))
                    _positionPerso.X -= walkSpeed;
                _perso.Play("walkWest");
            }
            //-=-=-=-=-=-=-=-=-=-HAUT-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------------------------------------------------------------
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);

                if (_keyboardState.IsKeyDown(Keys.Left))
                    _perso.Play("walkWest");
                else if (_keyboardState.IsKeyDown(Keys.Right))
                    _perso.Play("walkEast");
                else
                    _perso.Play("walkNorth");
                //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^



                if (!IsCollision(tx, ty))
                    _positionPerso.Y -= walkSpeed;

            }
            //-=-=-=-=-=-=-=-=-=-BAS-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------------------------------------------------------------

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);

                if (_keyboardState.IsKeyDown(Keys.Left))
                    _perso.Play("walkWest");
                else if (_keyboardState.IsKeyDown(Keys.Right))
                    _perso.Play("walkEast");
                else
                    _perso.Play("walkSouth");
                //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^
                if (!IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
            }
        }
        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
    }
}

