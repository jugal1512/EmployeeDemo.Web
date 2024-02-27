$(document).ready(function () {
    GetEmployee();
});

function GetEmployee() {
    $.ajax({
        url: '/Employee/getEmployeeList',
        type: 'Get',
        dataType: 'json',
        success: onSuccess
    });
}

function onSuccess(response)
{
    $("#DataTable").DataTable({
        bLengthChange: false,
        lengthMenu:[[5,10,-1],[5,10,"All"]],
        searching: false,
        data: response,
        columns: [
            {
                data: 'Id',
                render: function (data, type, row, meta) {
                    return '<button class="btn btnExpand" type="button"><i class="fa-solid fa-circle-plus"></i></button>'
                }
            },
            {
                data: 'Image',
                render: function (data, type, row, meta) {
                    return '<img src="/uploads/' + row.image +'" class="rounded-circle profileImage" height="50px" width="50px" />'
                }
            },
            {
                data: 'FirstName',
                render: function (data, type, row, meta) {
                    return row.firstName + " " + row.lastName
                }
            },
            {
                data: 'Email',
                render: function (data, type, row, meta) {
                    return row.email
                }
            },
            {
                data: 'Gender',
                render: function (data, type, row, meta) {
                    return row.gender
                }
            },
            {
                data: 'Designation',
                render: function (data, type, row, meta) {
                    return row.designation
                }
            },
            {
                data: 'Id',
                render: function (data, type, row, meta) {
                    return '<a href="/employee/edit/' + row.id + '"><i class="fa-solid fa-pen-to-square mx-5" style="color:#e2bd03;"></i></a>' + " " + '<a onclick="deleteSweetAlert(\'/employee/delete/'+row.id+'\')"><i class="fa-solid fa-trash" style="color: #e00b0b;"></i ></a>'          
                } 
            },
        ]
    });
}
