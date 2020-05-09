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

function addCustomTasks() {
    $.ajax({
        type: 'GET',
        contentType: "application/json",
        async: true,
        processData: false,
        url: '/calendar/addcustomtasks',
        success: messageOut,
        error: errorOnAjax
    });

    showCustomList();
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

function showCustomList() {
    var list = document.getElementById("addCustom")
    var displaySetting = list.style.display;

    if (displaySetting == 'block') {
        list.style.display = 'none';
    }

    else {
        list.style.display = 'block';
    }
}

function createCustomTask() {
    var list = document.getElementById("addCusTask")
    var displaySetting = list.style.display;

    if (displaySetting == 'block') {
        list.style.display = 'none';
    }

    else {
        list.style.display = 'block';
    }
}

function editCustom(key) {
    var tdObj = document.getElementById(key)
    var preText = tdObj.textContent.trim();
    var inputObj = $("<input type='text' />");
    tdObj.textContent = "";
    inputObj.val(preText).appendTo(tdObj).trigger("focus").trigger("select");
    inputObj.keyup(function (event) {
        if (13 == event.which) { // press ENTER-key
            var text = $(this).val();
            tdObj.textContent = text;
            var dataSend = JSON.stringify({
                'customTaskContent': key,
                'task': text
            });
            $.ajax({
                type: 'POST',
                contentType: "application/json",
                data: dataSend,
                url: '/calendar/EditCustom',
                error: errorOnAjax
            });
        }
        else if (27 == event.which) {  // press ESC-key
            tdObj.textContent = preText;
        }
    });
    inputObj.click(function () {
        return false;
    });
}

function deleteCustom(key) {
    var dataSend = JSON.stringify({
        'customTaskContent': key
    });
    $.ajax({
        type: 'POST',
        contentType: "application/json",
        data: dataSend,
        url: '/calendar/DeleteCustom',
        error: errorOnAjax
    });
    var table = document.getElementById("myTable");
    for (var i = 0; i < table.rows.length; i++) {
        if (table.rows[i].cells[0].id == key) {
            table.deleteRow(i);
        }
    }
}

function addCustom() {
    var task = document.getElementById("newTask").value;
    var dataSend = JSON.stringify({
        'titleCustom': task
    });
    $.ajax({
        type: 'POST',
        contentType: "application/json",
        data: dataSend,
        url: '/calendar/CreateCustom',
        success: addRow,
        error: errorOnAjax
    });
    //clear input box
    document.getElementById("newTask").value = "";
}

function addRow(data) {
    //add row to table
    var PK = data.PK;
    var task = data.title;
    var row = document.getElementById("myTable").insertRow(-1);
    var len = document.getElementById("myTable").rows.length;
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    cell1.innerHTML = task;
    cell1.id = PK;
    cell2.innerHTML = "<button class=\"MarkDownButtons\" onclick=\"editCustom(" + PK + ")\">Edit</button> <button class=\"MarkDownButtons\" onclick =\"deleteCustom(" + PK + ")\" > Delete </button>";
}

function AddSuggestion()
{
    var text = document.getElementById("sugg").innerHTML;
    var dataSend = JSON.stringify({
        'sugg': text
    });
    $.ajax({
        type: 'POST',
        contentType: "application/json",
        async: true,
        processData: false,
        data: dataSend,
        url: '/calendar/DailySugg',
        success: messageOut,
        error: errorOnAjax
    });
    
}