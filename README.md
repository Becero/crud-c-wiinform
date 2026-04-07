# CRUD C# WinForms - Projeto de Demonstracao e Ensino

Este projeto foi criado para **validar conhecimentos basicos de C# e .NET com Windows Forms**.
A ideia e praticar conceitos essenciais de um sistema real, mas com implementacao simples.

## Objetivo

Demonstrar na pratica:

- CRUD (Create, Read, Update, Delete)
- Organizacao de telas por menu
- Login, troca de senha e logout
- Perfil de usuario (admin e operador)
- Relatorios em tabela
- Movimentacao de estoque (entrada e saida)
- Persistencia em JSON (sem banco de dados)

## Tecnologias

- C#
- .NET 9
- WinForms
- JSON para armazenamento local

## Funcionalidades implementadas

- Cadastro de itens com campos: `Nome`, `Categoria`, `Quantidade`, `Preco`
- Busca por nome ou categoria
- Duplicar item
- Exportar itens para CSV
- Menu principal com telas de:
  - Dashboard
  - Cadastros
  - Movimentacoes
  - Financeiro
  - Relatorios
  - Usuario
  - Administracao
- Login obrigatorio ao abrir o sistema
- Troca de senha e logout
- Controle de usuarios com perfil
- Historico de movimentacoes de estoque

## Usuarios padrao

Para facilitar os testes, o sistema cria automaticamente:

- `admin` / `admin123`
- `operador` / `123456`

## Estrutura de dados

Os dados sao salvos em arquivos JSON no diretorio de execucao da aplicacao.

- `itens.json`: itens cadastrados
- `movimentos.json`: historico de movimentacoes
- `usuarios.json`: usuarios e senhas (hash)

## Como executar

1. Abra um terminal na pasta do projeto
2. Execute:

```bash
dotnet restore
dotnet run
```

## Roteiro de validacao (passo a passo)

1. Entrar com `admin`
2. Cadastrar alguns itens
3. Editar e excluir pelo CRUD
4. Fazer entrada e saida de estoque pelo menu `Movimentacoes`
5. Conferir tabela de movimentacoes
6. Abrir menu `Financeiro` e validar totais
7. Abrir `Relatorios` e validar estoque baixo
8. Exportar CSV
9. Trocar senha no menu `Usuario`
10. Fazer logout e login novamente

## Conceitos que este projeto ensina

- Separacao entre modelo e interface
- Tratamento basico de eventos em WinForms
- Fluxo de autenticacao simples
- Persistencia local sem banco
- Uso de listas e `DataGridView` para exibicao de dados
- Encadeamento de funcionalidades entre menus

## Limites atuais (propositalmente simples)

- Nao usa banco relacional
- Nao tem controle de permissao detalhado por tela/acao
- Nao possui testes automatizados
- Nao possui cadastro completo de categorias (somente agregacao)

## Proximos passos sugeridos para estudo

1. Migrar JSON para SQLite
2. Criar cadastro completo de categorias e fornecedores
3. Implementar filtros por periodo nos relatorios
4. Adicionar validacoes mais robustas
5. Criar testes unitarios para regras de negocio

## Licenca

Uso livre para estudo e demonstracao.