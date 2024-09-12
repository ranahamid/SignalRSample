

var newclockSpan = document.getElementById("cloakCounter");
var newstoneSpan = document.getElementById("stoneCounter");
var newwandSpan = document.getElementById("wandCounter");

//create connectin 
var connectionDeathlyHallows = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/deathlyhallows", signalR.HttpTransportType.WebSockets).build();
//connect to methods that hub invokes aka receive
connectionDeathlyHallows.on("updateDeathlyHallaowCount", (clock, stone, wand) =>
{
    newclockSpan.innerText = clock.toString();    
    newstoneSpan.innerText = stone.toString();
    newwandSpan.innerText = wand.toString(); 
})
 
//start connection
function fulfilled() {
    console.log("success");
    connectionDeathlyHallows.invoke("GetRaceStatus").then((raceCounter) => {
          
        newclockSpan.innerText = raceCounter.cloak.toString();
        newstoneSpan.innerText = raceCounter.stone.toString();
        newwandSpan.innerText = raceCounter. wand.toString(); 

    }); 
}
function rejected() {
    console.log("error");
}

connectionDeathlyHallows.start().then(fulfilled, rejected);