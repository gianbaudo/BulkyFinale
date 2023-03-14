var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    //per i dettagli si veda qui: https://datatables.net/manual/ajax
    dataTable = $('#tblData').DataTable({
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.13.2/i18n/it-IT.json"
        },
        ajax: {
            url: "/Admin/Product/GetAll"
        },
        columns: [
            { data: "title", width: "15%" },
            { data: "isbn", width: "15%" },
            { data: "price", width: "15%" },
            { data: "author", width: "15%" },
            { data: "category.name", width: "15%" },
            { data: "coverType.name", width: "15%" },
            {
                data: "id",
                render: function (data) {
                    //una multiline string deve essere racchiusa tra backtick
                    //il backtick si può ottenere sulla tastiera italiana con ALT+96
                    //https://superuser.com/questions/667622/italian-keyboard-entering-tilde-and-backtick-characters-without-changin
                    //tra i backtick mettiamo il codice HTML che deve essere renderizzato all'interno della colonna di DataTable
                    //per il momento gestiamo solo l'Edit, la Delete verrà sviluppata successivamente
                    return `
                    <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i>Edit</a>
                            <a  class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i>Delete</a>
                        </div>
                    `
                },
                width: "15%"
            },

        ]
    });
}
