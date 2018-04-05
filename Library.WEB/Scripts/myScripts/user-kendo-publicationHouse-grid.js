$(document).ready(function () {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (e) {
                $.ajax({
                    type: 'GET',
                    url: "GetAll/",
                    dataType: "json",
                    success: function (data) {
                        data.forEach(function (publicationHouse) {
                            var books = [];
                            publicationHouse.Books.forEach(function (book) {
                                books.push(book.Name);
                            });
                            publicationHouse.Books = books;
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
                id: "PublicationHouseId",
                fields: {
                    PublicationHouseId: { editable: false, nullable: true, type: "number" },
                    Name: { editable: false, nullable: true, type: "string" },
                    Adress: { editable: false, nullable: true, type: "string" },
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
                field: "Adress",
                title: "Adress",
                sortable: true
            },
            {
                field: "PublicationHouseId",
                title: "&nbsp",
                sortable: false,
                width: 105,
                template:
                '<a class="k-button k-button-icontext" href="GetByIdView/#= PublicationHouseId #" >Get</a>'
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