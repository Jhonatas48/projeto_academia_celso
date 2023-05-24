$(document).ready(function () {
    //encontra todos os botões de alternância
    var toggleButtons = $('.btn-toggle');

    //adc um evento de clique a todos os botões de alternância
    toggleButtons.click(function () {
        //encontra a checkbox correspondente ao botão clicado
        var checkbox = $(this).children(':checkbox');

        //atualiza o estado do botão de acordo com o valor da checkbox
        if (checkbox.prop('checked')) {
            $(this).addClass('active');

            $(this).removeClass('btn-outline-danger');
            $(this).addClass('btn-outline-primary');

            console.log('yeetus activatus');
        } else {
            $(this).removeClass('active');

            $(this).removeClass('btn-outline-primary');
            $(this).addClass('btn-outline-danger');

            console.log('yeetus deletus');
        }
    });
});