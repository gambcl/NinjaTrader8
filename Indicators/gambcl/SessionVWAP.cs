#region Using declarations
using System.ComponentModel;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Data;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators.gambcl
{
    public class SessionVWAP : Indicator
	{
		private double _cumulativeTypicalVolume = 0;
		private double _cumulativeVolume = 0;
		
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Volume-Weighted Average Price anchored to session.";
				Name										= "SessionVWAP";
				Calculate									= Calculate.OnBarClose;
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
				
				AddPlot(Brushes.DeepSkyBlue, "VWAP");
			}
			else if (State == State.Configure)
			{
			}
		}

		protected override void OnBarUpdate()
		{
			if (BarsPeriod.BarsPeriodType == BarsPeriodType.Day ||
				BarsPeriod.BarsPeriodType == BarsPeriodType.Week ||
				BarsPeriod.BarsPeriodType == BarsPeriodType.Month ||
				BarsPeriod.BarsPeriodType == BarsPeriodType.Year)
				return;
			
			// Increments
			double VolInc 			= VOL()[0];
			double TypicalVolInc 	= VOL()[0] * ((High[0] + Low[0] + Close[0]) / 3);

			if (Bars.IsFirstBarOfSession && IsFirstTickOfBar)
			{
				// Reset for new session.
				_cumulativeVolume = VolInc;
				_cumulativeTypicalVolume = TypicalVolInc;
			}
			else
			{
				_cumulativeVolume += VolInc;
				_cumulativeTypicalVolume += TypicalVolInc;
			}
			
			PlotVWAP[0] = _cumulativeTypicalVolume / _cumulativeVolume;
		}
		
		#region Properties

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> PlotVWAP
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
		private gambcl.SessionVWAP[] cacheSessionVWAP;
		public gambcl.SessionVWAP SessionVWAP()
		{
			return SessionVWAP(Input);
		}

		public gambcl.SessionVWAP SessionVWAP(ISeries<double> input)
		{
			if (cacheSessionVWAP != null)
				for (int idx = 0; idx < cacheSessionVWAP.Length; idx++)
					if (cacheSessionVWAP[idx] != null &&  cacheSessionVWAP[idx].EqualsInput(input))
						return cacheSessionVWAP[idx];
			return CacheIndicator<gambcl.SessionVWAP>(new gambcl.SessionVWAP(), input, ref cacheSessionVWAP);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.gambcl.SessionVWAP SessionVWAP()
		{
			return indicator.SessionVWAP(Input);
		}

		public Indicators.gambcl.SessionVWAP SessionVWAP(ISeries<double> input )
		{
			return indicator.SessionVWAP(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.gambcl.SessionVWAP SessionVWAP()
		{
			return indicator.SessionVWAP(Input);
		}

		public Indicators.gambcl.SessionVWAP SessionVWAP(ISeries<double> input )
		{
			return indicator.SessionVWAP(input);
		}
	}
}

#endregion
