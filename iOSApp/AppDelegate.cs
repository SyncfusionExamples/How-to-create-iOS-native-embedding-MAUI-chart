using Foundation;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Platform;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Core.Hosting;
using UIKit;

namespace iOSApp
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        MauiContext _mauiContext;
        public override UIWindow? Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
            builder.ConfigureSyncfusionCore();
            // Register the Window
            builder.Services.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(typeof(UIWindow), Window));
            MauiApp mauiApp = builder.Build();
            _mauiContext = new MauiContext(mauiApp.Services);

            SfCartesianChart chart = new SfCartesianChart();
            chart.Background = Colors.White;

            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title = new ChartAxisTitle
            {
                Text = "Name",
            };
            chart.XAxes.Add(primaryAxis);

            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title = new ChartAxisTitle
            {
                Text = "Height(in cm)",
            };
            chart.YAxes.Add(secondaryAxis);

            ColumnSeries series = new ColumnSeries();
            series.ShowDataLabels = true;
            series.ItemsSource = (new ViewModel()).Data;
            series.XBindingPath = "Name";
            series.YBindingPath = "Height";

            chart.Series.Add(series);

            UIView mauiView = chart.ToPlatform(_mauiContext);
            mauiView.Frame = Window!.Frame;

            // create a UIViewController
            var vc = new UIViewController();
            vc.View!.AddSubview(mauiView);
            Window.RootViewController = vc;

            // make the window visible
            Window.MakeKeyAndVisible();

            return true;
        }

    }

}


