using System.Runtime.Serialization;

namespace WeatherForecast.Models
{
	[DataContract]
	public class DataRangePoint
	{
		public DataRangePoint(string x, double[] y)
		{
			this.Label = x;
			this.Y = y;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "label")]
		public string Label = "";

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public double[] Y = null;
	}
}