function getEmployee(callback) {
    $('#loading-image').show();
    $.ajax({
        type: 'GET',
        url: "/Employees/getData",
        data: {
            id: id,
            search: searchKey
        }
        ,
        success: function (resultData) {
            callback(resultData);
        },
        complete: function () {
            $('#loading-image').hide();
        },
        error: function (resultData) { alert(resultData.Message) }
    });
}
