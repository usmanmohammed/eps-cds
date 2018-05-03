$(document).ready(function () {
    $('.fancy-image').fancybox({
        helpers:
        {
            title:
            {
                type: 'inside'
            },
            padding: 0,
            openEffect: 'elastic',
            closeBtn: true
        }
    });
    $(".content").htmlarea();
});