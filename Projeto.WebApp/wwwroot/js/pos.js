/* Categorias (inicio) */

$(document).on('click', '.pos-categoria', function (event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    let id = $(this).data("id");
    ReloadProdutosByCategoria(id);
});

function ReloadProdutosByCategoria(id) {
    $.ajax({
        url: '/Pos/ReloadProdutosByCategoria',
        data: {
            id: id,
        },
        success: function (data) {
            $("#produtosDiv").html(data);
        }
    });
}
/* Categorias (fim)*/

/* Produtos (inicio) */
$(document).on('click', '.pos-produto', function (event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    let id = $(this).data("id");
    AddProduto(id);
});

function AddProduto(id) {
    $.ajax({
        url: '/Pos/AddProduto',
        data: {
            id: id,
        },
        success: function (data) {
            $("#carrinhoDiv").html(data);
            Totais();
            Mensagens();
        }
    });
}
/* Produtos (fim)*/

/* Estoque (inicio) */
$(document).on('click', '.pos-estoque', function (event) {
    //event.stopPropagation();
    //event.stopImmediatePropagation();
    let id = $(this).data("id");
    DelProduto(id);
});

function DelProduto(id) {
    $.ajax({
        url: '/Pos/DelProduto',
        data: {
            id: id,
        },
        success: function (data) {
            $("#carrinhoDiv").html(data);
            Totais();
            Mensagens();
        }
    });
}
/* Estoque (fim)*/

function Totais() {
    $.ajax({
        url: '/Pos/Totais',
        success: function (data) {
            $("#totaisDiv").html(data);
        }
    });
}
function Mensagens() {
    $.ajax({
        url: '/Pos/MensagemPos',
        success: function (data) {
            $("#mensagemDiv").html(data);
            window.setTimeout(function () {
                $(".alert-mensagem").fadeTo(500, 0).slideUp(500, function () {
                    $(this).remove();
                });
            }, 5000);
        }
    });
}

/* Modal Fecho (inicio)*/

$(document).on('click', '#pos-fechar', function () {
    $("#modal").modal();
});

$(document).on('click', '#fechar-logout', function () {
    location.href = '/Login/Logout';
});

$(document).on('click', '#fechar-sessao', function () {
    location.href = '/Pos/FecharSessao';
});


/* Modal Fecho (fim)*/

/* Mensagens (inicio) */
window.setTimeout(function () {

    $(".alert-mensagem").fadeTo(500, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 5000);
/* Mensagens (fim) */