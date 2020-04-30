function filter() {
    var e = document.getElementById("moodDates");
    var selectedItem = e.options[e.selectedIndex].value;

    if (selectedItem == "all") {
        document.getElementById("allTimeResults").style.display = 'inline-table';
        document.getElementById("todayResults").style.display = 'none';
        document.getElementById("thisWeekResults").style.display = 'none';
    }

    else if (selectedItem == "today") {
        document.getElementById("allTimeResults").style.display = 'none';
        document.getElementById("todayResults").style.display = 'inline-table';
        document.getElementById("thisWeekResults").style.display = 'none';
    }

    else if (selectedItem == "week") {
        document.getElementById("allTimeResults").style.display = 'none';
        document.getElementById("todayResults").style.display = 'none';
        document.getElementById("thisWeekResults").style.display = 'inline-table';
    }

}
