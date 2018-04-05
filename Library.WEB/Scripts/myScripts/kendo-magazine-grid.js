$(document).ready(function () {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (e) {
                $.ajax({
                    type: 'GET',
                    url: "GetAll/",
                    dataType: "json",
                    success: function (data) {
                        return e.success(data);
                    }
                });
            },

            destroy: function (e) {
                $.ajax({
                    type: 'POST',
                    url: 'Delete/' + e.data.MagazineId,
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
                id: "MagazineId",
                fields: {
                    MagazineId: { editable: false, nullable: true, type: "number" },
                    Name: { editable: false, nullable: true, type: "string" },
                    Number: { editable: false, nullable: true, type: "number" },
                    YearOfPublishing: { editable: false, nullable: true, type: "number" }
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
            '<a class="add-link" href="Create"><img class="add-link" src="/Content/plus.svg" /></a>',
        }],
        columns: [
            {
                field: "Name",
                title: "Name",
                sortable: true
            },
            {
                field: "Number",
                title: "Number",
                sortable: true
            },
            {
                field: "YearOfPublishing",
                title: "Year of publishing",
                sortable: true
            },
            {
                field: "MagazineId",
                title: "&nbsp",
                sortable: false,
                width: 105,
                template:
                '<a class="k-button k-button-icontext" href="Get/#= MagazineId #" >Get</a>'
            },
            {
                field: "MagazineId",
                title: "&nbsp",
                sortable: false,
                width: 129,
                template:
                '<a class="k-button k-button-icontext" href="Update/#= MagazineId #" >Update</a>'
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