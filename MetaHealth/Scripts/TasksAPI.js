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

            $.ajax({
                type: 'GET',
                url: '/calendar/markdowntask?task=' + task + '&taskID=' + taskID,
                success: messageOut,
                error: errorOnAjax
            });
        }
    }
};

function errorOnAjax() {
    console.log('Error on AJAX Return');
}

function messageOut(data) {
    console.log('Message Sent');
}