
$(document).ready(function(){
    $('.lnk-analytics').click(function () {
        var lnk = $(this);
        var tr;
        if (lnk.is('span')) {
            tr = $(this).parent().parent();
        } else {
            tr = $(this).parent();
        }
        var taskId = tr.attr('taskid');
        document.location.href = '/Analytics/' + taskId;
    });
});