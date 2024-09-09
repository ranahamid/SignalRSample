//create connectin 
var connectionUserCount = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withAutomaticReconnect()
    .withUrl("/hubs/usercount", signalR.HttpTransportType.WebSockets).build();
//connect to methods that hub invokes aka receive
connectionUserCount.on("updateTotalViews", (value1/*, value2*/) => {

    var newCountSpan = document.getElementById("totalviewscounter");
    newCountSpan.innerText = value1.toString();

    //var newUserSpan = document.getElementById("totaluserscounter");
    //newUserSpan.innerText = value2.toString();
} )

connectionUserCount.on("updateTotalUsers", (/*value1,*/ value2) => {

    //var newCountSpan = document.getElementById("totalviewscounter");
    //newCountSpan.innerText = value1.toString();

    var newUserSpan = document.getElementById("totaluserscounter");
    newUserSpan.innerText = value2.toString();
})


//invoke hub methods aka send notification to hub
function newWindowLoadedOnClient() {

    //connectionUserCount.send("NewWindowLoaded");
    connectionUserCount.invoke("NewWindowLoaded","rana hamid").then((value) => 
        console.log("value is:" + value)
    );
}
//start connection
function fulfilled()
{
    console.log("success");
    newWindowLoadedOnClient();
}
function rejected()
{
    console.log("error");
}

connectionUserCount.onclose((error) =>
{
    document.body.style.background = "red";
    console.log("connection closed");
    console.log(error);
});

connectionUserCount.onreconnecting((error) =>
{
    document.body.style.background = "green";
    console.log("reconnecting");
    console.log(error);
});

connectionUserCount.onreconnected((connectionId) =>
{
    document.body.style.background = "yellow";
    console.log("reconnected");
    console.log(connectionId);

});



connectionUserCount.start().then(fulfilled, rejected);