var RequestDiv = $("#divListRequests");


    $('#btnSearchRequest').click(function (evt) {
        debugger;
        var form = $('#frmSearchRequest');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (res) {
            RequestDiv.html(res);
        });

    });


function DeleteRequest(id) {
    debugger;
    var url = $("#btnRequestDelete_" + id).data('url');
    $.post(url, { "id": id }, function (res) {
        if (res == "1") {
            updateRequest();
            alertt("تم الحذف بنجاح");
        }
    });
}

function ChangeStatusRequest(id) {
    debugger;
    var url = $("#btnRequestStatus_" + id).data('url');
    $.post(url, { "id": id }, function (res) {
        if (res == "1") {
            updateRequest();
            alertt("تم التعديل بنجاح");
        }
    });
}

function ConfirmChangeStatusRequest(id) {
    debugger;
    confirmAlert("هل تريد تغير الطلب؟", ChangeStatusRequest, id);
}
function ConfirmDeleteRequest(id) {
    debugger;
    confirmAlert("هل تريد حذف هذا الطلب؟", DeleteRequest, id);

}
function updateRequest() {
    var urlRequest = RequestDiv.data('request-url');
    $.get(urlRequest).done(function (res) {
        debugger;
        HrlpDiv.html(res);
    });

}





