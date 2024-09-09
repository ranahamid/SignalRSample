//create connectin 
var connectionUserCount = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
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

connectionUserCount.start().then(fulfilled, rejected);