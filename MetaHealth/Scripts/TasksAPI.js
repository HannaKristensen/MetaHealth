var CLIENT_ID = '180774585516-7nm3okoofj8am2efsvvdom563vdu056i.apps.googleusercontent.com';
var API_KEY = 'oujTGlZd2YcSU3wEAc_xReUV';

function () {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/Calendar/UpcomingEvents',
        success: displayTable,
        error: errorOnAjax
    });
};