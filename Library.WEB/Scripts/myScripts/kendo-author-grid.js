$(document).ready(function () {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (e) {
                $.ajax({
                    type: 'GET',
                    url: "Author/GetAll",
                    dataType: "json",
                    success: function (data) {
                        data.forEach(function (author) {
                            var books = [];
                            author.Books.forEach(function (book) {
                                books.push(book.Name);
                            });
                            author.Books = books;
                        });
                        return e.success(data);
                    }
                });
            },

            destroy: function (e) {
                $.ajax({
                    type: 'POST',
                    url: 'Author/Delete/' + e.data.AuthorId,
                    dataType: 'number',
                    success: function (data) {
                        $('#grid').data('kendoGrid').dataSource.read();
                        $('#grid').data('kendoGrid').refresh();
                        e.success(data);
                    },
                    error: function (data) {
                        $('#grid').data('kendoGrid').dataSource.read();
                        $('#grid').data('kendoGrid').refresh();
                        e.error("", "404", data);
                    }
                });
            },
        },

        schema:
        {
            model:
            {
                id: "AuthorId",
                fields: {
                    AuthorId: { editable: false, nullable: true, type: "number" },
                    Name: { editable: false, nullable: true, type: "string" },
                    Birthday: { editable: false, nullable: true, type: "number" },
                    Deathday: { editable: false, nullable: true, type: "number" },
                    Books: { editable: false, nullable: true, type: "string" }
                }
            }
        },
        batch: false,
        pageSize: 5

    });
    $("#grid").kendoGrid({
        dataSource: dataSource,
        groupable: true,
        sortable: true,
        resizable: true,
        editable: {
            mode: "inline",
            confirmation: false
        },
        toolbar: [{
            template:
            '<a class="add-link" href="Author/CreateView"><img class="add-link" src="/Content/plus.svg" /></a>',
        }],
        columns: [
            {
                field: "Name",
                title: "Name",
                sortable: true
            },
            {
                field: "Books",
                title: "Books",
                sortable: true
            },
            {
                field: "Birthday",
                title: "Birthday",
                sortable: true
            },
            {
                field: "Deathday",
                title: "Deathday",
                sortable: true
            },
            {
                field: "AuthorId",
                title: "&nbsp",
                sortable: false,
                width: 105,
                template:
                '<a class="k-button k-button-icontext" href="Author/GetByIdView/#= AuthorId #" >Get</a>'
            },
            {
                field: "AuthorId",
                title: "&nbsp",
                sortable: false,
                width: 129,
                template:
                '<a class="k-button k-button-icontext" href="Author/UpdateView/#= AuthorId #" >Update</a>'
            },
            {
                title: "&nbsp",
                width: 140,
                command: ['destroy']
            }
        ],
        height: "500px",
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
    }).data("kendoGrid");
});