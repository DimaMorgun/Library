$(document).ready(function () {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (e) {
                $.ajax({
                    type: 'GET',
                    url: "Book/GetAll",
                    dataType: "json",
                    success: function (data) {
                        data.forEach(function (book) {
                            var authors = [];
                            var publicationHouses = [];
                            book.Authors.forEach(function (author) {
                                authors.push(author.Name);
                            });
                            book.PublicationHouses.forEach(function (publicationHouse) {
                                publicationHouses.push(publicationHouse.Name);
                            });
                            book.Authors = authors;
                            book.PublicationHouses = publicationHouses;
                        });
                        return e.success(data);
                    }
                });
            },

            destroy: function (e) {
                $.ajax({
                    type: 'POST',
                    url: 'Book/Delete/' + e.data.BookId,
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
                id: "BookId",
                fields: {
                    BookId: { editable: false, nullable: true, type: "number" },
                    Name: { editable: false, nullable: true, type: "string" },
                    YearOfPublishing: { editable: false, nullable: true, type: "number" },
                    Authors: { editable: false, nullable: true, type: "string" },
                    PublicationHouses: { editable: false, nullable: true, type: "string" }
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
        editable: {
            mode: "inline",
            confirmation: false
        },
        toolbar: [{
            template:
            '<a class="add-link" href="Book/CreateView"><img class="add-link" src="/Content/plus.svg" /></a>',
        }],
        columns: [
            {
                field: "Name",
                title: "Name",
                sortable: true
            },
            {
                field: "Authors",
                title: "Authors",
                sortable: true
            },
            {
                field: "PublicationHouses",
                title: "Publication houses",
                sortable: true
            },
            {
                field: "YearOfPublishing",
                title: "Year",
                sortable: true
            },
            {
                field: "BookId",
                title: "&nbsp",
                sortable: false,
                width: 105,
                template:
                '<a class="k-button k-button-icontext" href="Book/GetByIdView/#= BookId #" >Get</a>'
            },
            {
                field: "BookId",
                title: "&nbsp",
                sortable: false,
                width: 129,
                template:
                '<a class="k-button k-button-icontext" href="Book/UpdateView/#= BookId #" >Update</a>'
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