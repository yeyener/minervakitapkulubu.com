// Contact Form Scripts
function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}

$(function () {
    $("#formJoin input,#formJoin textarea").jqBootstrapValidation({
        preventSubmit: true,
        submitError: function ($form, event, errors) {
            console.log(errors);
            $('#joinUsErrors').toggle(true).html('');
            $('#joinUsSuccess').toggle(false).html('');

            var errs = Object.values(errors).map(function (x) { return typeof x[0] === 'undefined' ? null : x[0] }).filter(function (x) { return x != null; });
            console.log(errs);
            for (var i in errs) {
                $('#joinUsErrors').append('<li>' + errs[i] + '</i>');
            }
        },
        submitSuccess: function ($form, event) {
            $('#joinUsErrors').toggle(false).html('');
            $('#joinUsSuccess').toggle(false).html('');
            event.preventDefault(); // prevent default submit behaviour
            
            $.ajax({
                url: "/Contact/JoinUs",
                type: "POST",
                data: getFormData($('#formJoin')),
                cache: false,
                success: function () {
                    $('#joinUsErrors').toggle(false).html('');
                    $('#joinUsSuccess').toggle(true).html('Başvurunuz başarıyla alındı');
                },
                error: function () {
                    $('#joinUsErrors').toggle(true).html('').append("<li>Çok pardon! Başvurunuz alınamadı! Daha sonra tekrar deneyebilir misin? Veya <a class='mail' href='mailto:minervakitapkulubu@gmail.com'>minervakitapkulubu@gmail.com</a>'a email atabilirsin.</li>");
                    $('#joinUsSuccess').toggle(false).html('');
                },
            });
        },
        filter: function() {
            return $(this).is(":visible");
        },
    });

    $("a[data-toggle=\"tab\"]").click(function(e) {
        e.preventDefault();
        $(this).tab("show");
    });
});
