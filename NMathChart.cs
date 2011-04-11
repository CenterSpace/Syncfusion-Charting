
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Syncfusion.Windows.Forms.Chart;

using CenterSpace.NMath.Analysis;
using CenterSpace.NMath.Core;
using CenterSpace.NMath.Matrix;

namespace CenterSpace.NMath.Charting.Syncfusion
{

  /// <summary>
  /// Class NMathChart provides static methods for plotting NMath types using Syncfusion
  /// Essential Chart for Windows Forms controls.
  /// </summary>
  /// <remarks>
  /// Overloads of the ToChart() function are provided for common NMath types. ToChart() returns
  /// an instance of Syncfusion.Windows.Forms.Chart.ChartControl, which can be customized as desired.
  /// <code>
  /// Polynomial poly = new Polynomial( new DoubleVector( 4, 2, 5, -2, 3 ) );
  /// ChartControl chart = NMathChart.ToChart( poly, -1, 1 );
  /// chart.Titles.Add("Hello World");
  /// </code>
  /// The default look of the chart is governed by static properties on this class: DefaultSize,
  /// DefaultTitleFont, DefaultAxisTitleFont, DefaultMajorGridLineColor, and DefaultMarker.
  /// <br/>
  /// For prototyping and debugging console applications, the Show() function shows a given chart
  /// in a default form.
  /// <code>
  /// NMathChart.Show( chart );
  /// </code>
  /// Note that when the window is closed, the chart is disposed.
  /// <br/>
  /// If you do not need to customize the chart, overloads of Show() are also provided for common
  /// NMath types.
  /// <code>
  /// NMathChart.Show( poly );
  /// </code>
  /// This is equivalent to calling:
  /// <code>
  /// NMathChart.Show( NMathChart.ToChart( poly ) );
  /// </code>
  /// The Save() function saves a chart to a file or stream. 
  /// <code>
  /// NMathChart.Save( chart, "chart.png" );
  /// </code>
  /// If you are developing a Windows Forms application using the Designer, add a ChartControl
  /// to your form, then update it with an NMath object using the appropriate Update() function
  /// after initialization. 
  /// <code>
  /// public Form1()
  /// {
  ///   InitializeComponent();
  ///   
  ///   Polynomial poly = new Polynomial( new DoubleVector( 4, 2, 5, -2, 3 ) );
  ///   NMathChart.Update( ref this.chart1, poly, -1, 1 );
  /// }
  /// </code>
  /// Titles are added only if the given chart does not currently contain any titles;
  /// chart.Series[0] is replaced, or added if necessary.
  /// </remarks>
  public class NMathChart
  {

    #region Static Variables ------------------------------------------------

    private static Size DEFAULT_SIZE = new Size( 500, 500 );
    private static Font DEFAULT_TITLE_FONT = new Font( "Trebuchet MS", 12F, FontStyle.Bold );
    private static Font DEFAULT_AXIS_TITLE_FONT = new Font( "Trebuchet MS", 10F, FontStyle.Bold );
    private static Color DEFAULT_MAJORGRID_LINECOLOR = Color.LightGray;
    private static ChartSymbolShape DEFAULT_MARKER = ChartSymbolShape.Circle;

    // not exposed
    private static Size DEFAULT_MARKER_SIZE = new Size( 7, 7 );
    private static ChartTextOrientation DEFAULT_POINT_LABEL_ORIENTATION = ChartTextOrientation.Up;

    #endregion Static Variables

    #region Static Properties -----------------------------------------------

    /// <summary>
    /// Gets and sets the default size for new charts.
    /// </summary>
    public static Size DefaultSize
    {
      get
      {
        return DEFAULT_SIZE;
      }
      set
      {
        DEFAULT_SIZE = value;
      }
    }

    /// <summary>
    /// Gets and sets the default primary title font for new charts.
    /// </summary>
    public static Font DefaultTitleFont
    {
      get
      {
        return DEFAULT_TITLE_FONT;
      }
      set
      {
        DEFAULT_TITLE_FONT = value;
      }
    }

    /// <summary>
    /// Gets and sets the default axis title font for new charts.
    /// </summary>
    public static Font DefaultAxisTitleFont
    {
      get
      {
        return DEFAULT_AXIS_TITLE_FONT;
      }
      set
      {
        DEFAULT_AXIS_TITLE_FONT = value;
      }
    }

    /// <summary>
    /// Gets and sets the default major grid line color for new charts.
    /// </summary>
    public static Color DefaultMajorGridLineColor
    {
      get
      {
        return DEFAULT_MAJORGRID_LINECOLOR;
      }
      set
      {
        DEFAULT_MAJORGRID_LINECOLOR = value;
      }
    }

    /// <summary>
    /// Gets and sets the default marker for new charts.
    /// </summary>
    public static ChartSymbolShape DefaultMarker
    {
      get
      {
        return DEFAULT_MARKER;
      }
      set
      {
        DEFAULT_MARKER = value;
      }
    }

    #endregion Static Properties

    #region Static Functions ------------------------------------------------

    #region FloatVector -----------------------------------------------------

    /// <summary>
    /// Returns a line chart containing the given vector data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( FloatVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given vector data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatVector y )
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
    /// NMathChart.Show( ToChart( y ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatVector y )
    {
      Show( ToChart( y ) );
    }

