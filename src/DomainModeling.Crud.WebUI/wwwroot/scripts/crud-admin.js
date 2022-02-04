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
            contentType: "application/json; charset=UTF-8",
            data: JSON.stringify(value),
            headers: {          
                "Accept": "application/json; charset=utf-8",         
                "Content-Type": "application/json; charset=utf-8"   
            },
            success: function(data, textStatus, jqXHR) {
                const url = String(window.location)
                window.location =url.substring(0, url.lastIndexOf('/'));
                alert(window.location)
            }
        });
    });

    $('table[data-source]').each(function() {
        const dataSource = $(this).data('source');

        $.ajax({
            url: dataSource,
            type: 'GET',
            dataType: 'json',
            headers: {          
                "Accept": "application/json; charset=utf-8"
            },
            success: (data) => {
                let html = '';

                for (record of data) {
                    html += '<tr data-id="' + record.id + '">';
                    for (prop in record) {
                        html += '<td>' + record[prop] + '</td>';
                    }
                    html += '<td>';
                    // html += '<a href="' + window.location + '/' + record.id + '"><i class="bi bi-eye-fill"></i></a>&nbsp;';
                    html += '<a href="' + window.location + '/edit/' + record.id + '"><i class="bi bi-pencil-fill"></i></a>&nbsp;';
                    html += '<a href="" onclick="alert(\'Delete\')"><i class="bi bi-trash-fill"></i>';
                    html += '</td>';
                    html += '</tr>\n';
                }

                $(this).find('tbody').html(html);
                $(this).removeClass('table-loading');
            }
        });
    });
});