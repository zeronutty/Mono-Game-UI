using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using UiTesting.Source.UI.Components;

namespace UiTesting.Source.UI.Rendering
{
    public enum BlendingMode
    {
        Default,
        None,
        Additive,
    }

    #region Classes

    public class DeviceStates
    {
        public readonly BlendState BlendState;
        public readonly RasterizerState RasterizerState;
        public readonly DepthStencilState DepthStencilState;
        public readonly SamplerState SamplerState;

        public DeviceStates()
        {
            BlendState = new BlendState();
            BlendState.AlphaBlendFunction = BlendState.AlphaBlend.AlphaBlendFunction;
            BlendState.AlphaDestinationBlend = BlendState.AlphaBlend.AlphaDestinationBlend;
            BlendState.AlphaSourceBlend = BlendState.AlphaBlend.AlphaSourceBlend;
            BlendState.BlendFactor = BlendState.AlphaBlend.BlendFactor;
            BlendState.ColorBlendFunction = BlendState.AlphaBlend.ColorBlendFunction;
            BlendState.ColorDestinationBlend = BlendState.AlphaBlend.ColorDestinationBlend;
            BlendState.ColorSourceBlend = BlendState.AlphaBlend.ColorSourceBlend;
            BlendState.ColorWriteChannels = BlendState.AlphaBlend.ColorWriteChannels;
            BlendState.ColorWriteChannels1 = BlendState.AlphaBlend.ColorWriteChannels1;
            BlendState.ColorWriteChannels2 = BlendState.AlphaBlend.ColorWriteChannels2;
            BlendState.ColorWriteChannels3 = BlendState.AlphaBlend.ColorWriteChannels3;
            BlendState.MultiSampleMask = BlendState.AlphaBlend.MultiSampleMask;

            RasterizerState = new RasterizerState();
            RasterizerState.CullMode = RasterizerState.CullNone.CullMode;
            RasterizerState.DepthBias = RasterizerState.CullNone.DepthBias;
            RasterizerState.FillMode = RasterizerState.CullNone.FillMode;
            RasterizerState.MultiSampleAntiAlias = RasterizerState.CullNone.MultiSampleAntiAlias;
            RasterizerState.ScissorTestEnable = RasterizerState.CullNone.ScissorTestEnable;
            RasterizerState.SlopeScaleDepthBias = RasterizerState.CullNone.SlopeScaleDepthBias;

            RasterizerState.ScissorTestEnable = true;

            SamplerState = new SamplerState();
            SamplerState.AddressU = SamplerState.AnisotropicClamp.AddressU;
            SamplerState.AddressV = SamplerState.AnisotropicClamp.AddressV;
            SamplerState.AddressW = SamplerState.AnisotropicClamp.AddressW;
            SamplerState.Filter = SamplerState.AnisotropicClamp.Filter;
            SamplerState.MaxAnisotropy = SamplerState.AnisotropicClamp.MaxAnisotropy;
            SamplerState.MaxMipLevel = SamplerState.AnisotropicClamp.MaxMipLevel;
            SamplerState.MipMapLevelOfDetailBias = SamplerState.AnisotropicClamp.MipMapLevelOfDetailBias;

            DepthStencilState = new DepthStencilState();
            DepthStencilState = DepthStencilState.None;
        }
    }

    public class UiRenderer : Component
    {
        #region Fields

        private SpriteBatch p_sb = null;
        private DeviceStates p_states = new DeviceStates();
        private BlendingMode p_BlendingMode = BlendingMode.Default;
        private Matrix? p_CustomMatrix;

        #endregion

        #region Porperties

        public Matrix? CustomMatrix { get { return p_CustomMatrix; } set { p_CustomMatrix = value; } }

        public virtual SpriteBatch SpriteBatch { get { return p_sb; } }

        #endregion

        #region Constructors

        public UiRenderer(UiManager2 uiManager2) : base(uiManager2)
        {
            p_sb = new SpriteBatch(uiManager2.GraphicsDevice);
        }

        #endregion

        #region Destructors

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(p_sb != null)
                {
                    p_sb.Dispose();
                    p_sb = null;
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Methods

        public override void Init()
        {
            base.Init();
        }

        #endregion
    }

    #endregion
}
