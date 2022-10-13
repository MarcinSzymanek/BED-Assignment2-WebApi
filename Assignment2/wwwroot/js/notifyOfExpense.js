"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messageHub").build();

connection.on("NotifyMessage", function (expense, modelname, jobname) {
    console.log("CALLED RECEIVE MESSAGE!!!");
    console.log("PLEAAAAAAAASE!!!");
    var now = new Date();
    var timestamp = new Date(expense.date);
  
    var li = document.createElement("li");
    li.textContent = `${now.toLocaleTimeString()} - New expense logged: `;
    var listInList = document.createElement("ul");
    li.appendChild(listInList);
    var modelP = document.createElement("li");
    var customerP = document.createElement("li");
    var amountP = document.createElement("li");
    var commentP = document.createElement("li");
    var dateP = document.createElement("li");
    listInList.append(modelP);
    listInList.appendChild(customerP);
    listInList.appendChild(amountP);
    listInList.appendChild(commentP);
    listInList.appendChild(dateP);
    listInList.style.fontSize = "12px";
    modelP.textContent = `Model: ${modelname}`;
    customerP.textContent = `Customer: ${jobname}`;
    amountP.textContent = `Amount: ${expense.amount}`;
    commentP.textContent = `Comment: ${expense.text}`;
    dateP.textContent = `Date: ${timestamp.toLocaleDateString()}`
    
    document.getElementById("messagesList").appendChild(li);
    
});

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});
;

