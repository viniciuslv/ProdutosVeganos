class Produto {
    removerProduto(btn) {
        var data = this.getData(btn);
        this.remover(data);
    }

    remover(data) {
        var token = $('input[name=__RequestVerificationToken]').val();
        var header = {};
        header['RequestVerificationToken'] = token;

        $.ajax({
            url: '/produtos/ExcluirProduto',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: header
        }).done(function () {
            this.removerLinha(data.Id);
        }.bind(this));
        return data;
    }

    getData(elemento) {
        var linhaDoItem = $(elemento).parents('[produto-id]');
        var produtoId = linhaDoItem.attr('produto-id');
        return {
            Id: produtoId,
        }
    }

    removerLinha(produtoId) {
        $('[produto-id=' + produtoId + ']').remove();
    }

}

var produto = new Produto();
