var ProblemDiv = $("#divListProblems");


    $('#btnSearchProblem').click(function (evt) {
        debugger;
        var form = $('#frmSearchProblem');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (res) {
            ProblemDiv.html(res);
        });

    });


function DeleteProblem(id) {
    debugger;
    var url = $("#btnProblemDelete_" + id).data('url');
    $.post(url, { "id": id }, function (res) {
        if (res == "1") {
            updateProblem();
            alertt("تم الحذف بنجاح");
        }
    });
}

function ChangeStatusProblem(id) {
    debugger;
    var url = $("#btnProblemStatus_" + id).data('url');
    $.post(url, { "id": id }, function (res) {
        if (res == "1") {
            updateProblem();
            alertt("تم التعديل بنجاح");
        }
    });
}

function ConfirmChangeStatusProblem(id) {
    debugger;
    confirmAlert("هل تريد تغير الطلب؟", ChangeStatusProblem, id);
}
function ConfirmDeleteProblem(id) {
    debugger;
    confirmAlert("هل تريد حذف هذا الطلب؟", DeleteProblem, id);
}
function updateProblem() {
    var urlProblem = ProblemDiv.data('request-url');
    $.get(urlProblem).done(function (res) {
        debugger;
        ProblemDiv.html(res);
    });

}




