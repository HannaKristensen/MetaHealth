jQuery(document).ready(function() {
    var model='@Model.MoodsByDate';
    var chart=new CanvasJS.Chart("moodgraph",{
        animationEnabled: false,
        theme: "light2",
        title: {
            text: "Simple Line Chart"
        },
        axisY: {
            includeZero: false
        },
        data: [{
            type: "line",
            indexLabelFontSize: 16,
            dataPoints: [

                { x: 1,y: 450 },
                { y: 414 },
                { y: 520,indexLabel: "\u2191 highest",markerColor: "red",markerType: "triangle" },
                { y: 460 },
                { y: 450 },
                { y: 500 },
                { y: 480 },
                { y: 480 },
                { y: 410,indexLabel: "\u2193 lowest",markerColor: "DarkSlateGrey",markerType: "cross" },
                { y: 500 },
                { y: 480 },
                { y: 510 }
            ]
        }]
    });
    chart.render();

});