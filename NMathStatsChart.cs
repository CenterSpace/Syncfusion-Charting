/////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2003-2011 CenterSpace Software, Inc. All Rights Reserved. //
//                                                                         //
// This code is free software under the Artistic license.                  //
//                                                                         //
// CenterSpace Software                                                    //
// 230 SW 3rd Street - Suite #311                                          //
// Corvallis, Oregon 97333                                                 //
// USA                                                                     //
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Syncfusion.Windows.Forms.Chart;

using CenterSpace.NMath.Analysis;
using CenterSpace.NMath.Core;
using CenterSpace.NMath.Matrix;
using CenterSpace.NMath.Stats;

namespace CenterSpace.NMath.Charting.Syncfusion
{
  /// <summary>
  /// Class NMathStatsChart provides static methods for plotting NMath Stats types using Syncfusion
  /// Essential Chart for Windows Forms controls.
  /// </summary>
  /// <remarks>
  /// Overloads of the ToChart() function are provided for common NMath Stats types. ToChart() returns
  /// an instance of Syncfusion.Windows.Forms.Chart.ChartControl, which can be customized as desired.
  /// <code>
  /// Double Matrix data = new DoubleMatrix( 100, 10, rnd );
  /// DoublePCA pca = new DoublePCA( data );
  /// ChartControl chart = NMathStatsChart.ToChart( pca );
  /// chart.Titles.Add("Hello World");
  /// </code>
  /// The default look of the chart is governed by static properties on this class: DefaultSize,
  /// DefaultTitleFont, DefaultAxisTitleFont, DefaultMajorGridLineColor, and DefaultMarker.
  /// <br/>
  /// For prototyping and debugging, the Show() function shows a given chart in a default form.
  /// <code>
  /// NMathStatsChart.Show( chart );
  /// </code>
  /// Note that when the window is closed, the chart is disposed.
  /// <br/>
  /// If you do not need to customize the chart, overloads of Show() are also provided for common
  /// NMath Stats types.
  /// <code>
  /// NMathStatsChart.Show( pca );
  /// </code>
  /// This is equivalent to calling:
  /// <code>
  /// NMathStatsChart.Show( NMathStatsChart.ToChart( pca ) );
  /// </code>
  /// The Save() function saves a chart to a file. 
  /// <code>
  /// NMathStatsChart.Save( chart, "chart.png" );
  /// </code>
  /// If you are developing a Windows Forms application using the Designer, add a ChartControl
  /// to your form, then update it with an NMath Stats object using the appropriate Update()
  /// function after initialization. 
  /// <code>
  /// public Form1()
  /// {
  ///   InitializeComponent();
  ///   
  ///   Double Matrix data = new DoubleMatrix( 100, 10, rnd );
  ///   DoublePCA pca = new DoublePCA( data );
  ///   NMathStatsChart.Update( ref this.chart1, pca );
  /// }
  /// </code>
  /// Titles are added only if the given chart does not currently contain any titles;
  /// chart.Series[0] is replaced, or added if necessary.
  /// </remarks>
  public class NMathStatsChart : NMathChart
  {

    #region Enumerations ----------------------------------------------------

    /// <summary>
    /// Enumeration for specifying the function to plot for a probability distribution.
    /// </summary>
    public enum DistributionFunction
    {
      /// <summary>
      /// Probability density function.
      /// </summary>
      PDF,

      /// <summary>
      /// Cumulative distribution function.
      /// </summary>
      CDF,
    }

    #endregion Enumerations

    #region Static Functions ------------------------------------------------

    #region IDFColumn -------------------------------------------------------

