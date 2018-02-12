class IngredientesProduto {
    removerIngrediente(btn) {
        var data = this.getData(btn);
        this.remover(data);
    }
    remover(data) {
        var token = $('input[name=__RequestVerificationToken]').val();
        var header = {};
        header['RequestVerificationToken'] = token;

        $.ajax({
            url: '/produtos/RemoverIngrediente',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: header
        }).done(function () {
            this.removerLinha(data.IngredienteId);
        }.bind(this));
        return data;
    }

    adicionarIngrediente(btn) {
        var data = this.getData(btn);
        this.adicionar(data);
    }
    adicionar(data) {
        var token = $('input[name=__RequestVerificationToken]').val();
        var header = {};
        header['RequestVerificationToken'] = token;

        $.ajax({
            url: '/produtos/AdicionarIngrediente',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: header
        }).done(function (response) {
            this.removerLinha(data.IngredienteId);
            $('#tabelaIngredientes').find('tbody:last').append(this.linhaIngredientes(data.IngredienteId, data.IngredienteNome));
        }.bind(this));
        return data;
    }

    digitarIngrediente(txt) {
        clearTimeout(delayTimer);
        delayTimer = setTimeout(function () {
            // Do the ajax stuff
            if (txt.value.length > 1) {
                this.consultarIngrediente(txt);
            }
        }.bind(this) , 500); // Will do the ajax stuff after 500 ms
    }

    consultarIngrediente(txt) {
        var ingrediente = { Nome: txt.value };
        var token = $('input[name=__RequestVerificationToken]').val();
        var header = {};
        header['RequestVerificationToken'] = token;

        $.ajax({
            url: '/produtos/ConsultarIngredientes',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(ingrediente),
            headers: header
        }).done(function (response) {
            this.carregarGrid(response);
        }.bind(this));
    }

    carregarGrid(listaIngredientes) {
        $('#tabelaPesquisa').find('tbody').empty();
        for (var i = 0; i < listaIngredientes.length; i++) {
            var obj = listaIngredientes[i];
            $('#tabelaPesquisa').find('tbody:last').append(this.linhaPesquisa(obj['id'], obj['nome']));
        }
    }

    linhaIngredientes(idIngrediente, nomeIngrediente) {
        var strTRIni = '<tr ingrediente-id="' + idIngrediente + '">';
        var strTDNome = '<td td-nome>' + nomeIngrediente + '</td>';
        var strTDBotao = '<td><a class="btn btn-info btn-sm" href="#" onclick="ingredientesProduto.removerIngrediente(this)"><span class="glyphicon glyphicon-remove"></span> Excluir</a></td>';
        var strTRFim = '</tr>';
        return strTRIni + strTDNome + strTDBotao + strTRFim;
    }
    linhaPesquisa(idIngrediente, nomeIngrediente) {
        var strTRIni = '<tr ingrediente-id="' + idIngrediente + '">';
        var strTDNome = '<td td-nome>' + nomeIngrediente + '</td>';
        var strTDBotao = '<td><a class="btn btn-info btn-sm" href="#" onclick="ingredientesProduto.adicionarIngrediente(this)"><span class="glyphicon glyphicon-plus"></span> Adicionar</a></td>';
        var strTRFim = '</tr>';
        return strTRIni + strTDNome + strTDBotao + strTRFim;
    }

    removerLinha(ingredienteId) {
        $('[ingrediente-id=' + ingredienteId + ']').remove();
    }

    getData(elemento) {
        var linhaDoItem = $(elemento).parents('[ingrediente-id]');
        var nomeIngrediente = linhaDoItem.find('[td-nome]').html();
        var ingredienteId = linhaDoItem.attr('ingrediente-id');
        var tabela = $(elemento).parents('[produto-id]');
        var produtoId = tabela.attr('produto-id');
        return {
            IngredienteId: ingredienteId,
            ProdutoId: produtoId,
            IngredienteNome: nomeIngrediente
        }
    }

}
var delayTimer;
var ingredientesProduto = new IngredientesProduto();

