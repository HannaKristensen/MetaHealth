function check() {

    var q1 = document.quiz.q1.value;
    var q2 = document.quiz.q2.value;
    var q3 = document.quiz.q3.value;
    var q4 = document.quiz.q4.value;
    var q5 = document.quiz.q5.value;
    var q6 = document.quiz.q6.value;
    var q7 = document.quiz.q7.value;
    var q8 = document.quiz.q8.value;
    var q9 = document.quiz.q9.value;
    var q10 = document.quiz.q10.value;
    var q11 = document.quiz.q11.value;
    var q12 = document.quiz.q12.value;
    var q13 = document.quiz.q13.value;

    //these are counters, the idea is to collect the number of answers for each question
    //and then make more decisions from there
    //i.e if the user answers a majority of 1's , we will give them the result for 1's
    var numOf1 = 0;
    var numOf2 = 0;
    var numOf3 = 0;
    var numOf4 = 0;

    if (q1 == 0 || q2 == 0 || q3 == 0 || q4 == 0 || q5 == 0 || q6 == 0 || q7 == 0 || q8 == 0 || q9 == 0 || q10 == 0 || q11 == 0 || q12 == 0 || q13 == 0) {
        document.getElementById("errorText").style.display = 'block';
    }

    else {

        //question 1 counter 
        if (q1 == 1) {
            numOf1++;
        }

        if (q1 == 2) {
            numOf2++;
        }

        if (q1 == 3) {
            numOf3++;
        }

        if (q1 == 4) {
            numOf4++;
        }


        //question 2 counter
        if (q2 == 1) {
            numOf1++;
        }

        if (q2 == 2) {
            numOf2++;
        }

        if (q2 == 3) {
            numOf3++;
        }

        if (q2 == 4) {
            numOf4++;
        }

        //question 3 counter
        if (q3 == 1) {
            numOf1++;
        }

        if (q3 == 2) {
            numOf2++;
        }

        if (q3 == 3) {
            numOf3++;
        }

        if (q3 == 4) {
            numOf4++;
        }

        //question 4 counter
        if (q4 == 1) {
            numOf1++;
        }

        if (q4 == 2) {
            numOf2++;
        }

        if (q4 == 3) {
            numOf3++;
        }

        if (q4 == 4) {
            numOf4++;
        }

        //question 5 counter
        if (q5 == 1) {
            numOf1++;
        }

        if (q5 == 2) {
            numOf2++;
        }

        if (q5 == 3) {
            numOf3++;
        }

        if (q5 == 4) {
            numOf4++;
        }

        //question 6 counter
        if (q6 == 1) {
            numOf1++;
        }

        if (q6 == 2) {
            numOf2++;
        }

        if (q6 == 3) {
            numOf3++;
        }

        if (q6 == 4) {
            numOf4++;
        }


        //question 7 counter
        if (q7 == 1) {
            numOf1++;
        }

        if (q7 == 2) {
            numOf2++;
        }

        if (q7 == 3) {
            numOf3++;
        }

        if (q7 == 4) {
            numOf4++;
        }

        //question 8 counter
        if (q8 == 1) {
            numOf1++;
        }

        if (q8 == 2) {
            numOf2++;
        }

        if (q8 == 3) {
            numOf3++;
        }

        if (q8 == 4) {
            numOf4++;
        }

        //question 9 counter
        if (q9 == 1) {
            numOf1++;
        }

        if (q9 == 2) {
            numOf2++;
        }

        if (q9 == 3) {
            numOf3++;
        }

        if (q9 == 4) {
            numOf4++;
        }

        //question 10 counter
        if (q10 == 1) {
            numOf1++;
        }

        if (q10 == 2) {
            numOf2++;
        }

        if (q10 == 3) {
            numOf3++;
        }

        if (q10 == 4) {
            numOf4++;
        }

        //question 11 counter
        if (q11 == 1) {
            numOf1++;
        }

        if (q11 == 2) {
            numOf2++;
        }

        if (q11 == 3) {
            numOf3++;
        }

        if (q11 == 4) {
            numOf4++;
        }

        //question 12 counter
        if (q12 == 1) {
            numOf1++;
        }

        if (q12 == 2) {
            numOf2++;
        }

        if (q12 == 3) {
            numOf3++;
        }

        if (q12 == 4) {
            numOf4++;
        }

        //question 13 counter
        if (q13 == 1) {
            numOf1++;
        }

        if (q13 == 2) {
            numOf2++;
        }

        if (q13 == 3) {
            numOf3++;
        }

        if (q13 == 4) {
            numOf4++;
        }



        //now we decide on which result to give the user
        //RESULT 1
        if (numOf1 >= numOf2 && numOf1 >= numOf3 && numOf1 >= numOf4) {
            //these are cases in which we have the same highest number of answers
            //these if statements will also take care of cases where there are 3 highest number
            //of answers.
            //ive just randolmy assigned these to pick one or the other in the case of an equal situation 
            if (numOf1 == numOf2) {
                document.getElementById("result1").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            else if (numOf1 == numOf3) {
                document.getElementById("result3").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            else if (numOf1 == numOf4) {
                document.getElementById("result4").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            //this is our best case scenario where over 50% of the answers are consistentley one answer 
            else {

                document.getElementById("result1").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';

            }
        }


        //RESULT 2
        if (numOf2 >= numOf1 && numOf2 >= numOf3 && numOf2 >= numOf4) {

            if (numOf2 == numOf1) {
                document.getElementById("result1").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            else if (numOf2 == numOf3) {
                document.getElementById("result3").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            else if (numOf2 == numOf4) {
                document.getElementById("result4").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            else {

                document.getElementById("result2").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';

            }
        }

        //RESULT 3 
        if (numOf3 >= numOf2 && numOf3 >= numOf1 && numOf3 >= numOf4) {

            if (numOf3 == numOf2) {
                document.getElementById("result2").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            else if (numOf3 == numOf1) {
                document.getElementById("result1").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            else if (numOf3 == numOf4) {
                document.getElementById("result4").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            else {

                document.getElementById("result3").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';

            }
        }

        //RESULT 4
        if (numOf4 >= numOf2 && numOf4 >= numOf3 && numOf4 >= numOf1) {

            if (numOf4 == numOf2) {
                document.getElementById("result2").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            else if (numOf4 == numOf3) {
                document.getElementById("result3").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            else if (numOf4 == numOf1) {
                document.getElementById("result4").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';
            }

            else {

                document.getElementById("result4").style.display = 'block';
                document.getElementById("resultTitleText").style.display = 'block';
                document.getElementById("errorText").style.display = 'none';

            }
        }

    }

    

}