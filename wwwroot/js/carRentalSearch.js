$(document).ready(function () {

    $('#searchBtnCar').click(function (event) {
        event.preventDefault(); 

        var formData = $('#carSearchForm').serialize(); 

        $('#loader').show(); 

        setTimeout(function () {
            $.ajax({
                url: $('#carSearchForm').attr('action'), //from _SearchBar.cshtml
                type: 'POST', 
                data: formData,
                success: function (response) {
                    $('#carList').html(response);  // Car > Index
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