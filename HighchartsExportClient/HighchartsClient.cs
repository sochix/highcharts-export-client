using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HighchartsExportClient
{
	public interface IHighchartsClient
	{
		Task<byte[]> GetChartImageFromOptions(string options);
		Task<byte[]> GetChartImageFromSvg(string svg);
		Task<string> GetChartImageLinkFromOptions(string options);
		Task<string> GetChartImageLinkFromSvg(string svg);
	}

	public class HighchartsSetting
	{
		/// <summary>
		/// Address of server where installed highcharts-export module. 
		/// http://www.highcharts.com/docs/export-module/setting-up-the-server
		/// </summary>
		public string ServerAddress { get; set; }

		/// <summary>
		/// The content-type of the file to output. 
		/// Can be one of 'image/png', 'image/jpeg', 'application/pdf', or 'image/svg+xml'.
		/// </summary>
		public string ExportImageType { get; set; }

		/// <summary>
		/// Set the exact pixel width of the exported image or pdf.
		/// This overrides the -scale parameter. The maximum allowed width is 2000px
		/// </summary>
		public int ImageWidth { get; set; }

		/// <summary>
		/// To scale the original SVG. 
		/// For example, if the chart.width option in the chart configuration is set to 600 and the scale is set to 2,
		/// the output raster image will have a pixel width of 1200. 
		/// So this is a convenient way of increasing the resolution without decreasing the font size and line widths in the chart.
		/// This is ignored if the -width parameter is set. 
		/// For now we allow a maximum scaling of 4. This is for ensuring a good repsonse time. 
		/// Scaling is a bit resource intensive.
		/// </summary>
		public int ScaleFactor { get; set; }

		/// <summary>
		/// The constructor name. Can be one of 'Chart' or 'StockChart'.
		/// This depends on whether you want to generate Highstock or basic Highcharts.
		/// Only applicable when using this in combination with the options parameter.
		/// </summary>
		public string ConstructorName { get; set; }

		/// <summary>
		/// Can be of true or false. Default is false.
		/// When setting async to true a download link is returned to the client, instead of an image.
		/// This download link can be reused for 30 seconds. After that, the file will be deleted from the server.
		/// </summary>
		public bool IsAsyncCall { get; set; }

		public HighchartsSetting()
		{
			ImageWidth = 400;
			ExportImageType = "png";
			ScaleFactor = 1;
		}
	}

	public class HighchartsClient : IHighchartsClient
	{
		private readonly HighchartsSetting _settings;
		public HighchartsClient(string serverAddress)
		{
			_settings = new HighchartsSetting
			{
				ServerAddress = serverAddress
			};
		}

		public HighchartsClient(HighchartsSetting settings)
		{
			_settings = settings;
		}

		private async Task<HttpResponseMessage> MakeRequest(Dictionary<string, string> settings)
		{
			using (var httpClient = new HttpClient())
			{
				var response =  await httpClient.PostAsync(_settings.ServerAddress, new FormUrlEncodedContent(settings));

				response.EnsureSuccessStatusCode();

				return response;
			}
		}

		private Dictionary<string, string> GetRequestSettings(string data, bool getLink, bool isSvg)
		{
			return new Dictionary<string, string>()
			{
				{ "async", getLink ? "true" : "false"},
				{ "type", _settings.ExportImageType},
				{ "width", _settings.ImageWidth.ToString() },
				{ isSvg ? "svg" : "options", data },
				{ "scaleFactor", _settings.ScaleFactor.ToString() }
			};
		}

		public async Task<byte[]> GetChartImageFromOptions(string options)
		{
			var request = GetRequestSettings(options, getLink: false, isSvg: false);
			var response = await MakeRequest(request);

			return await response.Content.ReadAsByteArrayAsync();
		}

		public async Task<byte[]> GetChartImageFromSvg(string svg)
		{
			var request = GetRequestSettings(svg, getLink: false, isSvg: true);
			var response = await MakeRequest(request);

			return await response.Content.ReadAsByteArrayAsync();
		}

		public async Task<string> GetChartImageLinkFromOptions(string options)
		{
			var request = GetRequestSettings(options, getLink: true, isSvg: false);

			var response = await MakeRequest(request);
			var filePath = await response.Content.ReadAsStringAsync();

			return $"{_settings.ServerAddress}{filePath}";
		}

		public async Task<string> GetChartImageLinkFromSvg(string svg)
		{
			var request = GetRequestSettings(svg, getLink: true, isSvg: true);

			var response = await MakeRequest(request);
			var filePath = await response.Content.ReadAsStringAsync();

			return $"{_settings.ServerAddress}{filePath}";
		}
	}
}
