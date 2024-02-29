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

function onSuccess(response) {
    var table = $("#DataTable").DataTable({
        bLengthChange: false,
        lengthMenu: [[3, 6, -1], [3, 6, "All"]],
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
                    return '<img src="/uploads/' + row.image + '" class="rounded-circle profileImage" height="50px" width="50px" />'
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
                    return '<a href="/employee/upsert/' + row.id + '"><i class="fa-solid fa-pen-to-square mx-5" style="color:#e2bd03;"></i></a>' + " " + '<a onclick="deleteSweetAlert(\'/employee/delete/' + row.id + '\')"><i class="fa-solid fa-trash" style="color: #e00b0b;"></i ></a>'
                }
            },
        ]
    });
        $('#DataTable tbody').on('click', '.btnExpand', function () {
            var tr = $(this).closest('tr');
            var row = table.row(tr);

            if (row.child.isShown()) {
                // This row is already open - close it
                row.child.hide();
                tr.removeClass('shown');
            } else {
                // Open this row
                row.child(format(row.data())).show();
                tr.addClass('shown');
            }
        });
}

function format(data) {
    return '<div>' + 
        '<strong>DOB</strong>: ' + data.dob + '<br>' +
        '<strong>JoiningDate</strong>: ' + data.joiningDate + '<br>' +
        '<strong>Description</strong>: ' + data.description + '<br>' +
        '<strong>SkillName</strong>: ' + data.skillName +
        '</div>';
}
