var dataTable;


//create connectin 
var connectionOrder = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/order", signalR.HttpTransportType.WebSockets).build();


//connect to methods that hub invokes aka receive
connectionOrder.on("newOrder", () => {

    dataTable.ajax.reload();
    toastr.success("New order received.");
})

//start connection
function fulfilled() {
    console.log("success");
}
function rejected() {
    console.log("error");
}

connectionOrder.start().then(fulfilled, rejected);


$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Home/GetAllOrder"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "15%" },
            { "data": "itemName", "width": "15%" },
            { "data": "count", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href=""
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> </a>
                      
					</div>
                        `
                },
                "width": "5%"
            }
        ]
    });
}
