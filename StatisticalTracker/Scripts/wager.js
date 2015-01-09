$(document).ready(function () {

    function updateGrid(e) {
        e.preventDefault();
        var url = $(this).attr('href');
        var grid = $(this).parents('.ajaxGrid');
        var id = grid.attr('id');
        grid.load(url + ' #' + id);
    };
    $('.ajaxGrid table thead tr a').live('click', updateGrid);
    $('.ajaxGrid table tfoot tr a').live('click', updateGrid);
});