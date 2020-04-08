function MarkDown() {
    //Reference the Table.
    var grid = document.getElementById("TaskTable");

    //Reference the CheckBoxes in Table.
    var checkBoxes = grid.getElementsByTagName("INPUT");

    //Loop through the CheckBoxes.
    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked) {
            var row = checkBoxes[i].parentNode.parentNode;
            var task = row.cells[1].innerHTML;
            var taskID = row.id;
            if (task == '#') {
                task = 'sharp';
            }

            $.ajax({
                type: 'GET',
                contentType: "application/json",
                async: true,
                processData: false,
                url: '/calendar/markdowntask?task=' + task + '&taskID=' + taskID,
                success: messageOut,
                error: errorOnAjax
            });

            confetti();
        }
    }
    $.ajax({
        type: 'GET',
        contentType: "application/json",
        async: true,
        processData: false,
        url: '/calendar/markdowntask',
        success: messageOut,
        error: errorOnAjax
    });
};

function errorOnAjax() {
    console.log('Error on AJAX Return');
}

function messageOut(data) {
    console.log('Message Sent');
    var obj = JSON.parse(data);
    var TaskTable = document.getElementById("TaskTable");

    for (var j = 0; j < TaskTable.rows.length; j++) {
        TaskTable.deleteRow(0);
    }

    $("#TaskTable").find("tr").remove();

    for (var i = 0; i < obj[0].length; i++) {
        var message = "";
        message += "<tr id=\"";
        message += obj[0][i];
        message += "\"><td><input type=\"checkbox\"></td><td>";
        message += obj[1][i];
        message += "</td></tr>";

        var tableRef = document.getElementById('TaskTable').getElementsByTagName('tbody')[0];

        var newRow = tableRef.insertRow(tableRef.rows.length);
        newRow.innerHTML = message;
        newRow.id = obj[0][i];
    }
}

function showRedirect() {
    document.getElementById("redirectInst").style.display = 'block';
}

function hideContent() {
    document.getElementById("eventsPage").style.display = 'none';
}

function showAddTask() {
    var addTask = document.getElementById("addTaskRow")
    var displaySetting = addTask.style.display;

    if (displaySetting == 'block') {
        addTask.style.display = 'none';
    }

    else {
        addTask.style.display = 'block';
    }
}

function showPreMadeTasks() {
    var multiTask = document.getElementById("preMadeTasks")
    var displaySetting = multiTask.style.display;

    if (displaySetting == 'block') {
        multiTask.style.display = 'none';
    }

    else {
        multiTask.style.display = 'block';
    }
}

function showAddEvent() {
    var event = document.getElementById("eventAddForm")
    var displaySetting = event.style.display;

    if (displaySetting == 'block') {
        event.style.display = 'none';
    }

    else {
        event.style.display = 'block';
    }
}