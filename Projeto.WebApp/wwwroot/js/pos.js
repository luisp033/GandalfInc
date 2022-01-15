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


/* Totais (inicio) */
function Totais() {
    $.ajax({
        url: '/Pos/Totais',
        success: function (data) {
            $("#totaisDiv").html(data);
        }
    });
}
/* Totais (fim) */

/* Modal Fecho (inicio) */

$(document).on('click', '#pos-fechar', function () {
    $("#modal-fechar").modal();
});

$("#modal-fechar").on('show.bs.modal', function () {
    $.ajax({
        url: '/Pos/ObtemTotaisSessao',
        success: function (data) {
            $("#totaisSessaoDiv").html(data);
        }
    });
});


$(document).on('click', '#fechar-logout', function () {
    location.href = '/Login/Logout';
});

$(document).on('click', '#fechar-sessao', function () {
    location.href = '/Pos/FecharSessao';
});

/* Modal Fecho (fim) */

/* Modal Cancelar Compra (inicio) */

$(document).on('click', '#pos-cancelar', function () {
    $("#modal-cancelar").modal();
});

$(document).on('click', '#cancelar-compra', function () {
    location.href = '/Pos/CancelarCompra';
});

/* Modal Cancelar Compra (fim) */

/* Modal Pagamento (inicio) */

$(document).on('click', '#pos-pagar', function () {
    $("#modal-pagamento").modal();
});

$(document).on('click', '.botao-pagar', function () {

    let id = $(this).data("tipo");
    $("#Tipo").val(id);

    var form = $("#form-pagamento");
    $.ajax({
        url: '/Pos/FinalizarCompra',
        method: 'post',
        data: form.serialize(),
        success: function (partialResult) {

            var isPago = partialResult.indexOf("PagamentoEfetuadoComSucesso") >= 0

            if (isPago) {
                location.href = '/Pos/Index';
            }
            else {
                $("#form-container").html(partialResult);
            }
        }
    });

});

/* Modal Pagamento (fim) */


/* Mensagens (inicio) */

TimeOutMensagens();

function Mensagens() {
    $.ajax({
        url: '/Pos/MensagemPos',
        success: function (data) {
            $("#mensagemDiv").html(data);
            TimeOutMensagens();
        }
    });
}

function TimeOutMensagens() {
    window.setTimeout(function () {

        $(".alert-mensagem").fadeTo(500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 5000);
}

/* Mensagens (fim) */