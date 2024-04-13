$(document).ready(function () {

    $('#searchBtnHotel').click(function (event) {
        event.preventDefault();

        var formData = $('#hotelSearchForm').serialize(); 

        $('#loader').show();

        setTimeout(function () {
            $.ajax({
                url: $('#hotelSearchForm').attr('action'), 
                type: 'GET', 
                data: formData,
                success: function (response) {
                   // $('#hotelList').html(response); 
                    $('#hotelContainer').html(response); 

                },
                error: function (xhr, status, error) {
                    // Handle error
                    console.error(xhr.responseText);
                },
                complete: function () {
                    $('#loader').hide();
                }
            })
        }, 1000);
    });
});