    /// <summary>
    /// Returns a line chart containing the given vector data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( FloatVector y, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given vector data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatVector y, Unit xUnits )
    {
      string title = "FloatVector";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      ChartSeries series = BindXY( xUnits.ToDoubleVector( y.Length ), y, ChartSeriesType.Line, ChartSymbolShape.None );
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
    /// NMathChart.Show( ToChart( y, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatVector y, Unit xUnits )
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
    public static ChartControl ToChart( FloatVector x, FloatVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, x, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given x-y data.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatVector x, FloatVector y )
    {
      string title = "FloatVector vs. FloatVector";
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
    /// NMathChart.Show( ToChart( x, y ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatVector x, FloatVector y )
    {
      Show( ToChart( x, y ) );
    }

    /// <summary>
    /// Returns a new line chart for the given vectors.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( FloatVector[] data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatVector[] data )
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
    /// NMathChart.Show( ToChart( data ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatVector[] data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the given vectors.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( FloatVector[] data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatVector[] data, Unit xUnits )
    {
      string title = "FloatVector[]";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Length; i++ )
      {
        ChartSeries series = BindXY( xUnits.ToDoubleVector( data[i].Length ), data[i], ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Vector {0}", i ); ;
        seriesList.Add( series );
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
    /// NMathChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatVector[] data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    #endregion FloatVector

    #region DoubleVector ----------------------------------------------------

    /// <summary>
    /// Returns a line chart containing the given vector data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DoubleVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given vector data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleVector y )
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
    /// NMathChart.Show( ToChart( y ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleVector y )
    {
      Show( ToChart( y ) );
    }

    /// <summary>
    /// Returns a line chart containing the given vector data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DoubleVector y, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given vector data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleVector y, Unit xUnits )
    {
      string title = "DoubleVector";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      ChartSeries series = BindXY( xUnits.ToDoubleVector( y.Length ), y, ChartSeriesType.Line, ChartSymbolShape.None );
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
    /// NMathChart.Show( ToChart( y, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleVector y, Unit xUnits )
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
    public static ChartControl ToChart( DoubleVector x, DoubleVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, x, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given x-y data.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleVector x, DoubleVector y )
    {
      string title = "DoubleVector vs. DoubleVector";
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
    /// NMathChart.Show( ToChart( x, y ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleVector x, DoubleVector y )
    {
      Show( ToChart( x, y ) );
    }

    /// <summary>
    /// Returns a new line chart for the given vectors.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DoubleVector[] data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleVector[] data )
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
    /// NMathChart.Show( ToChart( data ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleVector[] data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the given vectors.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DoubleVector[] data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the given data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleVector[] data, Unit xUnits )
    {
      string title = "DoubleVector[]";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Length; i++ )
      {
        ChartSeries series = BindXY( xUnits.ToDoubleVector( data[i].Length ), data[i], ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Vector {0}", i ); ;
        seriesList.Add( series );
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
    /// NMathChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleVector[] data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    #endregion DoubleVector

    #region FloatComplexVector ----------------------------------------------

    /// <summary>
    /// Returns a line chart containing the magnitude of the given complex vector data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot the given vector in the complex plane, use
    /// <code>
    /// ChartControl chart = ToChart( NMathFunctions.Real( y ), NMathFunctions.Imag( y ) );
    /// </code>
    /// </remarks>
    public static ChartControl ToChart( FloatComplexVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the given complex vector data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <remarks>
    /// To plot the given vector in the complex plane, use
    /// <code>
    /// Update( ref chart, NMathFunctions.Real( y ), NMathFunctions.Imag( y ) );
    /// </code>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexVector y )
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
    /// NMathChart.Show( ToChart( y ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatComplexVector y )
    {
      Show( ToChart( y ) );
    }

    /// <summary>
    /// Returns a line chart containing the magnitude of the given complex vector data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot the given vector in the complex plane, use
    /// <code>
    /// ChartControl chart = ToChart( NMathFunctions.Real( y ), NMathFunctions.Imag( y ) );
    /// </code>
    /// </remarks>
    public static ChartControl ToChart( FloatComplexVector y, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the given complex vector data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// To plot the given vector in the complex plane, use
    /// <code>
    /// Update( ref chart, NMathFunctions.Real( y ), NMathFunctions.Imag( y ) );
    /// </code>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexVector y, Unit xUnits )
    {
      string title = "FloatComplexVector";
      string xTitle = xUnits.Name;
      string yTitle = "Abs(Value)";
      ChartSeries series = BindXY( xUnits.ToDoubleVector( y.Length ), NMathFunctions.Abs( y ), ChartSeriesType.Line, ChartSymbolShape.None );
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
    /// NMathChart.Show( ToChart( y, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatComplexVector y, Unit xUnits )
    {
      Show( ToChart( y, xUnits ) );
    }

    /// <summary>
    /// Returns a new point (scatter) chart containing the magnitude of the given x-y complex data.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    public static ChartControl ToChart( FloatComplexVector x, FloatComplexVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, x, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the given x-y complex data.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexVector x, FloatComplexVector y )
    {
      string title = "FloatComplexVector vs. FloatComplexVector";
      string xTitle = "Abs(x)";
      string yTitle = "Abs(y)";
      ChartSeries series = BindXY( NMathFunctions.Abs( x ), NMathFunctions.Abs( y ), ChartSeriesType.Scatter, DefaultMarker );
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
    /// NMathChart.Show( ToChart( x, y ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatComplexVector x, FloatComplexVector y )
    {
      Show( ToChart( x, y ) );
    }

    /// <summary>
    /// Returns a new line chart for the magnitude of the given complex vectors.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( FloatComplexVector[] data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the given complex data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexVector[] data )
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
    /// NMathChart.Show( ToChart( data ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatComplexVector[] data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the magnitude of the given complex vectors.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( FloatComplexVector[] data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the given complex data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexVector[] data, Unit xUnits )
    {
      string title = "FloatComplexVector[]";
      string xTitle = xUnits.Name;
      string yTitle = "Abs(Value)";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Length; i++ )
      {
        ChartSeries series = BindXY( xUnits.ToDoubleVector( data[i].Length ), NMathFunctions.Abs( data[i] ), ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Vector {0}", i ); ;
        seriesList.Add( series );
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
    /// NMathChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatComplexVector[] data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    #endregion FloatComplexVector

    #region DoubleComplexVector ---------------------------------------------

    /// <summary>
    /// Returns a line chart containing the magnitude of the given complex vector data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot the given vector in the complex plane, use
    /// <code>
    /// ChartControl chart = ToChart( NMathFunctions.Real( y ), NMathFunctions.Imag( y ) );
    /// </code>
    /// </remarks>
    public static ChartControl ToChart( DoubleComplexVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the given complex vector data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <remarks>
    /// To plot the given vector in the complex plane, use
    /// <code>
    /// Update( ref chart, NMathFunctions.Real( y ), NMathFunctions.Imag( y ) );
    /// </code>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexVector y )
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
    /// NMathChart.Show( ToChart( y ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleComplexVector y )
    {
      Show( ToChart( y ) );
    }

    /// <summary>
    /// Returns a line chart containing the magnitude of the given complex vector data.
    /// </summary>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot the given vector in the complex plane, use
    /// <code>
    /// ChartControl chart = ToChart( NMathFunctions.Real( y ), NMathFunctions.Imag( y ) );
    /// </code>
    /// </remarks>
    public static ChartControl ToChart( DoubleComplexVector y, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, y, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the given complex vector data.
    /// </summary>
    /// <param name="chart">A chart.</param>
    /// <param name="y">The y values.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// To plot the given vector in the complex plane, use
    /// <code>
    /// Update( ref chart, NMathFunctions.Real( y ), NMathFunctions.Imag( y ) );
    /// </code>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexVector y, Unit xUnits )
    {
      string title = "DoubleComplexVector";
      string xTitle = xUnits.Name;
      string yTitle = "Abs(Value)";
      ChartSeries series = BindXY( xUnits.ToDoubleVector( y.Length ), NMathFunctions.Abs( y ), ChartSeriesType.Line, ChartSymbolShape.None );
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
    /// NMathChart.Show( ToChart( y, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleComplexVector y, Unit xUnits )
    {
      Show( ToChart( y, xUnits ) );
    }

    /// <summary>
    /// Returns a new point (scatter) chart containing the magnitude of the given x-y complex data.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    public static ChartControl ToChart( DoubleComplexVector x, DoubleComplexVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, x, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the given x-y complex data.
    /// </summary>
    /// <param name="x">The x values.</param>
    /// <param name="y">The y values.</param>
    /// <exception cref="MismatchedSizeException">Thrown if x and y have different lengths.</exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexVector x, DoubleComplexVector y )
    {
      string title = "DoubleComplexVector vs. DoubleComplexVector";
      string xTitle = "Abs(x)";
      string yTitle = "Abs(y)";
      ChartSeries series = BindXY( NMathFunctions.Abs( x ), NMathFunctions.Abs( y ), ChartSeriesType.Scatter, DefaultMarker );
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
    /// NMathChart.Show( ToChart( x, y ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleComplexVector x, DoubleComplexVector y )
    {
      Show( ToChart( x, y ) );
    }

    /// <summary>
    /// Returns a new line chart for the magnitude of the given complex vectors.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DoubleComplexVector[] data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the given complex data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexVector[] data )
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
    /// NMathChart.Show( ToChart( data ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleComplexVector[] data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the magnitude of the given complex vectors.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DoubleComplexVector[] data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the given complex data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexVector[] data, Unit xUnits )
    {
      string title = "DoubleComplexVector[]";
      string xTitle = xUnits.Name;
      string yTitle = "Abs(Value)";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Length; i++ )
      {
        ChartSeries series = BindXY( xUnits.ToDoubleVector( data[i].Length ), NMathFunctions.Abs( data[i] ), ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Vector {0}", i ); ;
        seriesList.Add( series );
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
    /// NMathChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleComplexVector[] data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    #endregion DoubleComplexVector

    #region FloatMatrix -----------------------------------------------------

    /// <summary>
    /// Returns a new line chart for the columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( FloatMatrix data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Cols data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatMatrix data )
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
    /// NMathChart.Show( ToChart( data ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( FloatMatrix data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( FloatMatrix data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Cols data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatMatrix data, Unit xUnits )
    {
      string title = "FloatMatrix";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Cols; i++ )
      {
        ChartSeries series = BindXY( xUnits.ToDoubleVector( data.Rows ), data.Col( i ), ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Col {0}", i ); ;
        seriesList.Add( series );
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
    /// NMathChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( FloatMatrix data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    /// <summary>
    /// Returns a new point chart for the specified columns of the given matrix (x, y) .
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( FloatMatrix data, int xColIndex, int yColIndex )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xColIndex, yColIndex );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatMatrix data, int xColIndex, int yColIndex )
    {
      if( xColIndex < 0 || xColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( xColIndex );
      }

      if( yColIndex < 0 || yColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( yColIndex );
      }

      string title = "FloatMatrix";
      string xTitle = String.Format( "Col {0}", xColIndex );
      string yTitle = String.Format( "Col {0}", yColIndex );
      ChartSeries series = BindXY( data.Col( xColIndex ), data.Col( yColIndex ), ChartSeriesType.Scatter, DefaultMarker );

      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, xColIndex, yColIndex ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( FloatMatrix data, int xColIndex, int yColIndex )
    {
      Show( ToChart( data, xColIndex, yColIndex ) );
    }

    /// <summary>
    /// Returns a new line chart for the specified columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( FloatMatrix data, int[] colIndices )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, colIndices );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first colIndices.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatMatrix data, int[] colIndices )
    {
      Update( ref chart, data, colIndices, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, colIndices ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( FloatMatrix data, int[] colIndices )
    {
      Show( ToChart( data, colIndices ) );
    }

    /// <summary>
    /// Returns a new line chart for the specified columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( FloatMatrix data, int[] colIndices, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, colIndices, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first colIndices.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatMatrix data, int[] colIndices, Unit xUnits )
    {
      string title = "FloatMatrix";
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

        ChartSeries series = BindXY( xUnits.ToDoubleVector( data.Rows ), data.Col( index ), ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Col {0}", index ); ;
        seriesList.Add( series );
      }
      Update( ref chart, seriesList, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, colIndices, xUnits ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( FloatMatrix data, int[] colIndices, Unit xUnits )
    {
      Show( ToChart( data, colIndices, xUnits ) );
    }

    #endregion FloatMatrix

    #region DoubleMatrix ----------------------------------------------------

    /// <summary>
    /// Returns a new line chart for the columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( DoubleMatrix data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Cols data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleMatrix data )
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
    /// NMathChart.Show( ToChart( data ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( DoubleMatrix data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( DoubleMatrix data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Cols data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleMatrix data, Unit xUnits )
    {
      string title = "DoubleMatrix";
      string xTitle = xUnits.Name;
      string yTitle = "Value";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Cols; i++ )
      {
        ChartSeries series = BindXY( xUnits.ToDoubleVector( data.Rows ), data.Col( i ), ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Col {0}", i ); ;
        seriesList.Add( series );
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
    /// NMathChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( DoubleMatrix data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    /// <summary>
    /// Returns a new point chart for the specified columns of the given matrix (x, y) .
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( DoubleMatrix data, int xColIndex, int yColIndex )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xColIndex, yColIndex );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleMatrix data, int xColIndex, int yColIndex )
    {
      if( xColIndex < 0 || xColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( xColIndex );
      }

      if( yColIndex < 0 || yColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( yColIndex );
      }

      string title = "DoubleMatrix";
      string xTitle = String.Format( "Col {0}", xColIndex );
      string yTitle = String.Format( "Col {0}", yColIndex );
      ChartSeries series = BindXY( data.Col( xColIndex ), data.Col( yColIndex ), ChartSeriesType.Scatter, DefaultMarker );

      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, xColIndex, yColIndex ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( DoubleMatrix data, int xColIndex, int yColIndex )
    {
      Show( ToChart( data, xColIndex, yColIndex ) );
    }

    /// <summary>
    /// Returns a new line chart for the specified columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( DoubleMatrix data, int[] colIndices )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, colIndices );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first colIndices.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleMatrix data, int[] colIndices )
    {
      Update( ref chart, data, colIndices, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, colIndices ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( DoubleMatrix data, int[] colIndices )
    {
      Show( ToChart( data, colIndices ) );
    }

    /// <summary>
    /// Returns a new line chart for the specified columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( DoubleMatrix data, int[] colIndices, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, colIndices, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified columns of the given matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first colIndices.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleMatrix data, int[] colIndices, Unit xUnits )
    {
      string title = "DoubleMatrix";
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

        ChartSeries series = BindXY( xUnits.ToDoubleVector( data.Rows ), data.Col( index ), ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Col {0}", index ); ;
        seriesList.Add( series );
      }
      Update( ref chart, seriesList, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, colIndices, xUnits ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( DoubleMatrix data, int[] colIndices, Unit xUnits )
    {
      Show( ToChart( data, colIndices, xUnits ) );
    }

    #endregion DoubleMatrix

    #region FloatComplexMatrix ----------------------------------------------

    /// <summary>
    /// Returns a new line chart for the magnitude of the columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( FloatComplexMatrix data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Cols data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexMatrix data )
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
    /// NMathChart.Show( ToChart( data ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( FloatComplexMatrix data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the magnitude of the columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( FloatComplexMatrix data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Cols data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexMatrix data, Unit xUnits )
    {
      string title = "FloatComplexMatrix";
      string xTitle = xUnits.Name;
      string yTitle = "Abs(Value)";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Cols; i++ )
      {
        ChartSeries series = BindXY( xUnits.ToDoubleVector( data.Rows ), NMathFunctions.Abs( data.Col( i ) ), ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Col {0}", i ); ;
        seriesList.Add( series );
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
    /// NMathChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( FloatComplexMatrix data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    /// <summary>
    /// Returns a new point chart for the magnitude of the specified columns of the given complex matrix (x, y) .
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( FloatComplexMatrix data, int xColIndex, int yColIndex )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xColIndex, yColIndex );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexMatrix data, int xColIndex, int yColIndex )
    {
      if( xColIndex < 0 || xColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( xColIndex );
      }

      if( yColIndex < 0 || yColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( yColIndex );
      }

      string title = "FloatComplexMatrix";
      string xTitle = String.Format( "Abs(Col {0})", xColIndex );
      string yTitle = String.Format( "Abs(Col {0})", yColIndex );
      ChartSeries series = BindXY( NMathFunctions.Abs( data.Col( xColIndex ) ), NMathFunctions.Abs( data.Col( yColIndex ) ), ChartSeriesType.Scatter, DefaultMarker );
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, xColIndex, yColIndex ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( FloatComplexMatrix data, int xColIndex, int yColIndex )
    {
      Show( ToChart( data, xColIndex, yColIndex ) );
    }

    /// <summary>
    /// Returns a new line chart for the magnitude of the specified columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( FloatComplexMatrix data, int[] colIndices )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, colIndices );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the specified columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first colIndices.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexMatrix data, int[] colIndices )
    {
      Update( ref chart, data, colIndices, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, colIndices ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( FloatComplexMatrix data, int[] colIndices )
    {
      Show( ToChart( data, colIndices ) );
    }

    /// <summary>
    /// Returns a new line chart for the magnitude of the specified columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( FloatComplexMatrix data, int[] colIndices, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, colIndices, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the specified columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first colIndices.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexMatrix data, int[] colIndices, Unit xUnits )
    {
      string title = "FloatComplexMatrix";
      string xTitle = xUnits.Name;
      string yTitle = "Abs(Value)";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < colIndices.Length; i++ )
      {
        int index = colIndices[i];
        if( index < 0 || index > data.Cols - 1 )
        {
          throw new Core.IndexOutOfRangeException( index );
        }

        ChartSeries series = BindXY( xUnits.ToDoubleVector( data.Rows ), NMathFunctions.Abs( data.Col( index ) ), ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Col {0}", index ); ;
        seriesList.Add( series );
      }
      Update( ref chart, seriesList, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, colIndices, xUnits ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( FloatComplexMatrix data, int[] colIndices, Unit xUnits )
    {
      Show( ToChart( data, colIndices, xUnits ) );
    }

    #endregion FloatComplexMatrix

    #region DoubleComplexMatrix ---------------------------------------------

    /// <summary>
    /// Returns a new line chart for the magnitude of the columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( DoubleComplexMatrix data )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Cols data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexMatrix data )
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
    /// NMathChart.Show( ToChart( data ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( DoubleComplexMatrix data )
    {
      Show( ToChart( data ) );
    }

    /// <summary>
    /// Returns a new line chart for the magnitude of the columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( DoubleComplexMatrix data, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first data.Cols data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexMatrix data, Unit xUnits )
    {
      string title = "DoubleComplexMatrix";
      string xTitle = xUnits.Name;
      string yTitle = "Abs(Value)";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < data.Cols; i++ )
      {
        ChartSeries series = BindXY( xUnits.ToDoubleVector( data.Rows ), NMathFunctions.Abs( data.Col( i ) ), ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Col {0}", i ); ;
        seriesList.Add( series );
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
    /// NMathChart.Show( ToChart( data, xUnits ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( DoubleComplexMatrix data, Unit xUnits )
    {
      Show( ToChart( data, xUnits ) );
    }

    /// <summary>
    /// Returns a new point chart for the magnitude of the specified columns of the given complex matrix (x, y) .
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( DoubleComplexMatrix data, int xColIndex, int yColIndex )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, xColIndex, yColIndex );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexMatrix data, int xColIndex, int yColIndex )
    {
      if( xColIndex < 0 || xColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( xColIndex );
      }

      if( yColIndex < 0 || yColIndex > data.Cols - 1 )
      {
        throw new Core.IndexOutOfRangeException( yColIndex );
      }

      string title = "DoubleComplexMatrix";
      string xTitle = String.Format( "Abs(Col {0})", xColIndex );
      string yTitle = String.Format( "Abs(Col {0})", yColIndex );
      ChartSeries series = BindXY( NMathFunctions.Abs( data.Col( xColIndex ) ), NMathFunctions.Abs( data.Col( yColIndex ) ), ChartSeriesType.Scatter, DefaultMarker );
      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="xColIndex">The index of the matrix column containing the x data.</param>
    /// <param name="yColIndex">The index of the matrix column containing the y data.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, xColIndex, yColIndex ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( DoubleComplexMatrix data, int xColIndex, int yColIndex )
    {
      Show( ToChart( data, xColIndex, yColIndex ) );
    }

    /// <summary>
    /// Returns a new line chart for the magnitude of the specified columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( DoubleComplexMatrix data, int[] colIndices )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, colIndices );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the specified columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first colIndices.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexMatrix data, int[] colIndices )
    {
      Update( ref chart, data, colIndices, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, colIndices ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( DoubleComplexMatrix data, int[] colIndices )
    {
      Show( ToChart( data, colIndices ) );
    }

    /// <summary>
    /// Returns a new line chart for the magnitude of the specified columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static ChartControl ToChart( DoubleComplexMatrix data, int[] colIndices, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, data, colIndices, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the magnitude of the specified columns of the given complex matrix.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// To plot matrix rows, call data.Transpose().
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first colIndices.Length data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexMatrix data, int[] colIndices, Unit xUnits )
    {
      string title = "DoubleComplexMatrix";
      string xTitle = xUnits.Name;
      string yTitle = "Abs(Value)";
      List<ChartSeries> seriesList = new List<ChartSeries>();
      for( int i = 0; i < colIndices.Length; i++ )
      {
        int index = colIndices[i];
        if( index < 0 || index > data.Cols - 1 )
        {
          throw new Core.IndexOutOfRangeException( index );
        }

        ChartSeries series = BindXY( xUnits.ToDoubleVector( data.Rows ), NMathFunctions.Abs( data.Col( index ) ), ChartSeriesType.Line, ChartSymbolShape.None );
        series.Text = String.Format( "Col {0}", index ); ;
        seriesList.Add( series );
      }
      Update( ref chart, seriesList, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="colIndices">The indices of the columns to plot.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( data, colIndices, xUnits ) );
    /// </code>
    /// To plot matrix rows, call data.Transpose().
    /// </remarks>
    public static void Show( DoubleComplexMatrix data, int[] colIndices, Unit xUnits )
    {
      Show( ToChart( data, colIndices, xUnits ) );
    }

    #endregion DoubleComplexMatrix

    #region FloatLeastSquares -----------------------------------------------

    /// <summary>
    /// Returns a new line chart comparing the specified right-hand side with that predicted by the given model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    public static ChartControl ToChart( FloatLeastSquares lsq, FloatVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, lsq, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified least squares model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatLeastSquares lsq, FloatVector y )
    {
      Update( ref chart, lsq, y, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( lsq, y ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatLeastSquares lsq, FloatVector y )
    {
      Show( ToChart( lsq, y ) );
    }

    /// <summary>
    /// Returns a new line chart comparing the specified right-hand side with that predicted by the given model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    public static ChartControl ToChart( FloatLeastSquares lsq, FloatVector y, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, lsq, y, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified least squares model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatLeastSquares lsq, FloatVector y, Unit xUnits )
    {
      if( y.Length != lsq.Yhat.Length )
      {
        throw new MismatchedSizeException( "Length of observed vector must equal the length of predicted vector." );
      }

      List<string> titles = new List<string>()
      {
        "FloatLeastSquares",
        String.Format("RSS = {0}", lsq.ResidualSumOfSquares),
      };
      string xTitle = xUnits.Name;
      string yTitle = "Value";

      DoubleVector scale = xUnits.ToDoubleVector( y.Length );
      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( scale, y, ChartSeriesType.Scatter, DefaultMarker ),
        BindXY( scale, lsq.Yhat, ChartSeriesType.Line, ChartSymbolShape.None ),
      };
      series[0].Text = "Y";
      series[1].Text = "YHat";

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( lsq, y, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatLeastSquares lsq, FloatVector y, Unit xUnits )
    {
      Show( ToChart( lsq, y, xUnits ) );
    }

    #endregion

    #region DoubleLeastSquares ----------------------------------------------
    
    /// <summary>
    /// Returns a new line chart comparing the specified right-hand side with that predicted by the given model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    public static ChartControl ToChart( DoubleLeastSquares lsq, DoubleVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, lsq, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified least squares model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleLeastSquares lsq, DoubleVector y )
    {
      Update( ref chart, lsq, y, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( lsq, y ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleLeastSquares lsq, DoubleVector y )
    {
      Show( ToChart( lsq, y ) );
    }

    /// <summary>
    /// Returns a new line chart comparing the specified right-hand side with that predicted by the given model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    public static ChartControl ToChart( DoubleLeastSquares lsq, DoubleVector y, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, lsq, y, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified least squares model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleLeastSquares lsq, DoubleVector y, Unit xUnits )
    {
      if( y.Length != lsq.Yhat.Length )
      {
        throw new MismatchedSizeException( "Length of observed vector must equal the length of predicted vector." );
      }

      List<string> titles = new List<string>()
      {
        "DoubleLeastSquares",
        String.Format("RSS = {0}", lsq.ResidualSumOfSquares),
      };
      string xTitle = xUnits.Name;
      string yTitle = "Value";

      DoubleVector scale = xUnits.ToDoubleVector( y.Length );
      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( scale, y, ChartSeriesType.Scatter, DefaultMarker ),
        BindXY( scale, lsq.Yhat, ChartSeriesType.Line, ChartSymbolShape.None ),
      };
      series[0].Text = "Y";
      series[1].Text = "YHat";

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( lsq, y, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleLeastSquares lsq, DoubleVector y, Unit xUnits )
    {
      Show( ToChart( lsq, y, xUnits ) );
    }

    #endregion

    #region FloatComplexLeastSquares ----------------------------------------

    /// <summary>
    /// Returns a new line chart comparing the specified right-hand side with that predicted by the given model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    public static ChartControl ToChart( FloatComplexLeastSquares lsq, FloatComplexVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, lsq, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified least squares model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexLeastSquares lsq, FloatComplexVector y )
    {
      Update( ref chart, lsq, y, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( lsq, y ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatComplexLeastSquares lsq, FloatComplexVector y )
    {
      Show( ToChart( lsq, y ) );
    }

    /// <summary>
    /// Returns a new line chart comparing the specified right-hand side with that predicted by the given model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    public static ChartControl ToChart( FloatComplexLeastSquares lsq, FloatComplexVector y, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, lsq, y, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified least squares model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, FloatComplexLeastSquares lsq, FloatComplexVector y, Unit xUnits )
    {
      if( y.Length != lsq.Yhat.Length )
      {
        throw new MismatchedSizeException( "Length of observed vector must equal the length of predicted vector." );
      }

      List<string> titles = new List<string>()
      {
        "FloatComplexLeastSquares",
        String.Format("RSS = {0}", lsq.ResidualSumOfSquares),
      };
      string xTitle = xUnits.Name;
      string yTitle = "Abs(Value)";

      DoubleVector scale = xUnits.ToDoubleVector( y.Length );
      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( scale, NMathFunctions.Abs(y), ChartSeriesType.Scatter, DefaultMarker ),
        BindXY( scale, NMathFunctions.Abs(lsq.Yhat), ChartSeriesType.Line, ChartSymbolShape.None ),
      };
      series[0].Text = "Y";
      series[1].Text = "YHat";

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( lsq, y, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( FloatComplexLeastSquares lsq, FloatComplexVector y, Unit xUnits )
    {
      Show( ToChart( lsq, y, xUnits ) );
    }

    #endregion

    #region DoubleComplexLeastSquares ---------------------------------------

    /// <summary>
    /// Returns a new line chart comparing the specified right-hand side with that predicted by the given model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    public static ChartControl ToChart( DoubleComplexLeastSquares lsq, DoubleComplexVector y )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, lsq, y );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified least squares model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexLeastSquares lsq, DoubleComplexVector y )
    {
      Update( ref chart, lsq, y, new Unit() );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( lsq, y ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleComplexLeastSquares lsq, DoubleComplexVector y )
    {
      Show( ToChart( lsq, y ) );
    }

    /// <summary>
    /// Returns a new line chart comparing the specified right-hand side with that predicted by the given model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <returns>A new chart.</returns>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    public static ChartControl ToChart( DoubleComplexLeastSquares lsq, DoubleComplexVector y, Unit xUnits )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, lsq, y, xUnits );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified least squares model.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleComplexLeastSquares lsq, DoubleComplexVector y, Unit xUnits )
    {
      if( y.Length != lsq.Yhat.Length )
      {
        throw new MismatchedSizeException( "Length of observed vector must equal the length of predicted vector." );
      }

      List<string> titles = new List<string>()
      {
        "DoubleComplexLeastSquares",
        String.Format("RSS = {0}", lsq.ResidualSumOfSquares),
      };
      string xTitle = xUnits.Name;
      string yTitle = "Abs(Value)";

      DoubleVector scale = xUnits.ToDoubleVector( y.Length );
      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( scale, NMathFunctions.Abs(y), ChartSeriesType.Scatter, DefaultMarker ),
        BindXY( scale, NMathFunctions.Abs(lsq.Yhat), ChartSeriesType.Line, ChartSymbolShape.None ),
      };
      series[0].Text = "Y";
      series[1].Text = "YHat";

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="lsq">Least squares object.</param>
    /// <param name="y">The right-hand side of the linear system.</param>
    /// <param name="xUnits">The units for the x-axis.</param>
    /// <exception cref="MismatchedSizeException">
    /// Thrown if the length of y does not equal the length of lsq.YHat.
    /// </exception>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( lsq, y, xUnits ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleComplexLeastSquares lsq, DoubleComplexVector y, Unit xUnits )
    {
      Show( ToChart( lsq, y, xUnits ) );
    }

    #endregion

    #region OneVariableFunction ---------------------------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( OneVariableFunction f, double xmin, double xmax, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, xmin, xmax, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, OneVariableFunction f, double xmin, double xmax, int numInterpolatedValues )
    {
      Update( ref chart, f, xmin, xmax, numInterpolatedValues, null );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, xmin, ymin, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( OneVariableFunction f, double xmin, double xmax, int numInterpolatedValues )
    {
      Show( ToChart( f, xmin, xmax, numInterpolatedValues ) );
    }

    /// <summary>
    /// Returns a new line chart by interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <param name="pointLabels">
    /// A dictionary mapping key x-values to labels. The specified points will be labelled on the interpolated curve.
    /// If necessary, the x-values will be inserted into the interpolated data series.
    /// </param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( OneVariableFunction f, double xmin, double xmax, int numInterpolatedValues, Dictionary<double, string> pointLabels )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, xmin, xmax, numInterpolatedValues, pointLabels );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <param name="pointLabels">
    /// A dictionary mapping key x-values to labels. The specified points will be labelled on the interpolated curve.
    /// If necessary, the x-values will be inserted into the interpolated data series.
    /// </param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, OneVariableFunction f, double xmin, double xmax, int numInterpolatedValues, Dictionary<double, string> pointLabels )
    {
      string title = "OneVariableFunction";
      string xTitle = "x";
      string yTitle = "f(x)";

      ChartSeries series = BindXY( f, xmin, xmax, numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None );

      if( pointLabels != null )
      {
        foreach( KeyValuePair<double, string> kvp in pointLabels )
        {
          for( int i = 0; i < series.Points.Count - 1; i++ )
          {
            if( series.Points[i].X == kvp.Key )
            {
              series.Styles[i].DisplayText = true;
              series.Styles[i].Text = kvp.Value;
              series.Styles[i].TextOrientation = DEFAULT_POINT_LABEL_ORIENTATION;
              series.Styles[i].Symbol.Shape = DefaultMarker;
              break;
            }
            if( series.Points[i].X < kvp.Key && kvp.Key < series.Points[i + 1].X )
            {
              ChartPoint p = new ChartPoint( kvp.Key, f.Evaluate( kvp.Key ) );
              series.Points.Insert(i, p);
              series.Styles[i].DisplayText = true;
              series.Styles[i].Text = kvp.Value;
              series.Styles[i].TextOrientation = DEFAULT_POINT_LABEL_ORIENTATION;
              series.Styles[i].Symbol.Shape = DefaultMarker;

              break;
            }
          }
        }
      }

      Update( ref chart, new List<ChartSeries>() { series }, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <param name="pointLabels">
    /// A dictionary mapping key x-values to labels. The specified points will be labelled on the interpolated curve.
    /// If necessary, the x-values will be inserted into the interpolated data series.
    /// </param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, xmin, ymin, pointLabels, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( OneVariableFunction f, double xmin, double xmax, int numInterpolatedValues, Dictionary<double, string> pointLabels )
    {
      Show( ToChart( f, xmin, xmax, numInterpolatedValues, pointLabels ) );
    }

    #endregion OneVariableFunction

    #region Polynomial ------------------------------------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( Polynomial f, double xmin, double xmax, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, xmin, xmax, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, Polynomial f, double xmin, double xmax, int numInterpolatedValues )
    {
      List<string> titles = new List<string>()
      {
        "Polynomial",
        "f(x) = " + f.ToString( "N2" ),
      };
      string xTitle = "x";
      string yTitle = "f(x)";

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( f, xmin, xmax, numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None )
      };

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, xmin, ymin, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( Polynomial f, double xmin, double xmax, int numInterpolatedValues )
    {
      Show( ToChart( f, xmin, xmax, numInterpolatedValues ) );
    }

    #endregion Polynomial

    #region LinearSpline ----------------------------------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( LinearSpline f, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, LinearSpline f, int numInterpolatedValues )
    {
      Update( ref chart, f, f.GetX( 0 ), f.GetX( f.NumberOfTabulatedValues - 1 ), numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( LinearSpline f, int numInterpolatedValues )
    {
      Show( ToChart( f, numInterpolatedValues ) );
    }

    /// <summary>
    /// Returns a new line chart by interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( LinearSpline f, double xmin, double xmax, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, xmin, xmax, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, LinearSpline f, double xmin, double xmax, int numInterpolatedValues )
    {
      string title = "LinearSpline";
      string xTitle = "x";
      string yTitle = "f(x)";

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( f, ChartSeriesType.Scatter, DefaultMarker ),
        BindXY( f, xmin, xmax, numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None )
      };
      series[0].Text = "Points";
      series[1].Text = "Spline";

      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, xmin, ymin, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( LinearSpline f, double xmin, double xmax, int numInterpolatedValues )
    {
      Show( ToChart( f, xmin, xmax, numInterpolatedValues ) );
    }

    #endregion LinearSpline

    #region ClampedCubicSpline ----------------------------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( ClampedCubicSpline f, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, ClampedCubicSpline f, int numInterpolatedValues )
    {
      Update( ref chart, f, f.GetX( 0 ), f.GetX( f.NumberOfTabulatedValues - 1 ), numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( ClampedCubicSpline f, int numInterpolatedValues )
    {
      Show( ToChart( f, numInterpolatedValues ) );
    }

    /// <summary>
    /// Returns a new line chart by interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( ClampedCubicSpline f, double xmin, double xmax, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, xmin, xmax, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, ClampedCubicSpline f, double xmin, double xmax, int numInterpolatedValues )
    {
      string title = "ClampedCubicSpline";
      string xTitle = "x";
      string yTitle = "f(x)";

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( f, ChartSeriesType.Scatter, DefaultMarker ),
        BindXY( f, xmin, xmax, numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None )
      };
      series[0].Text = "Points";
      series[1].Text = "Spline";

      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, xmin, ymin, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( ClampedCubicSpline f, double xmin, double xmax, int numInterpolatedValues )
    {
      Show( ToChart( f, xmin, xmax, numInterpolatedValues ) );
    }

    #endregion ClampedCubicSpline

    #region NaturalCubicSpline ----------------------------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( NaturalCubicSpline f, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, NaturalCubicSpline f, int numInterpolatedValues )
    {
      Update( ref chart, f, f.GetX( 0 ), f.GetX( f.NumberOfTabulatedValues - 1 ), numInterpolatedValues );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( NaturalCubicSpline f, int numInterpolatedValues )
    {
      Show( ToChart( f, numInterpolatedValues ) );
    }

    /// <summary>
    /// Returns a new line chart by interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( NaturalCubicSpline f, double xmin, double xmax, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, xmin, xmax, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, NaturalCubicSpline f, double xmin, double xmax, int numInterpolatedValues )
    {
      string title = "NaturalCubicSpline";
      string xTitle = "x";
      string yTitle = "f(x)";

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( f, ChartSeriesType.Scatter, DefaultMarker ),
        BindXY( f, xmin, xmax, numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None )
      };
      series[0].Text = "Points";
      series[1].Text = "Spline";

      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, xmin, ymin, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( NaturalCubicSpline f, double xmin, double xmax, int numInterpolatedValues )
    {
      Show( ToChart( f, xmin, xmax, numInterpolatedValues ) );
    }

    #endregion NaturalCubicSpline

    #region DoubleParameterizedFunction -------------------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given parameterized function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="parameters">The function parameters.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DoubleParameterizedFunction f, DoubleVector parameters, double xmin, double xmax, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, parameters, xmin, xmax, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The parameterized function.</param>
    /// <param name="parameters">The function parameters.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleParameterizedFunction f, DoubleVector parameters, double xmin, double xmax, int numInterpolatedValues )
    {
      string title = "DoubleParameterizedFunction";
      string xTitle = "x";
      string yTitle = "f(x)";

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( f, parameters, xmin, xmax, numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None )
      };

      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="parameters">The function parameters.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, parameters, xmin, ymin, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleParameterizedFunction f, DoubleVector parameters, double xmin, double xmax, int numInterpolatedValues )
    {
      Show( ToChart( f, parameters, xmin, xmax, numInterpolatedValues ) );
    }

    #endregion DoubleParameterizedFunction

    #region DoubleParameterizedDelegate -------------------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given parameterized function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="parameters">The function parameters.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( DoubleParameterizedDelegate f, DoubleVector parameters, double xmin, double xmax, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, parameters, xmin, xmax, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified parameterized function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="parameters">The function parameters.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, DoubleParameterizedDelegate f, DoubleVector parameters, double xmin, double xmax, int numInterpolatedValues )
    {
      string title = "DoubleParameterizedDelegate";
      string xTitle = "x";
      string yTitle = "f(x)";

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( f, parameters, xmin, xmax, numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None )
      };

      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="parameters">The function parameters.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, parameters, xmin, ymin, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( DoubleParameterizedDelegate f, DoubleVector parameters, double xmin, double xmax, int numInterpolatedValues )
    {
      Show( ToChart( f, parameters, xmin, xmax, numInterpolatedValues ) );
    }

    #endregion DoubleParameterizedDelegate

    #region NMathFunctions.DoubleUnaryFunction ------------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( NMathFunctions.DoubleUnaryFunction f, double xmin, double xmax, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, xmin, xmax, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, NMathFunctions.DoubleUnaryFunction f, double xmin, double xmax, int numInterpolatedValues )
    {
      string title = "NMathFunctions.DoubleUnaryFunction";
      string xTitle = "x";
      string yTitle = "f(x)";

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( new OneVariableFunction( f ), xmin, xmax, numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None )
      };

      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, xmin, ymin, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( NMathFunctions.DoubleUnaryFunction f, double xmin, double xmax, int numInterpolatedValues )
    {
      Show( ToChart( f, xmin, xmax, numInterpolatedValues ) );
    }

    #endregion NMathFunctions.DoubleUnaryFunction

    #region NMathFunctions.GeneralizedDoubleUnaryFunction -------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given parameterized function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="parameters">The function parameters.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( NMathFunctions.GeneralizedDoubleUnaryFunction f, DoubleVector parameters, double xmin, double xmax, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, f, parameters, xmin, xmax, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified function.
    /// </summary>
    /// <param name="f">The parameterized function.</param>
    /// <param name="parameters">The function parameters.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, NMathFunctions.GeneralizedDoubleUnaryFunction f, DoubleVector parameters, double xmin, double xmax, int numInterpolatedValues )
    {
      string title = "NMathFunctions.GeneralizedDoubleUnaryFunction";
      string xTitle = "x";
      string yTitle = "f(x)";

      DoubleParameterizedDelegate f2 = new DoubleParameterizedDelegate( new Func<DoubleVector, double, double>( f ) );

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( f2, parameters, xmin, xmax, numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None )
      };

      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="parameters">The function parameters.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( f, parameters, xmin, ymin, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( NMathFunctions.GeneralizedDoubleUnaryFunction f, DoubleVector parameters, double xmin, double xmax, int numInterpolatedValues )
    {
      Show( ToChart( f, parameters, xmin, xmax, numInterpolatedValues ) );
    }

    #endregion NMathFunctions.GeneralizedDoubleUnaryFunction

    #region PeakFinderSavitzkyGolay -----------------------------------------

    /// <summary>
    /// Returns a line chart showing the peaks in the data.
    /// </summary>
    /// <param name="pf">The peak finder.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Call LocatePeaks() on the peak finder prior to charting.
    /// </remarks>
    public static ChartControl ToChart( PeakFinderSavitzkyGolay pf )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, pf );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified peak finder.
    /// </summary>
    /// <param name="pf">The peak finder.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <remarks>
    /// Call LocatePeaks() on the peak finder prior to charting.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, PeakFinderSavitzkyGolay pf )
    {
      Update( ref chart, pf, 0, ( pf.InputData.Length - 1 ) * pf.AbscissaInterval );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="pf">The peak finder.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <remarks>
    /// Call LocatePeaks() on the peak finder prior to charting.
    /// <br/>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( pf ) );
    /// </code>
    /// </remarks>
    public static void Show( PeakFinderSavitzkyGolay pf )
    {
      Show( ToChart( pf ) );
    }

    /// <summary>
    /// Returns a line chart showing the peaks in the data.
    /// </summary>
    /// <param name="pf">The peak finder.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <returns>A new chart.</returns>
    /// <remarks>
    /// Call LocatePeaks() on the peak finder prior to charting.
    /// </remarks>
    public static ChartControl ToChart( PeakFinderSavitzkyGolay pf, double xmin, double xmax )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, pf, xmin, xmax );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified peak finder.
    /// </summary>
    /// <param name="pf">The peak finder.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <remarks>
    /// Call LocatePeaks() on the peak finder prior to charting.
    /// <br/>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, PeakFinderSavitzkyGolay pf, double xmin, double xmax )
    {
      string title = "PeakFinderSavitzkyGolay";
      string xTitle = "x";
      string yTitle = "y";

      // data
      DoubleVector x = new DoubleVector( pf.InputData.Length, 0, pf.AbscissaInterval );
      ChartSeries series = new ChartSeries()
      {
        Text = "Input Data",
        Type = ChartSeriesType.Line,
      };
      series.Style.Symbol.Shape = ChartSymbolShape.None;
      for( int i = 0; i < x.Length; i++ )
      {
        if ( x[i] >= xmin && x[i] <= xmax ) 
        {
          series.Points.Add( x[i], pf.InputData[i] );
        }
      }

      // peaks
      ChartSeries series2 = new ChartSeries()
      {
        Text = "Peaks",
        Type = ChartSeriesType.Scatter,
      };
      series2.Style.Symbol.Shape = DefaultMarker;

      for( int i = 0; i < pf.NumberPeaks; i++ )
      {
        Extrema extrema = pf[i];
        if( extrema.X >= xmin && extrema.X <= xmax )
        {
          ChartPoint point = new ChartPoint( extrema.X, extrema.Y );
          series2.Points.Add( point );
          series2.Styles[series2.Points.Count - 1].DisplayText = true;
          series2.Styles[series2.Points.Count - 1].Text = String.Format( "({0}, {1})", extrema.X.ToString( "N2" ), extrema.Y.ToString( "N2" ) );
          series2.Styles[series2.Points.Count - 1].TextOrientation = DEFAULT_POINT_LABEL_ORIENTATION;
        }
      }

      Update( ref chart, new List<ChartSeries>() { series, series2 }, title, xTitle, yTitle );
  
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="pf">The peak finder.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <remarks>
    /// Call LocatePeaks() on the peak finder prior to charting.
    /// <br/>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( pf, xmin, xmax ) );
    /// </code>
    /// </remarks>
    public static void Show( PeakFinderSavitzkyGolay pf, double xmin, double xmax )
    {
      Show( ToChart( pf, xmin, xmax ) );
    }

    #endregion PeakFinderSavitzkyGolay

    #region Bracket ---------------------------------------------------------

    /// <summary>
    /// Returns a line chart showing the bracketed region of contained function.
    /// </summary>
    /// <param name="bracket">The bracket, with contained function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values. The bracket Lower, Interior, and Upper values
    /// may additionally be inserted into the interpolated series.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( Bracket bracket, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, bracket, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified bracket.
    /// </summary>
    /// <param name="bracket">The bracket, with contained function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values. The bracket Lower, Interior, and Upper values
    /// may additionally be inserted into the interpolated series.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, Bracket bracket, int numInterpolatedValues )
    {

      double range = bracket.Upper - bracket.Lower;
      double xmin = bracket.Lower - ( range / 4 );
      double xmax = bracket.Upper + ( range / 4 );

      Dictionary<double, string> pointLabels = new Dictionary<double, string>()
      {
        { bracket.Lower, "Lower" },
        { bracket.Interior, "Interior" },
        { bracket.Upper, "Upper" },
      };

      Update( ref chart, bracket.Function, xmin, xmax, numInterpolatedValues, pointLabels );
      chart.Titles[0].Text = "Bracket";

    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="bracket">The bracket, with contained function.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values. The bracket Lower, Interior, and Upper values
    /// may additionally be inserted into the interpolated series.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( bracket, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( Bracket bracket, int numInterpolatedValues )
    {
      Show( ToChart( bracket, numInterpolatedValues ) );
    }

    #endregion Bracket

    #region Histogram -------------------------------------------------------

    /// <summary>
    /// Returns a bar chart showing the counts of each bin in the histogram.
    /// </summary>
    /// <param name="histogram">The histogram.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( Histogram histogram )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, histogram );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified histogram.
    /// </summary>
    /// <param name="histogram">The histogram.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, Histogram histogram )
    {
      string title = "Histogram";
      string xTitle = "Count";
      string yTitle = "Bin";

      DoubleVector y = new DoubleVector( histogram.NumBins );
      for( int i = 0; i < histogram.NumBins; i++ )
      {
        y[i] = histogram.Counts[i];
      }
      ChartSeries series = BindY( y, ChartSeriesType.Bar, ChartSymbolShape.None );

      Update( ref chart, series, title, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="histogram">The histogram.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( histogram ) );
    /// </code>
    /// </remarks>
    public static void Show( Histogram histogram )
    {
      Show( ToChart( histogram ) );
    }

    #endregion Histogram

    #region PolynomialLeastSquares ------------------------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given fitted polynomial.
    /// </summary>
    /// <param name="pls">A PolynomialLeastSquares object containing a least squares fit of a polynomial to
    /// the data.</param>
    /// <param name="x">The fitted x values.</param>
    /// <param name="y">The fitted y values.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart( PolynomialLeastSquares pls, DoubleVector x, DoubleVector y, int numInterpolatedValues )
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, pls, x, y, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified fitted polynomial.
    /// </summary>
    /// <param name="pls">A PolynomialLeastSquares object containing a least squares fit of a polynomial to
    /// the data.</param>
    /// <param name="x">The fitted x values.</param>
    /// <param name="y">The fitted y values.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update( ref ChartControl chart, PolynomialLeastSquares pls, DoubleVector x, DoubleVector y, int numInterpolatedValues )
    {
      List<string> titles = new List<string>()
      {
        "PolynomialLeastSquares",
        "f(x) = " + pls.FittedPolynomial.ToString( "N2" ),
      };
      string xTitle = "x";
      string yTitle = "y";

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( x, y, ChartSeriesType.Scatter, DefaultMarker ),
        BindXY( pls.FittedPolynomial, NMathFunctions.MinValue( x ), NMathFunctions.MaxValue( x ), numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None )
      };
      series[0].Text = "Points";
      series[1].Text = "Polynomial";

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="pls">A PolynomialLeastSquares object containing a least squares fit of a polynomial to
    /// the data.</param>
    /// <param name="x">The fitted x values.</param>
    /// <param name="y">The fitted y values.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( pls, x, y, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show( PolynomialLeastSquares pls, DoubleVector x, DoubleVector y, int numInterpolatedValues )
    {
      Show( ToChart( pls, x, y, numInterpolatedValues ) );
    }

    #endregion PolynomialLeastSquares

    #region OneVariableFunctionFitter<M> ------------------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given fitted function.
    /// </summary>
    /// <param name="ovf">A OneVariableFunctionFitter object containing a least squares fit of a
    /// parameterized function to the given x,y values.</param>
    /// <param name="x">The fitted x values.</param>
    /// <param name="y">The fitted y values.</param>
    /// <param name="solution">>The parameters of the function at the found minimum.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart<M>( OneVariableFunctionFitter<M> ovf, DoubleVector x, DoubleVector y, DoubleVector solution, int numInterpolatedValues )
        where M : INonlinearLeastSqMinimizer, new()
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, ovf, x, y, solution, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified fitted function.
    /// </summary>
    /// <param name="ovf">A OneVariableFunctionFitter object containing a least squares fit of a
    /// parameterized function to the given x,y values.</param>
    /// <param name="x">The fitted x values.</param>
    /// <param name="y">The fitted y values.</param>
    /// <param name="solution">>The parameters of the function at the found minimum.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update<M>( ref ChartControl chart, OneVariableFunctionFitter<M> ovf, DoubleVector x, DoubleVector y, DoubleVector solution, int numInterpolatedValues )
       where M : INonlinearLeastSqMinimizer, new()
    {
      List<string> titles = new List<string>()
      {
        "OneVariableFunctionFitter",
      };
      string xTitle = "x";
      string yTitle = "y";

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( x, y, ChartSeriesType.Scatter, DefaultMarker ),
        BindXY( ovf.Function, solution, NMathFunctions.MinValue( x ), NMathFunctions.MaxValue( x ), numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None )
      };
      series[0].Text = "Points";
      series[1].Text = "Function";

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="ovf">A OneVariableFunctionFitter object containing a least squares fit of a
    /// parameterized function to the given x,y values.</param>
    /// <param name="x">The fitted x values.</param>
    /// <param name="y">The fitted y values.</param>
    /// <param name="solution">>The parameters of the function at the found minimum.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( ovf, x, y, solution, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show<M>( OneVariableFunctionFitter<M> ovf, DoubleVector x, DoubleVector y, DoubleVector solution, int numInterpolatedValues )
       where M : INonlinearLeastSqMinimizer, new()
    {
      Show( ToChart( ovf, x, y, solution, numInterpolatedValues ) );
    }

    #endregion OneVariableFunctionFitter<M>

    #region BoundedOneVariableFunctionFitter<M> -----------------------------

    /// <summary>
    /// Returns a new line chart by interpolating over the given fitted function.
    /// </summary>
    /// <param name="ovf">A BoundedOneVariableFunctionFitter object containing a least squares fit of a
    /// parameterized function to the given x,y values.</param>
    /// <param name="x">The fitted x values.</param>
    /// <param name="y">The fitted y values.</param>
    /// <param name="solution">>The parameters of the function at the found minimum.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <returns>A new chart.</returns>
    public static ChartControl ToChart<M>( BoundedOneVariableFunctionFitter<M> ovf, DoubleVector x, DoubleVector y, DoubleVector solution, int numInterpolatedValues )
        where M : IBoundedNonlinearLeastSqMinimizer, new()
    {
      ChartControl chart = GetDefaultChart();
      Update( ref chart, ovf, x, y, solution, numInterpolatedValues );
      return chart;
    }

    /// <summary>
    /// Updates the given chart with the specified fitted function.
    /// </summary>
    /// <param name="ovf">A BoundedOneVariableFunctionFitter object containing a least squares fit of a
    /// parameterized function to the given x,y values.</param>
    /// <param name="x">The fitted x values.</param>
    /// <param name="y">The fitted y values.</param>
    /// <param name="solution">>The parameters of the function at the found minimum.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first two data series are replaced, or added if necessary.
    /// </remarks>
    public static void Update<M>( ref ChartControl chart, BoundedOneVariableFunctionFitter<M> ovf, DoubleVector x, DoubleVector y, DoubleVector solution, int numInterpolatedValues )
       where M : IBoundedNonlinearLeastSqMinimizer, new()
    {
      List<string> titles = new List<string>()
      {
        "BoundedOneVariableFunctionFitter",
      };
      string xTitle = "x";
      string yTitle = "y";

      List<ChartSeries> series = new List<ChartSeries>()
      {
        BindXY( x, y, ChartSeriesType.Scatter, DefaultMarker ),
        BindXY( ovf.Function, solution, NMathFunctions.MinValue( x ), NMathFunctions.MaxValue( x ), numInterpolatedValues, ChartSeriesType.Line, ChartSymbolShape.None )
      };
      series[0].Text = "Points";
      series[1].Text = "Function";

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Shows a new chart in a default form.
    /// </summary>
    /// <param name="ovf">A BoundedOneVariableFunctionFitter object containing a least squares fit of a
    /// parameterized function to the given x,y values.</param>
    /// <param name="x">The fitted x values.</param>
    /// <param name="y">The fitted y values.</param>
    /// <param name="solution">>The parameters of the function at the found minimum.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>
    /// NMathChart.Show( ToChart( ovf, x, y, solution, numInterpolatedValues ) );
    /// </code>
    /// </remarks>
    public static void Show<M>( BoundedOneVariableFunctionFitter<M> ovf, DoubleVector x, DoubleVector y, DoubleVector solution, int numInterpolatedValues )
       where M : IBoundedNonlinearLeastSqMinimizer, new()
    {
      Show( ToChart( ovf, x, y, solution, numInterpolatedValues ) );
    }

    #endregion BoundedOneVariableFunctionFitter<M>

    #region General ---------------------------------------------------------

    /// <summary>
    /// Shows the given chart in a default form.
    /// </summary>
    /// <param name="chart">The chart to display.</param>
    /// <remarks>
    /// Note that when the window is closed, the chart is disposed.
    /// </remarks>
    public static void Show( ChartControl chart )
    {
      Form form = new Form();
      form.Size = new Size( chart.Size.Width + 20, chart.Size.Height + 40 );
      form.Controls.Add( chart );
      Application.Run( form );
    }


    /// <summary>
    /// Save the chart image to the specified filename.
    /// </summary>
    /// <param name="chart">The chart.</param>
    /// <param name="filename">
    /// The filename with extension that specifies the type of format to save to.
    /// Supported formats are bmp, jpg, gif, tiff, wmf, emf, svg and eps.</param>
    /// <remarks>
    /// Equivalent to:
    /// <code>chart.SaveImage( filename );</code>
    /// </remarks>
    public static void Save( ChartControl chart, string filename )
    {
      chart.SaveImage( filename );
    }

    #endregion General

    #region Protected -------------------------------------------------------

    /// <summary>
    /// Returns a data series of the given data.
    /// </summary>
    /// <param name="data">The y values.</param>
    /// <param name="type">The chart type.</param>
    /// <param name="marker">The marker style to use.</param>
    /// <returns>A new data series.</returns>
    protected static ChartSeries BindY( FloatVector data, ChartSeriesType type, ChartSymbolShape marker )
    {
      ChartSeries series = new ChartSeries()
      {
        Type = type,
      };
      series.Style.Symbol.Shape = marker;

      for (int i = 0; i < data.Length; i++ )
      {
        series.Points.Add( i,  data[i] );
      }

      return series;
    }

    /// <summary>
    /// Returns a data series of the given data.
    /// </summary>
    /// <param name="data">The y values.</param>
    /// <param name="type">The chart type.</param>
    /// <param name="marker">The marker style to use.</param>
    /// <returns>A new data series.</returns>
    protected static ChartSeries BindY( DoubleVector data, ChartSeriesType type, ChartSymbolShape marker )
    {
      ChartSeries series = new ChartSeries()
      {
        Type = type,
      };
      series.Style.Symbol.Shape = marker;

      for( int i = 0; i < data.Length; i++ )
      {
        series.Points.Add( i, data[i] );
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
    protected static ChartSeries BindXY( FloatVector x, FloatVector y, ChartSeriesType type, ChartSymbolShape marker )
    {
      if( x.Length != y.Length )
      {
        throw new MismatchedSizeException( "x,y vector of unequal length", x.Length, y.Length );
      }

      ChartSeries series = new ChartSeries()
      {
        Type = type,
      };
      series.Style.Symbol.Shape = marker;

      for( int i = 0; i < x.Length; i++ )
      {
        series.Points.Add( x[i], y[i] );
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
    protected static ChartSeries BindXY( DoubleVector x, FloatVector y, ChartSeriesType type, ChartSymbolShape marker )
    {

      if( x.Length != y.Length )
      {
        throw new MismatchedSizeException( "x,y vector of unequal length", x.Length, y.Length );
      }

      ChartSeries series = new ChartSeries()
      {
        Type = type,
      };
      series.Style.Symbol.Shape = marker;

      for( int i = 0; i < x.Length; i++ )
      {
        series.Points.Add( x[i], y[i] );
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
    protected static ChartSeries BindXY( DoubleVector x, DoubleVector y, ChartSeriesType type, ChartSymbolShape marker )
    {

      if( x.Length != y.Length )
      {
        throw new MismatchedSizeException( "x,y vector of unequal length", x.Length, y.Length );
      }

      ChartSeries series = new ChartSeries()
      {
        Type = type,
      };
      series.Style.Symbol.Shape = marker;

      for( int i = 0; i < x.Length; i++ )
      {
        series.Points.Add( x[i], y[i] );
      }

      return series;
    }

    /// <summary>
    /// Returns a data series containing the tabulated function values.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="type">The chart type.</param>
    /// <param name="marker">The marker style to use.</param>
    /// <returns>A new data series.</returns>
    protected static ChartSeries BindXY( TabulatedFunction f, ChartSeriesType type, ChartSymbolShape marker )
    {
      DoubleVector x = new DoubleVector( f.NumberOfTabulatedValues );
      DoubleVector y = new DoubleVector( f.NumberOfTabulatedValues );

      for( int i = 0; i < f.NumberOfTabulatedValues; i++ )
      {
        x[i] = f.GetX( i );
        y[i] = f.GetY( i );
      }

      return BindXY( x, y, type, marker );
    }

    /// <summary>
    /// Returns a data series interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <param name="type">The chart type.</param>
    /// <param name="marker">The marker style to use.</param>
    /// <returns>A new data series.</returns>
    protected static ChartSeries BindXY( OneVariableFunction f, double xmin, double xmax, int numInterpolatedValues, ChartSeriesType type, ChartSymbolShape marker )
    {
      // switch if wrong order
      if( xmin > xmax )
      {
        double temp = xmin;
        xmin = xmax;
        xmax = temp;
      }

      DoubleVector x = new DoubleVector();
      if( numInterpolatedValues > 0 )
      {
        double step = ( xmax - xmin ) / ( numInterpolatedValues - 1 );
        x = new DoubleVector( numInterpolatedValues, xmin, step );
      }
      return BindXY( x, f.Evaluate( x ), type, marker );

    }

    /// <summary>
    /// Returns a data series interpolating over the given function.
    /// </summary>
    /// <param name="f">The function.</param>
    /// <param name="parameters">The function parameters.</param>
    /// <param name="xmin">The starting x value.</param>
    /// <param name="xmax">The ending x value.</param>
    /// <param name="numInterpolatedValues">The number of interpolated values.</param>
    /// <param name="type">The chart type.</param>
    /// <param name="marker">The marker style to use.</param>
    /// <returns>A new data series.</returns>
    protected static ChartSeries BindXY( DoubleParameterizedFunction f, DoubleVector parameters, double xmin, double xmax, int numInterpolatedValues, ChartSeriesType type, ChartSymbolShape marker )
    {
      // switch if wrong order
      if( xmin > xmax )
      {
        double temp = xmin;
        xmin = xmax;
        xmax = temp;
      }

      DoubleVector x = new DoubleVector();
      if( numInterpolatedValues > 0 )
      {
        double step = ( xmax - xmin ) / ( numInterpolatedValues - 1 );
        x = new DoubleVector( numInterpolatedValues, xmin, step );
      }

      DoubleVector y = new DoubleVector( x.Length );
      f.Evaluate(parameters, x, ref y );

      return BindXY( x, y, type, marker );
    }

    /// <summary>
    /// Updates the given chart with the given data series, title, and axis titles.
    /// </summary>
    /// <param name="chart">The chart.</param>
    /// <param name="series">The data Series.</param>
    /// <param name="titles">The title.</param>
    /// <param name="xTitle">The x axis title.</param>
    /// <param name="yTitle">The y axis title.</param>
    /// <remarks>
    /// The title is added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    protected static void Update( ref ChartControl chart, ChartSeries series, string title, string xTitle, string yTitle )
    {
      List<string> titles = new List<string>()
      {
        title
      };

      List<ChartSeries> seriesList = new List<ChartSeries>()
      {
        series
      };

      Update( ref chart, seriesList, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Updates the given chart with the given data series, titles, and axis titles.
    /// </summary>
    /// <param name="chart">The chart.</param>
    /// <param name="series">A list of Series.</param>
    /// <param name="title">The title.</param>
    /// <param name="xTitle">The x axis title.</param>
    /// <param name="yTitle">The y axis title.</param>
    /// <remarks>
    /// The title is added only if chart does not currently contain any titles.
    /// <br/>
    /// The first series.Count data series on chart are replaced, or added if necessary.
    /// </remarks>
    protected static void Update( ref ChartControl chart, List<ChartSeries> series, string title, string xTitle, string yTitle )
    {
      List<string> titles = new List<string>()
      {
        title
      };

      Update( ref chart, series, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Updates the given chart with the given data series, titles, and axis titles.
    /// </summary>
    /// <param name="chart">The chart.</param>
    /// <param name="series">The data Series.</param>
    /// <param name="titles">A list of title strings.</param>
    /// <param name="xTitle">The x axis title.</param>
    /// <param name="yTitle">The y axis title.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// chart.Series[0] is replaced, or added if necessary.
    /// </remarks>
    protected static void Update( ref ChartControl chart, ChartSeries series, List<string> titles, string xTitle, string yTitle )
    {
      List<ChartSeries> seriesList = new List<ChartSeries>()
      {
        series
      };

      Update( ref chart, seriesList, titles, xTitle, yTitle );
    }

    /// <summary>
    /// Updates the given chart with the given data series, titles, and axis titles.
    /// </summary>
    /// <param name="chart">The chart.</param>
    /// <param name="series">A list of Series.</param>
    /// <param name="titles">A list of title strings.</param>
    /// <param name="xTitle">The x axis title.</param>
    /// <param name="yTitle">The y axis title.</param>
    /// <remarks>
    /// Titles are added only if chart does not currently contain any titles.
    /// <br/>
    /// The first series.Count data series on chart are replaced, or added if necessary.
    /// </remarks>
    protected static void Update( ref ChartControl chart, List<ChartSeries> series, List<string> titles, string xTitle, string yTitle )
    {

       // use axis titles only if not already set on chart
      if( String.IsNullOrEmpty( chart.PrimaryXAxis.Title ) && !String.IsNullOrEmpty( xTitle ) )
      {
        chart.PrimaryXAxis.Title = xTitle;
        chart.PrimaryXAxis.TitleFont = DefaultAxisTitleFont;
        chart.PrimaryXAxis.GridLineType.ForeColor = DefaultMajorGridLineColor;
      }
      if( String.IsNullOrEmpty( chart.PrimaryYAxis.Title ) && !String.IsNullOrEmpty( yTitle ) )
      {
        chart.PrimaryYAxis.Title = yTitle;
        chart.PrimaryYAxis.TitleFont = DefaultAxisTitleFont;
        chart.PrimaryYAxis.GridLineType.ForeColor = DefaultMajorGridLineColor;
      }

      // add titles only if none already exist
      if( chart.Titles.Count == 0 && titles != null )
      {
        for( int i = 0; i < titles.Count; i++ )
        {
          ChartTitle title = new ChartTitle()
          {
            Text = titles[i],
            Font = chart.PrimaryXAxis.TitleFont,
          };
          if( i == 0 )
          {
            // main title
            title.Font = DefaultTitleFont;
          }
          chart.Titles.Add( title );
        }
      }

      // data series
      if( series != null )
      {
        for( int i = 0; i < series.Count; i++ )
        {
          series[i].Style.Symbol.Color = chart.Model.ColorModel.GetColor( i );
          series[i].Style.Symbol.Size = DEFAULT_MARKER_SIZE;

          if( chart.Series.Count > i )
          {
            // replace existing series
            chart.Series.BeginUpdate();
            chart.Series.RemoveAt( i );
            chart.Series.Insert( i, series[i] );
            chart.Series.EndUpdate();
          }
          else
          {
            // add new series to chart
            chart.Series.Add( series[i] );
          }
        }

        // add a legend, if necessary
        if( series.Count > 1 && chart.Legends.Count == 0 )
        {
          ChartLegend legend = new ChartLegend();
          legend.ShowBorder = true;
          legend.RepresentationType = ChartLegendRepresentationType.Rectangle;
          chart.Legends.Add( legend );
        }
      }
    }

    /// <summary>
    /// Gets a new chart of the default size.
    /// </summary>
    protected static ChartControl GetDefaultChart()
    {
      ChartControl chart = new ChartControl()
      {
        Size = DefaultSize,
      };
      chart.Legends.Clear();
      return chart;
    }

    #endregion Protected

    #endregion Static Functions

    #region Nested Classes --------------------------------------------------

    /// <summary>
    /// Class Unit represents a unit of physical quantity.
    /// </summary>
    public class Unit
    {

      #region Constructors ----------------------------------------------------

      /// <summary>
      /// Default constructor.
      /// </summary>
      /// <remarks>
      /// Constructs a Unit with start = 0, step = 1, and name = "Index";
      /// </remarks>
      public Unit()
        : this (0, 1, "Index")
      {}

      /// <summary>
      /// Constructs a Unit with the specified starting value, step size, and name.
      /// </summary>
      /// <param name="start">The starting value.</param>
      /// <param name="step">The step size.</param>
      /// <param name="name">The name.</param>
      public Unit( double start, double step, string name )
      {
        Start = start;
        Step = step;
        Name = name;
      }

      #endregion

      #region Properties ------------------------------------------------------

      /// <summary>
      /// Gets and sets the starting value.
      /// </summary>
      public double Start { get; set; }

      /// <summary>
      /// Gets and sets the step size.
      /// </summary>
      public double Step { get; set; }

      /// <summary>
      /// Gets and sets the name.
      /// </summary>
      public string Name { get; set; }

      #endregion

      #region Member Functions ------------------------------------------------

      public DoubleVector ToDoubleVector( int length )
      {
        return new DoubleVector( length, Start, Step );
      }

      #endregion

    }

    #endregion

  }
}
