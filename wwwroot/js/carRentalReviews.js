// Function to load comments
function loadCarRentalReviews(carRentalId) {
    $.ajax({
        url: '/TravelManagement/CarRentalReview/GetCarRentalReviews?carRentalId=' + carRentalId,
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
            $('#carRentalReviewsList').html(commentsHtml);
        }
    });
}

$(document).ready(function () {
    var carRentalId = $('#carRentalReviews input[name="CarRentalId"]').val();

    loadCarRentalReviews(carRentalId);

    // Submit event for addCommentForm
    $('#addCarRentalReviewForm').submit(function (e) {
        e.preventDefault(); //this is very important to not refresh page
        var formData = {
            CarRentalId: carRentalId,
            Content: $('#carRentalReviews textarea[name="Content"]').val()
        };

        $.ajax({
            url: '/TravelManagement/CarRentalReview/AddCarRentalReview',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#carRentalReviews textarea[name="Content"]').val(''); // Clear the textarea
                    loadCarRentalReviews(carRentalId); // Reload comments after adding new one
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