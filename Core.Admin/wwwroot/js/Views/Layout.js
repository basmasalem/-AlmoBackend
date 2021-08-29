
/*$(function () {*/


    function hello() {
        console.log("hhh");
    }
    function alertt(msg) {
        swal({
            title: msg,
            icon: "success",
            button: "إغلاق",
        });
    }
    function warningAlert(msg) {
        swal({
            title: msg,
            icon: "warning",
            button: "إغلاق",
        });
    }
    function confirmAlert(msg, func, id) {
        debugger;
        swal({
            title: "هل انت متأكد؟",
            text: msg,
            icon: "warning",
            buttons: ["لا", "نعم"],
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    func(id);
                } else {

                }
            });
    }

    function isNumber(evt) {
        debugger;
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }

/*});*/