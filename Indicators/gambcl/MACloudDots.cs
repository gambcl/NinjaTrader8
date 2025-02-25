#region Using declarations
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Gui;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators.gambcl
{
    [Gui.CategoryOrder("Parameters", 1)]
    [Gui.CategoryOrder("Display", 2)]
    public class MACloudDots : Indicator
	{
        #region Members
        private MACloud _maCloud;
        #endregion

        #region Indicator methods
        protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Indicator showing the MA Cloud trend.";
				Name										= "MACloudDots";
				Calculate									= Calculate.OnPriceChange;
                IsOverlay									= false;
                DisplayInDataBox							= false;
                DrawOnPricePanel							= false;
                DrawHorizontalGridLines						= true;
                DrawVerticalGridLines						= true;
                PaintPriceMarkers							= false;
                ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
                //Disable this property if your indicator requires custom values that cumulate with each new market data event. 
                //See Help Guide for additional information.
                IsSuspendedWhileInactive = true;
                MAType = NinjaTrader.NinjaScript.Indicators.gambcl.MACloudEnums.MATypeEnum.EMA;
                FastPeriod = 9;
                SlowPeriod = 21;
                DisplayLevel = 50;
                BullishTrendBrush = Brushes.Green;
                BearishTrendBrush = Brushes.Red;
                AddPlot(new Stroke(Brushes.Transparent, 6), PlotStyle.Dot, "Dots");
            }
            else if (State == State.Configure)
			{
                _maCloud = MACloud(MAType, FastPeriod, SlowPeriod, Brushes.Transparent, Brushes.Transparent, 0, false, 0, string.Empty, false, string.Empty, string.Empty);
			}
		}

        public override string DisplayName
        {
            get { return Name + "(" + MAType + "," + FastPeriod + "," + SlowPeriod + ")"; }
        }

        protected override void OnBarUpdate()
		{
            if (CurrentBar < Math.Max(FastPeriod, SlowPeriod))
                return;

            _maCloud.Update();

            var maFast = _maCloud.FastMA[0];
            var maSlow = _maCloud.SlowMA[0];

            Dots[0] = DisplayLevel;
            if (maFast > maSlow)
            {
                PlotBrushes[0][0] = BullishTrendBrush;
            }
            else if (maFast < maSlow)
            {
                PlotBrushes[0][0] = BearishTrendBrush;
            }
            else
            {
                PlotBrushes[0][0] = Brushes.Transparent;
            }
        }
        #endregion

        #region Properties
        [NinjaScriptProperty]
        [Display(Name = "MA Type", Description = "The type of Moving Average.", Order = 1, GroupName = "Parameters")]
        public NinjaTrader.NinjaScript.Indicators.gambcl.MACloudEnums.MATypeEnum MAType
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name = "FastPeriod", Description = "The period of the fast Moving Average.", Order = 2, GroupName = "Parameters")]
        public int FastPeriod
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name = "SlowPeriod", Description = "The period of the slow Moving Average.", Order = 3, GroupName = "Parameters")]
        public int SlowPeriod
        { get; set; }

        [NinjaScriptProperty]
        [Range(0, int.MaxValue)]
        [Display(Name = "DisplayLevel", Description = "Value level at which the row of dots will be displayed.", Order = 1, GroupName = "Display")]
        public int DisplayLevel
        { get; set; }

        [NinjaScriptProperty]
        [XmlIgnore]
        [Display(Name = "BullishTrendBrush", Description = "Dot color used to indicate a bullish trend.", Order = 2, GroupName = "Display")]
        public Brush BullishTrendBrush
        { get; set; }

        [Browsable(false)]
        public string BullishTrendBrushSerializable
        {
            get { return Serialize.BrushToString(BullishTrendBrush); }
            set { BullishTrendBrush = Serialize.StringToBrush(value); }
        }

        [NinjaScriptProperty]
        [XmlIgnore]
        [Display(Name = "BearishTrendBrush", Description = "Dot color used to indicate a bearish trend.", Order = 3, GroupName = "Display")]
        public Brush BearishTrendBrush
        { get; set; }

        [Browsable(false)]
        public string BearishTrendBrushSerializable
        {
            get { return Serialize.BrushToString(BearishTrendBrush); }
            set { BearishTrendBrush = Serialize.StringToBrush(value); }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> Dots
        {
            get { return Values[0]; }
        }
        #endregion
    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private gambcl.MACloudDots[] cacheMACloudDots;
		public gambcl.MACloudDots MACloudDots(NinjaTrader.NinjaScript.Indicators.gambcl.MACloudEnums.MATypeEnum mAType, int fastPeriod, int slowPeriod, int displayLevel, Brush bullishTrendBrush, Brush bearishTrendBrush)
		{
			return MACloudDots(Input, mAType, fastPeriod, slowPeriod, displayLevel, bullishTrendBrush, bearishTrendBrush);
		}

		public gambcl.MACloudDots MACloudDots(ISeries<double> input, NinjaTrader.NinjaScript.Indicators.gambcl.MACloudEnums.MATypeEnum mAType, int fastPeriod, int slowPeriod, int displayLevel, Brush bullishTrendBrush, Brush bearishTrendBrush)
		{
			if (cacheMACloudDots != null)
				for (int idx = 0; idx < cacheMACloudDots.Length; idx++)
					if (cacheMACloudDots[idx] != null && cacheMACloudDots[idx].MAType == mAType && cacheMACloudDots[idx].FastPeriod == fastPeriod && cacheMACloudDots[idx].SlowPeriod == slowPeriod && cacheMACloudDots[idx].DisplayLevel == displayLevel && cacheMACloudDots[idx].BullishTrendBrush == bullishTrendBrush && cacheMACloudDots[idx].BearishTrendBrush == bearishTrendBrush && cacheMACloudDots[idx].EqualsInput(input))
						return cacheMACloudDots[idx];
			return CacheIndicator<gambcl.MACloudDots>(new gambcl.MACloudDots(){ MAType = mAType, FastPeriod = fastPeriod, SlowPeriod = slowPeriod, DisplayLevel = displayLevel, BullishTrendBrush = bullishTrendBrush, BearishTrendBrush = bearishTrendBrush }, input, ref cacheMACloudDots);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.gambcl.MACloudDots MACloudDots(NinjaTrader.NinjaScript.Indicators.gambcl.MACloudEnums.MATypeEnum mAType, int fastPeriod, int slowPeriod, int displayLevel, Brush bullishTrendBrush, Brush bearishTrendBrush)
		{
			return indicator.MACloudDots(Input, mAType, fastPeriod, slowPeriod, displayLevel, bullishTrendBrush, bearishTrendBrush);
		}

		public Indicators.gambcl.MACloudDots MACloudDots(ISeries<double> input , NinjaTrader.NinjaScript.Indicators.gambcl.MACloudEnums.MATypeEnum mAType, int fastPeriod, int slowPeriod, int displayLevel, Brush bullishTrendBrush, Brush bearishTrendBrush)
		{
			return indicator.MACloudDots(input, mAType, fastPeriod, slowPeriod, displayLevel, bullishTrendBrush, bearishTrendBrush);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.gambcl.MACloudDots MACloudDots(NinjaTrader.NinjaScript.Indicators.gambcl.MACloudEnums.MATypeEnum mAType, int fastPeriod, int slowPeriod, int displayLevel, Brush bullishTrendBrush, Brush bearishTrendBrush)
		{
			return indicator.MACloudDots(Input, mAType, fastPeriod, slowPeriod, displayLevel, bullishTrendBrush, bearishTrendBrush);
		}

		public Indicators.gambcl.MACloudDots MACloudDots(ISeries<double> input , NinjaTrader.NinjaScript.Indicators.gambcl.MACloudEnums.MATypeEnum mAType, int fastPeriod, int slowPeriod, int displayLevel, Brush bullishTrendBrush, Brush bearishTrendBrush)
		{
			return indicator.MACloudDots(input, mAType, fastPeriod, slowPeriod, displayLevel, bullishTrendBrush, bearishTrendBrush);
		}
	}
}

#endregion
