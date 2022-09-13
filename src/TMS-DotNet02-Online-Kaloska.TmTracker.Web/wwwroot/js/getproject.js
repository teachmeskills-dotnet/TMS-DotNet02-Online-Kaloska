﻿$(function () {

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
let projectId;
const menuList = document.querySelector(".right-menu");
function menu(event) {
    // Блокируем всплывание события contextmenu
    event = event || window.event;
    event.cancelBubble = true;
    menuList.style.top = event.clientY + 'px';
    menuList.style.left = event.clientX + 'px';
    menuList.classList.add("right-menu-active");
    projectId = (event.target.id) || event.target.parentNode.id;
    return false;
}
$(document).on('click', function () {
    menuList.classList.remove("right-menu-active");
});
menuList.addEventListener("click", event => {
    event.stopPropagation();
})
$('#delete').click(function () {
    if (confirm("Удалить проект?")) {
        $.ajax({
            url: "/Project/DeleteProject",
            type: "Post",
            data: { projectId },
            success: function (result) {
            },
            statusCode: {
                200: function () {
                    alert('Проект удален');
                    location.reload();
                },
                400: function () {
                    alert('Нет прав');
                    location.reload();
                }
            }
        });
    };
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