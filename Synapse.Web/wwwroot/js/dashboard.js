
// Dashboard.js
// ====================================================================
// This file should not be included in your project.
// This is just a sample how to initialize plugins or components.
//
// - ThemeOn.net -


$(window).on('load', function() {


    // Network chart ( Morris Line Chart )
    // =================================================================
    // Require MorrisJS Chart
    // -----------------------------------------------------------------
    // http://morrisjs.github.io/morris.js/
    // =================================================================

    var day_data = [
        {"elapsed": "Oct-12", "value": 24, b:2},
        {"elapsed": "Oct-13", "value": 34, b:22},
        {"elapsed": "Oct-14", "value": 33, b:7},
        {"elapsed": "Oct-15", "value": 22, b:6},
        {"elapsed": "Oct-16", "value": 28, b:17},
        {"elapsed": "Oct-17", "value": 60, b:15},
        {"elapsed": "Oct-18", "value": 60, b:17},
        {"elapsed": "Oct-19", "value": 70, b:7},
        {"elapsed": "Oct-20", "value": 67, b:18},
        {"elapsed": "Oct-21", "value": 86, b: 18},
        {"elapsed": "Oct-22", "value": 86, b: 18},
        {"elapsed": "Oct-23", "value": 113, b: 29},
        {"elapsed": "Oct-24", "value": 130, b: 23},
        {"elapsed": "Oct-25", "value": 114, b:10},
        {"elapsed": "Oct-26", "value": 80, b:22},
        {"elapsed": "Oct-27", "value": 109, b:7},
        {"elapsed": "Oct-28", "value": 100, b:6},
        {"elapsed": "Oct-29", "value": 105, b:17},
        {"elapsed": "Oct-30", "value": 110, b:15},
        {"elapsed": "Oct-31", "value": 102, b:17},
        {"elapsed": "Nov-01", "value": 107, b:7},
        {"elapsed": "Nov-02", "value": 60, b:18},
        {"elapsed": "Nov-03", "value": 67, b: 18},
        {"elapsed": "Nov-04", "value": 76, b: 18},
        {"elapsed": "Nov-05", "value": 73, b: 29},
        {"elapsed": "Nov-06", "value": 94, b: 13},
        {"elapsed": "Nov-07", "value": 135, b:2},
        {"elapsed": "Nov-08", "value": 154, b:22},
        {"elapsed": "Nov-09", "value": 120, b:7},
        {"elapsed": "Nov-10", "value": 100, b:6},
        {"elapsed": "Nov-11", "value": 130, b:17},
        {"elapsed": "Nov-12", "value": 100, b:15},
        {"elapsed": "Nov-13", "value": 60, b:17},
        {"elapsed": "Nov-14", "value": 70, b:7},
        {"elapsed": "Nov-15", "value": 67, b:18},
        {"elapsed": "Nov-16", "value": 86, b: 18},
        {"elapsed": "Nov-17", "value": 86, b: 18},
        {"elapsed": "Nov-18", "value": 113, b: 29},
        {"elapsed": "Nov-19", "value": 130, b: 23},
        {"elapsed": "Nov-20", "value": 114, b:10},
        {"elapsed": "Nov-21", "value": 80, b:22},
        {"elapsed": "Nov-22", "value": 109, b:7},
        {"elapsed": "Nov-23", "value": 100, b:6},
        {"elapsed": "Nov-24", "value": 105, b:17},
        {"elapsed": "Nov-25", "value": 110, b:15},
        {"elapsed": "Nov-26", "value": 102, b:17},
        {"elapsed": "Nov-27", "value": 107, b:7},
        {"elapsed": "Nov-28", "value": 60, b:18},
        {"elapsed": "Nov-29", "value": 67, b: 18},
        {"elapsed": "Nov-30", "value": 76, b: 18},
        {"elapsed": "Des-01", "value": 73, b: 29},
        {"elapsed": "Des-02", "value": 94, b: 13},
        {"elapsed": "Des-03", "value": 79, b: 24}
    ];

    var chart = Morris.Area({
        element : 'morris-chart-network',
        data: day_data,
        axes:false,
        xkey: 'elapsed',
        ykeys: ['value', 'b'],
        labels: ['Download Speed', 'Upload Speed'],
        yLabelFormat :function (y) { return y.toString() + ' Mb/s'; },
        gridEnabled: false,
        gridLineColor: 'transparent',
        lineColors: ['#82c4f8','#0d92fc'],
        lineWidth:[0,0],
        pointSize:[0,0],
        fillOpacity: 1,
        gridTextColor:'#999',
        parseTime: false,
        resize:true,
        behaveLikeLine : true,
        hideHover: 'auto'
    });





    // HDD USAGE - SPARKLINE LINE AREA CHART
    // =================================================================
    // Require sparkline
    // -----------------------------------------------------------------
    // http://omnipotent.net/jquery.sparkline/#s-about
    // =================================================================
    var hddSparkline = function() {
        $("#demo-sparkline-area").sparkline([57,69,70,62,73,79,76,77,73,52,57,50,60,55,70,68], {
            type: 'line',
            width: '100%',
            height: '40',
            spotRadius: 5,
            lineWidth: 1.5,
            lineColor:'rgba(255,255,255,.85)',
            fillColor: 'rgba(0,0,0,0.03)',
            spotColor: 'rgba(255,255,255,.5)',
            minSpotColor: 'rgba(255,255,255,.5)',
            maxSpotColor: 'rgba(255,255,255,.5)',
            highlightLineColor : '#ffffff',
            highlightSpotColor: '#ffffff',
            tooltipChartTitle: 'Usage',
            tooltipSuffix:' %'

        });
    }




    // EARNING - SPARKLINE LINE CHART
    // =================================================================
    // Require sparkline
    // -----------------------------------------------------------------
    // http://omnipotent.net/jquery.sparkline/#s-about
    // =================================================================
    var earningSparkline = function(){
        $("#demo-sparkline-line").sparkline([345,404,305,455,378,567,586,685,458,742,565], {
            type: 'line',
            width: '100%',
            height: '40',
            spotRadius: 4,
            lineWidth:1,
            lineColor:'#ffffff',
            fillColor: false,
            minSpotColor :false,
            maxSpotColor : false,
            highlightLineColor : '#ffffff',
            highlightSpotColor: '#ffffff',
            tooltipChartTitle: 'Earning',
            tooltipPrefix :'$ ',
            spotColor:'#ffffff',
            valueSpots : {
                '0:': '#ffffff'
            }
        });
    }



    // SALES - SPARKLINE BAR CHART
    // =================================================================
    // Require sparkline
    // -----------------------------------------------------------------
    // http://omnipotent.net/jquery.sparkline/#s-about
    // =================================================================

    var barEl = $("#demo-sparkline-bar");
    var barValues = [40,32,65,53,62,55,24,67,45,70,45,56,34,67,76,32,65,53,62,55,24,67,45,70,45,56,70,45,56,34,67,76,32,65,53,62,55];
    var barValueCount = barValues.length;
    var barSpacing = 1;
    var salesSparkline = function(){
         barEl.sparkline(barValues, {
            type: 'bar',
            height: 55,
            barWidth: Math.round((barEl.parent().width() - ( barValueCount - 1 ) * barSpacing ) / barValueCount),
            barSpacing: barSpacing,
            zeroAxis: false,
            tooltipChartTitle: 'Daily Sales',
            tooltipSuffix: ' Sales',
            barColor: 'rgba(255,255,255,.7)'
        });
    }


    $(window).on('resizeEnd', function(){
        hddSparkline();
        earningSparkline();
        salesSparkline();
		
    })
    //hddSparkline();
    //earningSparkline();
    salesSparkline();


//multi chart

/*
 * Play with this code and it'll update in the panel opposite.
 *
 * Why not try some of the options above?
 */

var arrtrimonth = [
    { y: 'Sep 10', a: 150},
	{ y: 'Sep 11', a: 1560},
    { y: 'Sep 12', a: 1150},
    { y: 'Sep 13', a: 1350},
	{ y: 'Sep 14', a: 1500},
	{ y: 'Sep 15', a: 3630},
	{ y: 'Sep 16', a: 3154},
	{ y: 'Sep 17', a: 2250},
	{ y: 'Sep 18', a: 1500},
    { y: 'Sep 19', a: 3150},
    { y: 'Sep 20', a: 1364},
	{ y: 'Sep 21', a: 1600},
	{ y: 'Sep 22', a: 1000},
	{ y: 'Sep 23', a: 1264},
	{ y: 'Sep 24', a: 1000},
	{ y: 'Sep 25', a: 3523},
    { y: 'Sep 26', a: 3650},
    { y: 'Sep 27', a: 3890},
	{ y: 'Sep 28', a: 3621},
	{ y: 'Sep 29', a: 3200},
	{ y: 'Sep 30', a: 3154},
	{ y: 'Oct 01', a: 3012},
	{ y: 'Oct 02', a: 2510},
    { y: 'Oct 03', a: 2550},
    { y: 'Oct 04', a: 2364},
	{ y: 'Oct 05', a: 2600},
	{ y: 'Oct 06', a: 1000},
	{ y: 'Oct 07', a: 1264},
	{ y: 'Oct 08', a: 1500},
	{ y: 'Oct 09', a: 1561},
    { y: 'Oct 10', a: 1550},
	{ y: 'Oct 11', a: 1360},
    { y: 'Oct 12', a: 1350},
    { y: 'Oct 13', a: 1365},
	{ y: 'Oct 14', a: 2500},
	{ y: 'Oct 15', a: 2630},
	{ y: 'Oct 16', a: 2154},
	{ y: 'Oct 17', a: 2250},
	{ y: 'Oct 18', a: 2350},
    { y: 'Oct 19', a: 2150},
    { y: 'Oct 20', a: 2364},
	{ y: 'Oct 21', a: 2600},
	{ y: 'Oct 22', a: 2000},
	{ y: 'Oct 23', a: 2264},
	{ y: 'Oct 24', a: 1000},
	{ y: 'Oct 25', a: 1523},
    { y: 'Oct 26', a: 1150},
    { y: 'Oct 27', a: 1352},
	{ y: 'Oct 28', a: 2500},
	{ y: 'Oct 29', a: 2600},
	{ y: 'Oct 30', a: 2154},
	{ y: 'Oct 31', a: 2154},
	{ y: 'Nov 01', a: 2000},
	{ y: 'Nov 02', a: 2510},
    { y: 'Nov 03', a: 2150},
    { y: 'Nov 04', a: 3364},
	{ y: 'Nov 05', a: 3600},
	{ y: 'Nov 06', a: 3000},
	{ y: 'Nov 07', a: 3264},
	{ y: 'Nov 08', a: 3000},
	{ y: 'Nov 09', a: 3625},
    { y: 'Nov 10', a: 3150},
	{ y: 'Nov 11', a: 2560},
    { y: 'Nov 12', a: 2150},
    { y: 'Nov 13', a: 2350},
	{ y: 'Nov 14', a: 2500},
	{ y: 'Nov 15', a: 2630},
	{ y: 'Nov 16', a: 2154},
	{ y: 'Nov 17', a: 2250},
	{ y: 'Nov 18', a: 2476},
    { y: 'Nov 19', a: 2150},
    { y: 'Nov 20', a: 2364},
	{ y: 'Nov 21', a: 2600},
	{ y: 'Nov 22', a: 2000},
	{ y: 'Nov 23', a: 2264},
	{ y: 'Nov 24', a: 3000},
	{ y: 'Nov 25', a: 2523},
    { y: 'Nov 26', a: 3150},
    { y: 'Nov 27', a: 3352},
	{ y: 'Nov 28', a: 3500},
	{ y: 'Nov 29', a: 3600},
	{ y: 'Nov 30', a: 3154},
	{ y: 'Dec 01', a: 3000},
	{ y: 'Dec 02', a: 3510},
    { y: 'Dec 03', a: 3150},
    { y: 'Dec 04', a: 3364},
	{ y: 'Dec 05', a: 3600},
	{ y: 'Dec 06', a: 3000},
	{ y: 'Dec 07', a: 3264},
	{ y: 'Dec 08', a: 3000}
 ];

var arrmonth = [
	{ y: 'Nov 09',  a: 3000},
    { y: 'Nov 10', a:1150},
	{ y: 'Nov 11',  a: 1560},
    { y: 'Nov 12', a:1150},
    { y: 'Nov 13', a: 1350},
	{ y: 'Nov 14', a: 1500},
	{ y: 'Nov 15',  a: 1630},
	{ y: 'Nov 16', a: 1154},
	{ y: 'Nov 17',  a: 1250},
	{ y: 'Nov 18',  a: 3500},
    { y: 'Nov 19', a:1150},
    { y: 'Nov 20', a: 1364},
	{ y: 'Nov 21', a: 1600},
	{ y: 'Nov 22',  a: 1000},
	{ y: 'Nov 23', a: 1264},
	{ y: 'Nov 24',  a: 1000},
	{ y: 'Nov 25',  a: 1523},
    { y: 'Nov 26', a:1150},
    { y: 'Nov 27', a: 1352},
	{ y: 'Nov 28', a: 1500},
	{ y: 'Nov 29',  a: 1600},
	{ y: 'Nov 30', a: 1154},
	{ y: 'Dec 01',  a: 1000},
	{ y: 'Dec 02',  a: 1510},
    { y: 'Dec 03', a:1150},
    { y: 'Dec 04', a: 1364},
	{ y: 'Dec 05', a: 1600},
	{ y: 'Dec 06',  a: 1000},
	{ y: 'Dec 07', a: 1264},
	{ y: 'Dec 08',  a: 1000}
 ];
var arrtwoweek = [
	{ y: 'Nov 25',  a: 500},
    { y: 'Nov 26', a:1150},
    { y: 'Nov 27', a: 350},
	{ y: 'Nov 28', a: 500},
	{ y: 'Nov 29',  a: 600},
	{ y: 'Nov 30', a: 1154},
	{ y: 'Dec 01',  a: 1000},
	{ y: 'Dec 02',  a: 500},
    { y: 'Dec 03', a:1150},
    { y: 'Dec 04', a: 364},
	{ y: 'Dec 05', a: 600},
	{ y: 'Dec 06',  a: 1000},
	{ y: 'Dec 07', a: 1264},
	{ y: 'Dec 08',  a: 1000}
 ];
	
var arrweek = [
	{ y: 'Dec 02',  a: 500},
    { y: 'Dec 03', a:1150},
    { y: 'Dec 04', a: 100},
	{ y: 'Dec 05', a: 486},
	{ y: 'Dec 06',  a: 1000},
	{ y: 'Dec 07', a: 1865},
	{ y: 'Dec 08',  a: 1000}
 ];

var arrday = [
	{ y: '01', a: 1500},
    { y: '02', a:1150},
    { y: '03', a: 1100},
	{ y: '04', a: 510},
	{ y: '05', a: 1320},
	{ y: '06', a: 2800},
	{ y: '07', a: 3000},
	{ y: '08', a: 1500},
    { y: '09', a:1150},
    { y: '10', a: 1100},
	{ y: '11', a: 1011},
	{ y: '12', a: 1320},
	{ y: '13', a: 2625},
	{ y: '14', a: 2468},
	{ y: '15', a: 1500},
    { y: '16', a:1150},
    { y: '17', a: 1000},
	{ y: '18', a: 1011},
	{ y: '19', a: 1000},
	{ y: '20', a: 3000},
	{ y: '21', a: 896},
	{ y: '22', a: 500},
    { y: '23', a: 1150},
    { y: '24', a: 1058}
];

    function analyticsChr(cv, dobj) {
        alert('analytics invoked');
        switch (cv) {
        case 1:
            break;
        case 2:
            break;
        case 3:
            break;
        case 4:
            break;
        default:
            getMap('maptrimonth', dobj.dAnalyticsResult);
        }
    }

   // analyticsChr(0);


    function getMap(val, arr) {
 //   function getMap(arrCollection) {
        //alert('get map invoked');
        //var arr = arrCollection;
        //if (val == 'maptrimonth') var arr = arrtrimonth;
        //else if (val == 'mapmonth') var arr = arrmonth;
        //else if (val == 'maptwoweek') var arr = arrtwoweek;
        //else if (val == 'mapweek') var arr = arrweek;
        //else if (val == 'mapday') var arr = arrday;
        //else var arr = arrtrimonth;
        //alert(val);
        
        new Morris.Area({
            // ID of the element in which to draw the chart.        
            element: 'maparea',
            width: '100%',
            // Chart data records -- each entry in this array corresponds to a point on
            // the chart.
            data: arr,
            // The name of the data record attribute that contains x-values.
            xkey: 'y',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['a'],
            parseTime: false,
            lineColors: ["#0078D8", "#00BCD4", "#BA68C8", "#FFA726"],

            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['SMS'],
            hideHover: 'auto',
            pointSize: 0,
            lineWidth: 0,
            resize: true
        });
    }


//dynamic graphs for year, month, week, day
 /*var a = [arryear,arrmonth,arrtwoweek,arrweek,arrday];


    var index =0;
    while (index < a.length) {
    new Morris.Area({
        // ID of the element in which to draw the chart.        
		element: 'areaweek'+index,
        // Chart data records -- each entry in this array corresponds to a point on
        // the chart.
        data:a[index],
        // The name of the data record attribute that contains x-values.
        xkey: 'y',
        // A list of names of data record attributes that contain y-values.
        ykeys: ['a'],		
		parseTime: false,
		lineColors: ["#0078D8","#00BCD4","#BA68C8","#FFA726"],
		
        // Labels for the ykeys -- will be displayed when you hover over the
        // chart.
        labels: ['SMS'],        
        hideHover:'auto',
		pointSize: 0,
		lineWidth: 0
    });
    index++

    }*/
//end off dynamic graphs

//get graph by button click

//$('#areaweek0').hide();

$('.gls').on('click', function(){
	var id = $(this).attr("name");
	 $("#maparea").empty();
	//alert(id);
	getMap(id);
		
});
	
	
	
//end of graph button click

    // PANEL OVERLAY
    // =================================================================
    // Require Nifty js
    // -----------------------------------------------------------------
    // http://www.themeon.net
    // =================================================================
    $('#demo-panel-network-refresh').niftyOverlay().on('click', function(){
        var $el = $(this), relTime;

        $el.niftyOverlay('show');


        relTime = setInterval(function(){
            $el.niftyOverlay('hide');
            clearInterval(relTime);
        },2000);
    });


    // WELCOME NOTIFICATIONS
    // =================================================================
    // Require Admin Core Javascript
    // =================================================================
    //$.niftyNoty({
    //    type: 'dark',
    //    title: 'Welcome to Synapse Messaging.',
    //   // message: 'You will notice that you now have an admin menu<br> that appears on the right hand side.',
    //    container: 'floating',
    //    timer: 5000
    //});

});