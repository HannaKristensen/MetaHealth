const DEBUG_ENABLED = false;
const NUMBER_ROWS = 12;
const NUMBER_COLS = 20;

var score;

var grid;

function restartGame() {
    onRestart(grid);
    grid = new Array(NUMBER_COLS);
    for (var currentCol = 0; currentCol < NUMBER_COLS; currentCol++) {
        grid[currentCol] = new Array(NUMBER_ROWS);
    }

    onLoad(grid);
}

$(document).ready(function () {
    $("img").click(function (event) {
        imageClicked(event, grid);
        return true;
    });

    grid = new Array(NUMBER_COLS);
    for (var currentCol = 0; currentCol < NUMBER_COLS; currentCol++) {
        grid[currentCol] = new Array(NUMBER_ROWS);
    }

    onLoad(grid);
});

/**
 * Function is called when the page has loaded
 * @param grid(Multidimensional array of strings) board state
 * @pre grid has been initialised as a blank multidimensional array
 */
function onLoad(grid) {
    if (DEBUG_ENABLED == true) {
        $("#debug").show();
    } else {
        $("#debug").hide();
    }
    $("#dialog").hide();
    setScoreText(0);
    score = 0;
    populateGrid(grid);
}

/**
 * Function is called when restart link is clicked
 * @param grid(Multidimensional array of strings) board state
 */
function onRestart(grid) {
    score = 0;
    setScoreText(score);
    populateGrid(grid);
}

/**
 * Function is called when image is clicked
 * @param eventObject(object) Object contain details about image clicked
 * @param grid(Multidimensional array of strings) board state
 * @pre assumes that image has id in format slot[0-19]x[0-11]
 */
function imageClicked(event, grid) {
    var id = event.target.id;
    var x = parseInt(id.match(/\d+/));
    var y = parseInt(id.match(/\d+$/));
    slotClicked(x, y, grid);
}

/**
 * Function shows dialog menu with text message
 * @param text(string) message that is displayed in dialog
 */
function showDialog(text) {
    var dialogText = document.getElementById("dialogText");
    dialogText.innerHTML = text;
    $("#dialog").slideDown("fast");
}

/**
 * Function hides dialog menu
 */
function hideDialog() {
    $("#dialog").slideUp("fast");
}

/**
 * Function sets the player score text
 * @param score(int) player score to set
 */
function setScoreText(score) {
    var scoreText = document.getElementById("scoreTxt");
    scoreText.innerHTML = "Score: " + score;
}

/**
 * Function sets the contents of a slot
 * @param x(int) represents the xth horizontal slot
 * @param y(int) represents the yth vertical slot
 * @param type(string) type of item to fill the space with
 * @pre   x is between 0-19 inclusive and y is between 0-11 inclusive
 * @pre   type is either "blank", "blue", "green", "red", "yellow"
 */
function setSlotType(x, y, type) {
    var elementId = "slot" + x + "x" + y;
    var img = document.getElementById(elementId);
    if (type == "blank") {
        img.src = 'https://color-hex.org/colors/e9e6d4.png';
    } else if (type == "red") {
        img.src = 'https://i.imgur.com/HsghsKC.jpg';
    } else if (type == "yellow") {
        img.src = 'https://www.enasco.com/medias/9723819D-main-530Wx530H?context=bWFzdGVyfHJvb3R8NDU2OXxpbWFnZS9qcGVnfGgyZS9oYjMvODg0NDM5NDU2MTU2Ni5qcGd8NzM0YjgzZTk4MjYwMWU2MDNkZTc5NGQ1OTA5ZTM1MjAzNjNmNDZmNzk5MWU2NWZjY2NlYWNmZWMzOTI1NzdhNA';
    } else if (type == "blue") {
        img.src = 'https://www.nafc.uhi.ac.uk/t4-media/one-web/nafc/research/document/marine-spatial-planning/dark-blue-box.png';
    } else if (type == "green") {
        img.src = 'https://img.buzzfeed.com/buzzfeed-static/static/2020-02/26/18/enhanced/63fa524113ec/enhanced-1674-1582742675-2.png?downsize=600:*&output-format=auto&output-quality=auto';
    }
}

