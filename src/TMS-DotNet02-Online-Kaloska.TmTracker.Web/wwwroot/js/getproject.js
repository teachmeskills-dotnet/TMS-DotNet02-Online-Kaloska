$(function () {

    //Optional: turn the chache off
    $.ajaxSetup({ cache: false });

    $('#btnCreate').click(function () {
        $('#dialogContent').load(this.href, function () {
            $('#dialogDiv').modal({
                backdrop: 'static',
                keyboard: true
            }, 'show');
            bindForm(this);
        });
        return false;
    });
});

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#dialogDiv').modal('hide');
                    // Refresh:
                    // location.reload();
                } else {
                    $('#dialogContent').html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}

$('[name="favt"]').click(function () {
    $.get("/Project/IsFav", { isFav: false, projectId: this.id }, function (result) {
        if (result) {
            location.reload();
        } 
    });
     /*location.reload();*/
});

$('[name="favf"]').click(function () {
    $.get("/Project/IsFav", { isFav: true, projectId: this.id }, function (result) {
        if (result) {
            location.reload();
        }
    });
    /*location.reload();*/
});