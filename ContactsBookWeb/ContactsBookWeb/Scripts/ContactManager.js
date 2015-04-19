function SendIdToEdit(index) {
    $.ajax({
        url: "/Contacts/EditContact",
        type: "Get",
        data: { id: index },
        success: function (data) {
            $("#editContact").empty();
            $("#editContact").append(data);
        }
    });
}



function SendIdToDelete(index) {
    $.ajax({
        url: "/Contacts/DeleteContact",
        data: { id: index },
        success: function () {
            document.location.href = "/Home/Index";
            location.reload();
        }
    });
}

function SendContactDataToEdit() {
    $.ajax({
        type: "POST",
        url: "/Contacts/EditContact",
        data: JSON.stringify(
        {
            Id: $("#contactID").val().trim(),
            FirstName: $("#firstName").val().trim(),
            SecondName: $("#secondName").val().trim(),
            BirthYear: $("#birthYear").val().trim(),
            PhoneNumber: $("#phoneNumber").val().trim()
        }),
        success: function (data) {
            if (data.success) {
                window.location.href = "/Home/Index";
            } else {
                $("#editContact").empty();
                $("#editContact").append(data);
            }
        },
        contentType: "application/json"
    });
}

function SendContactDataToCreate() {
    $.ajax({
        type: "POST",
        url: "/Contacts/CreateContact",
        data: JSON.stringify(
        {
            FirstName: $("#firstName").val().trim(),
            SecondName: $("#secondName").val().trim(),
            BirthYear: $("#birthYear").val().trim(),
            PhoneNumber: $("#phoneNumber").val().trim()
        }),
        success: function (data) {
            if (data.success) {
                window.location.href = "/Home/Index";
                location.reload();
            } else {
                $("#editContact").empty();
                $("#editContact").append(data);
            }
        },
        contentType: "application/json"
    });
}

function InitializeCreateMenu() {
    $.ajax({
        url: "/Contacts/CreateContact",
        type: "Get",
        success: function(data) {
            $("#editContact").empty();
            $("#editContact").append(data);
        }
    });

}

function RedirectToSaveContacts() {
    window.location.href = "/Contacts/SaveContactsToXml";
}

function DownloadListOfContacts() {
    window.location.href = "/Contacts/DownloadListOfContacts";
}


function SortContacts(sortBy) {
    
    var searchResult = GetContactsList();
    searchResult.sort(Comparer(sortBy));
    RefreshTableBody(searchResult);
   
}
function GetContactsList() {
    var rows = $("tbody tr", $("#contactsTable")).map(function () {
            return [$("td", this).map(function () {
                return this.innerHTML;
            }).get()];
        }).get();
    var contactsList = new Array();
    var i;
    for (i = 1; i < rows.length; i++) {
        contactsList[i - 1] = {
            Id: rows[i][0],
            FirstName: rows[i][1],
            SecondName: rows[i][2],
            BirthYear: rows[i][3],
            PhoneNumber: rows[i][4]
        };
    }
    return contactsList;
}

function Comparer(field) {
    return function(obj1, obj2) {
        return obj1[field].toLowerCase() > obj2[field].toLowerCase();
    }
}

function RefreshTableBody(data) {
    $("#tableBody").empty();
    for (var i = 0; i < data.length; i++) {
        var row = $("<tr onclick='SendIdToEdit(" + data[i].Id + ")'/>");
        row.append(
            "<td class='hideTd'>" + data[i].Id + "</td>"+
            "<td>" + data[i].FirstName + "</td>" +
            "<td>" + data[i].SecondName + "</td>" +
            "<td>" + data[i].BirthYear + "</td>" +
            "<td>" + data[i].PhoneNumber + "</td>"
        );
        $("#tableBody").append(row);
    }
}

function Search() {
    var searchData = $("#inpSearch").val().trim().toLowerCase();
    if (searchData === "") {
        window.location.reload();
        return;
    }
    var contactsList = GetContactsList();
    var searchResult = new Array();
    var j = 0;
    for (var i = 0; i < contactsList.length; i++) {
        if (contactsList[i].FirstName.toLowerCase().indexOf(searchData) > -1 || contactsList[i].SecondName.toLowerCase().indexOf(searchData) > -1
                || contactsList[i].PhoneNumber.toLowerCase().indexOf(searchData) > -1) {
                searchResult[j] = contactsList[i];
                j++;
               }
    }
    RefreshTableBody(searchResult);
}