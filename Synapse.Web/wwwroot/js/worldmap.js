$(document).mouseover(function (e) {
    var x = e.pageX + 10 + 'px';
    var y = e.pageY + 10 + 'px';

    //var img = $('<img src="" alt="myimage" />');
    var img = $('.details').css('width', '250px');
    var div = $('<div>').css({
        "position": "absolute",
        "left": x,
        "top": y
    });
    div.append(img);
    $(document.body).append(div);
});

var zoom = d3.behavior.zoom().scaleExtent([1, 8]).on("zoom", zoomed);
/* JavaScript goes here. */
// globals used in graph   34a7dc
var mapdata = {};
var palette = ['#347dbf', '#347dbf', '#347dbf', '#347dbf', '#347dbf', '#347dbf', '#347dbf', '#347dbf', '#347dbf', '#347dbf'];
var width = 1080, height = 750, scale0 = (width - 1) / 2 / Math.PI;
var minDocCount = 0, quantiles = {};
// projection definitions
var projection = d3.geo.mercator()
    .scale((width + 1) / 2 / Math.PI)
    .translate([width / 2, height / 1.5])
    .precision(.1);
var path = d3.geo.path().projection(projection);
var graticule = d3.geo.graticule();
//var zoom = d3.behavior.zoom()
//    .translate([width / 2, height / 2])
//    .scale(scale0)
//    .scaleExtent([scale0, 8 * scale0])
//    .on("zoom", zoomed);
// SVG related definitions
var svg = d3.select('#mappad')
            //.classed("svg-container", true) //container class to make it responsive
            .append('svg')
            //.attr({'width': width, 'height': height})
            .attr("viewBox", "0 0 1080 750")
            .attr("preserveAspectRatio", "none")
          //.call(d3.behavior.zoom().on("zoom", function () {
          //    svg.attr("transform", "translate(" + d3.event.translate + ")" + " scale(" + d3.event.scale + ")")
          //}))
    .call(zoom)
    .on("wheel.zoom", null)
            //.classed("svg-content-responsive", true)
            .append('g');
if ((navigator.userAgent.indexOf("MSIE") != -1) || (!!document.documentMode == true)) //IF IE > 10
{
    $('#mappad svg').parent().css("height", "750px");
} else {
    $('#mappad svg').parent().css("height", 'auto');
};

var filter = svg.append('defs')
    .append('filter')
    .attr({ 'x': 0, 'y': 0, 'width': 1, 'height': 1, 'id': 'gray-background' });


 //svg.call(zoom)
 //   .call(zoom.event);
/*filter.append('feFlood')
    .attr('flood-color', '#f2f2f2')
    .attr('result', 'COLOR');
filter.append('feMorphology')
    .attr('operator', 'dilate')
    .attr('radius', '.9')
    .attr('in', 'SourceAlpha')
    .attr('result', 'MORPHED');
filter.append('feComposite')
    .attr('in', 'SourceGraphic')
    .attr('in2', 'MORPHED')
    .attr('result', 'COMP1');
filter.append('feComposite')
    .attr('in', 'COMP1')
    .attr('in2', 'COLOR');*/

svg.append("path")
    .datum(graticule)
    .attr("class", "graticule")
    .attr("d", path);

//d3.json('../Content/mockelasticdata.json', function (error, mockdata) {
//    if (error) return console.error(error);
//    console.log('mockdata', mockdata);
//    mapdata = mockdata;
//    draw(mockdata)
//});

