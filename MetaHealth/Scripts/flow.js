
var flowchartMap = {
    "start": {
        "text": "Where would you like to begin?",
        "outcomes": {
            "Mind": "choiceMind", "Body": "choiceBody", "Spirit": "choiceSpirit"
        }
    },
    "choiceMind": {
        "text": "Of these three activities which sounds the best for you?",
        "outcomes": {
            "Write": "choiceWrite", "Something creative": "choiceCreative", "I need to do nothing": "choiceNone"
        }
    },
    "choiceBody": {
        "text": "You walk into the cave.",
        "outcomes": {}
    },
    "choiceSpirit": {
        "text": "You walk away from the cave, to search for food. You find berries. Do you eat them or not?",
        "outcomes": {}
    },

    "choiceWrite": {
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
function runFlowchart() {
    var situation = flowchartMap[situation];
    var choices = flowchartMap.outcomes;
    var text = flowchartMap.text;
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

runFlowchart();