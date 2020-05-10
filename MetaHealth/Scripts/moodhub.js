function filter() {
    var e=document.getElementById("moodDates");
    var selectedItem=e.options[e.selectedIndex].value;
    switch(selectedItem) {
        case "today": {
            document.getElementById("allTimeResults").style.display='none';
            document.getElementById("todayResults").style.display='inline-table';
            document.getElementById("thisWeekResults").style.display='none';
            //document.getElementById("specificDate").style.display='none';
            break;
        }

        case "week": {
            document.getElementById("allTimeResults").style.display='none';
            document.getElementById("todayResults").style.display='none';
            document.getElementById("thisWeekResults").style.display='inline-table';
            //document.getElementById("specificDate").style.display='none';
            break;
        }

        case "specificDate": {
            document.getElementById("allTimeResults").style.display='none';
            document.getElementById("todayResults").style.display='none';
            document.getElementById("thisWeekResults").style.display='none';
            //document.getElementById("specificDate").style.display='inline-table';
            break;
        }

        default:
            document.getElementById("allTimeResults").style.display='inline-table';
            document.getElementById("todayResults").style.display='none';
            document.getElementById("thisWeekResults").style.display='none';
            //document.getElementById("specificDate").style.display='none';
            break;
    }
}
