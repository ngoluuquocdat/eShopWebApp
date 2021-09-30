// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('body').on('click', '.btn-add-cart', function (e) {
    e.preventDefault();
    const id = $(this).data('id')
    const culture = $('#hidCulture').val()
    $.ajax({
        type: "POST",
        datatype: 'json',
        url: "/" + culture + '/Cart/AddToCart',
        data: {
            productId: id,
            languageId: culture
        },
        success: function (res) {
            $('#lbl_number_items_header').text(res.length);
        },        
        error: function (err) {
            console.log(err);
        }
    });
})


function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
