$(function () {
    $('input#search').click(function (e) {
        e.preventDefault();
        searchObjectState();
    });

    $('input#uploadFile').click(function (e) {
        e.preventDefault();
        uploadFiles(); 
    });
});

function displayProcessingError(error) {
    $('#error_section').show();
    $('#error_code').text(error.status);
    $('#error_msg').text(error.statusText);
    $('#res_msg').text(error.responseText);
}

function uploadFiles() {
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
            $('#file_upload_section').hide();
            $('#error_section').hide();
            $('#upload_success_section').show();
        },
        error: function (error) {
            displayProcessingError(error);
        }
    });
}

function searchObjectState() {
    $.ajax({
        url: '/api/ObjectTransaction',
        type: 'get',
        data: $('form').serialize(),
        contentType: false,
        processData: false,
        success: function (response) {
            $('div#searchResults').empty();
            $('#error_section').hide();
            $.each(response, function (index) {
                $('div#searchResults').append('<h5>********Object Results**************</h5>');
                $('div#searchResults').append('<p> Object Type ---> ' + this.objectType + '</p>');
                $('div#searchResults').append('<p> Object Changes ---> ' + this.objectChanges + '</p>');
            });
        },
        error: function (error) {
            displayProcessingError(error);
        }
    });
}