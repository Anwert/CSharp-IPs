$(document).ready(() => { 
    let t = $('#tblIP').DataTable( {
        "sAjaxSource": "IP/GetIPList",
        "columns": [
            { "data": "id" },
            { "data": "subnet" },
            { 
                "data": "actions",
                "defaultContent": '<div><button id ="del">Delete</button> | <button id="update">Update</button></div>'
            }
        ],
        "fnServerData": (sSource, aoData, fnCallback) => {
            $.ajax( {
                "dataType": 'json', 
                "type": "GET", 
                "url": sSource, 
                "success": (res) => {
                    if (res.error) {
                        alert(res.message);
                        t.clear().draw();
                    } else {
                        t.rows.add(res.data);
                        t.draw(); 
                    }
                },
                "failure": (res) => {
                    alert(`Error while getting data from server, response: ${res}`);
                    t.clear().draw();
                }
            });
        }
    });
    

    $('#create').on('click', () => {
        let subnet = $("#createText").val();
        $.ajax({
            type: "POST",
            url: "/IP/CreateIP",
            data: {subnet},
            dataType: "json",
            success: (res) => {
                if (res.error) {
                    alert(res.message);
                } else {
                    if (res.ip) {
                        $("#createText").val("");
                        let row = {
                            id: res.ip.id,
                            subnet: res.ip.subnet,
                        }
                        t.row.add(row);
                        t.draw();
                    } else {
                        alert(`Response from Server is invalid, response: ${res.message}`);
                    }
                }
            },
            error: (d) => {
                console.log(d);
                alert("Error while posting data to server");
            }
        })
    })

    $('#tblIP tbody').on('click', '#del', function () {
        let id = t.row($(this).parents('tr')).data().id;
        $.ajax({
            type: "DELETE",
            url: "/IP/DeleteIP",
            data: {id},
            dataType: "json",
            success: (res) => {
                if (res.error) {
                    alert(res.message);
                } else {
                    t.row($(this).parents('tr')).remove().draw();
                }
            },
            error: (d) => {
                console.log(d);
                alert("Error while deleting");
            }
        })
    });

    $('#tblIP tbody').on('click', '#update', function () {
        let id = t.row($(this).parents('tr')).data().id;
        window.location.href = `/IP/Update/${id}`;
    });

    $('#groupIPs').on('click', () => {
        $.ajax({
            type: "GET",
            url: "/IP/GroupIPs",
            dataType: "json", 
            success: (res) => {
                if (res.error) {
                    alert(res.message);
                    $("#groupedIPsTableContainer").empty();
                } else {
                    console.log(res.data);
                    $("#groupedIPsTableContainer").empty();
                    createGroupedIPsTable(res.data);
                }
            },
            error: (d) => {
                console.log(d);
                alert("Error while getting data from server");
            }
        })
    })

    let createGroupedIPsTable = (data) => {
        $('#groupedIPsTableContainer').append(`
            <table id="groupedIPsTable">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Subnet</th>
                        <th>Inner subnets</th>
                    </tr>
                </thead>
                <tbody>
                    ${createGroupedIPsTableBody(data)}
                <tbody>
            </table>
        `);
        let table = $('#groupedIPsTable').DataTable();
        table.draw();
    };

    let createGroupedIPsTableBody = (data) => {
        let ret = "";
        data.forEach(el => {
            ret += `
                <tr>
                    ${createIpsList(el.iPsList)}
                    <td>
                        ${createInnerTable(el.innerIPsList)}
                    </td>
                </tr>
            `
        });
        return ret;
    }

    let createIpsList = (iPsList) => {
        ret = '<td>';

        iPsList.forEach(el => {
            ret += `${el.id} <br/>`
        })

        ret += '</td><td>';

        iPsList.forEach(el => {
            ret += `${el.subnet} <br/>`
        });

        ret += '</td>';

        return ret;
    }

    let createInnerTable = (innerIPsList) => {
        if (!Array.isArray(innerIPsList) || !innerIPsList.length) return "There is no inner subnets :c"
        ret = `
            <table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Subnet</th>
                    </tr>
                </thead>
                <tbody>
        `;
        innerIPsList.forEach(el => {
            ret += `
                <tr>
                    <td>${el.id}</td>
                    <td>${el.subnet}</td>
                </tr>
            `
        });
        ret += `<tbody></table>`
        return ret;
    }
});