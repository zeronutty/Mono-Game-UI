using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UiTesting.Source
{
    public class DrawUtils
    {
        private Stack<RenderTarget2D> p_RenderTargets = new Stack<RenderTarget2D>();

        RenderTarget2D p_LastRenderTarget = null;

        public virtual Color FixColorOpacity(Color? color)
        {
            return FixColorOpacity(color ?? Color.White);
        }

        public virtual Color FixColorOpacity(Color color)
        {
            return color *= (color.A / 255);
        }

        public void PushRenderTarget(RenderTarget2D target)
        {
            if(!UiManager.GetActiveUserInterface().UseRenderTarget)
            {
                
            }

            p_RenderTargets.Push(target);
        }

        public void PopRenderTargets()
        {
            p_RenderTargets.Pop();
        }

        public virtual void StartDraw(SpriteBatch spriteBatch, bool isDisabled)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, UiManager.GetActiveUserInterface().BlendState, UiManager.GetActiveUserInterface().SamplerState,
                DepthStencilState.None, RasterizerState.CullCounterClockwise);

            UpdateRenderTarget(spriteBatch);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture, Rectangle DistinRect, Rectangle ScourceRect, Color color)
        {
            Rectangle[] ScoutRects = TextureUtils.CreatePatches(texture.Bounds, 5, 5, 5, 5);
            Rectangle[] DistRects = TextureUtils.CreatePatches(DistinRect, 5, 5, 5, 5);

            for (int i = 0; i < ScoutRects.Length; i++)
            {
                spriteBatch.Draw(texture, DistRects[i], ScoutRects[i], color);
            }
        }
        
        public virtual void EndDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
        }

        protected virtual void UpdateRenderTarget(SpriteBatch spriteBatch)
        {
            RenderTarget2D newRenderTarget = null;
            if (p_RenderTargets.Count > 0)
            {
                newRenderTarget = p_RenderTargets.Peek();
            }
            else
            {
                newRenderTarget = UiManager.GetActiveUserInterface().RenderTarget;
            }

            if (p_LastRenderTarget != newRenderTarget)
            {
                p_LastRenderTarget = newRenderTarget;
                spriteBatch.GraphicsDevice.SetRenderTarget(p_LastRenderTarget);
            }
        }
    }
}
