$(document).ready(function () {
    // create MultiSelect from select HTML element
    var authors = $("#authors").kendoMultiSelect({
        autoClose: false
    }).data("kendoMultiSelect");
    var publicationHouses = $("#publicationHouses").kendoMultiSelect({
        autoClose: false
    }).data("kendoMultiSelect");
    var books = $("#books").kendoMultiSelect({
        autoClose: false
    }).data("kendoMultiSelect");
});