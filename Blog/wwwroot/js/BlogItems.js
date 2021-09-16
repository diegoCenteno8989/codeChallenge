var next;
$(document).ready(function () {
    getApi();
    getLocal();
});

function getLocal() {
    $.getJSON('/Blog/GetAll', function (data) {

        var blog = data.data;

        for (var i = 0; i < data.data.length; i++) {
            $('#divPrincipal').append(
                "<div id='divBlog' class='col-7' >" +
                '<h2>' + blog[i].blogTitle + ' - FROM LOCAL' + '</h2>' +
                "<div style='font-size:20px; text-align: justify;'>" + blog[i].blogDescription + "</div>" +
                "</div >");

            $('#divPrincipal').append(
                "<div id='divImagen" + next + "' class='col-5' style='width: 700px; height: 300px; border: 1px solid black;' ></div>"
            );

            if (blog[i].blogUrlImage != null) {

                $('#divImagen' + next).css("background-image", "url(img/" + blog[i].blogUrlImage + ")");
                $('#divImagen' + next).css("background-position", "center");
                $('#divImagen' + next).css("margin-bottom", "10px");
            }
            else {
                $('#divImagen' + next).css("margin-bottom", "10px");
                $('#divImagen' + next).css("background-image", "url(img/nofoto.jpg)");
                $('#divImagen' + next).css("background-position", "center");
            }

            next++;
        }


    });
}

function getApi() {
   
    $.getJSON('https://gnews.io/api/v4/search?q=watches&token=757acb1c37fb43e3266e715c13a49cf2', function (data) {

        var d = data.articles;
        next = d.length;
        $.each(d, function (i, item) {

            $('#divPrincipal').append(
                "<div id='divBlog' class='col-7' >" +
                '<h2>' + item.title + ' - FROM API' + '</h2>' +
                "<div style='font-size:20px; text-align: justify;'>" + item.content + "</div>" +
                "</div >");

            $('#divPrincipal').append(
                "<div id='divImagen" + i + "' class='col-5' style='width: 700px; height: 300px; border: 1px solid black;' ></div>"
            );

            if (item.image != null) {

                $('#divImagen' + i).css("background-image", "url(" + item.image + ")");
                $('#divImagen' + i).css("background-position", "center");
                $('#divImagen' + i).css("margin-bottom", "10px");
            }
            else {
                $('#divImagen' + i).css("margin-bottom", "10px");
                $('#divImagen' + i).css("background-image", "url(img/nofoto.jpg)");
                $('#divImagen' + i).css("background-position", "center");
            }
            
        });
    });
    
}