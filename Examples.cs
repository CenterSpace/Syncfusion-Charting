using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CenterSpace.NMath.Analysis;
using CenterSpace.NMath.Core;
using CenterSpace.NMath.Stats;
using CenterSpace.NMath.Charting.Syncfusion;

using Syncfusion.Windows.Forms.Chart;


namespace CenterSpace.NMath.Charting.Syncfusion
{
  class Examples
  {
    static void Main( string[] args )
    {
      // Class NMathChart and NMathStatsChart provide static methods for plotting NMath
      // types using Syncfusion Essential Chart for Windows Forms controls.

      // EXAMPLE 1: CURVE FITTING

      // This NMath code fits a 4-parameter logistic function to data measuring the evolution
      // of an algal bloom in the Adriatic Sea. 
      DoubleVector x = new DoubleVector( 11, 15, 18, 23, 26, 31, 39, 44, 54, 64, 74 );
      DoubleVector y = new DoubleVector( 0.00476, 0.0105, 0.0207, 0.0619, 0.337, 0.74, 1.7, 2.45, 3.5, 4.5, 5.09 );
      DoubleVector start = new DoubleVector( 4, 0.1 );
      OneVariableFunctionFitter<TrustRegionMinimizer> fitter =
        new OneVariableFunctionFitter <TrustRegionMinimizer>( AnalysisFunctions.FourParameterLogistic );
      DoubleVector solution = fitter.Fit( x, y, start );

      // For prototyping and debugging console applications, Show() plots common NMath types
      // and displays the chart in a default form.
      int numInterpolatedValues = 100;
      NMathChart.Show( fitter, x, y, solution, numInterpolatedValues );

      // The default look of the chart is governed by static properties: DefaultSize,
      // DefaultTitleFont, DefaultAxisTitleFont, DefaultMajorGridLineColor, and DefaultMarker.

      // For more control, ToChart() returns an instance of Syncfusion.Windows.Forms.Chart.ChartControl,
      // which can be customized as desired.
      ChartControl chart = NMathChart.ToChart( fitter, x, y, solution, numInterpolatedValues );
      chart.Titles[0].Text = "Algal Bloom in the Adriatic Sea";
      chart.PrimaryXAxis.Title = "Days";
      chart.PrimaryYAxis.Title = "Size (mm2)";
      chart.Series[0].Text = "Observed";
      chart.Series[1].Text = "Fitted 4PL";
      chart.BackColor = Color.Beige;
      NMathChart.Show( chart );

      // If you are developing a Windows Forms application using the Designer, add a ChartControl
      // to your form, then update it with an NMath object using the appropriate Update() function
      // after initialization. 

      // InitializeComponent();
      // NMathChart.Update( ref this.chart1, fitter, x, y, solution, numInterpolatedValues );


      // EXAMPLE 2: FFT

      // This chart shows a complex signal vector with three component sine waves.
      int n = 100;
      DoubleVector t = new DoubleVector( n, 0, 0.1 );
      DoubleVector signal = new DoubleVector( n );
      for( int i = 0; i < n; i++ )
      {
        signal[i] = Math.Sin( 2 * Math.PI * t[i] ) + 2 * Math.Sin( 2 * Math.PI * 2 * t[i] ) + 3 * Math.Sin( 2 * Math.PI * 3 * t[i] );
      }
      chart = NMathChart.ToChart( signal, new NMathChart.Unit( 0, 0.1, "Time (s)" ) );
      chart.Titles[0].Text = "Signal";
      chart.ChartArea.PrimaryYAxis.Title = "Voltage";
      NMathChart.Show( chart );

      // We use NMath to compute the forward discrete fourier transform, then plot the power in the frequency domain.
      DoubleForward1DFFT fft = new DoubleForward1DFFT( n );
      fft.FFTInPlace( signal );
      DoubleSymmetricSignalReader reader = fft.GetSignalReader( signal );
      DoubleComplexVector unpacked = reader.UnpackSymmetricHalfToVector();
      chart = NMathChart.ToChart( unpacked, new NMathChart.Unit( 0, 0.1, "Frequency (Hz)" ) );
      chart.Titles[0].Text = "FFT";
      chart.ChartArea.PrimaryYAxis.Title = "Power";
      NMathChart.Show( chart );


      // EXAMPLE 3: PEAK FINDING

      // NMath class PeakFinderSavitzkyGolay uses smooth Savitzky-Golay derivatives to find peaks in data.
      // A peak is defined as a smoothed derivative zero crossing.
      double step_size = 0.1;
      x = new DoubleVector( 1000, 0.01, step_size );
      y = NMathFunctions.Sin( x ) / x;
      int width = 5;
      int polynomial_degree = 4;
      PeakFinderSavitzkyGolay pf = new PeakFinderSavitzkyGolay( y, width, polynomial_degree );
      pf.AbscissaInterval = step_size;
      pf.SlopeSelectivity = 0;
      pf.RootFindingTolerance = 0.0001;
      pf.LocatePeaks();

      // Plot the peaks.
      double xmin = 20;
      double xmax = 50;
      NMathChart.Show( pf, xmin, xmax );

     
      // EXAMPLE 4: K-MEANS CLUSTERING

      // The k-means clustering method assigns data points into k groups such that the sum of squares from points
      // to the computed cluster centers is minimized. Here we cluster 30 points in 3-dimensional space into 5 clusters.
      DoubleMatrix data = new DoubleMatrix( @"30 x 3 [
        0.62731478808400   0.71654239725005   0.11461282117064
        0.69908013774534   0.51131144816890   0.66485556714021
        0.39718395379261   0.77640121193349   0.36537389168912
        0.41362889533818   0.48934547589850   0.14004445653473
        0.65521294635567   0.18590445122522   0.56677280030311
        0.83758509883186   0.70063540514612   0.82300831429067
        0.37160803224266   0.98270880190626   0.67394863209536
        0.42525315848265   0.80663774928874   0.99944730494940
        0.59466337145257   0.70356765500360   0.96163640714857
        0.56573857208571   0.48496371932457   0.05886216545559
        1.36031117091978   1.43187338560697   1.73265064912939
        1.54851281373460   1.63426595631548   1.42222658611939
        1.26176956987179   1.80302634023193   1.96136999885631
        1.59734484793384   1.08388100700103   1.07205923855201
        1.04927799659601   1.94546278791039   1.55340796803039
        1.57105749438466   1.91594245989412   1.29198392114244
        1.70085723323733   1.60198742363800   1.85796351308408
        1.96228825871716   1.25356057873233   1.33575513868621
        1.75051823194427   1.87345080554039   1.68020385037051
        1.73999304537847   1.51340070999628   1.05344442131849
        2.35665553727760   2.67000386489368   2.90898934903532
        2.49830459603553   2.20087641229516   2.59624713810572
        2.43444053822029   2.27308816154697   2.32895530216404
        2.56245841710735   2.62623463865051   2.47819442572535
        2.61662113016546   2.53685169481751   2.59717077926034
        2.11333998089856   2.05950405092050   2.16144875489995
        2.89825174061313   2.08896175947532   2.82947425087386
        2.75455137523865   2.27130817438170   2.95612240635488
        2.79112319571067   2.40907231577105   2.59554799520203
        2.81495206793323   2.47404145037448   2.02874821321149 ]" );
      KMeansClustering km = new KMeansClustering( data );
      ClusterSet clusters = km.Cluster( 5 );

      // We have to specify which plane to plot.
      int xColIndex = 0;
      int yColIndex = 1;
      NMathStatsChart.Show( clusters, data, xColIndex, yColIndex );
  
    }
  }
}
