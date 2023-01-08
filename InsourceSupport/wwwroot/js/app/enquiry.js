

let enquiryModel = {
    init: function () {

        $("#SoftwareId").on("change", function () {
            let val = $(this).val();
            enquiryModel.loadListForModules(val);
        })
        //$("#reportForm").validate({
        //    rules: {
        //        Email: {
        //            required: true
        //        },
        //    },
        //    messages: {
        //        Email: {
        //            required: 'Email or Contact number required'
        //        }
        //    }
        //});

        //$("#postEnquiry").click(function (e) {
        //    /*$("#reportForm").valid();*/
        //    e.preventDefault();
        //    var formData = new FormData($("#reportForm")[0]);
        //    //var paramObj = {};
        //    //    $.each($("#reportForm").serializeArray(), function (_, kv) {
        //    //    paramObj[kv.name] = kv.value;
        //    //});
        //    $.ajax({
        //        type: 'POST',
        //        url: "/home/create",
        //        processData: false,
        //        contentType: false,
        //        data: formData,
        //        success: function (res) {
        //            if (res.IsSuccess) {

        //                alert("Success");
        //            }
        //            else {
        //                alert(res.Message);
        //            }
        //        },
        //        complete: function () {

        //        },
        //        error: function (resultData) { alert(resultData.Message) }
        //    });
        //})

    },
    loadListForModules: function (val) {
        if (val) {
            $("#ModuleId").loadList({
                url: "/lookup/getmodulesbysoftwareid?id=" + val,
                onSuccess: function (data) {
                    /*alert("working");*/
                }
            })
        }
        else {
            $("#ModuleId").empty();
        }

    }

}

$(() => {
    enquiryModel.init();
})