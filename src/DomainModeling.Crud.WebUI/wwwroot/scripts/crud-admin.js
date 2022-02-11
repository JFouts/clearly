$(document).ready(function () {
    $('form[data-source]').submit(function(e) {
        e.preventDefault();

        const dataSource = $(this).data('source');
        const dataType = $(this).data('type');

        const data = new FormData($(e.target)[0]);
        const value = Object.fromEntries(data.entries());

        $(this).find('select[multiple]').each(function() {
            const name = $(this).prop('name');
            value[name] = data.getAll(name);
        })

        if (dataType == 'create') {
            value.id = '00000000-0000-0000-0000-000000000000';
        }

        const url = dataType == 'edit' ? dataSource + '/' + value.id : dataSource;
        const method = dataType == 'edit' ? 'PUT' : 'POST';

        console.log(value)

        $.ajax({
            url: url,
            type: method,
            contentType: "application/json; charset=UTF-8",
            data: JSON.stringify(value),
            headers: {          
                "Accept": "application/json; charset=utf-8",         
                "Content-Type": "application/json; charset=utf-8"   
            },
            success: function(data, textStatus, jqXHR) {
                const url = String(window.location)

                if (dataType == 'edit') {
                    window.location = url.substring(0, url.lastIndexOf('/', url.lastIndexOf('/')-1));
                } else {
                    window.location = url.substring(0, url.lastIndexOf('/'));
                }
            }
        });
    });

    $('.default-focus input:text:visible:first').focus();
});