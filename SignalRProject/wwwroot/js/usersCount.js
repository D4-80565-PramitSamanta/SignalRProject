// by now signalR has been installed and added in index.html
// now create connection
var connectionToUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/usercount").build();

//rcv notificaton from hub
//define methods here which are called from hub 
connectionToUserCount.on("UpdateTotalViews", (val) => {
    var newCountSpan = document.getElementById("totalViewsCount");
    newCountSpan.innerText = val.toString();
})

connectionToUserCount.on("UpdateTotalUsers", (val) => {
    var newCountSpan = document.getElementById("totalUsersCount");
    newCountSpan.innerText = val.toString();
})

// if something happen here invoke the hub method 
function newWindowLoadedOnClinet() {
    connectionToUserCount.send("NewWindowLoaded");
}

function fullfilled() {
    console.log("hub is up and running");
    newWindowLoadedOnClinet();
}

function rejected() {
    console.log("hub is down")
}

connectionToUserCount.start().then(fullfilled, rejected);