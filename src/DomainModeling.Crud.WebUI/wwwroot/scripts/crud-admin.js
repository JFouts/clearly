$(document).ready(function () {
    $('form[data-source]').submit(function(e) {
        e.preventDefault();

        const dataSource = $(this).data('source');

        const data = new FormData($(e.target)[0]);
        const value = Object.fromEntries(data.entries());

        $(this).find('select[multiple]').each(function() {
            const name = $(this).prop('name');
            value[name] = data.getAll(name);
        })

        value['id'] = '00000000-0000-0000-0000-000000000000';

        $.ajax({
            url: dataSource,
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=UTF-8",
            data: JSON.stringify(value),
            headers: {          
                "Accept": "application/json; charset=utf-8",         
                "Content-Type": "application/json; charset=utf-8"   
            } 
        });
    });
});