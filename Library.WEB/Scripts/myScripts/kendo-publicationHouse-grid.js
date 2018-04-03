$(document).ready(function () {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (e) {
                $.ajax({
                    type: 'GET',
                    url: "PublicationHouse/GetAll",
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

            destroy: function (e) {
                $.ajax({
                    type: 'POST',
                    url: 'PublicationHouse/Delete/' + e.data.PublicationHouseId,
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
        toolbar: [{
            template:
            '<a class="add-link" href="PublicationHouse/CreateView"><img class="add-link" src="/Content/plus.svg" /></a>',
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
                '<a class="k-button k-button-icontext" href="PublicationHouse/GetByIdView/#= PublicationHouseId #" >Get</a>'
            },
            {
                field: "PublicationHouseId",
                title: "&nbsp",
                sortable: false,
                width: 129,
                template:
                '<a class="k-button k-button-icontext" href="PublicationHouse/UpdateView/#= PublicationHouseId #" >Update</a>'
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