using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace ScreenManager
{
    class Screen1 : Screen
    {

        #region ATTRIBUTS
        //ATTRIBUTS***********************************************************
        Rectangle imageHome;
        Rectangle button_play;
        Rectangle button_parametre;
        Rectangle button_credit;
        Rectangle button_title;
        #endregion

        #region CONSTRUCTOR & SCREEN_SPEC

        //CONSTRUCTOR**************************************************
        public Screen1(GraphicsDevice device)
            : base(device, "screen1")
        {
            button_play = new Rectangle(260, 400, 266, 83);
            imageHome = new Rectangle(200, 75, 400, 400);
            button_parametre = new Rectangle(610, 440, 142, 60);
            button_credit = new Rectangle(610, 520, 142, 60);
            button_title = new Rectangle(295, 75, 210, 62);
        }


        //METHODES************************************************************
        public override bool Init()
        {
            return base.Init();
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        #endregion

        #region UPDATE & DRAW

        // UPDATE AND DRAW *******************************************************


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _device.Clear(Color.Gainsboro);

            spriteBatch.Draw(FreadGame.Ressources.button_play, button_play, Color.White);
            spriteBatch.Draw(FreadGame.Ressources.imageHome, imageHome, Color.White);
            spriteBatch.Draw(FreadGame.Ressources.button_parametre, button_parametre, Color.White);
            spriteBatch.Draw(FreadGame.Ressources.button_credit, button_credit, Color.White);
            spriteBatch.Draw(FreadGame.Ressources.button_title, button_title, Color.White);

            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime, MouseState Mouse, KeyboardState keyboard)
        {
            // Check if m is pressed and go to screen2
            if ((button_play.Contains(Mouse.X, Mouse.Y) && Mouse.LeftButton == ButtonState.Pressed) || keyboard.IsKeyDown(Keys.Enter))
            {

                if (!(Process.GetProcessesByName("Paramètres").Length > 0) && (!(Process.GetProcessesByName("credit").Length > 0)))
                {
                    SCREEN_MANAGER.goto_screen("screen2");
                    FreadGame.GameMain.IsGameStart = true;//POUR FAIRE APPARAITRE LE PERSONNAGE
                }

            }

            if (button_parametre.Contains(Mouse.X, Mouse.Y) && Mouse.LeftButton == ButtonState.Pressed)
            {
                if (!(Process.GetProcessesByName("Paramètres").Length > 0) && (!(Process.GetProcessesByName("credit").Length > 0)))
                {
                    System.Diagnostics.Process.Start("Paramètres.exe");
                }

            }

            if (button_credit.Contains(Mouse.X, Mouse.Y) && Mouse.LeftButton == ButtonState.Pressed)
            {
                if (!(Process.GetProcessesByName("Paramètres").Length > 0) && (!(Process.GetProcessesByName("credit").Length > 0)))
                {
                    System.Diagnostics.Process.Start("credit.exe");
                }

            }
            base.Update(gameTime, Mouse, keyboard);
        }

        #endregion
    }
}
