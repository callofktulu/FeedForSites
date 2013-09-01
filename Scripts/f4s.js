
$(function () {

    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        /*
        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-f4s-target"));
            $target.replaceWith(data);
            //$(data).insertAfter('.inner');
            $('#fit').val("");
            $('#delete-dialog').hide();
        });
        */
        $.ajax(options).done(function (data) {
            if (data != null)
            {
                var $target = $($form.attr("data-f4s-target"));
                //var $fitfit = $('#fit').val.toString;
                //$target.replaceWith(data);
                $($target).prepend($('#fit').val());
              
            }
            $('#fit').val("");
            $('#delete-dialog').hide();
        });

        return false;
    };

    $("form[data-f4s-ajax='true']").submit(ajaxFormSubmit);

    var deleteLinkObj;
    // delete Link
    $('.delete-link').click(function () {
        deleteLinkObj = $(this);  //for future use
        $('#delete-dialog').dialog('open');
        return false; // prevents the default behaviour
    });

    $('#delete-dialog').dialog({
        autoOpen: false, width: 400, resizable: false, modal: true, //Dialog options
        buttons: {
            "Continue": function () {
                $.post(deleteLinkObj[0].href, function (data) {  //Post to action
                    if (data == "True") {
                        deleteLinkObj.parent("div").parent("div").hide('fast'); //Hide Row
                    }
                    else {
                        alert("Couldn't delete fitfit else");
                    }
                });
                $(this).dialog("close");
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });
})

