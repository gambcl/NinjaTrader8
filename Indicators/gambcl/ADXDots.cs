#region Using declarations
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
    public class ADXDots : Indicator
	{
		private ADX _adx;

		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Row of colored dots indicating ADX strength.";
				Name										= "ADXDots";
				Calculate									= Calculate.OnPriceChange;
				IsOverlay									= false;
				DisplayInDataBox							= false;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				Period										= 14;
				DisplayLevel								= 50;
				MediumTrendThreshold						= 15;
				StrongTrendThreshold						= 23;
				WeakTrendColor = new SolidColorBrush(Color.FromRgb(225, 190, 231));
				WeakTrendColor.Freeze();
                MediumTrendColor = new SolidColorBrush(Color.FromRgb(186, 104, 200));
                MediumTrendColor.Freeze();
                StrongTrendColor = new SolidColorBrush(Color.FromRgb(123, 31, 162));
                StrongTrendColor.Freeze();
				AddPlot(new Stroke(Brushes.White, 8), PlotStyle.Dot, "Dots");
			}
			else if (State == State.Configure)
			{
                _adx = ADX(Period);
			}
		}

		protected override void OnBarUpdate()
		{
			if (CurrentBar < Period)
				return;

			var adx = _adx[0];

			Dots[0] = DisplayLevel;

			if (adx >= StrongTrendThreshold)
			{
                PlotBrushes[0][0] = StrongTrendColor;
            }
            else if (adx >= MediumTrendThreshold)
            {
                PlotBrushes[0][0] = MediumTrendColor;
            }
            else
            {
                PlotBrushes[0][0] = WeakTrendColor;
            }
        }

        #region Properties
        [NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="Period", Description="Number of bars used in the calculation", Order=1, GroupName="Parameters")]
		public int Period
		{ get; set; }

		[NinjaScriptProperty]
		[Range(0, int.MaxValue)]
		[Display(Name="DisplayLevel", Description="Value level at which the row of dots will be displayed", Order=1, GroupName="Display")]
		public int DisplayLevel
		{ get; set; }

        [NinjaScriptProperty]
        [Range(0, double.MaxValue)]
        [Display(Name = "MediumTrendThreshold", Description = "Minimum ADX value to be considered a medium trend", Order = 2, GroupName = "Display")]
        public double MediumTrendThreshold
        { get; set; }

        [NinjaScriptProperty]
        [Range(0, double.MaxValue)]
        [Display(Name = "StrongTrendThreshold", Description = "Minimum ADX value to be considered a strong trend", Order = 3, GroupName = "Display")]
        public double StrongTrendThreshold
        { get; set; }

        [XmlIgnore]
        [Display(Name = "WeakTrendColor", Description = "Dot color used to indicate a weak trend", Order = 4, GroupName = "Display")]
        public Brush WeakTrendColor
        { get; set; }
        
		[Browsable(false)]
        public string WeakTrendColorSerialize
        {
            get { return Serialize.BrushToString(WeakTrendColor); }
            set { WeakTrendColor = Serialize.StringToBrush(value); }
        }

        [XmlIgnore]
        [Display(Name = "MediumTrendColor", Description = "Dot color used to indicate a medium trend", Order = 5, GroupName = "Display")]
        public Brush MediumTrendColor
        { get; set; }

        [Browsable(false)]
        public string MediumTrendColorSerialize
        {
            get { return Serialize.BrushToString(MediumTrendColor); }
            set { MediumTrendColor = Serialize.StringToBrush(value); }
        }

        [XmlIgnore]
        [Display(Name = "StrongTrendColor", Description = "Dot color used to indicate a strong trend", Order = 6, GroupName = "Display")]
        public Brush StrongTrendColor
        { get; set; }

        [Browsable(false)]
        public string StrongTrendColorSerialize
        {
            get { return Serialize.BrushToString(StrongTrendColor); }
            set { StrongTrendColor = Serialize.StringToBrush(value); }
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
		private gambcl.ADXDots[] cacheADXDots;
		public gambcl.ADXDots ADXDots(int period, int displayLevel, double mediumTrendThreshold, double strongTrendThreshold)
		{
			return ADXDots(Input, period, displayLevel, mediumTrendThreshold, strongTrendThreshold);
		}

		public gambcl.ADXDots ADXDots(ISeries<double> input, int period, int displayLevel, double mediumTrendThreshold, double strongTrendThreshold)
		{
			if (cacheADXDots != null)
				for (int idx = 0; idx < cacheADXDots.Length; idx++)
					if (cacheADXDots[idx] != null && cacheADXDots[idx].Period == period && cacheADXDots[idx].DisplayLevel == displayLevel && cacheADXDots[idx].MediumTrendThreshold == mediumTrendThreshold && cacheADXDots[idx].StrongTrendThreshold == strongTrendThreshold && cacheADXDots[idx].EqualsInput(input))
						return cacheADXDots[idx];
			return CacheIndicator<gambcl.ADXDots>(new gambcl.ADXDots(){ Period = period, DisplayLevel = displayLevel, MediumTrendThreshold = mediumTrendThreshold, StrongTrendThreshold = strongTrendThreshold }, input, ref cacheADXDots);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.gambcl.ADXDots ADXDots(int period, int displayLevel, double mediumTrendThreshold, double strongTrendThreshold)
		{
			return indicator.ADXDots(Input, period, displayLevel, mediumTrendThreshold, strongTrendThreshold);
		}

		public Indicators.gambcl.ADXDots ADXDots(ISeries<double> input , int period, int displayLevel, double mediumTrendThreshold, double strongTrendThreshold)
		{
			return indicator.ADXDots(input, period, displayLevel, mediumTrendThreshold, strongTrendThreshold);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.gambcl.ADXDots ADXDots(int period, int displayLevel, double mediumTrendThreshold, double strongTrendThreshold)
		{
			return indicator.ADXDots(Input, period, displayLevel, mediumTrendThreshold, strongTrendThreshold);
		}

		public Indicators.gambcl.ADXDots ADXDots(ISeries<double> input , int period, int displayLevel, double mediumTrendThreshold, double strongTrendThreshold)
		{
			return indicator.ADXDots(input, period, displayLevel, mediumTrendThreshold, strongTrendThreshold);
		}
	}
}

#endregion
