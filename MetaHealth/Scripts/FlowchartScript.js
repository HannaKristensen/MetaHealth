$(document).ready(function() {
    var form=document.getElementById("addTaskForm");
    form.style.display="none";
});


var flowchartMap={
    "start": {
        "text": "Sometimes when we're feeling overwhelmed it can be tough to figure out what it is we need. "+ 
            "With this tool, we can help you narrow down what your priority should be in this moment. When you're done, "+
            "you can use your suggestion to add a task to your list (if you're logged in with us). Where would you like to begin?",
        "outcomes": {
            "Mind": "Mind",
            "Body": "Body",
            "Soul": "Soul"
        }
    },
//#####################################################################################
//MIND
//######################################################################################
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
                    "I need to settle myself": "SettleSelf",
                    "I need to burn off some energy": "ExcessEnergy"
                }
            },
            "SettleSelf": {
                "text": "Looks like the best thing here would be some relaxation techniques."+" "+
                    "A good place to start would be some yoga poses."+" "+
                    "Click <a href='https://www.youtube.com/watch?v=BiWDsfZ3zbo'><em style='color:blue'>here</a></em>"+" "+
                    "to be taken to a routine",
            },
            "ExcessEnergy": {
                "text": "Sometimes when we're feeling stagnant it helps to do something physical. Take a walk and make it a point to "+
                    "to take pictures of 5 things that catch your eye while you're out. Combining creativity with a physical activity engages "+
                    "multiple parts of your brain and helps you stay connected to the world around you."
            },
        "Unmotivated": {
            "text": "Are you feeling like you're unmotivated intellectually or in a more external way?",
            "outcomes": {
                "I feel stagnant on an intellectual level": "Intellectual",
                "I just feel unmotived in my life in general": "GenerallyUnmotivated"
            }
        },
            "Intellectual": {
                "text": "It might be time to learn something new. Is there something you've always wondered but haven't looked up? "+
                    "Start on a wikipedia page of something that interests you and start following links."
            },
            "GenerallyUnmotivated": {
                "text": "How energetic are you feeling?",
                "outcomes": {
                    "Not energetic at all": "NoEnergy",
                    "I have a little bit of getup and go in me": "SomeEnergy",
                }
            },
            "NoEnergy": {
                "text": "That's okay! Sometimes it helps to make a plan knowing that we have no obligation to put in place just yet. "+
                    "Make a todo list of things you want to do and put it in an order of priority. Break bigger tasks into smaller ones. "+
                    "Whenever you get some energy you'll know exactly where to start."
            },
            "SomeEnergy": {
                "text": "Pick an area of your home that's in need of some tidying/reorganization and get started. It can be something small, "+
                        "like a cluttered tupperware drawer, or something bigger, like a messy closet."
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
                "text": "Being able to look inside for why you may be feeling overwhelmed is very brave! In any order you feel like, "+
                    "write down the 'who, what, where, when, and why' of your feelings of being overwhelemd and try to be as kind and "+
                    "objective as possible."
            },
        "Anxious": {
            "text": "Anxiety is not a fun feeling. If you had to choose, would you say your anxiety is coming from external forces or internal forces?",
            "outcomes": {
                "Internal": "Internal",
                "External":"External"
            }
        },
            "Internal": {
                "text": "General anxiety about your abilities is common. See if you can list three times that you've done something "+
                    "that proves this fear wrong. If you're having a hard time coming up with something like that, list three things you "+
                    "know you're really good at. Focusing on our positive qualities helps us face negative feelings in a more productive way."
            },
            "External": {
                "text": "Can anything be currently done about what is making you anxious?",
                "outcomes": {
                    "Yes":"YesAnxious",
                    "No":"NoAnxious"
                }
            },
                "YesAnxious": {
                    "text": "It sounds like you have this taken care of but you are still feeling concerned.",
                    "outcomes": {
                        "It's making me really anxious":"Anxious"
                    }
                },
                "NoAnxious": {
                    "text": "Oftentimes when we're experiencing stress from a situation that we have no control over, it's the lack of control itself "+
                        "that can make it seem so overwhelming. All you have agency over is your own thoughts and feelings. Meditating on how you feel "+
                        "and allowing yourself to sit in your discomfort without taking action sounds counterintuitive but may be the best course of action "+
                        "at this time. Perhaps think of a time you've faced similar feelings of a lack of control and consider how that situation turned out. "+
                        "Chances are if you're sitting here and going through this flowchart, you're doing okay."
    },
