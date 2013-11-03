
$(document).ready(function(){
    $('.lnk-analytics').click(function () {
        var taskId = $(this).parent('tr').attr('taskid');
        document.location.href = '/Analytics/' + taskId;
    });
});