var datatable;

$(document).ready(function () {

    loadDataTable();
    var id = document.getElementById("blogId");

    if (id.value > 0) {
        $('#myModal').modal('show');
    }
});

function limpiar() {
    var blogId = document.getElementById("blogId");
    var blogTitle = document.getElementById("bTitle");
    var blogDescrip = document.getElementById("bDescription");
    var blogImage = document.getElementById("bUrlImage");

    blogId.value = 0;
    blogTitle.value = "";
    blogDescrip.value = "";
    blogImage.value = "";
}


function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Blog/GetAll"
        },
        "columns": [
            { "data": "blogTitle", "width": "25%" },
            { "data": "blogDescription", "width": "30%" },
            { "data": "blogUrlImage", "width": "20%" },
            {
                "data": "blogId",
                "render": function (data) {
                    return `
                        <div>
                            <a href="/Blog/New/${data}" class="btn btn-success text-white" style="cursor:pointer;">

Edit
</a>
                        </div>

                            `;
                }, "width": "10%"
            },
            {
                "data": "blogId",
                "render": function (data) {
                    return `
                        <div>
                            <a onClick=Delete("/Blog/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">

Delete
</a>
                        </div>

                            `;
                }, "width": "10%"
            }
        ]
    });
}

function Delete(url){
    swal({
        title: "Are you sure to delete?",
        text: "This record cannot be recovered",
        icon: "warning",
        buttons: true   
    }).then((deleteItem) => {

        if (deleteItem) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        alert(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        alert(data.message);
                    }
                }
            })
        }

    })
}