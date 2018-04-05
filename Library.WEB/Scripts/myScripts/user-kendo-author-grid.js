$(document).ready(function () {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (e) {
                $.ajax({
                    type: 'GET',
                    url: "GetAll/",
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
                '<a class="k-button k-button-icontext" href="Get/#= AuthorId #" >Get</a>'
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