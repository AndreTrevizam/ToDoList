# ToDoList

Aplicação de lista de tarefas em .NET, organizada como monorepo com uma **API REST** e um **cliente desktop** multiplataforma.

- **API** — ASP.NET Core com autenticação JWT, controle de acesso por roles (RBAC) e persistência em SQLite via Entity Framework Core.
- **Desktop** — Avalonia UI com padrão MVVM (CommunityToolkit.Mvvm). Roda em Windows, macOS e Linux.
- **Shared** — DTOs compartilhados entre API e Desktop, garantindo que os dois lados falem o mesmo contrato.

> **Status:** a API está funcional. O Desktop ainda é o template inicial do Avalonia e não consome a API.

## Stack

| Camada | Tecnologia |
|---|---|
| Runtime | .NET 10 |
| API | ASP.NET Core (Controllers) |
| Banco | SQLite + Entity Framework Core 10 |
| Autenticação | JWT Bearer + `PasswordHasher` (ASP.NET Core Identity) |
| Desktop | Avalonia 12 + CommunityToolkit.Mvvm |

## Estrutura

```
ToDoList/
├── ToDoList.slnx               # solution com os três projetos
├── Directory.Build.props       # configurações comuns (net10.0, Nullable, ImplicitUsings)
└── src/
    ├── ToDoList.Api/           # API REST
    │   ├── Controllers/        # endpoints HTTP
    │   ├── Data/               # AppDbContext (EF Core)
    │   ├── Extensions/         # helpers de claims do token
    │   ├── Mappings/           # conversão entidade → DTO
    │   ├── Migrations/         # migrations do EF Core
    │   ├── Models/             # entidades do banco (TodoTask, User, UserRole)
    │   ├── Services/           # regras de negócio
    │   └── ToDoList.http       # requisições de exemplo
    ├── ToDoList.Shared/        # DTOs (sem dependências)
    └── ToDoList.Desktop/       # app Avalonia (Views, ViewModels)
```

Dependências entre projetos:

```
ToDoList.Api ─────┐
                  ├──► ToDoList.Shared
ToDoList.Desktop ─┘
```

API e Desktop não se referenciam: comunicam-se apenas via HTTP. As entidades do EF ficam restritas à API.

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- Ferramenta de migrations do EF Core:

  ```bash
  dotnet tool install --global dotnet-ef
  ```

## Instalação

**1. Clonar o repositório**

```bash
git clone https://github.com/AndreTrevizam/ToDoList.git
cd ToDoList
```

**2. Restaurar as dependências**

```bash
dotnet restore
```

**3. Configurar a chave de assinatura do JWT**

A chave fica fora do repositório, em [user-secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets). Ela precisa ter pelo menos 32 bytes:

```bash
dotnet user-secrets set "Jwt:Key" "$(openssl rand -base64 48)" --project src/ToDoList.Api
```

> No Windows sem `openssl`, use qualquer string aleatória longa (64+ caracteres).

Sem essa chave a API não inicia e exibe o comando acima no erro.

**4. Criar o banco de dados**

```bash
dotnet ef database update --project src/ToDoList.Api
```

Isso cria o arquivo `src/ToDoList.Api/todolist.db` com todas as tabelas.

## Como rodar

### API

```bash
dotnet run --project src/ToDoList.Api
```

A API sobe em `http://localhost:5002`. Para usar HTTPS (`https://localhost:7151`):

```bash
dotnet run --project src/ToDoList.Api --launch-profile https
```

Em desenvolvimento, o documento OpenAPI fica em `http://localhost:5002/openapi/v1.json`.

### Desktop

```bash
dotnet run --project src/ToDoList.Desktop
```

### Build de tudo

```bash
dotnet build ToDoList.slnx
```

## Endpoints

Todas as rotas têm o prefixo `/api/v1`.

| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| `POST` | `/auth/register` | Público | Cria usuário (role `User`) e retorna token |
| `POST` | `/auth/login` | Público | Autentica e retorna token |
| `GET` | `/tasks` | Autenticado | Lista as tasks do usuário logado |
| `POST` | `/tasks` | Autenticado | Cria task para o usuário logado |
| `GET` | `/tasks/all` | Admin | Lista as tasks de todos os usuários |
| `GET` | `/users` | Admin | Lista todos os usuários |

Respostas de erro relevantes: `401` (sem token ou token inválido), `403` (sem permissão para a rota), `409` (email já cadastrado).

### Exemplo

```bash
# registrar
curl -X POST http://localhost:5002/api/v1/auth/register \
  -H "Content-Type: application/json" \
  -d '{"name":"Maria","email":"maria@exemplo.com","password":"Senha123!"}'

# usar o token retornado
curl http://localhost:5002/api/v1/tasks \
  -H "Authorization: Bearer <token>"
```

O arquivo `src/ToDoList.Api/ToDoList.http` traz o fluxo completo (registro → login → tasks) e pode ser executado direto pelo VS Code (extensão REST Client), Rider ou Visual Studio.

## Autenticação e permissões

- Senhas são armazenadas com hash (`PasswordHasher`), nunca em texto puro.
- O token JWT expira em 60 minutos (configurável em `Jwt:ExpiresMinutes` no `appsettings.json`) e carrega o id, email, nome e role do usuário.
- O dono de uma task é sempre obtido do token, nunca do corpo da requisição.
- Todo usuário registrado recebe a role `User`. Não existe endpoint para virar admin — a promoção é feita direto no banco:

  ```bash
  sqlite3 src/ToDoList.Api/todolist.db \
    "UPDATE Users SET Role = 'Admin' WHERE Email = 'maria@exemplo.com';"
  ```

  Depois disso o usuário precisa fazer login novamente para receber um token com a nova role.

## Migrations

Após alterar as entidades em `Models/` ou o `AppDbContext`:

```bash
dotnet ef migrations add NomeDaMigration --project src/ToDoList.Api
dotnet ef database update --project src/ToDoList.Api
```
