//import { event } from "jquery";

let lbl_houseJoined = document.getElementById("lbl_houseJoined");


let btn_un_gryffindor = document.getElementById("btn_un_gryffindor");
let btn_un_slytherin = document.getElementById("btn_un_slytherin");
let btn_un_hufflepuff = document.getElementById("btn_un_hufflepuff");
let btn_un_ravenclaw = document.getElementById("btn_un_ravenclaw");

let btn_gryffindor = document.getElementById("btn_gryffindor");
let btn_slytherin = document.getElementById("btn_slytherin");
let btn_hufflepuff = document.getElementById("btn_hufflepuff");
let btn_ravenclaw = document.getElementById("btn_ravenclaw");

let trigger_gryffindor = document.getElementById("trigger_gryffindor");
let trigger_slytherin = document.getElementById("trigger_slytherin");
let trigger_hufflepuff = document.getElementById("trigger_hufflepuff");
let trigger_ravenclaw = document.getElementById("trigger_ravenclaw");


//create connectin 
var connectionHouseGroup = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/houseGroupHub", signalR.HttpTransportType.WebSockets).build();

//connect to methods that hub invokes aka receive
btn_gryffindor.addEventListener("click", function ()
{
    event.preventDefault();
    connectionHouseGroup.invoke("JoinHouse", "Gryffindor");
});
btn_slytherin.addEventListener("click", function () {
    event.preventDefault();
    connectionHouseGroup.invoke("JoinHouse", "Slytherin");
});
btn_hufflepuff.addEventListener("click", function () {
    event.preventDefault();
    connectionHouseGroup.invoke("JoinHouse", "Hufflepuff");
});
btn_ravenclaw.addEventListener("click", function () {
    event.preventDefault();
    connectionHouseGroup.invoke("JoinHouse", "Ravenclaw");
});

//unsubscribe
btn_un_gryffindor.addEventListener("click", function () {
    event.preventDefault();
    connectionHouseGroup.invoke("LeaveHouse", "Gryffindor");
});
btn_un_slytherin.addEventListener("click", function () {
    event.preventDefault();
    connectionHouseGroup.invoke("LeaveHouse", "Slytherin");
});
btn_un_hufflepuff.addEventListener("click", function () {
    event.preventDefault();
    connectionHouseGroup.invoke("LeaveHouse", "Hufflepuff");
});
btn_un_ravenclaw.addEventListener("click", function () {
    event.preventDefault();
    connectionHouseGroup.invoke("LeaveHouse", "Ravenclaw");
});

//trigger
trigger_gryffindor.addEventListener("click", function () {
    event.preventDefault();
    connectionHouseGroup.invoke("TriggerNotifyHouse", "Gryffindor");
});
trigger_slytherin.addEventListener("click", function () {
    event.preventDefault();
    connectionHouseGroup.invoke("TriggerNotifyHouse", "Slytherin");
});
trigger_hufflepuff.addEventListener("click", function () {
    event.preventDefault();
    connectionHouseGroup.invoke("TriggerNotifyHouse", "Hufflepuff");
});
trigger_ravenclaw.addEventListener("click", function () {
    event.preventDefault();
    connectionHouseGroup.invoke("TriggerNotifyHouse", "Ravenclaw");
});


connectionHouseGroup.on("triggerNotification", (houseName) => {   
        toastr.info(`A new notification for ${houseName} has been launced.`); 
})

connectionHouseGroup.on("subscriptionstatusall", (houseName, hasSubscribed) => {
    if (hasSubscribed)
        toastr.info(`Someone Joined to ${houseName}`);
    else
        toastr.error(`Someone Leaved from ${houseName}`);
});

//on lbl_houseJoined
connectionHouseGroup.on("subscriptionStatus", (strGroupJoined, houseName, hasSubscribed) => {
    
    lbl_houseJoined.innerText = strGroupJoined;
    if (hasSubscribed)
    {
        //subscribe
        if (houseName == "Gryffindor")
        {
            btn_gryffindor.style.display = "none";
            btn_un_gryffindor.style.display = "";
        }
        else if (houseName == "Slytherin")
        {
            btn_slytherin.style.display = "none";
            btn_un_slytherin.style.display = "";
        }
        else if (houseName == "Hufflepuff") {
            btn_hufflepuff.style.display = "none";
            btn_un_hufflepuff.style.display = "";
        }
        else if (houseName == "Ravenclaw") {
            btn_ravenclaw.style.display = "none";
            btn_un_ravenclaw.style.display = "";
        }
        toastr.success(`You have subscribed successfully, ${houseName }`);
    }
    else
    {
        //unsubscribe
        if (houseName == "Gryffindor") {
            btn_gryffindor.style.display = "";
            btn_un_gryffindor.style.display = "none";
        }
        else if (houseName == "Slytherin") {
            btn_slytherin.style.display = "";
            btn_un_slytherin.style.display = "none";
        }
        else if (houseName == "Hufflepuff") {
            btn_hufflepuff.style.display = "";
            btn_un_hufflepuff.style.display = "none";
        }
        else if (houseName == "Ravenclaw") {
            btn_ravenclaw.style.display = "";
            btn_un_ravenclaw.style.display = "none";
        }
        toastr.warning(`You have unsubscribed successfully, ${houseName}`);
    }
});

//start connection
function fulfilled() {
    console.log("success");
 
}
function rejected() {
    console.log("error");
}

connectionHouseGroup.start().then(fulfilled, rejected);