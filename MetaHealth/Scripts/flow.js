
var flowchartMap = {
    "start": {
        "text": "Where would you like to begin?",
        "outcomes": {
            "Mind": "Mind", "Body": "Body", "Spirit": "Spirit"
        }
    },
    "Mind": {
        "text": "Of these three activities which sounds the best for you?",
        "outcomes": {
            "Write": "Write", "Something creative": "Creative", "I need to do nothing": "None"
        }
    },
    "Body": {
        "text": "You walk into the cave.",
        "outcomes": {}
    },
    "Spirit": {
        "text": "You walk away from the cave, to search for food. You find berries. Do you eat them or not?",
        "outcomes": {}
    },

    "Write": {
        "text": "Write 5 things you like about yourself."
    },

    "choiceCreative": {
        "text": "Draw a picture of something you loved to eat as a child."
    },

    "choiceNone": {
        "text": "Do you want to sit in silence or play some music?",
        "outcomes": {
            "Sit in silence": "choiceSilence", "play some music": "choiceMusic"
        }
    },

    "choiceSilence": {
        "text": "Meditate for 5 minutes on a problem you've solved in the past."
    },

    "coiceMusic": {
        "text": "Go here and close your eyes and think happy thoughts-->https://www.youtube.com/watch?v=wb_E3HnLwG4"
    }
}

var situation = "start";

function testClick() {
    $('#testClass').text("this worked");
}

function runFlowchart() {
    var map = flowchartMap[situation];
    var choices = map.outcomes;
    var text = map.text;
    $("#chartText").text(text);


    $("#buttons").empty();

    for (var choice in choices) {
        if (choices.hasOwnProperty(choice)) {
            var $button = $('<input type="button" value="' + choice + '"/>');
            $button.click({ "nextsit": choices[choice] }, function (evt) {
                situation = evt.nextsit;
                runFlowchart();
            });
            $("#buttons").append($button);
        }
    }
}