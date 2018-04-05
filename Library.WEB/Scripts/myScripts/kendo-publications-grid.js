$(document).ready(function () {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (e) {
                $.ajax({
                    type: 'GET',
                    url: "GetAll/",
                    dataType: "json",
                    success: function (data) {
                        var dataSample = [], Id = {};
                        data.Books.forEach(function (book) {
                            dataSample.push({ PublicationId: book.BookId, Name: book.Name, Type: 'Book' });
                        });
                        data.Magazines.forEach(function (magazine) {
                            dataSample.push({ PublicationId: magazine.MagazineId, Name: magazine.Name, Type: 'Magazine' });
                        });
                        data.Brochures.forEach(function (brochures) {
                            dataSample.push({ PublicationId: brochures.BrochureId, Name: brochures.Name, Type: 'Brochure' });
                        });
                        data = dataSample;
                        return e.success(data);
                    }
                });
            },

            destroy: function (e) {
                $.ajax({
                    type: 'POST',
                    url: '/' + e.data.Type + '/Delete/' + e.data.PublicationId,
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
                id: "PublicationId",
                fields: {
                    PublicationId: { editable: false, nullable: true, type: "number" },
                    Name: { editable: false, nullable: true, type: "string" },
                    Type: { editable: false, nullable: true, type: "string" }
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
        columns: [
            {
                field: "Name",
                title: "Name",
                sortable: true
            },
            {
                field: "Type",
                title: "Type",
                sortable: true
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