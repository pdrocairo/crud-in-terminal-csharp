# 🛍️ CRUD em Terminal - C# com .NET 10

Um sistema completo de **CRUD em terminal** desenvolvido em C# com .NET 10, oferecendo funcionalidades robustas de gerenciamento de clientes, produtos, categorias e vendas com autenticação, carrinho de compras e painel administrativo.

![.NET](https://img.shields.io/badge/.NET-10-blueviolet)
![Language](https://img.shields.io/badge/Language-C%23-green)
![License](https://img.shields.io/badge/License-MIT-blue)

---

## ✨ Características Principais

### 👥 Sistema de Autenticação
- ✅ Autenticação com email e senha
- ✅ Dois níveis de acesso: **Admin** e **Cliente**
- ✅ Criação de novas contas de cliente
- ✅ Admin padrão pré-configurado

### 🛒 Funcionalidades do Cliente
- 📦 Listar todos os produtos disponíveis
- 🛍️ Adicionar produtos ao carrinho
- 👁️ Visualizar carrinho com detalhes (quantidade, preço unitário, subtotal)
- ✅ Finalizar compra e confirmar pedido
- 📋 Consultar histórico completo de pedidos realizados

### 🔧 Funcionalidades do Administrador

**Gerenciamento de Clientes**
- ➕ Adicionar novo cliente
- 📊 Listar todos os clientes cadastrados
- ✏️ Atualizar dados de cliente
- ❌ Excluir cliente

**Gerenciamento de Produtos**
- ➕ Adicionar novo produto com categoria
- 📊 Listar todos os produtos
- ✏️ Atualizar informações de produto
- ❌ Excluir produto
- 🏷️ Aplicar descontos em produtos
- 📈 Reajustar preços

**Gerenciamento de Categorias**
- ➕ Adicionar nova categoria
- 📊 Listar categorias
- ✏️ Atualizar categoria
- ❌ Excluir categoria

**Relatórios**
- 📊 Visualizar todas as vendas realizadas
- 📝 Detalhes completos de itens por venda
- 💰 Cálculo automático de totais

---

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Versão | Descrição |
|-----------|--------|-----------|
| **.NET** | 10 | Framework base |
| **C#** | Latest | Linguagem de programação |
| **Console** | Built-in | Interface de linha de comando |
| **DAO Pattern** | - | Camada de acesso aos dados |

---

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

## 📁 Estrutura do Projeto

```
crud-in-terminal-csharp/
│
├── models/                          # Modelos de dados e DAO
│   ├── Cliente.cs                   # Modelo de cliente
│   ├── Produto.cs                   # Modelo de produto
│   ├── Categoria.cs                 # Modelo de categoria
│   ├── Venda.cs                     # Modelo de venda
│   ├── VendaItem.cs                 # Modelo de item de venda
│   ├── ClienteDAO.cs                # Acesso a dados de clientes
│   ├── ProdutoDAO.cs                # Acesso a dados de produtos
│   ├── CategoriaDAO.cs              # Acesso a dados de categorias
│   ├── VendaDAO.cs                  # Acesso a dados de vendas
│   └── VendaItemDAO.cs              # Acesso a dados de itens de venda
│
├── views/
│   └── View.cs                      # Lógica de negócio e fluxo de menus
│
├── templates/
│   └── UI.cs                        # Interface com usuário e exibição
│
├── Program.cs                       # Ponto de entrada da aplicação
│
├── README.md                        # Este arquivo
└── .gitignore                       # Arquivos a ignorar no Git
```

---

## 🚀 Como Executar

### Pré-requisitos

- **[.NET 10 SDK](https://dotnet.microsoft.com/download)** instalado
- **Visual Studio 2022** ou **Visual Studio Code** com extensão C#
- **Git** (opcional, para clonar o repositório)

### Passos de Instalação

#### 1️⃣ Clone o repositório
```bash
git clone https://github.com/seu-usuario/crud-in-terminal-csharp.git
cd crud-in-terminal-csharp
```

#### 2️⃣ Restaure as dependências
```bash
dotnet restore
```

#### 3️⃣ Compile o projeto
```bash
dotnet build
```

#### 4️⃣ Execute a aplicação
```bash
dotnet run
```

---

## 📖 Como Usar

### 🔑 Credenciais Padrão

**Admin:**
```
Email: admin
Senha: admin
```

### 📱 Menu Inicial

```
=======TELA INICIAL========
1. Login
2. Criar Conta
3. Sair 
```

### 👤 Fluxo de Cliente

#### Exemplo: Realizar uma compra

1. **Tela Inicial** → Selecione "1. Login"
2. **Login** → Digite email e senha
3. **Menu Principal do Cliente:**
   ```
      =========MENU PRINCIPAL=========
      1. Listar produtos
      2. Inserir produtos no Carrinho
      3. Ver Carrinho
      4. Finalizar Compra
      5. Meus Pedidos
      6. Sair
   ```
4. **Selecione "2"** → Escolha um produto e quantidade
5. **Selecione "3"** → Visualize seu carrinho
6. **Selecione "4"** → Finalize a compra

#### Exemplo: Criar uma nova conta

1. **Tela Inicial** → Selecione "2. Criar Conta"
2. Preencha os dados solicitados:
   - Nome completo
   - Email
   - Telefone
   - Senha
3. Conta criada com sucesso!
4. Faça login com suas credenciais

---

## 🔐 Fluxo do Administrador

### Menu Principal do Admin

```
=========MENU ADMIN=========
----- Clientes -----
1. Inserir  2. Listar  3. Atualizar  4. Excluir
----- Categorias -----
5. Inserir  6. Listar  7. Atualizar  8. Excluir
----- Produtos -----
9. Inserir  10. Listar  11. Atualizar  12. Excluir
13. Listar Vendas  14. Aplicar Desconto  15. Reajustar Preco  16. Sair
```

---

## 🔍 Funcionalidades Detalhadas

### 🛒 Carrinho de Compras

- **Adicionar múltiplos produtos** de diferentes categorias
- **Visualizar detalhes:** ID, Nome do Produto, Quantidade, Preço Unitário, Subtotal
- **Cálculo automático** do total
- **Atualizar estoque** automaticamente ao finalizar compra

### 💾 Persistência de Dados

Todos os dados são salvos e recuperados através do padrão **DAO (Data Access Object)**:
- Clientes
- Produtos
- Categorias
- Vendas e itens de venda

### 🎯 Validações

- ✅ Email único para novos clientes
- ✅ Validação de estoque
- ✅ Validação de quantidade
- ✅ Validação de porcentagens (0-100)
- ✅ Autenticação obrigatória
- ✅ Mensagens de erro claras

---

## 🖥️ Interface do Usuário

A aplicação oferece:

- **Menus intuitivos** e bem organizados
- **Pausas automáticas** entre operações para melhor visualização
- **Mensagens de confirmação** para cada ação
- **Listagens formatadas** e fáceis de ler
- **Navegação clara** entre seções

---

## 📊 Exemplo de Listagem de Produtos

```
Id: 1 | Descrição: Notebook | Preço: 2500.00 | Estoque: 10 | Categoria: 1
Id: 2 | Descrição: Mouse | Preço: 50.00 | Estoque: 100 | Categoria: 2
Id: 3 | Descrição: Teclado | Preço: 150.00 | Estoque: 50 | Categoria: 2
```

---

## 📝 Exemplo de Relatório de Vendas

```
Data: 01/12/2024 | Total: 2600.00 | Cliente: João Silva
  Item ID:1 | Produto: Notebook | Qtd: 1 | Preço: 2500.00 | Subtotal: 2500.00
  Item ID:2 | Produto: Mouse | Qtd: 1 | Preço: 50.00 | Subtotal: 50.00
  Total calculado: 2550.00
```

---

## 🔄 Fluxo de Dados

```
┌─────────────┐
│   Usuario   │
└──────┬──────┘
       │
       ▼
┌─────────────┐
│   UI.cs     │ ◄─── Exibe menus e lê inputs
└──────┬──────┘
       │
       ▼
┌─────────────┐
│  View.cs    │ ◄─── Lógica de negócio
└──────┬──────┘
       │
       ▼
┌─────────────┐
│  *DAO.cs    │ ◄─── Acesso aos dados
└──────┬──────┘
       │
       ▼
┌─────────────┐
│  Models     │ ◄─── Estrutura de dados
└─────────────┘
```

---

## 🐛 Troubleshooting

### Problema: Aplicação não inicia
**Solução:** Verifique se .NET 10 está instalado
```bash
dotnet --version
```

### Problema: Dados não são salvos
**Solução:** Verifique se a pasta de dados tem permissões de escrita

### Problema: Erro ao fazer login
**Solução:** Verifique se o email e senha estão corretos

---

## 📚 Padrões de Design Utilizados

- **MVC (Model-View-Controller):** Separação entre dados, lógica e apresentação
- **DAO (Data Access Object):** Abstração do acesso aos dados
- **Singleton:** Utilizados nos DAOs para gerenciar dados

---

## 🎯 Melhorias Futuras

- [ ] Banco de dados (SQL Server, PostgreSQL)
- [ ] Interface gráfica (Windows Forms, WPF)
- [ ] Autenticação com JWT
- [ ] API REST
- [ ] Relatórios em PDF
- [ ] Sistema de notificações
- [ ] Dashboard administrativo
- [ ] Integração com sistemas de pagamento

---

## 📄 Licença

Este projeto está licenciado sob a licença **MIT**. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 👤 Autor

**PEDRO CAIRO BERNARDO DE OLIVEIRA**

- GitHub: [@pdrocairo]([https://github.com/seu-usuario](https://github.com/pdrocairo))
- Email: pedro.cairo21@gmail.com

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Para contribuir:

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

---

## 📞 Suporte

Se encontrar problemas ou tiver dúvidas, abra uma **Issue** no repositório.

---

## ⭐ Se gostou do projeto, deixe uma estrela!

```
⭐ ⭐ ⭐ ⭐ ⭐
```

---

**Última atualização:** Maio de 2026
