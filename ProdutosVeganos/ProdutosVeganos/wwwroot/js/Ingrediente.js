class Ingrediente {
    removerIngrediente(btn) {
        var data = this.getData(btn);
        this.remover(data);
    }

    remover(data) {
        var token = $('input[name=__RequestVerificationToken]').val();
        var header = {};
        header['RequestVerificationToken'] = token;

        $.ajax({
            url: '/ingredientes/ExcluirIngrediente',
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
        var linhaDoItem = $(elemento).parents('[ingrediente-id]');
        var ingredienteId = linhaDoItem.attr('ingrediente-id');
        return {
            Id: ingredienteId,
        }
    }

    removerLinha(ingredienteId) {
        $('[ingrediente-id=' + ingredienteId + ']').remove();
    }

}

var ingrediente = new Ingrediente();
