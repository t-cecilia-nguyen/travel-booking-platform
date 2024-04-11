// Function to load comments
function loadHotelReviews(hotelId) {
    $.ajax({
        url: '/TravelManagement/HotelReview/GetHotelReviews?hotelId=' + hotelId,
        method: 'GET',
        success: function (data) {

            var commentsHtml = '';
            for (var i = 0; i < data.length; i++) {
                //   alert(data[i]);
                commentsHtml += '<div class="allReviews">';
                commentsHtml += '<p>' + data[i].content + '</p>';
                commentsHtml += '<span>Posted on: ' + new Date(data[i].createdDate).toLocaleString() + '</span>';
                commentsHtml += '</div>';
            }
            $('#hotelReviewsList').html(commentsHtml);
        }
    });
}

$(document).ready(function () {
    var hotelId = $('#hotelReviews input[name="HotelId"]').val();

    loadHotelReviews(hotelId);

    // Submit event for addCommentForm
    $('#addHotelReviewForm').submit(function (e) {
        e.preventDefault(); //this is very important to not refresh page
        var formData = {
            HotelId: hotelId,
            Content: $('#hotelReviews textarea[name="Content"]').val()
        };

        $.ajax({
            url: '/TravelManagement/HotelReview/AddHotelReview',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#HotelReviews textarea[name="Content"]').val(''); // Clear the textarea
                    loadHotelReviews(hotelId); // Reload comments after adding new one
                } else {
                    alert(response.message);
                }
            },
            error: function (xhr, status, error) {
                alert("Error: " + error);
            }
        });
    });
});