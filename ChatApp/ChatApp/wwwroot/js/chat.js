"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").withAutomaticReconnect().build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", async function (event) {
    if (connection.state == "Disconnected") {
        await connection.start();
    }

    var user = $("#talkingTo").text();
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

$(".userOnline").click(function () {
    $("#talkingTo").text(this.text);

    $.get("/api/Message?userName=" + this.text, function (data) {
        $("#messagesList").empty();

        var i = 0;
        for (i = 0; i < data.length; i++) {
            var encodedMsg = data[i].sentFrom + " says " + data[i].text;
            var li = document.createElement("li");
            li.textContent = encodedMsg;
            document.getElementById("messagesList").appendChild(li);
        }
    });
});