/*************************************************
 * Array of self care suggestions
 * make the suggestion, then pick a random number and have that print 
 * each time the page is loaded
 * **********************************************/

var selfCareSuggestions=[
    "Make sure you're properly hydrated!",
    "Take the time to write in a journal.",
    "Do 5 sun salutations.",
    "Say or write down 3 things that you like about yourself",
    "Declutter one surface or shelf in your home.",
    "Devote some time to calling a loved one just to chat.",
    "Today should be the day you buy yourself a treat you don't normally allow yourself",
    "Donate your time/money to a cause you believe in.",
    "Spend a few minutes researching something you've always wondered about but never had the time to look into.",
    "List 5 accomplishments that you're proud of.",
    "Set a timer for 3 minutes and practice meditating.",
    "Put on something clean and comfortable and snuggle up with a blanket and a warm beverage.",
    "Say 'No' when you don't feel like doing something.",
    "Take an impromptu day trip to a place you love going."
]

//so this does log to the console but we're still not properly adding this string to the home page... sooo close
$("selfcare").ready(function() {
    var randNum=Math.floor(Math.random()*selfCareSuggestions.length);
    var string="<p id=\"sugg\">"+selfCareSuggestions[randNum]+"</p>";
    document.getElementById("suggestion").innerHTML=string;
});



