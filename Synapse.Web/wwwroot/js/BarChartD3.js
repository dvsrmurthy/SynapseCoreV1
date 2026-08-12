
var margin = { top: 0, right: 60, bottom: 60, left: 40 },
            width = 800 - margin.left - margin.right,
            height = 300 - margin.top - margin.bottom;

//var formatPercent = d3.format(".0%");

//x labels padding
var x = d3.scale.ordinal()
    .rangeRoundBands([0, width], .1, .1);

var y = d3.scale.linear()
    .range([height, 0]);

var xAxis = d3.svg.axis()
    .scale(x)
    .orient("bottom");

var yAxis = d3.svg.axis()
    .scale(y)
    .orient("left");
    //.tickFormat(formatPercent);
if ((navigator.userAgent.indexOf("MSIE") != -1) || (!!document.documentMode == true)) //IF IE > 10
{
    $('#chart').css("height", "350px");
} else {
    $('#chart').css("height", 'auto');
};
var chart = d3.select("#chart")
    .append("svg")
    //.attr("preserveAspectRatio", "xMinYMin meet")
    .attr("viewBox", "-60 -20 800 300") //append svg element inside #chart
// .attr("width", width + (2 * margin.left) + margin.right) //set width
//.attr("height", height + margin.top + margin.bottom).attr("class", "chart"); //set height

function BarchartData(data) {
    //initchart();
    $(chart[0][0]).html("");
    //x.domain(data.map(function(d) { return d.letter }));
    x.domain(data.map(function (d) { return d.Letter }));
    //y.domain([0, d3.max(data, function(d) { return d.frequency })]);
    y.domain([0, d3.max(data, function (d) { return d.Freq / 1 })]);
    data.forEach(function (d) {
        d.Freq = +d.Freq;
    });

    x.domain(data.map(function (d) { return d.Letter; }));
    y.domain([0, d3.max(data, function (d) { return d.Freq; })]);

    chart.append("g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + height + ")")
        .call(xAxis);        

    chart.append("g")
        .attr("class", "y axis")
        .call(yAxis)
      .append("text")
        .attr("transform", "rotate(-90)")
        .attr("y", 1)
        .attr("dy", ".71em")
        .style("text-anchor", "end")
        .style("font-size", "8px")
        .text("SMS Traffic");

    chart.selectAll(".bar")
        .data(data)
      .enter().append("rect")
        .attr("class", "bar")
        .attr("x", function (d) { return x(d.Letter); })

        .attr("width", x.rangeBand())
        .attr("height", 0)
        .on("mouseover", function () { tooltip.style({ "display": 'block', "opacity": '1' }); })
        .on("mouseout", function () { tooltip.style({ "display": "none", "opacity": '0' }); })
        .on("mousemove", function (d) {
            var xPosition = x(d.Letter);
            var yPosition = y(d.Freq) - 15;
            tooltip.attr("transform", "translate(" + xPosition + "," + yPosition + ")");
            tooltip.select("text").text(numberWithThousands(d.Freq)).style("opacity", '1');
        })
        .attr("y", height)
		.transition().duration(1000)
		.delay(function (d, i) { return i * 20; })
		.attr({
		    "y": function (d) { return y(d.Freq) },
		    "height": function (d) { return height - y(d.Freq); },
		})
    //.style("fill", function (d, i) {return "rgb(20, 20 ," + ((i * 30) + 100) + ")";})

    //xAxis tooltip
    d3.selectAll('.x .tick, .x .tick .line')
    .data(data)
    .on("mouseover", function () { tooltip.style({ "display": 'block', "opacity": '1' }); })
        .on("mouseout", function () { tooltip.style({ "display": "none", "opacity": '0' }); })
        .on("mousemove", function (d) {
            var xPosition = x(d.Letter);
            var yPosition = y(d.Freq) - 15;
            tooltip.attr("transform", "translate(" + xPosition + "," + yPosition + ")");
            tooltip.select("text").text(numberWithThousands(d.Freq)).style("opacity", '1');
        })

    //Bar Values top
    //svg.selectAll(".text")
    //.data(data)
    //.enter()
    //.append("g")
    //    .append("text")
    //.attr('class','textval')
    //.text(function (d) { return numberWithThousands(d.Freq); })
    //.attr("x", function (d, i) {return x(d.Letter) + x.rangeBand()/2;})
    //.attr("y", height)
    //.transition().duration(1000)
    //.delay(function (d, i) { return i * 20; })
    //.attr({
    //    "y": function (d) { return y(d.Freq) - 5 },
    //    //"height": function (d) { return height - y(d.Freq); },
    //})
    //.style("text-anchor", "middle")
    //.style("font-size", "8px")

    //.style("fill", function (d) { return "rgb(74, 174, " + ((d.Freq * 30) + 100) + ")"; })


    //d3.select("input").on("change", change);

    //var sortTimeout = setTimeout(function () {
    //    d3.select("input").property("checked", true).each(change);
    //}, 2000);

    //function change() {
    //    clearTimeout(sortTimeout);

    //    // Copy-on-write since tweens are evaluated after a delay.
    //    var x0 = x.domain(data.sort(this.checked
    //        ? function (a, b) { return b.Freq - a.Freq; }
    //        : function (a, b) { return d3.ascending(a.Letter, b.Letter); })
    //        .map(function (d) { return d.Letter; }))
    //        .copy();

    //    svg.selectAll(".bar")
    //        .sort(function (a, b) { return x0(a.Letter) - x0(b.Letter); });

    //    var transition = svg.transition().duration(750),
    //        delay = function (d, i) { return i * 50; };

    //    transition.selectAll(".bar")
    //        .delay(delay)
    //        .attr("x", function (d) { return x0(d.Letter); });

    //    transition.select(".x.axis")
    //        .call(xAxis)
    //      .selectAll("g")
    //        .delay(delay);
    //}
    $('.x text, .y text').css("font-size", "9px");
    $('.y.axis > text').addClass('ylabel');
    // Prep the tooltip bits, initial display is hidden
    var tooltip = chart.append("g")
      .attr("class", "tipval")
      .style("display", "none");

    tooltip.append("rect")
      //.attr("width", 60)
      //.attr("height", 20)
      .attr("fill", "white")
      .style("opacity", 0.6);

    tooltip.append("text")
      .attr("x", x.rangeBand() / 2)
      .attr("dy", "1.2em")
      .style({ "text-anchor": "middle", "font-size": "8px", "font-weight": "bold" })

    $('#chart').find('.x.axis text').attr('x', 10).attr("transform", "rotate(30)");
   // $('.textval').attr("transform", "rotate(-90)");
}
function type(d) {
    //d.letter = +d.letter; // coerce to number
    d.Letter = +d.Letter;
    return d;
}