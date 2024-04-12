$(document).ready(function () {

    $('#searchBtn').click(function () {
        event.preventDefault();

        var formData = $('#flightSearchForm').serialize();

        $('#loader').show();

        setTimeout(function () {
            $.ajax({
                url: $('#flightSearchForm').attr('action'),
                type: 'GET',
                data: formData,
                success: function (response) {
                    $('#flightList').html(response);
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                },
                complete: function () {

                    $('#loader').hide();
                }
            });
        }, 1000);

    });
});

