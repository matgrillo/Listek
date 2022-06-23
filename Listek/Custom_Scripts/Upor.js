$(document).ready(function () {

    $.ajax({
        url: '/ToBuys/Uporabnik',
        type: 'GET'
        datatype: "Json"
        success: function (data) {
            $('#ime').html(data);
            
        }
    })

});