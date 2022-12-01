## Exercício para 29/11/2022

* Criar um sistema de exibição de contas a Pagar e Receber
* Inclua no sistema um campo de formulário Select
* CRUD COMPLETO
* Alterar visualmente (layout com boostrap)

## Projeto evoluído para Entrega Final

* Inclusão de esquema de autenticação (Identity), com customização dos forms de login, registro e tradução das mensagens de erro do Identity para pt-BR.
* Cadastros só podem ser acessados por usuários autenticados.
* Cadastro/Edição de Conta a Pagar não permite duplicação considerando Tipo, Valor e Vencimento.
* Substituição do enum de Tipos de Conta a Pagar por tabela no BD (com possibilidade de cadastro/edição de Tipos de Conta).
* Dashboard com possibilidade de seleção do período e resumo do total a pagar, total pago e total não pago no período.
* Inclusão de mensagens de erro/sucesso no cadastro/edição/remoção com bootstrap alert.
* Uso de partial view (_Form, _AlertSucesso, _AlertErro).
* Substituição do simple-dataTables pelo dataTables, com plugin de tradução para pt-BR e customização para ordenação correta de data com uso do moment.js.

## Contas a Pagar - Dashboard
![Lab.Contas - Dashboard](https://github.com/andressatt/learning-aspnetcore/blob/main/Lab.Contas/print/contas-pagar-dashboard.PNG?raw=true)

## Contas a Pagar - Listagem
![Lab.Contas - Cadastro](https://github.com/andressatt/learning-aspnetcore/blob/main/Lab.Contas/print/contas-pagar-listagem.PNG?raw=true)

## Contas a Pagar - Cadastro
![Lab.Contas - Cadastro](https://github.com/andressatt/learning-aspnetcore/blob/main/Lab.Contas/print/contas-pagar-cadastro-select.PNG?raw=true)

## Tipos de Conta a Pagar - Listagem
![Lab.Contas - Cadastro](https://github.com/andressatt/learning-aspnetcore/blob/main/Lab.Contas/print/tipos-conta-pagar-listagem.PNG?raw=true)

## Registrar
![Lab.Contas - Registrar](https://github.com/andressatt/learning-aspnetcore/blob/main/Lab.Contas/print/registrar.PNG?raw=true)
