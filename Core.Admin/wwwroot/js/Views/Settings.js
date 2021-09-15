

$('#btnSaveSettings').click(function (evt) {
    debugger;
    $('#btnSaveSettings').attr('disabled', 'disabled');
    if ($('#frmAddSettings').valid()) {
        if ($("#TermsAndConditions").val() == "" || $("#PrivacyPolcy").val() == "" || $("#Credits").val() == "" || $("#StudyPlan").val() == "" || $("#AboutApp").val() == "" ) {
            warningAlert("يجب ادخال كل البيانات");
        
            return false;
        }
        
        var form = $('#frmAddSettings');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (res) {
            if (res == 1) {
                alertt("تم الحفظ بنجاح");
            }
            else
                warningAlert("حصل خطأ اثناء الحفظ");

        });
    }
    else
        $('#btnSaveSettings').removeAttr('disabled');



});





