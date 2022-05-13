let timePassed = Number(sessionStorage.getItem('timePassed'));

if (timePassed === null) {
    sessionStorage.setItem('timePassed', 0);
}
//sessionStorage.setItem('timePassed', 0);
let timerInterval = null;

document.getElementById("base-timer-label").innerHTML = formatTime(timePassed);
function startTimer() {
    timerInterval = setInterval(() => {

        // Количество времени, которое прошло, увеличивается на  1
        timePassed = timePassed += 1;
        sessionStorage.setItem('timePassed', timePassed);
        document.getElementById("base-timer-label").innerHTML = formatTime(timePassed);
    }, 1000);
}

var ButtomTimer = document.getElementById("buttomTimer");
var Elepsed = document.getElementById("elapsed");
function buttomTimer() {
    if (Elepsed.classList.contains("base-timer__path-elapsed")) {
        Elepsed.classList.remove("base-timer__path-elapsed");
        //Elepsed.classList.add("inputStop");
        startTimer();
    }
    else {
        Elepsed.classList.add("base-timer__path-elapsed");
        //Elepsed.classList.remove("inputStop");
        clearInterval(timerInterval);
    }
    //if (!ButtomTimer.classList.contains("inputStop")) {
    //    ButtomTimer.classList.add("inputStop");
    //    ButtomTimer.value = "Stop";
    //    startTimer();

    //}
    //else {
    //    ButtomTimer.value = "Start";
    //    ButtomTimer.classList.remove("inputStop");
    //    clearInterval(timerInterval);
    //}
}
function Timer(projectId) {
    var goal = $('select[name=goalselect]').val();
    if (timePassed != 0) {
        sessionStorage.setItem('timePassed', 0);
        $.get("/Record/AddTimer", { time: timePassed, projectId: projectId, goal: goal }, function (result) {
            if (result) {
                location.reload();
                timePassed = 0;
            }
        })
        timePassed = 0;
    }
};

function formatTime(time) {

    let hours = Math.floor(time / 3600);

    let minutes = Math.floor(((time / 3600) - hours) * 60);

    let seconds = time % 60;

    if (seconds < 10) {
        seconds = `0${seconds} `;
    }
    if (minutes < 10) {
        minutes = `0${minutes} `;
    }

    return `${hours}: ${minutes}: ${seconds} `;
}

