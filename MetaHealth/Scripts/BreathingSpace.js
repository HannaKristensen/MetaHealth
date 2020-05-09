function heart() {

    $("#heartGif").fadeIn("slow")
    document.getElementById("meditationRow").style.display = 'block';
    document.getElementById("breatheText").style.visibility = 'visible';
    document.getElementById("starGif").style.display = 'none';
    document.getElementById("triGif").style.display = 'none';
    document.getElementById("circleGif").style.display = 'none';

}

function star() {

    $("#starGif").fadeIn("slow")
    document.getElementById("meditationRow").style.display = 'block';
    document.getElementById("breatheText").style.visibility = 'visible';
    document.getElementById("heartGif").style.display = 'none';
    document.getElementById("triGif").style.display = 'none';
    document.getElementById("circleGif").style.display = 'none';

}

function circle() {

    $("#circleGif").fadeIn("slow")
    document.getElementById("meditationRow").style.display = 'block';
    document.getElementById("breatheText").style.visibility = 'visible';
    document.getElementById("starGif").style.display = 'none';
    document.getElementById("triGif").style.display = 'none';
    document.getElementById("heartGif").style.display = 'none';

}

function triangle() {

    $("#triGif").fadeIn("slow")
    document.getElementById("meditationRow").style.display = 'block';
    document.getElementById("breatheText").style.visibility = 'visible';
    document.getElementById("starGif").style.display = 'none';
    document.getElementById("heartGif").style.display = 'none';
    document.getElementById("circleGif").style.display = 'none';
}

