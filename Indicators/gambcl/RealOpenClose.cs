#region Using declarations
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Data;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators.gambcl
{
    public class RealOpenClose : Indicator
	{
        #region Members
        private SharpDX.Direct2D1.Brush _realOpenBrushDx;
        private SharpDX.Direct2D1.Brush _realCloseBrushDx;
        #endregion

        #region Indicator methods
        protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Shows the real Open and Close levels for candles.";
				Name										= "RealOpenClose";
				Calculate									= Calculate.OnPriceChange;
				IsOverlay									= true;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				ShowRealOpen								= true;
				RealOpenBrush								= Brushes.White;
				ShowRealClose								= true;
				RealCloseBrush								= Brushes.Fuchsia;
				Width										= 1;
			}
			else if (State == State.Configure)
			{
            }
        }

        public override string DisplayName
        {
            get { return Name + "(" + ShowRealOpen + "," + ShowRealClose + ")"; }
        }

        protected override void OnBarUpdate()
		{
		}

        protected override void OnRender(ChartControl chartControl, ChartScale chartScale)
        {
			if (!IsVisible)
				return;

            base.OnRender(chartControl, chartScale);
            
			if (chartControl == null || chartScale == null || ChartBars == null || RenderTarget == null)
				return;

			for (int i = ChartBars.FromIndex; i <= ChartBars.ToIndex; i++)
			{
                var barX = chartControl.GetXByBarIndex(ChartBars, i);
                var nextBarX = chartControl.GetXByBarIndex(ChartBars, i + 1);
                var width = nextBarX - barX;
                barX -= (width / 2);
                
				if (ShowRealOpen && _realOpenBrushDx != null && !_realOpenBrushDx.IsDisposed)
				{
                    var realOpen = Bars.GetOpen(i);
                    var openY = chartScale.GetYByValue(realOpen);
                    RenderTarget.DrawLine(new SharpDX.Vector2(barX, openY), new SharpDX.Vector2(barX + width, openY), _realOpenBrushDx, Width);
                }

				if (ShowRealClose && _realCloseBrushDx != null && !_realCloseBrushDx.IsDisposed)
				{
                    var realClose = Bars.GetClose(i);
                    var closeY = chartScale.GetYByValue(realClose);
                    RenderTarget.DrawLine(new SharpDX.Vector2(barX, closeY), new SharpDX.Vector2(barX + width, closeY), _realCloseBrushDx, Width);
                }
            }
        }

        public override void OnRenderTargetChanged()
        {
            if (_realOpenBrushDx != null)
                _realOpenBrushDx.Dispose();

            if (_realCloseBrushDx != null)
                _realCloseBrushDx.Dispose();

            if (RenderTarget != null)
            {
                try
                {
                    _realOpenBrushDx = RealOpenBrush.ToDxBrush(RenderTarget);
                    _realCloseBrushDx = RealCloseBrush.ToDxBrush(RenderTarget);
                }
                catch (Exception e) { }
            }
        }
        #endregion

        #region Properties
        [NinjaScriptProperty]
		[Display(Name="ShowRealOpen", Description="Show the real Open price of each candle, using the specified color.", Order=1, GroupName="Parameters")]
		public bool ShowRealOpen
		{ get; set; }

		[NinjaScriptProperty]
		[XmlIgnore]
		[Display(Name="RealOpenBrush", Order=2, GroupName="Parameters")]
		public Brush RealOpenBrush
		{ get; set; }

		[Browsable(false)]
		public string RealOpenBrushSerializable
		{
			get { return Serialize.BrushToString(RealOpenBrush); }
			set { RealOpenBrush = Serialize.StringToBrush(value); }
		}			

		[NinjaScriptProperty]
		[Display(Name="ShowRealClose", Description="Show the real Close price of each candle, using the specified color.", Order=3, GroupName="Parameters")]
		public bool ShowRealClose
		{ get; set; }

		[NinjaScriptProperty]
		[XmlIgnore]
		[Display(Name="RealCloseBrush", Order=4, GroupName="Parameters")]
		public Brush RealCloseBrush
		{ get; set; }

		[Browsable(false)]
		public string RealCloseBrushSerializable
		{
			get { return Serialize.BrushToString(RealCloseBrush); }
			set { RealCloseBrush = Serialize.StringToBrush(value); }
		}			

		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="Width", Description="Controls the thickness of the real Open/Close levels.", Order=5, GroupName="Parameters")]
		public int Width
		{ get; set; }
		#endregion
	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private gambcl.RealOpenClose[] cacheRealOpenClose;
		public gambcl.RealOpenClose RealOpenClose(bool showRealOpen, Brush realOpenBrush, bool showRealClose, Brush realCloseBrush, int width)
		{
			return RealOpenClose(Input, showRealOpen, realOpenBrush, showRealClose, realCloseBrush, width);
		}

		public gambcl.RealOpenClose RealOpenClose(ISeries<double> input, bool showRealOpen, Brush realOpenBrush, bool showRealClose, Brush realCloseBrush, int width)
		{
			if (cacheRealOpenClose != null)
				for (int idx = 0; idx < cacheRealOpenClose.Length; idx++)
					if (cacheRealOpenClose[idx] != null && cacheRealOpenClose[idx].ShowRealOpen == showRealOpen && cacheRealOpenClose[idx].RealOpenBrush == realOpenBrush && cacheRealOpenClose[idx].ShowRealClose == showRealClose && cacheRealOpenClose[idx].RealCloseBrush == realCloseBrush && cacheRealOpenClose[idx].Width == width && cacheRealOpenClose[idx].EqualsInput(input))
						return cacheRealOpenClose[idx];
			return CacheIndicator<gambcl.RealOpenClose>(new gambcl.RealOpenClose(){ ShowRealOpen = showRealOpen, RealOpenBrush = realOpenBrush, ShowRealClose = showRealClose, RealCloseBrush = realCloseBrush, Width = width }, input, ref cacheRealOpenClose);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.gambcl.RealOpenClose RealOpenClose(bool showRealOpen, Brush realOpenBrush, bool showRealClose, Brush realCloseBrush, int width)
		{
			return indicator.RealOpenClose(Input, showRealOpen, realOpenBrush, showRealClose, realCloseBrush, width);
		}

		public Indicators.gambcl.RealOpenClose RealOpenClose(ISeries<double> input , bool showRealOpen, Brush realOpenBrush, bool showRealClose, Brush realCloseBrush, int width)
		{
			return indicator.RealOpenClose(input, showRealOpen, realOpenBrush, showRealClose, realCloseBrush, width);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.gambcl.RealOpenClose RealOpenClose(bool showRealOpen, Brush realOpenBrush, bool showRealClose, Brush realCloseBrush, int width)
		{
			return indicator.RealOpenClose(Input, showRealOpen, realOpenBrush, showRealClose, realCloseBrush, width);
		}

		public Indicators.gambcl.RealOpenClose RealOpenClose(ISeries<double> input , bool showRealOpen, Brush realOpenBrush, bool showRealClose, Brush realCloseBrush, int width)
		{
			return indicator.RealOpenClose(input, showRealOpen, realOpenBrush, showRealClose, realCloseBrush, width);
		}
	}
}

#endregion