function getWorlddata(mockdata) {
    mapdata = mockdata;
    draw(mockdata)
}
function draw(data) {
    // var localstoreWorldData = localStorage.getItem('worldmapData');
    // if (localstoreWorldData && localstoreWorldData.length) {
    //     localstoreWorldData = JSON.parse(localstoreWorldData);
    //     console.log('localstoreWorldData',localstoreWorldData);
    //     if (localstoreWorldData) {
    //         processWorldD(localstoreWorldData, data);
    //         //no need proceed further
    //         return;
    //     }
    // }
    d3.json('../Content/world.json', function (error, world) {
        if (error) return console.error(error);
        //console.log('world', world);
        processWorldD(world, data);
        //localStorage.setItem('worldmapData', JSON.stringify(world));
    });
}
function processWorldD(world, data) {
    for (var idx = 0; idx < data.aggregations.world_map.buckets.length; idx++) {
        var cCode = data.aggregations.world_map.buckets[idx].key.toUpperCase();
        var doc_count = data.aggregations.world_map.buckets[idx].doc_count;
        var delivery_rate = data.aggregations.world_map.buckets[idx].delivery_rate;
        for (var wdx = 0; wdx < world.objects.subunits.geometries.length; wdx++) {
            var cName = world.objects.subunits.geometries[wdx].id.toUpperCase();
            if (cCode === cName) {
                world.objects.subunits.geometries[wdx].properties.doc_count = doc_count;
                world.objects.subunits.geometries[wdx].properties.delivery_rate = delivery_rate;
            }

        }
    }
    var subunits = topojson.feature(world, world.objects.subunits);
    subunits.features = subunits.features.filter(function (d) { return d.id !== "ATA"; });
   // console.log('subunits', subunits);
    minDocCount = d3.min(subunits.features, function (d) { return d.properties.doc_count; });
   // console.log('minDocCount', minDocCount);
    var doc_counts = subunits.features.map(function (d) { return d.properties.doc_count; });
    doc_counts = doc_counts.filter(function (d) { return d; }).sort(d3.ascending);
    //console.log('doc_counts',doc_counts);
    quantiles['0.95'] = d3.quantile(doc_counts, '0.95');
    var countries = svg.selectAll('path.subunit')
        .data(subunits.features).enter();
    countries.insert('path', '.graticule')
        .attr('class', function (d) { return 'subunit ca' + d.id; })
        .style('fill', heatColor)
        .attr('d', path)
        .on('mouseover', mouseoverLegend).on('mouseout', mouseoutLegend);

    countries.append('svg:text')
        .attr('class', function (d) { return 'subunit-label la' + d.id + d.properties.name.replace(/[ \.#']+/g, ''); })
        //.attr('transform', function(d) { return 'translate('+ path.centroid(d) +')'; })
        .attr('transform', function (d) { return 'translate(' + (width - (12 * d.properties.name.length)) + ',' + (15) + ')'; })
        .attr('dy', '.35em')
        .attr('filter', 'url(#gray-background)')
        .append('svg:tspan')
        .attr('x', 0)
        .attr('dy', 5)
        .text(function (d) { return d.properties.name; })
        .append('svg:tspan')
        .attr('x', 0)
        .attr('dy', 20)
        .text(function (d) { return d.properties.doc_count ? d.properties.doc_count : ''; })
        .append('svg:tspan')
        .attr('x', 0)
        .attr('dy', 35)
        .text(function (d) { return d.properties.delivery_rate ? d.properties.delivery_rate : ''; });
}

function mouseoverLegend(datum, index) {
    d3.selectAll('.subunit-label.la' + datum.id + datum.properties.name.replace(/[ \.#']+/g, ''))
        .style('display', 'inline-block');
    d3.selectAll('.subunit.ca' + datum.id)
        .style('fill', '#999999');
    $('.details').html("<div class='country-tooltipbg'><table class='country-tooltiptable'><tr><td colspan='2' class='tooltip-cname'>" + datum.properties.name + "</td></tr><tr><td width='130'>Total Messages Sent :</td><td>" + datum.properties.doc_count + "</td></tr><tr><td>Delivery Rate :</td><td>" + datum.properties.delivery_rate + "</td></tr></table></div>").show();
}

function mouseoutLegend(datum, index) {
    d3.selectAll('.subunit-label.la' + datum.id + datum.properties.name.replace(/[ \.#']+/g, ''))
        .style('display', 'none');
    d3.selectAll('.subunit.ca' + datum.id)
        .style('fill', heatColor(datum));
    $('.details').hide();
}

function coutryclicked(datum, index) {
    //filter event for this country should be applied here
    //console.log('coutryclicked datum', datum);
    //$('.details').html("<table width='100%' style='font-size: 16px'><tr><td>" + datum.properties.name + "</td><td>" + datum.properties.doc_count + "</td></tr></table>");
}
function heatColor(d) {
    if (quantiles['0.95'] === 0 && minDocCount === 0) return '#79c6e9';
    if (!d.properties.doc_count) return '#79c6e9';
    if (d.properties.doc_count > quantiles['0.95']) return palette[(palette.length - 1)];
    if (quantiles['0.95'] == minDocCount) return palette[(palette.length - 1)];
    var diffDocCount = quantiles['0.95'] - minDocCount;
    var paletteInterval = diffDocCount / palette.length;
    var diffDocCountDatum = quantiles['0.95'] - d.properties.doc_count;
    var diffDatumDiffDoc = diffDocCount - diffDocCountDatum;
    var approxIdx = diffDatumDiffDoc / paletteInterval;
    if (!approxIdx || Math.floor(approxIdx) === 0) approxIdx = 0;
    else approxIdx = Math.floor(approxIdx) - 1;
    return palette[approxIdx];
}

//function zoomed() {
//    projection
//        .translate(zoom.translate())
//        .scale(zoom.scale());

//    svg.selectAll("path")
//        .attr("d", path);
//}
function zoomed() {
    svg.attr("transform",
        "translate(" + zoom.translate() + ")" +
        "scale(" + zoom.scale() + ")"
    );
}

function interpolateZoom(translate, scale) {
    var self = this;
    return d3.transition().duration(350).tween("zoom", function () {
        var iTranslate = d3.interpolate(zoom.translate(), translate),
            iScale = d3.interpolate(zoom.scale(), scale);
        return function (t) {
            zoom
                .scale(iScale(t))
                .translate(iTranslate(t));
            zoomed();
        };
    });
}

function zoomClick() {
    var clicked = d3.event.target,
        direction = 1,
        factor = 0.2,
        target_zoom = 1,
        center = [width / 2, height / 2],
        extent = zoom.scaleExtent(),
        translate = zoom.translate(),
        translate0 = [],
        l = [],
        view = { x: translate[0], y: translate[1], k: zoom.scale() };

    d3.event.preventDefault();
    direction = (this.id === 'zoom_in') ? 1 : -1;
    target_zoom = zoom.scale() * (1 + factor * direction);

    if (target_zoom < extent[0] || target_zoom > extent[1]) { return false; }

    translate0 = [(center[0] - view.x) / view.k, (center[1] - view.y) / view.k];
    view.k = target_zoom;
    l = [translate0[0] * view.k + view.x, translate0[1] * view.k + view.y];

    view.x += center[0] - l[0];
    view.y += center[1] - l[1];

    interpolateZoom([view.x, view.y], view.k);
}

d3.selectAll('button').on('click', zoomClick);