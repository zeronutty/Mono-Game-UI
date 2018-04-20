using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace UiTesting.Source
{
    public enum Fonts_Types
    {

    }

    public enum Texture_Types
    {

    }

    public static class ContentLoader
    {
        #region Fields
        private static ContentManager p_ContentManager;

        private static Dictionary<string, SpriteFont> p_Fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, Texture2D> p_Textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, Video> p_Videos = new Dictionary<string, Video>();

        private static Dictionary<string, Rectangle[]> p_SlicedTextures = new Dictionary<string, Rectangle[]>();

        private static string p_TextureNotFound = "NullTexture";
        private static string p_FontNotFound = "NullFont";
        private static string p_VideoNotFound = "NullVideo";
        #endregion

        #region Methods

        public static SpriteFont GetFontByName(string font_Types)
        {
            SpriteFont font;
            p_Fonts.TryGetValue(font_Types, out font);

            return font;
        }

        public static Texture2D GetTextureByName(string texture_Types)
        {
            Texture2D texture;
            p_Textures.TryGetValue(texture_Types, out texture);

            return texture;
        }

        public static Video GetVideoByName(string video_Types)
        {
            Video video;
            p_Videos.TryGetValue(video_Types, out video);

            return video;
        }

        private static Texture2D LoadTexture(string _textureName)
        {
            if(!p_Textures.ContainsKey(_textureName))
            {
                try
                {
                    Texture2D texture = p_ContentManager.Load<Texture2D>(Path.Combine("Textures", _textureName));
                    p_Textures.Add(_textureName, texture);
                    return texture;
                }
                catch(Exception) when (_textureName != p_TextureNotFound)
                {
                    return LoadTexture(p_TextureNotFound);
                }
            }

            return GetTextureByName(_textureName);
        }

        private static SpriteFont LoadFont(string _fontName)
        {
            if (!p_Textures.ContainsKey(_fontName))
            {
                try
                {
                    SpriteFont texture = p_ContentManager.Load<SpriteFont>(Path.Combine("Fonts", _fontName));
                    p_Fonts.Add(_fontName, texture);
                    return texture;
                }
                catch (Exception) when (_fontName != p_FontNotFound)
                {
                    return LoadFont(p_FontNotFound);
                }
            }

            return GetFontByName(_fontName);
        }

        private static Video LoadVideo(string _videoName)
        {
            if(!p_Videos.ContainsKey(_videoName))
            {
                try
                {
                    Video video = p_ContentManager.Load<Video>(Path.Combine("Videos", _videoName));
                    p_Videos.Add(_videoName, video);
                }
                catch(Exception) when (_videoName != p_VideoNotFound)
                {
                    return LoadVideo(p_VideoNotFound);
                }
            }

            return GetVideoByName(_videoName);
        }

        private static void SliceTextures()
        {
            foreach(KeyValuePair<string, Texture2D> e in p_Textures)
            {
                Rectangle[] sliced = TextureUtils.CreatePatches(e.Value.Bounds, 10, 10, 10, 10);
                p_SlicedTextures.Add(e.Key, sliced);
            }
        }

        public static void LoadContent(ContentManager contentManager)
        {
            p_ContentManager = contentManager;

            try
            {
                string[] files = Directory.GetFiles(Path.Combine(contentManager.RootDirectory, "Textures"));
                for(int i = 0; i < files.Length; i++)
                {
                    string temp = files[i].Remove(0, "Content\\Textures\\".Length);
                    LoadTexture(Path.GetFileNameWithoutExtension(temp));
                }

                files = Directory.GetFiles(Path.Combine(contentManager.RootDirectory, "Fonts"));
                for (int i = 0; i < files.Length; i++)
                {
                    string temp = files[i].Remove(0, "Content\\Fonts\\".Length);
                    LoadFont(Path.GetFileNameWithoutExtension(temp));
                }

                files = Directory.GetFiles(Path.Combine(contentManager.RootDirectory, "Videos"));
                for(int i = 0; i < files.Length; i++)
                {
                    string temp = files[i].Remove(0, "Content\\Videos\\".Length);
                    LoadVideo(Path.GetFileNameWithoutExtension(temp));
                }

                SliceTextures();
            }
            catch(Exception)
            {
                Console.WriteLine("FailedLoading");
                return;
            }
        }

        public static void UnloadContent()
        {

        }

        #endregion
    }
}
