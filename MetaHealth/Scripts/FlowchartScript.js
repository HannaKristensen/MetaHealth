var flowchartMap={
    "start": {
        "text": "Where would you like to begin?",
        "outcomes": {
            "Mind": "Mind",
            "Body": "Body",
            "Soul": "Soul"
        }
    },
    // region Mind
    "Mind": {
        "text": "Of these three feelings, which sounds the closest to how you feel?",
        "outcomes": {
            "I'm pretty bored": "Bored",
            "I've been feeling overwhelmed": "Overwhelmed",
            "I have been experiencing some anxiety lately": "Anxious"
        }
    },
    "Bored": {
        "text": "What would you say the biggest cause of your boredom is?",
        "outcomes": {
            "I'm feeling restless": "Restless",
            "I'm feeling unmotivated": "Unmotivated"
        }
    },
    "Restless": {
        "text": "Are you trying to burn off energy or remain still and calm?",
        "outcomes": {
            "I need to settle myself": "Settle",
            "I need to burn off some energy": "Excess Energy"
        }
    },
    "Settle": {
        "text": "Looks like the best thing here would be some relaxation techniques."+" "+
            "A good place to start would be some yoga poses."+" "+
            "Click <a href='https://www.youtube.com/watch?v=BiWDsfZ3zbo'><em style='color:blue'>here</a></em>"+" "+
            "to be taken to a routine"
    },
    "Excess Energy": {
        "text": "Sometimes when we're feeling bored in the brain it helps do something physical. Take a walk and make it a point to "+
            "to take pictures of 5 things that catch your eye while you're out. Combining creativity with a physical activity engages "+
            "multiple parts of your brain and helps you stay engaged with the world around you."
    },
    "Unmotivated": {
        "text": "Are you feeling like you unmotivated intellectually or in a more external way?",
        "outcomes": {
            "I feel stagnant on an intellectual level": "Intellectual",
            "I just feel unmotived in my life in general": "Generally Unmotivated"
        }
    },
    "Intellectual": {
        "text": "It might be time to learn something new. Is there something you've always wondered but haven't looked up? "+
            "Start on a wikipedia page of something that interests you and start following links."
    },
    "Generally Unmotivated": {
        "text": "How energetic are you feeling?",
        "outcomes": {
            "Not energetic at all": "No Energy",
            "I have a little bit of getup and go in me": "Some Energy",
        }
    },
    "No Energy": {
        "text": "That's okay! Sometimes it helps to make a plan knowing that we have no obligation to put in place just yet. "+
            "Make a todo list of things you want to do and put it in an order of priority. Break bigger tasks into smaller ones. "+
            "Whenever you get some energy you'll know exactly where to start."
    },
    "Some Energy": {
        "text": "Pick an area of your home that's in need of some tidying/reorganization and get started. It can be something small "+
                "like a cluttered tupperware drawer, or something bigger like a messy closet."
        },
    "Overwhelmed": {
        "text": "Feeling overwhelmed/anxious is totally normal and recognizing when you need a break is a great skill to have! "+
            "Are you looking for a distraction or some introspection?",
        "outcomes": {
            "I would like a distraction": "Distraction",
            "I think I need to look inward": "Introspection",
        }
    },
    "Distraction": {
        "text": "Shutting off your brain for a moment is always nice. Set a timer for 5 minutes and click "+
            "<a href='http://interactive.usc.edu/projects/cloud/flowing/'target='_blank'><em style='color:blue'>here</em></a> to play a relaxing game. "+
            "Be sure you have your flash player enabled."
    },
    "Introspection": {
        "text": "Being able to look inside for why you may be feeling overwhelmed/anxious is very brave! In any order you feel like, "+
            "write down the 'who, what, where, when, and why' of your feelings of being overwhelemd and try to be as kind and "+
            "objective as possible."
    },
    "Anxious": {
        "text": "Anxiety is not a fun feeling. If you had to choose, would you say you're anxiety is coming from external forces or internal forces?",
        "outcomes": {
            "Internal": "Internal",
            "External":"External"
        }
    },
        "Internal": {
            "text": "General anxiety about your abilities is common. See if you can list three times that you've done something "+
                "that proves this fear wrong. If you're having a hard time coming up with something like that, list three things you "+
                "know you're really good at. Focusing on our positive qualities helps us face negative feelings in a more productiv way."
        },
        "External": {
            "text": "Can anything be currently done about what is making you anxious?",
            "outcomes": {
                "Yes":"AnxiousYes",
                "No":"AnxiousNo"
            }
        },
            "AnxiousYes": {
                "text": "It sounds like you have this taken care of and are feeling concerned.",
                "outcomes": {
                    "I do but I'm still anxious":"Overwhelmed"
                }
            },
            "AnxiousNo": {
                "text": "Oftentimes when we're experiencing stress from a situation that we have no control over, it's the lack of control itself "+
                    "that can make it seem so overwhelming. All you have agency over is your own thoughts and feelings. Meditating on how you feel "+
                    "and allowing yourself to sit in your discomfort without taking action sounds counterintuitive but may be the best course of action "+
                    "at this time. Perhaps think of a time you've faced similar feelings of a lack of control and consider how that situation turned out. "+
                    "Chances are if you're sitting here and going through this flowchart, you're doing okay."
            },
    

}           

//uses the length of the array and a random value between 0 and 1 to choose a color
function generateColor(array)
{
    return (array[Math.floor(Math.random() * array.length)]);
}

//Function to restart flowchart when needed
function startScript()
{
    var situation = "start";
    runFlowchart(situation);
}

//Takes in string "arg" that indicates the starting point in the map
function runFlowchart(arg) {
    var div = document.getElementById("startFlowchart");//entry point for function
    if (div.style.display != "none") //checks if start button is clicked, if not then hide it
    {
        div.style.display = "none";
    }
    //initializing necessary variables
    var colorArr = [];
    var map = flowchartMap[arg];
    var choices = map.outcomes;
    var prompt = ('<div class="textFlowchart">' + map.text + '</div>');
    $("#chartText").html(prompt);
    $("#buttons").empty();
    for (var choice in choices)
    {
        if (choices.hasOwnProperty(choice))
        {
            var $button = $('<input type="button" button class="btn btn-default" value="' + choice + '"/>');
            //              orange   dk orange   yellow      blue     dark blue    green
            var colors = ['#EE8535', '#ED5441', '#F0C032', '#49C7CA', '#248D83', '#6C984B']
            var randomColor = generateColor(colors);
            while (colorArr.includes(randomColor))
            {
                randomColor = generateColor(colors);
            }
            colorArr.push(randomColor);
            $button.css("background-color", randomColor);
            $button.css("border", "1px solid " + randomColor);
            $button.click({ "nextsit": choices[choice] }, function (evt) {
                situation = evt.data.nextsit;
                colorArr.length = 0;//empty out array after buttons are added
                runFlowchart(situation);
            });
            $("#buttons").append($button);
        }
    }
}