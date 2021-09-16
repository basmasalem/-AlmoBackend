var ReportDiv = $("#divListReports");


    $('#btnSearchReport').click(function (evt) {
        debugger;
        var form = $('#frmSearchReport');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (res) {
            ReportDiv.html(res);
        });

    });







