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
        },

        schema:
        {
            model:
            {
                id: "BrochureId",
                fields: {
                    BrochureId: { editable: false, nullable: true, type: "number" },
                    Name: { editable: false, nullable: true, type: "string" },
                    TypeOfCover: { editable: false, nullable: true, type: "string" },
                    NumberOfPages: { editable: false, nullable: true, type: "number" }
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
                field: "TypeOfCover",
                title: "Type of cover",
                sortable: true
            },
            {
                field: "NumberOfPages",
                title: "Number of pages",
                sortable: true
            },
            {
                field: "BrochureId",
                title: "&nbsp",
                sortable: false,
                width: 105,
                template:
                '<a class="k-button k-button-icontext" href="GetByIdView/#= BrochureId #" >Get</a>'
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