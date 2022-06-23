$(document).ready(function() {

    $.ajax({
        url: '/ToBuys/BuildToDoTable',
        success: function(result) {
        $('#tableDiv').html(result);
}
})

});