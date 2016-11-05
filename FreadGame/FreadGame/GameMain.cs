using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;


namespace FreadGame
{
    class GameMain
    {
        //Attributs
        Player LocalPlayer;

        static public bool IsGameStart;

        //Constructeur

        public GameMain()
        {
            LocalPlayer = new Player();
            IsGameStart = false;
        }

        //Methodes

        public void Draw(SpriteBatch spriteBatch)
        {
            LocalPlayer.Draw(spriteBatch);
        }
        public void Update(KeyboardState keyboard)
        {
            LocalPlayer.Update(keyboard);

        }
    }
}
