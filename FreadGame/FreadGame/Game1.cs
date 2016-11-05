using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace FreadGame
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameMain Main;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";

        }


        protected override void Initialize()
        {
            IsMouseVisible = true;

            ScreenManager.SCREEN_MANAGER.add_screen(new ScreenManager.Screen1(GraphicsDevice));
            ScreenManager.SCREEN_MANAGER.add_screen(new ScreenManager.Screen2(GraphicsDevice));
            ScreenManager.SCREEN_MANAGER.add_screen(new ScreenManager.Screen3(GraphicsDevice));

            ScreenManager.SCREEN_MANAGER.goto_screen("screen1");

            base.Initialize();
            Ressources.ListenMusicHome();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.LoadContent(Content);

            ScreenManager.SCREEN_MANAGER.Init();
            Main = new GameMain();     

        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            Main.Update(Keyboard.GetState());

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            ScreenManager.SCREEN_MANAGER.Update(gameTime, Mouse.GetState(), Keyboard.GetState());
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); //COMMANDE OBLIGE POUR QUE LE PROGRAMME NE PLANTE PAS
            ScreenManager.SCREEN_MANAGER.Draw(gameTime, spriteBatch);
            if (GameMain.IsGameStart == true)
            {
                Main.Draw(spriteBatch);// POUR AFFICHER LE PERSONNAGE
            }

            spriteBatch.End();//COMMANDE OBLIGE POUR QUE LE PROGRAMME NE PLANTE PAS



            base.Draw(gameTime);
        }
    }
}
