let userRegistration = {
    init: function () {
        const systemUser = [{
            Value: 'true',
            Name: 'Yes',
        }, {
            Value: 'false',
            Name: 'No',
        }];

        $('#UserName').dxTextBox({
            name: 'UserName',
            label: "Username",
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: 'Please enter your Username'
            }]
        });



        $('#Email').dxTextBox({
            name: 'Email',
            label: 'Email',
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: 'Please enter your Email'
            }, {
                type: 'email',
                message: 'Email is invalid',
            }]
        });
        $('#Password').dxTextBox({
            name: 'Password',
            label: 'Password',
            mode: 'password',
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: 'Please enter your Password'
            }]
        });
        $('#FullName').dxTextBox({
            name: 'FullName',
            label: 'Full Name',
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: 'Please enter your Fullname'
            }]
        });
        $('#ContactNumber').dxTextBox({
            name: 'ContactNumber',
            label: 'Contact Number',
            mask: '(000)00-00000',
            
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: 'Please enter your Contact Number'
            }]
        });
        $('#RoleId').dxSelectBox({
            dataSource: [],
            showClearButton: true,
            name: 'RoleId',
            displayExpr: 'Name',
            valueExpr: 'Id',
            label: 'Role',
            labelMode: "static",
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: 'Please enter your Role'
            }]
        });
        $('#IsSystemUser').dxSelectBox({
            dataSource: systemUser,
            displayExpr: 'Name',
            valueExpr: 'Value',
            name: 'IsSystemUser',
            label: 'IsSystemUser',
            labelMode: "static",
            placeholder: "Are you a system user?"
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: 'Please choose wheather you are a system user.'
            }]
        });
        $("#registerUserSubmit").dxButton({
            name: 'registerUserSubmit',
            stylingMode: 'contained',
            text: 'Submit',
            type: 'default',
            width: 150,
            useSubmitBehavior: true,
        });
        $.ajax({
            type: 'GET',
            url: "/admin/user/getRoleList",
            success: function (resultData) {
                $('#RoleId').dxSelectBox('instance').option('dataSource', resultData)
            },
            error: function (resultData) { alert(resultData.Message) }
        });
        //$('#summary').dxValidationSummary();

        //$.ajax({
        //    success: function (res) {
        //        d.resolve(res.HasError);

        //    }
        //})

    }
}
$(function () {
    userRegistration.init();
})