    /// <summary>
    /// Returns a line chart containing the given column data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="InvalidArgumentException">Thrown if the given column is not numeric.</exception>
    public static ChartControl ToChart( IDFColumn y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given column data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <exception cref="InvalidArgumentException">Thrown if the given column is not numeric.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, IDFColumn y )
    {
      Update( ref chart, y, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <exception cref="InvalidArgumentException">Thrown if the given column is not numeric.</exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( y ) );
    /// </code>
    /// </remarks>
    public static void Show( IDFColumn y )
    {
      Show( ToChart( y ) );
    }

    /// <summary>
    /// Returns a line chart containing the given column data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="InvalidArgumentException">Thrown if the given column is not numeric.</exception>
    public static ChartControl ToChart( IDFColumn y, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given column data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="InvalidArgumentException">Thrown if the given column is not numeric.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, IDFColumn y, Unit xUnits )
    {
      string title = "IDFColumn";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      ChartSeries series = BindXY( xUnits.ToDoubleVector( y.Count ), y, ChartSeriesType.Line, ChartSymbolShape.None );
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="InvalidArgumentException">Thrown if the given column is not numeric.</exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( y, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( IDFColumn y, Unit xUnits )
    {
      Show( ToChart( y, xUnits ) );
    }

    /// <summary>
    /// Returns a new point (scatter) chart containing the given x-y data.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    /// <exception cref="InvalidArgumentException">Thrown if either column is not numeric.</exception>
    public static ChartControl ToChart( IDFColumn x, IDFColumn y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, x, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given x-y data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    /// <exception cref="InvalidArgumentException">Thrown if either column is not numeric.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, IDFColumn x, IDFColumn y )
    {
      string title = "IDFColumn vs. IDFColumn";
      string xTitle = "x";
      string yTitle = "y";
      ChartSeries series = BindXY( x, y, ChartSeriesType.Scatter, DefaultMarker );
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    /// <exception cref="InvalidArgumentException">Thrown if either column is not numeric.</exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( x, y ) );
    /// </code>
    /// </remarks>
    public static void Show( IDFColumn x, IDFColumn y )
    {
      Show( ToChart( x, y ) );
    }

    /// <summary>
    /// Returns a new line chart for the given columns.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// </remarks>
    public static ChartControl ToChart( IDFColumn[] data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, IDFColumn[] data )
    {
      Update( ref chart, data, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( data ) );
    /// </code>
    /// </remarks>
    public static void Show( IDFColumn[] data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the given columns.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// </remarks>
    public static ChartControl ToChart( IDFColumn[] data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, IDFColumn[] data, Unit xUnits )
    {
      string title = "IDFColumn[]";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Length; i++ )
      {
        if( data[i].IsNumeric )
        {
          ChartSeries series = BindXY( xUnits.ToDoubleVector( data[i].Count ), data[i], ChartSeriesType.Line, ChartSymbolShape.None );
          series.Text = data[i].Name;
          seriesList.Add( series );
        }
      }
      Update( ref chart, seriesList, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( IDFColumn[] data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    #endregion IDFColumn

    #region DFIntColumn -----------------------------------------------------

    /// <summary>
    /// Returns a line chart containing the given column data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DFIntColumn y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given column data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DFIntColumn y )
    {
      Update( ref chart, y, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( y ) );
    /// </code>
    /// </remarks>
    public static void Show( DFIntColumn y )
    {
      Show( ToChart( y ) );
    }

    /// <summary>
    /// Returns a line chart containing the given column data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DFIntColumn y, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given column data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DFIntColumn y, Unit xUnits )
    {
      string title = "DFIntColumn";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      ChartSeries series = BindXY( xUnits.ToDoubleVector( y.Count ), y, ChartSeriesType.Line, ChartSymbolShape.None );
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( y, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( DFIntColumn y, Unit xUnits )
    {
      Show( ToChart( y, xUnits ) );
    }

    /// <summary>
    /// Returns a new point (scatter) chart containing the given x-y data.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    public static ChartControl ToChart( DFIntColumn x, DFIntColumn y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, x, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given x-y data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DFIntColumn x, DFIntColumn y )
    {
      string title = "DFIntColumn vs. DFIntColumn";
      string xTitle = "x";
      string yTitle = "y";
      ChartSeries series = BindXY( x, y, ChartSeriesType.Scatter, DefaultMarker );
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( x, y ) );
    /// </code>
    /// </remarks>
    public static void Show( DFIntColumn x, DFIntColumn y )
    {
      Show( ToChart( x, y ) );
    }

    /// <summary>
    /// Returns a new line chart for the given columns.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// </remarks>
    public static ChartControl ToChart( DFIntColumn[] data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DFIntColumn[] data )
    {
      Update( ref chart, data, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// <br/>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( data ) );
    /// </code>
    /// </remarks>
    public static void Show( DFIntColumn[] data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the given columns.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// </remarks>
    public static ChartControl ToChart( DFIntColumn[] data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DFIntColumn[] data, Unit xUnits )
    {
      string title = "DFIntColumn[]";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Length; i++ )
      {
        if( data[i].IsNumeric )
        {
          ChartSeries series = BindXY( xUnits.ToDoubleVector( data[i].Count ), data[i], ChartSeriesType.Line, ChartSymbolShape.None );
          series.Text = data[i].Name;
          seriesList.Add( series );
        }
      }
      Update( ref chart, seriesList, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// <br/>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( DFIntColumn[] data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    #endregion DFIntColumn

    #region DFNumericColumn -------------------------------------------------

    /// <summary>
    /// Returns a line chart containing the given column data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DFNumericColumn y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given column data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DFNumericColumn y )
    {
      Update( ref chart, y, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( y ) );
    /// </code>
    /// </remarks>
    public static void Show( DFNumericColumn y )
    {
      Show( ToChart( y ) );
    }

    /// <summary>
    /// Returns a line chart containing the given column data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DFNumericColumn y, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given column data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DFNumericColumn y, Unit xUnits )
    {
      string title = "DFNumericColumn";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      ChartSeries series = BindXY( xUnits.ToDoubleVector( y.Count ), y, ChartSeriesType.Line, ChartSymbolShape.None );
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( y, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( DFNumericColumn y, Unit xUnits )
    {
      Show( ToChart( y, xUnits ) );
    }

    /// <summary>
    /// Returns a new point (scatter) chart containing the given x-y data.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    public static ChartControl ToChart( DFNumericColumn x, DFNumericColumn y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, x, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given x-y data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DFNumericColumn x, DFNumericColumn y )
    {
      string title = "DFNumericColumn vs. DFNumericColumn";
      string xTitle = "x";
      string yTitle = "y";
      ChartSeries series = BindXY( x, y, ChartSeriesType.Scatter, DefaultMarker );
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( x, y ) );
    /// </code>
    /// </remarks>
    public static void Show( DFNumericColumn x, DFNumericColumn y )
    {
      Show( ToChart( x, y ) );
    }

    /// <summary>
    /// Returns a new line chart for the given columns.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DFNumericColumn[] data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DFNumericColumn[] data )
    {
      Update( ref chart, data, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( data ) );
    /// </code>
    /// </remarks>
    public static void Show( DFNumericColumn[] data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the given columns.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DFNumericColumn[] data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DFNumericColumn[] data, Unit xUnits )
    {
      string title = "DFNumericColumn[]";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Length; i++ )
      {
        if( data[i].IsNumeric )
        {
          ChartSeries series = BindXY( xUnits.ToDoubleVector( data[i].Count ), data[i], ChartSeriesType.Line, ChartSymbolShape.None );
          series.Text = data[i].Name;
          seriesList.Add( series );
        }
      }
      Update( ref chart, seriesList, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( DFNumericColumn[] data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    #endregion DFNumericColumn

    #region DataFrame -------------------------------------------------------

    /// <summary>
    /// Returns a new line chart for the columns of the given data frame.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// </remarks>
    public static ChartControl ToChart( DataFrame data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the columns of the given data frame.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Cols data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DataFrame data )
    {
      Update( ref chart, data, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( data ) );
    /// </code>
    /// Non-numeric columns are ignored.
    /// </remarks>
    public static void Show( DataFrame data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the columns of the given data frame.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// </remarks>
    public static ChartControl ToChart( DataFrame data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the columns of the given data frame.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Non-numeric columns are ignored.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Cols data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DataFrame data, Unit xUnits )
    {
      string title = "DataFrame";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Cols; i++ )
      {
        if( data[i].IsNumeric )
        {
          ChartSeries series = BindXY( xUnits.ToDoubleVector( data[i].Count ), data[i], ChartSeriesType.Line, ChartSymbolShape.None );
          series.Text = data.ColumnNames[i];
          seriesList.Add( series );
        }
      }
      Update( ref chart, seriesList, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// Non-numeric columns are ignored.
    /// </remarks>
    public static void Show( DataFrame data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    /// <summary>
    /// Returns a new point chart for the specified columns of the given data frame (x, y) .
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the column containing the x data.</param>
    /// <param name="yColIndex">The index of the column containing the y data.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="InvalidArgumentException">Thrown if either column is not numeric.</exception>
    public static ChartControl ToChart( DataFrame data, int xColIndex, int yColIndex )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xColIndex, yColIndex );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the columns of the given data frame.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the column containing the x data.</param>
    /// <param name="yColIndex">The index of the column containing the y data.</param>
    /// <exception cref="InvalidArgumentException">Thrown if either column is not numeric.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DataFrame data, int xColIndex, int yColIndex )
    {
      if( xColIndex < 0 || xColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( xColIndex );
      }

      if( yColIndex < 0 || yColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( yColIndex );
      }

      string title = "DataFrame";
      string xTitle = data.ColumnNames[xColIndex];
      string yTitle = data.ColumnNames[yColIndex];
      ChartSeries series = BindXY( data[xColIndex], data[yColIndex], ChartSeriesType.Scatter, DefaultMarker );
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the column containing the x data.</param>
    /// <param name="yColIndex">The index of the column containing the y data.</param>
    /// <exception cref="InvalidArgumentException">Thrown if either column is not numeric.</exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( data, xColIndex, yColIndex ) );
    /// </code>
    /// </remarks>
    public static void Show( DataFrame data, int xColIndex, int yColIndex )
    {
      Show( ToChart( data, xColIndex, yColIndex ) );
    }

    /// <summary>
    /// Returns a new line chart for the specified columns of the given data frame.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <exception cref="InvalidArgumentException">Thrown if any column is not numeric.</exception>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DataFrame data, int[] colIndices )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, colIndices );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified columns of the given data frame.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <exception cref="InvalidArgumentException">Thrown if any column is not numeric.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first colIndices.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DataFrame data, int[] colIndices )
    {
      Update( ref chart, data, colIndices, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the rows to plot.</param>
    /// <exception cref="InvalidArgumentException">Thrown if any column is not numeric.</exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( data, colIndices ) );
    /// </code>
    /// </remarks>
    public static void Show( DataFrame data, int[] colIndices )
    {
      Show( ToChart( data, colIndices ) );
    }

    /// <summary>
    /// Returns a new line chart for the specified columns of the given data frame.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="InvalidArgumentException">Thrown if any column is not numeric.</exception>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DataFrame data, int[] colIndices, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, colIndices, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified columns of the given data frame.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="InvalidArgumentException">Thrown if any column is not numeric.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first colIndices.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DataFrame data, int[] colIndices, Unit xUnits )
    {
      string title = "DataFrame";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < colIndices.Length; i++ )
      {
        int index = colIndices[i];
        if( index < 0 || index > data.Cols - 1 )
        {
          throw new Core.IndexOutOfRangeException( index );
        }

        ChartSeries series = BindXY( xUnits.ToDoubleVector( data[index].Count ), data[index], ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = data.ColumnNames[index];
        seriesList.Add( series );
      }
      Update( ref chart, seriesList, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the rows to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="InvalidArgumentException">Thrown if any column is not numeric.</exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( data, colIndices, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( DataFrame data, int[] colIndices, Unit xUnits )
    {
      Show( ToChart( data, colIndices, xUnits ) );
    }

    #endregion DataFrame

    #region BetaDistribution ------------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( BetaDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, BetaDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "BetaDistribution",
        String.Format("\u03B1={0}, \u03B2={1}", dist.Alpha, dist.Beta)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( BetaDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region BinomialDistribution --------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( BinomialDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, BinomialDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      List<string> titles = new List<string>()
      {
        "BinomialDistribution",
        String.Format("n={0}, p={1}", dist.N, dist.P)
      };
      UpdateDiscreteDistribution( ref chart, dist, titles, function );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function ) );
    /// </code>
    /// </remarks>
    public static void Show( BinomialDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      Show( ToChart( dist, function ) );
    }

    #endregion

    #region ChiSquareDistribution -------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( ChiSquareDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, ChiSquareDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "ChiSquareDistribution",
        String.Format("df={0}", dist.DegreesOfFreedom)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( ChiSquareDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region ExponentialDistribution -----------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( ExponentialDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, ExponentialDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "ExponentialDistribution",
        String.Format("\u03BB={0}", dist.Lambda)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( ExponentialDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region FDistribution ---------------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( FDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "FDistribution",
        String.Format("df1={0}, df2={1}", dist.DegreesOfFreedom1, dist.DegreesOfFreedom2 )
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( FDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region GammaDistribution -----------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( GammaDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, GammaDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "GammaDistribution",
        String.Format("\u03B1={0}, \u03B2={1}", dist.Alpha, dist.Beta)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( GammaDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region GeometricDistribution -------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( GeometricDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, GeometricDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      List<string> titles = new List<string>()
      {
        "GeometricDistribution",
        String.Format("p={0}", dist.P)
      };
      UpdateDiscreteDistribution( ref chart, dist, titles, function );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function ) );
    /// </code>
    /// </remarks>
    public static void Show( GeometricDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      Show( ToChart( dist, function ) );
    }

    #endregion

    #region JohnsonDistribution ---------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( JohnsonDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, JohnsonDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "JohnsonDistribution",
        String.Format("\u03B3={0}, \u03B4={1}, \u03BE={2}, \u03BB={3}", dist.Gamma, dist.Delta, dist.Xi, dist.Lambda)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( JohnsonDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region LogisticDistribution --------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( LogisticDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, LogisticDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "LogisticDistribution",
        String.Format("location={0}, scale={1}", dist.Location, dist.Scale)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( LogisticDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region LognormalDistribution -------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( LognormalDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, LognormalDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "LognormalDistribution",
        String.Format("\u03BC={0}, \u03C3={1}", dist.Mu, dist.Sigma)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( LognormalDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region NegativeBinomialDistribution ------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( NegativeBinomialDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, NegativeBinomialDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      List<string> titles = new List<string>()
      {
        "NegativeBinomialDistribution",
        String.Format("n={0}, p={1}", dist.N, dist.P)
      };
      UpdateDiscreteDistribution( ref chart, dist, titles, function );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function ) );
    /// </code>
    /// </remarks>
    public static void Show( NegativeBinomialDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      Show( ToChart( dist, function ) );
    }

    #endregion

    #region NormalDistribution ----------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( NormalDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, NormalDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "NormalDistribution",
        String.Format("\u03BC={0}, \u03C3\u00B2={1}", dist.Mean, dist.Variance)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( NormalDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region PoissonDistribution ---------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( PoissonDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, PoissonDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      List<string> titles = new List<string>()
      {
        "PoissonDistribution",
        String.Format("\u03BB={0}", dist.Mean)
      };
      UpdateDiscreteDistribution( ref chart, dist, titles, function );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function ) );
    /// </code>
    /// </remarks>
    public static void Show( PoissonDistribution dist, DistributionFunction function = DistributionFunction.PDF )
    {
      Show( ToChart( dist, function ) );
    }

    #endregion

    #region TDistribution ---------------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( TDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, TDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "TDistribution",
        String.Format("df={0}", dist.DegreesOfFreedom)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( TDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region TriangularDistribution ------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( TriangularDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, TriangularDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "TriangularDistribution",
        String.Format("lower={0}, upper={1}, mode={2}", dist.LowerLimit, dist.UpperLimit, dist.Mode)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( TriangularDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region UniformDistribution ---------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( UniformDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, UniformDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "UniformDistribution",
        String.Format("lower={0}, upper={1}", dist.LowerLimit, dist.UpperLimit)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( UniformDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region WeibullDistribution ---------------------------------------------

    /// <summary>
    /// Returns a new line chart plotting the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( WeibullDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, dist, function, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified distribution.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Plots the specified function of the given distribution for 0.0001 &lt;= p &lt;= 0.9999.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, WeibullDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      List<string> titles = new List<string>()
      {
        "WeibullDistribution",
        String.Format("scale={0}, shape={1}", dist.Scale, dist.Shape)
      };
      UpdateContinuousDistribution( ref chart, dist, titles, function, numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="dist">The distribution.</param>
    /// <param name="function">The distribution function to plot.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( dist, function, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( WeibullDistribution dist, DistributionFunction function = DistributionFunction.PDF, int numInterpolatedValues = 100 )
    {
      Show( ToChart( dist, function, numInterpolatedValues ) );
    }

    #endregion

    #region LinearRegression ------------------------------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given linear regression.
    /// </summary>
    /// <param name="lr">Linear Regression.</param>
    /// <param name="predictorIndex">The predictor (independent) variable to plot on the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if the given predictorIndex is outside the range of columns in lr.PredictorMatrix.
    /// </exception>
    /// <remarks>
    /// The multidimensional linear regression fit is plotted projected onto the plane of the specified 
    /// predictor variable.
    /// </remarks>
    public static ChartControl ToChart( LinearRegression lr, int predictorIndex )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, lr, predictorIndex );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified linear regression.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="lr">Linear Regression.</param>
    /// <param name="predictorIndex">The predictor (independent) variable to plot on the x-axis.</param>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if the given predictorIndex is outside the range of columns in lr.PredictorMatrix.
    /// </exception>
    /// <remarks>
    /// The multidimensional linear regression fit is plotted projected onto the plane of the specified 
    /// predictor variable.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, LinearRegression lr, int predictorIndex )
    {
      if( predictorIndex < 0 || predictorIndex > lr.PredictorMatrix.Cols )
      {
        throw new Core.IndexOutOfRangeException( predictorIndex );
      }

      List<string> titles = new List<string>()
      {
        "LinearRegression",
      };
      string xTitle = "Independent Variable " + predictorIndex;
      string yTitle = "Dependent Variable";

      // create version of predictor matrix with all other columns zeroed out
      DoubleMatrix projection = new DoubleMatrix( lr.PredictorMatrix.Rows, lr.PredictorMatrix.Cols );
      projection[Slice.All, predictorIndex] = lr.PredictorMatrix.Col( predictorIndex );

      DoubleMatrix data = new DoubleMatrix( lr.NumberOfObservations, 3 );
      data[Slice.All, 0] = lr.PredictorMatrix.Col( predictorIndex );          // x
      data[Slice.All, 1] = lr.Observations;                                   // y
      data[Slice.All, 2] = lr.PredictedObservations( projection );            // y predicted

      data = NMathFunctions.SortByColumn( data, 0 );

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( data.Col(0), data.Col(1), ChartSeriesType.Scatter, DefaultMarker ),

        // only necessary to plot endpoints of line
        BindXY( data.Col(0)[new Slice(0, 2, data.Rows - 1)], data.Col(2)[new Slice(0, 2, data.Rows - 1)], ChartSeriesType.Line, ChartSymbolShape.None )
      };
      series[0].Text = "Observed";
      series[1].Text = "Predicted";

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="lr">Linear Regression.</param>
    /// <param name="predictorIndex">The predictor (independent) variable to plot on the x-axis.</param>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if the given predictorIndex is outside the range of columns in lr.PredictorMatrix.
    /// </exception>
    /// <remarks>
    /// The multidimensional linear regression fit is plotted projected onto the plane of the specified 
    /// predictor variable.
    /// <br/>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( lr, predictorIndex ) );
    /// </code>
    /// </remarks>
    public static void Show( LinearRegression lr, int predictorIndex )
    {
      Show( ToChart( lr, predictorIndex ) );
    }

    #endregion

    #region ClusterSet ------------------------------------------------------

    /// <summary>
    /// Returns a new point chart plotting the given clusters.
    /// </summary>
    /// <param name="clusters">The cluster assignments.</param>
    /// <param name="data">A matrix of data. Each row in the matrix represents an object that was clustered.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="Core.IndexOutOfRangeException">Thrown if either column index is outside the
    /// range of the columns of the given data matrix.</exception>
    /// <remarks>
    /// Instances of class ClusterSet are created by ClusterAnalysis, KMeanClustering, and NMFClustering objects
    /// and cannot be constructed independently.
    /// <br/>
    /// Objects are shown plotted in the specified x,y plane, colored according to their cluster assignment.
    /// </remarks>
    public static ChartControl ToChart( ClusterSet clusters, DoubleMatrix data, int xColIndex, int yColIndex )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, clusters, data, xColIndex, yColIndex );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified clusters.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="clusters">The cluster assignments.</param>
    /// <param name="data">A matrix of data. Each row in the matrix represents an object that was clustered.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <exception cref="Core.IndexOutOfRangeException">Thrown if either column index is outside the
    /// range of the columns of the given data matrix.</exception>
    /// <remarks>
    /// Instances of class ClusterSet are created by ClusterAnalysis, KMeanClustering, and NMFClustering objects
    /// and cannot be constructed independently.
    /// <br/>
    /// Objects are shown plotted in the specified x,y plane, and colored according to their cluster assignment.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first clusters.NumberOfClusters data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, ClusterSet clusters, DoubleMatrix data, int xColIndex, int yColIndex )
    {

      if( xColIndex < 0 || xColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( xColIndex );
      }

      if( yColIndex < 0 || yColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( yColIndex );
      }

      List<string> titles = new List<string>()
      {
        "ClusterSet",
      };
      string xTitle = String.Format( "Col {0}", xColIndex );
      string yTitle = String.Format( "Col {0}", yColIndex );

      List<ChartSeries> series = new List<ChartSeries>();
      for( int i = 0; i < clusters.NumberOfClusters; i++ )
      {
        int[] members = clusters.Cluster( i );
        ChartSeries s = new ChartSeries()
        {
          Text = "Cluster " + i,
          Type = ChartSeriesType.Scatter,
        };
        s.Style.Symbol.Shape = DefaultMarker;
        for( int j = 0; j < members.Length; j++ )
        {
          if( members[j] < 0 || members[j] > data.Rows) 
          {
            throw new Core.IndexOutOfRangeException( members[j] );
          }
          s.Points.Add( data[members[j], xColIndex], data[members[j], yColIndex] );
        }
        series.Add( s );
      }

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="clusters">The cluster assignments.</param>
    /// <param name="data">A matrix of data. Each row in the matrix represents an object that was clustered.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <exception cref="Core.IndexOutOfRangeException">Thrown if either column index is outside the
    /// range of the columns of the given data matrix.</exception>
    /// <remarks>
    /// Instances of class ClusterSet are created by ClusterAnalysis, KMeanClustering, and NMFClustering objects
    /// and cannot be constructed independently.
    /// <br/>
    /// Objects are shown plotted in the specified x,y plane, colored according to their cluster assignment.
    /// <br/>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( clusters, data, xColIndex, yColIndex ) );
    /// </code>
    /// </remarks>
    public static void Show( ClusterSet clusters, DoubleMatrix data, int xColIndex, int yColIndex )
    {
      Show( ToChart( clusters, data, xColIndex, yColIndex ) );
    }

    #endregion

    #region FloatPCA --------------------------------------------------------

    /// <summary>
    /// Returns a Scree chart for the given principal component analysis.
    /// </summary>
    /// <param name="pca">A principal component analysis object.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( FloatPCA pca )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, pca );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with a Scree chart for the given principal component analysis.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="pca">A principal component analysis object.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatPCA pca )
    {
      string title = "FloatPCA";
      string xTitle = "Principal Component";
      string yTitle = "Proportion of Variance";
      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindY( pca.VarianceProportions, ChartSeriesType.Line, DefaultMarker ),
        BindY( pca.CumulativeVarianceProportions, ChartSeriesType.Line, DefaultMarker )
      };
      series[0].Text = "Variance Proportions";
      series[1].Text = "Cummulative Variance Proportions";
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="pca">A principal component analysis object.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( pca ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatPCA pca )
    {
      Show( ToChart( pca ) );
    }

    /// <summary>
    /// Returns a point (scatter) chart plotting the specified two principal components
    /// against one another.
    /// </summary>
    /// <param name="pca">A principal component analysis object.</param>
    /// <param name="xIndex">The index of the principal component to use for the x values.</param>
    /// <param name="yIndex">The index of the principal component to use for the y values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( FloatPCA pca, int xIndex, int yIndex )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, pca, xIndex, yIndex );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with a point (scatter) chart plotting the specified two
    /// principal components against one another.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="pca">A principal component analysis object.</param>
    /// <param name="xIndex">The index of the principal component to use for the x values.</param>
    /// <param name="yIndex">The index of the principal component to use for the y values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatPCA pca, int xIndex, int yIndex )
    {
      if( xIndex < 0 || xIndex > pca.Scores.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( xIndex );
      }

      if( yIndex < 0 || yIndex > pca.Scores.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( yIndex );
      }

      string title = "FloatPCA";
      string xTitle = String.Format( "PCA {0}", xIndex );
      string yTitle = String.Format( "PCA {0}", yIndex );
      ChartSeries series = BindXY( pca.Scores.Col( xIndex ), pca.Scores.Col( yIndex ), ChartSeriesType.Scatter, DefaultMarker );
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="pca">A principal component analysis object.</param>
    /// <param name="xIndex">The index of the principal component to use for the x values.</param>
    /// <param name="yIndex">The index of the principal component to use for the y values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( pca, xIndex, yIndex ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatPCA pca, int xIndex, int yIndex )
    {
      Show( ToChart( pca, xIndex, yIndex ) );
    }

    #endregion FloatPCA

    #region DoublePCA -------------------------------------------------------

    /// <summary>
    /// Returns a Scree chart for the given principal component analysis.
    /// </summary>
    /// <param name="pca">A principal component analysis object.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DoublePCA pca )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, pca );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with a Scree chart for the given principal component analysis.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="pca">A principal component analysis object.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoublePCA pca )
    {
      string title = "DoublePCA";
      string xTitle = "Principal Component";
      string yTitle = "Proportion of Variance";
      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindY( pca.VarianceProportions, ChartSeriesType.Line, DefaultMarker ),
        BindY( pca.CumulativeVarianceProportions, ChartSeriesType.Line, DefaultMarker )
      };
      series[0].Text = "Variance Proportions";
      series[1].Text = "Cummulative Variance Proportions";
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="pca">A principal component analysis object.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( pca ) );
    /// </code>
    /// </remarks>
    public static void Show( DoublePCA pca )
    {
      Show( ToChart( pca ) );
    }

    /// <summary>
    /// Returns a point (scatter) chart plotting the specified two principal components
    /// against one another.
    /// </summary>
    /// <param name="pca">A principal component analysis object.</param>
    /// <param name="xIndex">The index of the principal component to use for the x values.</param>
    /// <param name="yIndex">The index of the principal component to use for the y values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DoublePCA pca, int xIndex, int yIndex )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, pca, xIndex, yIndex );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with a point (scatter) chart plotting the specified two
    /// principal components against one another.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="pca">A principal component analysis object.</param>
    /// <param name="xIndex">The index of the principal component to use for the x values.</param>
    /// <param name="yIndex">The index of the principal component to use for the y values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoublePCA pca, int xIndex, int yIndex )
    {
      string title = "DoublePCA";
      string xTitle = String.Format( "PCA {0}", xIndex );
      string yTitle = String.Format( "PCA {0}", yIndex );
      ChartSeries series = BindXY( pca.Scores.Col(xIndex), pca.Scores.Col(yIndex), ChartSeriesType.Scatter, DefaultMarker );
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="pca">A principal component analysis object.</param>
    /// <param name="xIndex">The index of the principal component to use for the x values.</param>
    /// <param name="yIndex">The index of the principal component to use for the y values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( pca, xIndex, yIndex ) );
    /// </code>
    /// </remarks>
    public static void Show( DoublePCA pca, int xIndex, int yIndex )
    {
      Show( ToChart( pca, xIndex, yIndex ) );
    }

    #endregion DoublePCA

    #region GoodnessOfFit ---------------------------------------------------

    /// <summary>
    /// Returns a bar chart plotting the model parameter values and <c>1 - alpha</c> confidence intervals.
    /// </summary>
    /// <param name="gof">A goodness of fit object.</param>
    /// <param name="alpha">A significance level.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( GoodnessOfFit gof, double alpha )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, gof, alpha );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with a bar chart plotting the model parameter values and <c>1 - alpha</c> confidence intervals.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="gof">A goodness of fit object.</param>
    /// <param name="alpha">A significance level.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, GoodnessOfFit gof, double alpha )
    {
      List<string> titles = new List<string>()
      {
        "GoodnessOfFit",
         string.Format( "R2: {0}; adjusted R2: {1}", gof.RSquared, gof.AdjustedRsquared ),
         string.Format( "F-statistic: {0} on {1} and {2} DF,  p-value: {3}", gof.FStatistic, gof.ModelDegreesOfFreedom, gof.ErrorDegreesOfFreedom, gof.FStatisticPValue ),
      };
      string xTitle = "Parameter";
      string yTitle = "Value";

      ChartSeries series = new ChartSeries()
      {
        Type = ChartSeriesType.Column
      };
      series.ConfigItems.ErrorBars.Enabled = true;
      series.ConfigItems.ErrorBars.SymbolShape = ChartSymbolShape.None;
      for( int i = 0; i < gof.Parameters.Length; i++ )
      {
        GoodnessOfFitParameter param = gof.Parameters[i];
        Interval ci = param.ConfidenceInterval( alpha );
        series.Points.Add( new ChartPoint( i, new double[] { param.Value, param.Value - ci.Min, ci.Max - param.Value } ) );
      }

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="gof">A goodness of fit object.</param>
    /// <param name="alpha">A significance level.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathStatsChart.Show( ToChart( gof, alpha ) );
    /// </code>
    /// </remarks>
    public static void Show( GoodnessOfFit gof, double alpha )
    {
      Show( ToChart( gof, alpha ) );
    }

    #endregion GoodnessOfFit

    #region Protected -------------------------------------------------------

    /// <summary>
    /// Returns a data series of the given data.
    /// </summary>
    /// <param name="x">The x data.</param>
    /// <param name="y">The y data.</param>
    /// <param name="type">The chart type.</param>
    /// <param name="marker">The marker style to use.</param>
    /// <returns>A new data series.</returns>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    protected static ChartSeries BindXY( DoubleVector x, IDFColumn y, ChartSeriesType type, ChartSymbolShape marker )
    {

      if( x.Length != y.Count )
      {
        throw new MismatchedSizeException( "x,y data of unequal length", x.Length, y.Count );
      }

      ChartSeries series = new ChartSeries()
      {
        Text = y.Name,
        Type = type,
      };
      series.Style.Symbol.Shape = marker;

      if( y is DFNumericColumn )
      {
        for( int i = 0; i < x.Length; i++ )
        {
          series.Points.Add( x[i], (double)y[i] );
        }
      }
      else if( y is DFIntColumn )
      {
        for( int i = 0; i < x.Length; i++ )
        {
          series.Points.Add( x[i], (int)y[i] );
        }
      }
      else if( y.IsNumeric )
      {
        for( int i = 0; i < x.Length; i++ )
        {
          series.Points.Add( x[i], Double.Parse( y[i].ToString() ) );
        }
      }
      else
      {
        throw new Core.InvalidArgumentException( "Column must be numeric." );
      }

      return series;
    }

    /// <summary>
    /// Returns a data series of the given data.
    /// </summary>
    /// <param name="x">The x data.</param>
    /// <param name="y">The y data.</param>
    /// <param name="type">The chart type.</param>
    /// <param name="marker">The marker style to use.</param>
    /// <returns>A new data series.</returns>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    protected static ChartSeries BindXY( IDFColumn x, IDFColumn y, ChartSeriesType type, ChartSymbolShape marker )
    {

      if( x.Count != y.Count )
      {
        throw new MismatchedSizeException( "x,y column of unequal length", x.Count, y.Count );
      }

      ChartSeries series = new ChartSeries()
      {
        Type = type,
      };
      series.Style.Symbol.Shape = marker;

      if( x is DFNumericColumn && y is DFNumericColumn )
      {
        for( int i = 0; i < x.Count; i++ )
        {
          series.Points.Add( (double)x[i], (double)y[i] );
        }
      }
      else if( x is DFNumericColumn && y is DFIntColumn )
      {
        for( int i = 0; i < x.Count; i++ )
        {
          series.Points.Add( (double)x[i], (int)y[i] );
        }
      }
      else if( x is DFIntColumn && y is DFNumericColumn )
      {
        for( int i = 0; i < x.Count; i++ )
        {
          series.Points.Add( (int)x[i], (double)y[i] );
        }
      }
      else if( x is DFIntColumn && y is DFIntColumn )
      {
        for( int i = 0; i < x.Count; i++ )
        {
          series.Points.Add( (int)x[i], (int)y[i] );
        }
      }
      else if( x.IsNumeric && y.IsNumeric )
      {
        for( int i = 0; i < x.Count; i++ )
        {
          series.Points.Add( Double.Parse( x[i].ToString() ), Double.Parse( y[i].ToString() ) );
        }
      }
      else
      {
        throw new Core.InvalidArgumentException( "Column must be numeric." );
      }

      return series;
    }

    /// <summary></summary>
    protected static void UpdateContinuousDistribution( ref ChartControl chart, ProbabilityDistribution dist, List<string> titles, DistributionFunction function, int numInterpolatedValues )
    {
      string xTitle = "x";
      string yTitle;

      double xmin = dist.InverseCDF( 0.0001 );
      double xmax = dist.InverseCDF( 0.9999 );

      OneVariableFunction f;
      
      if( function == DistributionFunction.PDF )
      {
        yTitle = "Probability Density Function";
        f = new OneVariableFunction( delegate( double x ) { return dist.PDF( x ); } );
      }
      else
      {
        yTitle = "Cumulative Distribution Function";
        f = new OneVariableFunction( delegate( double x ) { return dist.CDF( x ); } );
      }
      

      ChartSeries series = BindXY( f, xmin, xmax, numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None );

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary></summary>
    protected static void UpdateDiscreteDistribution( ref ChartControl chart, ProbabilityDistribution dist, List<string> titles, DistributionFunction function )
    {
      string xTitle = "x";
      string yTitle;

      int xmin = (int)Math.Floor( dist.InverseCDF( 0.0001 ) );
      int xmax = (int)Math.Ceiling( dist.InverseCDF( 0.9999 ) );
      DoubleVector x = new DoubleVector( xmax - xmin + 1, xmin, 1 );
      DoubleVector y;

      if( function == DistributionFunction.PDF )
      {
        yTitle = "Probability Mass Function";
        y = x.Apply( dist.PDF );
      }
      else
      {
        yTitle = "Cumulative Distribution Function";
        y = x.Apply( dist.CDF );
      }

      ChartSeries series = BindXY( x, y, ChartSeriesType.Column, ChartSymbolShape.None );

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    #endregion Protected

    #endregion Static Functions

  }
}

