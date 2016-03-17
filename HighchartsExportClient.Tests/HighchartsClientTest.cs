using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace HighchartsExportClient.Tests
{
	[TestClass]
	public class HighchartsClientTest
	{
		private readonly string _highchartsServer = "http://export.highcharts.com/";
		private readonly string _svg = "<svg xmlns:xlink=\"http://www.w3.org/1999/xlink\" version=\"1.1\" style=\"font-family:&quot;Lucida Grande&quot;, &quot;Lucida Sans Unicode&quot;, Arial, Helvetica, sans-serif;font-size:12px;\" xmlns=\"http://www.w3.org/2000/svg\" width=\"600\" height=\"400\"><desc>Created with Highcharts 4.1.5</desc><defs><clipPath id=\"highcharts-1\"><rect x=\"0\" y=\"0\" width=\"511\" height=\"278\"></rect></clipPath></defs><rect x=\"0\" y=\"0\" width=\"600\" height=\"400\" strokeWidth=\"0\" fill=\"#FFFFFF\" class=\" highcharts-background\"></rect><g class=\"highcharts-grid\" zIndex=\"1\"></g><g class=\"highcharts-grid\" zIndex=\"1\"><path fill=\"none\" d=\"M 79 325.5 L 590 325.5\" stroke=\"#D8D8D8\" stroke-width=\"1\" zIndex=\"1\" opacity=\"1\"></path><path fill=\"none\" d=\"M 79 279.5 L 590 279.5\" stroke=\"#D8D8D8\" stroke-width=\"1\" zIndex=\"1\" opacity=\"1\"></path><path fill=\"none\" d=\"M 79 232.5 L 590 232.5\" stroke=\"#D8D8D8\" stroke-width=\"1\" zIndex=\"1\" opacity=\"1\"></path><path fill=\"none\" d=\"M 79 186.5 L 590 186.5\" stroke=\"#D8D8D8\" stroke-width=\"1\" zIndex=\"1\" opacity=\"1\"></path><path fill=\"none\" d=\"M 79 140.5 L 590 140.5\" stroke=\"#D8D8D8\" stroke-width=\"1\" zIndex=\"1\" opacity=\"1\"></path><path fill=\"none\" d=\"M 79 93.5 L 590 93.5\" stroke=\"#D8D8D8\" stroke-width=\"1\" zIndex=\"1\" opacity=\"1\"></path><path fill=\"none\" d=\"M 79 46.5 L 590 46.5\" stroke=\"#D8D8D8\" stroke-width=\"1\" zIndex=\"1\" opacity=\"1\"></path></g><g class=\"highcharts-axis\" zIndex=\"2\"><path fill=\"none\" d=\"M 334.5 325 L 334.5 335\" stroke=\"#C0D0E0\" stroke-width=\"1\" opacity=\"1\"></path><path fill=\"none\" d=\"M 590.5 325 L 590.5 335\" stroke=\"#C0D0E0\" stroke-width=\"1\" opacity=\"1\"></path><path fill=\"none\" d=\"M 78.5 325 L 78.5 335\" stroke=\"#C0D0E0\" stroke-width=\"1\" opacity=\"1\"></path><path fill=\"none\" d=\"M 79 325.5 L 590 325.5\" stroke=\"#C0D0E0\" stroke-width=\"1\" zIndex=\"7\" visibility=\"visible\"></path></g><g class=\"highcharts-axis\" zIndex=\"2\"><text x=\"25\" zIndex=\"7\" text-anchor=\"middle\" transform=\"translate(0,0) rotate(270 25 186)\" class=\" highcharts-yaxis-title\" style=\"color:#707070;fill:#707070;\" visibility=\"visible\" y=\"186\">Values</text></g><g class=\"highcharts-series-group\" zIndex=\"3\"><g class=\"highcharts-series\" visibility=\"visible\" zIndex=\"0.1\" transform=\"translate(79,47) scale(1 1)\" clip-path=\"url(#highcharts-1)\"><path fill=\"none\" d=\"M 127.75 231.66666666666666 L 383.25 46.333333333333314\" stroke=\"#7cb5ec\" stroke-width=\"2\" zIndex=\"1\" stroke-linejoin=\"round\" stroke-linecap=\"round\"></path><path fill=\"none\" d=\"M 117.75 231.66666666666666 L 127.75 231.66666666666666 L 383.25 46.333333333333314 L 393.25 46.333333333333314\" stroke-linejoin=\"round\" visibility=\"visible\" stroke=\"rgba(192,192,192,0.0001)\" stroke-width=\"22\" zIndex=\"2\" class=\" highcharts-tracker\" style=\"\"></path></g><g class=\"highcharts-markers highcharts-tracker\" visibility=\"visible\" zIndex=\"0.1\" transform=\"translate(79,47) scale(1 1)\" style=\"\" clip-path=\"none\"><path fill=\"#7cb5ec\" d=\"M 383 42.333333333333314 C 388.328 42.333333333333314 388.328 50.333333333333314 383 50.333333333333314 C 377.672 50.333333333333314 377.672 42.333333333333314 383 42.333333333333314 Z\"></path><path fill=\"#7cb5ec\" d=\"M 127 227.66666666666666 C 132.328 227.66666666666666 132.328 235.66666666666666 127 235.66666666666666 C 121.672 235.66666666666666 121.672 227.66666666666666 127 227.66666666666666 Z\"></path></g></g><text x=\"300\" text-anchor=\"middle\" class=\"highcharts-title\" zIndex=\"4\" style=\"color:#333333;font-size:18px;fill:#333333;width:536px;\" y=\"24\"><tspan>Chart title</tspan></text><g class=\"highcharts-legend\" zIndex=\"7\" transform=\"translate(253,359)\"><g zIndex=\"1\"><g><g class=\"highcharts-legend-item\" zIndex=\"1\" transform=\"translate(8,3)\"><path fill=\"none\" d=\"M 0 11 L 16 11\" stroke=\"#7cb5ec\" stroke-width=\"2\"></path><path fill=\"#7cb5ec\" d=\"M 8 7 C 13.328 7 13.328 15 8 15 C 2.6719999999999997 15 2.6719999999999997 7 8 7 Z\"></path><text x=\"21\" style=\"color:#333333;font-size:12px;font-weight:bold;cursor:pointer;fill:#333333;\" text-anchor=\"start\" zIndex=\"2\" y=\"15\"><tspan>Series 1</tspan></text></g></g></g></g><g class=\"highcharts-axis-labels highcharts-xaxis-labels\" zIndex=\"7\"><text x=\"206.75\" style=\"color:#606060;cursor:default;font-size:11px;fill:#606060;width:246px;text-overflow:clip;\" text-anchor=\"middle\" transform=\"translate(0,0)\" y=\"344\" opacity=\"1\">Jan</text><text x=\"462.25\" style=\"color:#606060;cursor:default;font-size:11px;fill:#606060;width:246px;text-overflow:clip;\" text-anchor=\"middle\" transform=\"translate(0,0)\" y=\"344\" opacity=\"1\">1</text></g><g class=\"highcharts-axis-labels highcharts-yaxis-labels\" zIndex=\"7\"><text x=\"64\" style=\"color:#606060;cursor:default;font-size:11px;fill:#606060;width:188px;text-overflow:clip;\" text-anchor=\"end\" transform=\"translate(0,0)\" y=\"329\" opacity=\"1\">-0.25</text><text x=\"64\" style=\"color:#606060;cursor:default;font-size:11px;fill:#606060;width:188px;text-overflow:clip;\" text-anchor=\"end\" transform=\"translate(0,0)\" y=\"283\" opacity=\"1\">0</text><text x=\"64\" style=\"color:#606060;cursor:default;font-size:11px;fill:#606060;width:188px;text-overflow:clip;\" text-anchor=\"end\" transform=\"translate(0,0)\" y=\"236\" opacity=\"1\">0.25</text><text x=\"64\" style=\"color:#606060;cursor:default;font-size:11px;fill:#606060;width:188px;text-overflow:clip;\" text-anchor=\"end\" transform=\"translate(0,0)\" y=\"190\" opacity=\"1\">0.5</text><text x=\"64\" style=\"color:#606060;cursor:default;font-size:11px;fill:#606060;width:188px;text-overflow:clip;\" text-anchor=\"end\" transform=\"translate(0,0)\" y=\"144\" opacity=\"1\">0.75</text><text x=\"64\" style=\"color:#606060;cursor:default;font-size:11px;fill:#606060;width:188px;text-overflow:clip;\" text-anchor=\"end\" transform=\"translate(0,0)\" y=\"97\" opacity=\"1\">1</text><text x=\"64\" style=\"color:#606060;cursor:default;font-size:11px;fill:#606060;width:188px;text-overflow:clip;\" text-anchor=\"end\" transform=\"translate(0,0)\" y=\"51\" opacity=\"1\">1.25</text></g><g class=\"highcharts-tooltip\" zIndex=\"8\" style=\"cursor:default;padding:0;white-space:nowrap;\" transform=\"translate(0,-9999)\"><path fill=\"none\" d=\"M 3 0 L 13 0 C 16 0 16 0 16 3 L 16 13 C 16 16 16 16 13 16 L 3 16 C 0 16 0 16 0 13 L 0 3 C 0 0 0 0 3 0\" isShadow=\"true\" stroke=\"black\" stroke-width=\"5\" transform=\"translate(1, 1)\" opacity=\"0.049999999999999996\"></path><path fill=\"none\" d=\"M 3 0 L 13 0 C 16 0 16 0 16 3 L 16 13 C 16 16 16 16 13 16 L 3 16 C 0 16 0 16 0 13 L 0 3 C 0 0 0 0 3 0\" isShadow=\"true\" stroke=\"black\" stroke-width=\"3\" transform=\"translate(1, 1)\" opacity=\"0.09999999999999999\"></path><path fill=\"none\" d=\"M 3 0 L 13 0 C 16 0 16 0 16 3 L 16 13 C 16 16 16 16 13 16 L 3 16 C 0 16 0 16 0 13 L 0 3 C 0 0 0 0 3 0\" isShadow=\"true\" stroke=\"black\" stroke-width=\"1\" transform=\"translate(1, 1)\" opacity=\"0.15\"></path><path fill=\"rgba(249, 249, 249, .85)\" d=\"M 3 0 L 13 0 C 16 0 16 0 16 3 L 16 13 C 16 16 16 16 13 16 L 3 16 C 0 16 0 16 0 13 L 0 3 C 0 0 0 0 3 0\"></path><text x=\"8\" zIndex=\"1\" style=\"font-size:12px;color:#333333;fill:#333333;\" transform=\"translate(0,20)\"></text></g><text x=\"590\" text-anchor=\"end\" zIndex=\"8\" style=\"cursor:pointer;color:#909090;font-size:9px;fill:#909090;\" y=\"395\">Highcharts.com</text></svg>";

		[TestMethod]
		public async Task GetImageBytesFromOptions_Works()
		{
			var client = new HighchartsClient(_highchartsServer);

			var options = new
			{
				xAxis = new
				{
					categories = new[] { "Jan", "Feb", "Mar" }
				},
				series = new[]
				{
					new { data = new[] {29.9, 71.5, 106.4} }
				}
			};

			var res = await client.GetChartImageFromOptionsAsync(JsonConvert.SerializeObject(options));

			Assert.IsNotNull(res);
			File.WriteAllBytes("__imageFromBytes_defaultSettings.png", res);
		}

		[TestMethod]
		public async Task GetImageBytesFromOptions_CustomSettings_Works()
		{
			var settings = new HighchartsSetting
			{
				ExportImageType = "jpg",
				ScaleFactor = 4,
				ImageWidth = 1500,
				ServerAddress = _highchartsServer
			};

			var client = new HighchartsClient(settings);

			var options = new
			{
				xAxis = new
				{
					categories = new[] { "Jan", "Feb", "Mar" }
				},
				series = new[]
				{
					new { data = new[] {29.9, 71.5, 106.4} }
				}
			};

			var res = await client.GetChartImageFromOptionsAsync(JsonConvert.SerializeObject(options));

			Assert.IsNotNull(res);
			File.WriteAllBytes($"__imageFromBytes_customSettings.{settings.ExportImageType}", res);
		}

		[TestMethod]
		public async Task GetImageLinkFromOptions_Works()
		{
			var client = new HighchartsClient(_highchartsServer);

			var options = new
			{
				xAxis = new
				{
					categories = new[] { "Jan", "Feb", "Mar" }
				},
				series = new[]
				{
					new { data = new[] {29.9, 71.5, 106.4} }
				}
			};

			var res = await client.GetChartImageLinkFromOptionsAsync(JsonConvert.SerializeObject(options));

			Assert.IsNotNull(res);
			Console.WriteLine(res);
		}

		[TestMethod]
		public async Task GetImageLinkFromSvg_Works()
		{
			var client = new HighchartsClient(_highchartsServer);

			var res = await client.GetChartImageLinkFromSvgAsync(_svg);

			Assert.IsNotNull(res);
			Console.WriteLine(res);
		}

		[TestMethod]
		public async Task GetImageBytesFromSvg_Works()
		{
			var client = new HighchartsClient(_highchartsServer);

			var res = await client.GetChartImageFromSvgAsync(_svg);

			Assert.IsNotNull(res);
			File.WriteAllBytes("__imageFromSvg_defaultSettings.png", res);
		}
	}
}
