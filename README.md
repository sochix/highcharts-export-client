#highcharts-export-client

[![Build status](https://ci.appveyor.com/api/projects/status/lx03i6374237hj73?svg=true)](https://ci.appveyor.com/project/sochix/highcharts-export-client)
[![NuGet version](https://badge.fury.io/nu/highcharts-export-client.svg)](https://www.nuget.org/packages/highcharts-export-client/)

Highcharts-export-client is [highcharts-export-server](http://www.highcharts.com/docs/export-module/export-module-overview) API wrapper. 
With help of this library you can generate fancy charts and export it to png, pdf or svg formats. 
See example below:

![Highchart chart](https://habrastorage.org/files/33f/df6/a79/33fdf6a791ae4f6982043455fc4e20b4.png)


#Installation

Install via NuGet

```
PM > Install-Package Newtonsoft.Json
```

#Requirements

* In development, you can use default highcharts server [http://export.highcharts.com/](http://export.highcharts.com/).

* In production, library needs a server with installed [highcharts-export-server](link). Basic installation steps described [here](http://www.highcharts.com/docs/export-module/setting-up-the-server) & [here](http://withr.me/set-up-highcharts-export-server-on-ubuntu-server-12-dot-04-step-by-step/).

#Usage
##Draw chart from Highchart Options

![Basic Chart](https://habrastorage.org/files/e03/b5a/884/e03b5a884e7a43ad9b25d65314012444.jpg)

Options is a highchart options.

```
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

	var res = await client.GetChartImageFromOptions(JsonConvert.SerializeObject(options));
	
	File.WriteAllBytes("image.png", res);
```

##Draw chart from SVG

To draw a chart from SVG file, use this method:
```
	var res = await client.GetChartImageFromSvg(_svg);
```

##Chart settings
You can pass settings to library constructor for controlling result file properties.
Full settings description see on [highcharts-export-server page](http://www.highcharts.com/docs/export-module/export-module-overview).

```
	var settings = new HighchartsSetting
	{
		ExportImageType = "jpg", // possible png, pdf, jpg 
		ScaleFactor = 4, 
		ImageWidth = 1500, // max 1600 px
		ServerAddress = _highchartsServer
	};

	var client = new HighchartsClient(settings);
```

##Async mode
You can use library in async mode. In this mode, each call will return a link to image. 
Image will be stored on the server for 15 minutes. See full documentation on [highcharts-export-server](http://www.highcharts.com/docs/export-module/export-module-overview).

```
	var res = await client.GetChartImageLinkFromOptions(JsonConvert.SerializeObject(options));
```

#License

The MIT License

Copyright (c) 2016 Ilya Pirozhenko http://www.sochix.ru/

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