//#####################################################################################
//BODY
//#####################################################################################
    "Body": {
        "text":"First thing's first. Have you eaten recently?",
        "outcomes": {
            "No I haven't":"NoFood",
            "Yes I've eaten":"YesFood"
        }
    },
        "NoFood": {
            "text": "Is this because you're feeling anxious?",
            "outcomes": {
                "Yes. It's hard to eat when I'm stressed.": "Anxious",
                "No. I just haven't had time today":"Physiological"
            }
        },
        "Physiological": {
            "text": "As counterintuive as it is, sometimes we deny ourselves what we truly need in order to help us solve problems. "+
                "Back in caveman days temporary neglect of basic needs meant that we were ready to flee from danger. Chances are a sabertooth tiger is "+
                "not about to attack so you need to address your immediate physiological needs before being able to move foward."
        },
        "YesFood": {
            "text": "Good! Ideally it was a healthy snack but the fact that you took the time to fuel up is great! What's the last thing you drank?",
            "outcomes": {
                "Water":"YesWater",
                "Not water":"NoWater"
            }
    },
            "NoWater": {
                "text": "Before we go any further, it might be nice to at least have a glass near by.",
                "outcomes": {
                    "Okay.":"YesWater"
                }
            },
            "YesWater": {
                "text": "Great! Now that our basic needs are met, which of these feels most relevant to you?",
                "outcomes": {
                    "I feel uncomfortable in my own skin":"Uncomfortable",
                    "I feel stiff and/or sedentary":"Sedentary"
                }
            },

                "Uncomfortable": {
                    "text": "What do you think is causing this?",
                    "outcomes": {
                        "I'm not feeling great about my physical apperance": "Appearance",
                        "I'm feeling constantly worn down and tired":"Exhausted"
                    }
                },
                    "Appearance": {
                        "text": "As easy as it would be to tell you that you are beautiful inside and out and should embrace your body and looks "+
                            "this can be a bit condescending to some. While it may be true that you are physically a work of art, if you don't feel "+
                            "that way then that should be what should be addressed. Pick a handful of 'safe' outfits that are clean and comforable. "+
                            "These should be things that are easy to put on that make you feel safe in your own body. Brushing your teeth is an easy way "+
                            "to be proactive towards looking and feeling your best."
                    },
    "Exhausted": {
        "text": "Have you slept recently?",
        "outcomes": {
            "Yeah I slept.":"YesSleep",
            "Not really.":"NoSleep"
        }
    },
            "YesSleep": {
                "text": "Sometimes we don't realize how exhausting being a human can be. If you've been eating well and drinking water as well as "+
                    "getting regular sleep of at least 7 hours a night and still are feeling tired, the answer might lie in a number of areas. "+
                    "You might want to consider introducing vitamin D into your morning routine. Sometimes chronic fatigue can come from stress "+
                    "that we might not even know we're experiencing. It's our body's way of telling us to slow down."
            },
            "NoSleep": {
                "text": "Is it because you're feeling anxious?",
                "outcomes": {
                    "Yes": "Anxious",
                    "No":"Physiological"
                }
            },
        "Sedentary": {
            "text": "How are your energy levels at the moment?",
            "outcomes": {
                "I'm feeling alright": "NotStagnant",
                "I'm not feeling super energetic right now":"YesStagnant"
            }
        },
            "NotStagnant": {
                "text": "Depending on your level of mobility it might be a good idea to go for a walk or perhaps you could do some boxing, which "+
                    "can be done from a seated position as well. Physical health is incredibly important to your overall well-being. Get your "+
                    "blood pumping doing some cardio for at least 20 minutes and reap the benefits of less stress and better sleep."
            },
            "YesStagnant": {
                "text": "Take a nice hot bath or shower and appreciate the feeling of the water on your skin. It doesn't have to be anything more than "+
                        "sitting still and appreciating yourself for allowing you that moment of peace."
            },
//#####################################################################################
 //SOUL  
