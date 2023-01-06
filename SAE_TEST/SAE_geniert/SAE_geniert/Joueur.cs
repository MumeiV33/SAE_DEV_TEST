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
using System;
using MonoGame.Extended.Content;

namespace SAE_geniert
{
    internal class Joueur
    {
        //-----> Perso
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        private int _sensPerso;
        private int _vitessePerso;
    }
    //private bool IsCollision(ushort x, ushort y)
    //{
    //    // définition de tile qui peut être null (?)
    //    TiledMapTile? tile;
    //    if (mapLayer.TryGetTile(x, y, out tile) == false)
    //        return false;
    //    if (!tile.Value.IsBlank)
    //        return true;
    //    return false;
    //}
}
