using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;



namespace ScreenManager
{
    class Screen3 : Screen
    {
        #region ATTRIBUTS
        Rectangle button_credit;
        Rectangle endScreenRect;
        #endregion

        #region CONSTRUCTOR
        public Screen3(GraphicsDevice device)
            : base(device, "screen3")
        {
            button_credit = new Rectangle(350, 460, 100, 50);
            endScreenRect = new Rectangle(0, 0, 800, 600);
        }

        #endregion

        public override bool Init()
        {






            return base.Init();
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        #region METHODES


        #endregion

        #region DRAW & UPDATE
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _device.Clear(Color.Gainsboro);

            spriteBatch.Draw(FreadGame.Ressources.endScreen, endScreenRect, Color.White);
            spriteBatch.Draw(FreadGame.Ressources.button_credit, button_credit, Color.White);
            spriteBatch.DrawString(FreadGame.Ressources.myFont, " Bravo !! ", new Vector2(350, 90), Color.Black);// map text
            spriteBatch.DrawString(FreadGame.Ressources.myFont, " Vous avez gagne la DEMO de FREAD 2.5 ! ", new Vector2(120, 120), Color.Black);
            spriteBatch.DrawString(FreadGame.Ressources.myFont, " Temps mis : " + ScreenManager.Screen2.timerS.ToString("0.00") + " s !", new Vector2(255, 310), Color.Black);
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime, MouseState Mouse, KeyboardState keyboard)
        {

            if (button_credit.Contains(Mouse.X, Mouse.Y) && Mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if ((!(Process.GetProcessesByName("credit").Length > 0)))
                {
                    System.Diagnostics.Process.Start("credit.exe");
                }

            }

            base.Update(gameTime, Mouse, keyboard);
        }

        #endregion

    }
}