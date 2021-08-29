var UserDiv = $("#divListUsers");
$(function () {

    $('#btnSearchUser').click(function (evt) {
        debugger;
        var form = $('#frmSearchUser');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (res) {
            UserDiv.html(res);
        });

    });
});

function DeleteUser(id) {
    debugger;
    var url = $("#btnUserDelete_" + id).data('url');
    $.post(url, { "id": id }, function (res) {
        if (res == "1") {
            updateUser();
            alert("تم الحذف بنجاح");
        }
    });
}

function ChangeStatusUser(id) {
    debugger;
    var url = $("#btnUserStatus_" + id).data('url');
    $.post(url, { "id": id }, function (res) {
        if (res == "1") {
            updateUser();
            alertt("تم التعديل بنجاح");
        }
    });
}

function ConfirmChangeStatusUser(id) {
    debugger;
    confirmAlert("هل تريد تغير المستخدم؟", ChangeStatusUser, id);
}
function ConfirmDeleteUser(id) {
    debugger;
    confirmAlert("هل تريد حذف هذا المستخدم؟", DeleteUser, id);
}


$('#btnSaveUser').click(function (evt) {
    debugger;
    $('#btnSaveUser').attr('disabled', 'disabled');
    if ($('#frmAddUser').valid()) {

        
        var form = $('#frmAddUser');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (res) {
            if (res == 1) {
              
                alertt("تم الحفظ بنجاح");
                setTimeout(function () { window.location = "/User/Index"; }, 2000);
            }
            else
                warningAlert("حصل خطأ اثناء الحفظ");

        });
    }
    else
        $('#btnSaveUser').removeAttr('disabled');



});
function updateUser() {
    var urlUser = UserDiv.data('request-url');
    $.get(urlUser).done(function (res) {
        debugger;
        UserDiv.html(res);
    });

}