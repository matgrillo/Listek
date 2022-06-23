$(document).ready(function() {

    $('.ActiveCheck').change(function() { //ko se spremeni vrednost vseh "ActiveCheck" checkboxov 
        var self = $(this);
        var id = self.attr('id'); // poišči Id iz atributa
        var value = self.prop('checked'); // poišči value checkboxa

        $.ajax({
            url: '/ToBuys/AJAXEdit', // kliče funkcijo ajaxedit iz urlja podanega
            data: {//podatki, ki se prenesejo v funkcijo ajaxedit
                id: id,
                value: value
            },
            type: 'POST',
            success: function (result) { //update in vrne tablediv
                $('#tableDiv').html(result);
            }
        });
    });

});