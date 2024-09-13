 
//create connectin 
var connectionChat = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/chat", signalR.HttpTransportType.WebSockets).build();

document.getElementById("sendMessage").disabled = true;

//connect to methods that hub invokes aka receive
connectionChat.on("MessageReceived", (user, message) => {

    var li = document.createElement("li");
    li.textContent = `${user} says ${message}`;
    document.getElementById("messagesList").appendChild(li);
});


document.getElementById("sendMessage").addEventListener("click", function (event)
{
    var senderEmail = document.getElementById("senderEmail").value;
   // var receiverEmail = document.getElementById("receiverEmail").value;
    var chatMessage = document.getElementById("chatMessage").value; 

    connectionChat.invoke("SendMessageToAll", senderEmail, chatMessage).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();

});

//start connection
function fulfilled() {

    document.getElementById("sendMessage").disabled = false;
     
}
function rejected() {
    console.log("error");
}

connectionChat.start().then(fulfilled, rejected);