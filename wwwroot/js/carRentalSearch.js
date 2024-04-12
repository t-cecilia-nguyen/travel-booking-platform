
$(document).ready(function () {

    $('#searchBtnCar').click(function (event) {
        event.preventDefault();

        console.log("Search button clicked"); // Debugging: Check if the search button click event is triggered

        var formData = $('#carSearchForm').serialize();

        console.log("Form data:", formData); // Debugging: Check if form data is serialized correctly

        $('#loader').show();

        console.log("Loader shown"); // Debugging: Check if the loader is displayed

        setTimeout(function () {
            $.ajax({
                url: $('#carSearchForm').attr('action'), //from _SearchBar.cshtml
                type: 'GET',
                data: formData,
                success: function (response) {
                    console.log("Ajax request successful"); // Debugging: Check if Ajax request is successful
                    $('#carList').html(response);  // Car > Index
                },
                error: function (xhr, status, error) {
                    console.error("Ajax request failed:", error); // Debugging: Log any errors that occur
                },
                complete: function () {
                    console.log("Ajax request complete"); // Debugging: Check if Ajax request is complete
                    $('#loader').hide();
                }
            });
        }, 1000);
    });
});

/*
$(document).ready(function () {

    $('#searchBtnCar').click(function (event) {
        event.preventDefault(); 

        var formData = $('#carSearchForm').serialize(); 

        $('#loader').show(); 

        setTimeout(function () {
            $.ajax({
                url: $('#carSearchForm').attr('action'), //from _SearchBar.cshtml
                type: 'GET', 
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
*/