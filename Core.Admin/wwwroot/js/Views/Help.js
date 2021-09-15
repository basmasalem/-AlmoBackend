var HelpDiv = $("#divListHelps");


    $('#btnSearchHelp').click(function (evt) {
        debugger;
        var form = $('#frmSearchHelp');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (res) {
            HelpDiv.html(res);
        });

    });


function DeleteHelp(id) {
    debugger;
    var url = $("#btnHelpDelete_" + id).data('url');
    $.post(url, { "id": id }, function (res) {
        if (res == "1") {
            updateHelp();
            alertt("تم الحذف بنجاح");
        }
    });
}

function ChangeStatusHelp(id) {
    debugger;
    var url = $("#btnHelpStatus_" + id).data('url');
    $.post(url, { "id": id }, function (res) {
        if (res == "1") {
            updateHelp();
            alertt("تم التعديل بنجاح");
        }
    });
}

function ConfirmChangeStatusHelp(id) {
    debugger;
    confirmAlert("هل تريد تغير الطلب؟", ChangeStatusHelp, id);
}
function ConfirmDeleteHelp(id) {
    debugger;
    confirmAlert("هل تريد حذف هذا الطلب؟", DeleteHelp, id);
}
function updateHelp() {
    var urlHelp = HelpDiv.data('request-url');
    $.get(urlHelp).done(function (res) {
        debugger;
        HelpDiv.html(res);
    });

}




