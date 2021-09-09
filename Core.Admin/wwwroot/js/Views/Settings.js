

$('#btnSaveSettings').click(function (evt) {
    debugger;
    $('#btnSaveSettings').attr('disabled', 'disabled');
    if ($('#frmAddSettings').valid()) {
        if ($("#TermsAndConditions").val() == "" || $("#PrivacyPolcy").val() == "" ) {
            warningAlert("يجب ادخال كل البيانات");
        
            return false;
        }
        
        var form = $('#frmAddSettings');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (res) {
            if (res == 1) {

                alertt("تم الحفظ بنجاح");
                //setTimeout(function () { window.location = "/Settings/Index"; }, 2000);
            }
            else
                warningAlert("حصل خطأ اثناء الحفظ");

        });
    }
    else
        $('#btnSaveSettings').removeAttr('disabled');



});





