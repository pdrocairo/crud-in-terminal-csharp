# CRUD in Terminal - C# (Console)

Projeto de terminal para operações CRUD em C#, com foco em aprendizado e estrutura clara.

## Visão Geral
- **Linguagem:** C#
- **Formato:** Console application
- **Arquitetura:** DAO simples com modelos em `models/`, views em `views/` e templates em `templates/`.

## Estrutura do Projeto

- `Program.cs` - ponto de entrada
- `models/` - entidades e DAOs
- `views/` - classes de exibição
- `templates/` - UI em terminal

## Diagramas

Diagrama de Casos de Uso:

![Diagrama de Casos de Uso](docs\images\Casos%20de%20Uso.png)

Diagrama de Pacotes:

![Diagrama de Pacotes](docs\images\DiagramaPacotes.png)

Entidades (modelo de dados):

![Diagrama de Entidades](docs\images\entidades.png)

View e UI (fluxo de apresentação):

![View](docs\images\View.png)

![UI](docs\images\UI.png)

## Minhas Entidades

- `Cliente` — nome, email, telefone, id
- `Categoria` — id, nome, descrição
- `Produto` — id, nome, preço, categoriaId
- `Venda` — id, clienteId, data, total
- `VendaItem` — vendaId, produtoId, quantidade, preço

## Como executar

No Windows com .NET instalado:

```powershell
dotnet build
dotnet run
```

## Observações

Este README inclui diagramas salvos na raiz do projeto. Sinta-se à vontade para substituir ou atualizar as imagens conforme preferir.

---
Gerado por automação — ajuste conforme preferir.
