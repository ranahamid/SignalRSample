


//create connectin 
var connectionNotification= new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/notification", signalR.HttpTransportType.WebSockets).build();


document.getElementById("sendButton").disabled = true;
document.getElementById("sendButton").addEventListener("click", function (event) {
    debugger
    var msg = document.getElementById("notificationInput").value;
   

    connectionNotification.send("SendMessage", msg);
    document.getElementById("notificationInput").value = "";
    event.preventDefault();
});

connectionNotification.start().then(function ()
{ 
    document.getElementById("sendButton").disabled = false;
    connectionNotification.invoke("LoadMessages");
});



//connect to methods that hub invokes aka receive
connectionNotification.on("LoadNotification", (messages, notificationCount) => {

    document.getElementById("notificationCounter").innerHTML = "<span>("+ notificationCount+")<span>";
    document.getElementById("messageList").innerHTML = "";
  
    for (let i = messages.length - 1; i >= 0; i--) {
        debugger
        var li = document.createElement("li");
        li.textContent = "Notitifaction- " + messages[i];
        document.getElementById("messageList").appendChild(li);
    }

})

//start connection
function fulfilled() {
    console.log("success"); 
}
function rejected() {
    console.log("error");
}

connectionNotification.start().then(fulfilled, rejected);