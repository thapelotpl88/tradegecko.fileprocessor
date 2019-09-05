$(function () {
    $('input#search').click(function (e) {
        e.preventDefault();
        $.get('/api/ObjectTransaction', $('form').serialize(), function (response) {

        });
    });

    $('input#uploadFile').click(function (e) {
        e.preventDefault();
        var formData = new FormData();
        var file = $('#file')[0].files[0]; 
        formData.append('file', file); 

        $.ajax({
            url: '/api/ObjectTransaction',
            type: 'post',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
            }
        }); 
    });
});