//#####################################################################################
    "Soul": {
        "text": "Which area would you like to focus on?",
        "outcomes": {
            "I'm feeling disconnected": "Disconnected",
            "I'm feeling unfullfilled/empty": "Empty"
        }
    },
        "Disconnected": {
            "text": "Have you spoken to a loved one recenetly?",
            "outcomes": {
                "I have, yes": "Misunderstood",
                "I have not":"SeekSupport"
            }
        },
            "Misunderstood": {
                "text": "It's good that you've been reaching out. But it seems like something else is going on. Do you feel like "+ 
                    "people aren't really understanding what you're going through?",
                "outcomes": {
                    "Yes": "SeekProfSupport",
                    "No":"ConnectToSelf"
                }
            },
                "SeekProfSupport": {
                    "text": "Sometimes even our loved ones don't have the tools necessary to help us cope. While it's good that you feel connected, "+
                        "it's not unusal to also need a professional. Seek out local counselor in your area. You can use our Therapy Finder tool to find "+
                        "a local counselor for your needs."
                },
                "ConnectToSelf": {
                    "text": "Sometimes when we feel disconnected even when we're with people it's because we're trying to rehydrate from a dried up lake "+
                        "(metaphorically speaking). Now might be a good time to take yourself out on a date and really appreciate being alone doing "+
                        "something you really love."
                },
        "SeekSupport": {
            "text": "Is there someone you feel safe talking to?",
            "outcomes": {
                "Yes I believe so": "MaybeSupport",
                "I'm not sure":"NoSupport"
            }
        },
            "MaybeSupport": {
                "text": "It's good that you have someone you feel you can talk to. Often times we might feel like we're burdening someone with our problems "+
                    "but asking someone if they have a moment to let you vent can take the pressure off asking someone to listen."
            },
            "NoSupport": {
                "text": "It's easy to feel like we have no one to talk to you. This may or may not be true.",
                "outcomes": {
                    "I can think of maybe one person": "YesConnectToPerson",
                    "No I think I'm honestly alone":"Alone"
                }
            },
                "YesConnectToPerson": {
                    "text": "Wonderful! You the makings of a support system. It's healthy to vent and humans are not meant to be alone. Give your loved ones "+
                        "a call and make a connection, however superficial it may feel."
                },
                "Alone": {
                    "text": " If you've really thought about the people you know and came up with no one to talk to, then it may be time "+
                        "to seek out resource groups in your area. You can use our Therapy Finder tool to find a professional counselor. "+
                        "In the meantime,giving someone else a positive interaction can do wonders for mood. "+
                        "Self care can be something as simple as being sure that you leave people in a better mood than when you found them."
                },
    "Empty": {
        "text": "How do you feel about your progress regarding your personal goals?",
        "outcomes": {
            "I feel generally pretty good": "GoalsAreGood",
            "I feel off track from where I would like to be":"GoalsAreBad"
        }
    },
            "GoalsAreGood": {
                "text": "That's great that you're feeling good about your goals! Perhaps the answer lies externally. Maybe now would be the "+
                    "time to donate your time and/or money to a local charity. Non profits are always needing help in some way or another. Check out "+
                    "local businesses that could benefit from you. It's good for the community and good for the soul."
            },
            "GoalsAreBad": {
                "text": "Often we can feel bad about ourselves because we feel like we're not keeping up with others. This often has more to do with "+
                    "internalized pressure than anything else. In times like these it's good to reflect on goals you've achieved in your life, starting "+
                    "ones you're most proud of. You might be surprised at how much it really is once you have it all written down."
            }
}//End of flowChartMap(...)          

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

function showForm() {
    var div=document.getElementById("addTaskForm");
    if(div.style.display!="none") {
        div.style.display="none";
    }
    else {
        div.style.display="inherit";
    }
}

function addTaskScript() {
    var $taskButton=$('<input type="button" button class=btn btn-default onclick="showForm()" value="Add to my task list"/>');
    $taskButton.css("padding","5px");
    $("#addToTaskList").append($taskButton);
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
    var prompt=('<div class="textFlowchart">'+map.text+'</div>');
    $("#chartText").html(prompt);
    $("#buttons").empty();
    $("#addToTaskList").empty();
    for (var choice in choices)
    {
        if(choices.hasOwnProperty(choice)) {
            var $button=$('<input type="button" button class="btn btn-default" value="'+choice+'"/>');
            //           orange   dk orange   yellow    blue     dark blue    green
            var colors=['#EE8535','#ED5441','#F0C032','#49C7CA','#248D83','#6C984B']
            var randomColor=generateColor(colors);
            while(colorArr.includes(randomColor)) {
                randomColor=generateColor(colors);
            }
            colorArr.push(randomColor);
            $button.css("background-color",randomColor);
            $button.css("border","1px solid "+randomColor);
            $button.click({ "nextsit": choices[choice] },function(evt) {
                situation=evt.data.nextsit;
                colorArr.length=0;//empty out array after buttons are added
                runFlowchart(situation);
            });
            $("#buttons").append($button);
        }  
    }
    if(choices==null) {
        addTaskScript();
    }
}