/**
 * Logs message into text area
 * @param message(string) message to be prepended to contents of textarea
 */
function log(message) {
    var debugTextArea = document.getElementById("debugText");
    debugTextArea.innerHTML = message + "\n" + debugTextArea.innerHTML;
}

/**
 * Clears debug text area
 */
function clearDebug() {
    var debugTextArea = document.getElementById("debugText");
    debugTextArea.innerHTML = "";
}

/**
 * Hides the debug div
 */
function hideDebug() {
    $("#debug").slideUp();
}

function populateGrid(grid) {
    var types = new Array("blue", "green", "red", "yellow");

    for (var currentCol = 0; currentCol < NUMBER_COLS; currentCol++) {
        for (var currentRow = 0; currentRow < NUMBER_ROWS; currentRow++) {
            var type = types[Math.floor(Math.random() * types.length)];
            grid[currentCol][currentRow] = type;
            setSlotType(currentCol, currentRow, type);
        }
    }
}

/**
 * Function is called when slot on play field is selected
 * @param x(int) represents the xth horizontal slot
 * @param y(int) represents the yth vertical slot
 * @param grid(Multidimensional array of strings) board state
 * @pre   x is between 0-19 inclusive and y is between 0-11 inclusive
 */
function slotClicked(x, y, grid) {
    var type = grid[x][y];
    var blocksCleared = checkNeighbor(x, y, type, grid);
    if (blocksCleared == 1) {
        setSlotType(x, y, type);
        grid[x][y] = type;
        return;
    }

    score = score + blocksCleared * blocksCleared;
    setScoreText(score);
    collapseDown(grid);
    collapseAcross(grid);
}

function checkNeighbor(x, y, type, grid) {
    var numberMatches = 1;
    setSlotType(x, y, "blank");
    grid[x][y] = "blank";

    if (isOfType(x - 1, y, type, grid) == true) {
        numberMatches += checkNeighbor(x - 1, y, type, grid);
    }

    if (isOfType(x + 1, y, type, grid) == true) {
        numberMatches += checkNeighbor(x + 1, y, type, grid);
    }

    if (isOfType(x, y - 1, type, grid) == true) {
        numberMatches += checkNeighbor(x, y - 1, type, grid);
    }

    if (isOfType(x, y + 1, type, grid) == true) {
        numberMatches += checkNeighbor(x, y + 1, type, grid);
    }
    return numberMatches;
}

function isOfType(x, y, type, grid) {
    if (x < 0 || x >= NUMBER_COLS ||
        y < 0 || y >= NUMBER_ROWS) {
        return false;
    }
    return grid[x][y] == type;
}

function collapseDown(grid) {
    for (var x = 0; x < NUMBER_COLS; x++) {
        var numberEmptySpaces = 0;
        for (var y = 0; y < NUMBER_ROWS; y++) {
            if (grid[x][y] == "blank") {
                numberEmptySpaces = numberEmptySpaces + 1;
            } else if (numberEmptySpaces > 0) {
                grid[x][y - numberEmptySpaces] = grid[x][y];
                setSlotType(x, y - numberEmptySpaces, grid[x][y]);
                grid[x][y] = "blank";
                setSlotType(x, y, "blank");
            }
        }
    }
}

function collapseAcross(grid) {
    var blankColumns = 0;
    for (var x = 0; x < NUMBER_COLS; x++) {
        var isBlank = true;
        for (var y = 0; y < NUMBER_ROWS; y++) {
            if (grid[x][y] != "blank") {
                isBlank = false;
            }
            var gridStatus = grid[x][y];
            grid[x][y] = "blank";
            setSlotType(x, y, "blank");
            grid[x - blankColumns][y] = gridStatus;
            setSlotType(x - blankColumns, y, gridStatus);
        }
        if (isBlank == true) {
            blankColumns = blankColumns + 1;
        }
    }
}