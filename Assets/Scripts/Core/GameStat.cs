using System;
namespace Sram
{
	public class GameStat 
	{
		protected float QuantitylostPerSecond;
		public float RatioLostPerSecond { get; set; }
		public float Quantity { get; protected set; }
		public float MaxQuantity { get; private set; }

		float QuantityToDeduceEachSecond {
			get {
				return this.QuantitylostPerSecond 
					* this.RatioLostPerSecond; // ratio can be positive or negative
			}
		}

		private TimeSpan RemainingTimeSpan{
			get{
				if(this.QuantityToDeduceEachSecond==0)
					return TimeSpan.Zero;
				return new TimeSpan (0, 0, (int)(Quantity / this.QuantityToDeduceEachSecond)).Duration();
			}
		}
		private float RemainingTime1000{
			get{
				if(this.QuantityToDeduceEachSecond==0)
					return 0;
				//return (Quantity / this.QuantityToDeduceEachSecond) / 86400 * 1000f;
				return GameTimeConverter.ToBase1000(Quantity / this.QuantityToDeduceEachSecond);
			}
		}

		public string RemainingTime{
			get{
//				int h = this.RemainingTimeSpan.Hours;
//				int m= this.RemainingTimeSpan.Minutes;
//				int s= this.RemainingTimeSpan.Seconds;
////				return (h>9?"0"+h.ToString ():h.ToString ())+":"
//					+(m>9?"0"+m.ToString ():m.ToString ())+":"
//						+(s>9?"0"+s.ToString ():s.ToString ());
				//return h.ToString ("0:00")+":"+m.ToString ("0:00")+":"+s.ToString ("0:00");
				return string.Format (@"{0:hh\:mm\:ss}", this.RemainingTimeSpan);
			}
		}
		public string RemainingTimeBase1000{
			get{
				return Math.Floor(Math.Abs (RemainingTime1000)).ToString () + " uT";
			}
		}

		protected GameStat(float lostPerSecond, float ratioLostPerSecond, float defaultQuantity) {
			QuantitylostPerSecond = lostPerSecond;
			RatioLostPerSecond = ratioLostPerSecond;
			Quantity = defaultQuantity;
			MaxQuantity = defaultQuantity;
		}

		public static GameStat GetStat (float lostPerSecond, float ratioLostPerSecond, float defaultQuantity)
		{
			return new GameStat (lostPerSecond, ratioLostPerSecond, defaultQuantity);
		}

		public virtual void Update (float currentElapsed, float timeAccelerator)
		{
			if (currentElapsed > 1) {	// 1 for 1 seconds
				// applying a ratio to quantity lost per second
				//this.Quantity += this.QuantityToDeduceEachSecond / (1 / timeAccelerator);
				this.Quantity -= this.QuantityToDeduceEachSecond / (1 / timeAccelerator);
			}
		}

		public float GetPercent()
		{
			return (float)Math.Round ((this.Quantity / this.MaxQuantity * 100), 1, MidpointRounding.AwayFromZero);
		}
	}
}

