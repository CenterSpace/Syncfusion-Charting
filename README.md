
Syncfusion / NMath Charting 
===========================

In order to use and run these examples, the CenterSpace NMath suite and Syncfusion Chart package must
be installed.  Trial verions of NMath are available at: 
 <a href="www.centerspace.net/downloads/trial-verions/"> Nmath
trial verions </a>, and Syncfusion Essential Studio User Interface Edition can be downloaded
<a href="www.syncfusion.com/products/user-interface-edition"> here  </a>.

<h2> Class Usage </h2>
Class NMathChart and NMathStatsChart provide static methods for plotting NMath and NMath Stats
numerical types using Syncfusion Essential Chart for Windows Forms controls.

Overloads of the ToChart() function are provided for common NMath types. ToChart() returns
an instance of Syncfusion.Windows.Forms.Chart.ChartControl, which can be customized as desired.

    Polynomial poly = new Polynomial( new DoubleVector( 4, 2, 5, -2, 3 ) );
    ChartControl chart = NMathChart.ToChart( poly, -1, 1 );
    chart.Titles.Add("Hello World");

The default look of the chart is governed by static properties on this class: DefaultSize,
DefaultTitleFont, DefaultAxisTitleFont, DefaultMajorGridLineColor, and DefaultMarker.

For prototyping and debugging console applications, the Show() function shows a given chart
in a default form.

    NMathChart.Show( chart );

Note that when the window is closed, the chart is disposed.

If you do not need to customize the chart, overloads of Show() are also provided for common
NMath types.

    NMathChart.Show( poly );

This is equivalent to calling:

    NMathChart.Show( NMathChart.ToChart( poly ) );

The Save() function saves a chart to a file. 

    NMathChart.Save( chart, "chart.png" );

If you are developing a Windows Forms application using the Designer, add a ChartControl
to your form, then update it with an NMath object using the appropriate Update() function
after initialization. 

    public Form1()
    {
      InitializeComponent();
  
      Polynomial poly = new Polynomial( new DoubleVector( 4, 2, 5, -2, 3 ) );
      NMathChart.Update( ref this.chart1, poly, -1, 1 );
    }

Titles are added only if the given chart does not currently contain any titles;
chart.Series[0] is replaced, or added if necessary.

For examples demonstrating the use of NMathChart and NMathStatsChart, see Examples.cs.
