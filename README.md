# Sistema de Gestão de Ordens de Serviço

Sistema desktop desenvolvido em Windows Forms + .NET Framework 4.6 para gestão de Ordens de Serviço com controle financeiro, auditoria e relatórios gerenciais.

---

## Arquitetura

O projeto segue uma arquitetura em camadas com separação rígida de responsabilidades:

| Camada | Responsabilidade |
|--------|-----------------|
| `App.Domain` | Entidades, enums, regras de negócio puras |
| `App.Infrastructure` | Repositórios, UnitOfWork, conexão, logging |
| `App.Application` | Services, DTOs, Commands, orquestração de casos de uso |
| `App.UI` | Windows Forms, ReportViewer, ponto de entrada |

### Princípios aplicados

- `App.UI` conhece apenas `App.Application` — nunca referencia Infrastructure ou Domain diretamente
- `App.Application` expõe DTOs e Commands — a UI nunca manipula entidades de domínio
- Repositórios não contêm lógica de negócio — apenas acesso a dados
- Services centralizam todas as regras e validações
- `ServiceFactory` em `App.Application` compõe toda a infraestrutura internamente

---

## Decisões técnicas

### Acesso a dados

- Npgsql puro com `NpgsqlConnection` e `NpgsqlCommand` — sem ORM
- Parâmetros nomeados em todas as queries para prevenir SQL injection
- `using` statements garantem descarte correto de conexões e comandos
- `UnitOfWork` encapsula conexão e transação — toda operação que envolve múltiplas tabelas ocorre em uma única transação com rollback automático em falha

### Controle de concorrência otimista

- Campo `versao` (integer) em `ordens_servico`, incrementado a cada UPDATE
- O WHERE do UPDATE inclui `versao = @versao` — se outro usuário salvou antes, `ExecuteNonQuery` retorna 0 linhas afetadas
- O repositório lança `ConcorrenciaException` que sobe pela cadeia até a UI
- A UI recarrega a OS e exibe mensagem amigável ao usuário

### Auditoria

- Toda alteração de status, itens e valor total gera um registro em `auditorias`
- O snapshot JSON é gerado com `Newtonsoft.Json` sobre a entidade após a alteração
- A auditoria ocorre dentro da mesma transação — ou tudo persiste ou nada persiste

### Performance

- Todas as listagens usam LIMIT/OFFSET para paginação — nunca carrega tudo
- Itens da OS não são carregados na grid principal — apenas ao abrir a OS
- Índices em `documento`, `data_abertura`, `status` e `cliente_id`
- Partial index em OS com status Aberta ou Em andamento — as mais consultadas

### Relatório

- `RelatorioService` executa query com JOIN e agrupamento no banco
- O resultado é projetado em DTOs no `App.Application`
- A UI monta DataTables e os entrega ao ReportViewer como `ReportDataSource`
- Exportação para PDF via `LocalReport.Render("PDF")`

---

## Requisitos

- .NET Framework 4.6
- PostgreSQL 13 ou superior
- Visual Studio 2019 ou superior

### Pacotes NuGet

| Pacote | Versão |
|--------|--------|
| Npgsql | 4.1.14 |
| Newtonsoft.Json | 13.0.4 |
| Microsoft.ReportViewer.WinForms | 10.0.40219.1 |
| Microsoft.ReportViewer.Common | 10.0.40219.1 |

---

## Como rodar

### 1. Crie o banco de dados

Execute no psql ou pgAdmin:

    CREATE DATABASE gestao_os WITH ENCODING 'UTF8';

### 2. Execute o script SQL

Via terminal:

    psql -U postgres -d gestao_os -f database.sql

Ou abra o `database.sql` no pgAdmin, conecte no banco `gestao_os` e execute com F5.

### 3. Configure a string de conexão

Edite o `App.config` no projeto `App.UI`:

    <connectionStrings>
      <add name="PostgresConnection"
           connectionString="Host=localhost;Port=5432;Database=gestao_os;Username=postgres;Password=SUA_SENHA"
           providerName="Npgsql" />
    </connectionStrings>
    <appSettings>
      <add key="LogPath" value="logs\app.log" />
      <add key="UsuarioAtual" value="sistema" />
    </appSettings>

### 4. Compile e execute

Defina `App.UI` como projeto de inicialização e pressione F5.

---

## Estrutura de pastas

    src/
      App.Domain/
        Entities/         — Cliente, Servico, OrdemServico, OrdemServicoItem,
                            HistoricoStatus, Auditoria
        Enums/            — StatusOrdemServico, TipoPessoa

      App.Infrastructure/
        Data/
          Repositories/   — ClienteRepository, ServicoRepository,
                            OrdemServicoRepository, AuditoriaRepository,
                            RelatorioRepository
          ConnectionFactory.cs
          UnitOfWork.cs
        Exceptions/       — ConcorrenciaException, DocumentoDuplicadoException
        Logging/          — FileLogger

      App.Application/
        Commands/         — CadastrarClienteCommand, AtualizarClienteCommand,
                            CadastrarServicoCommand, AtualizarServicoCommand,
                            SalvarItensOsCommand, MudarStatusOsCommand
        DTOs/             — ClienteDto, ServicoDto, OrdemServicoDto,
                            OrdemServicoItemDto, RelatorioDto,
                            RelatorioGrupoDto, RelatorioItemDto
        Exceptions/       — ConcorrenciaException, DocumentoDuplicadoException
        Services/         — ClienteService, ServicoService,
                            OrdemServicoService, RelatorioService
        ServiceFactory.cs

      App.UI/
        Forms/
          Clientes/       — FormClientes, FormCadastroCliente
          Servicos/       — FormServicos, FormCadastroServico
          OrdemServico/   — FormOrdemServico, FormCadastroOS, FormEditarOS
          Relatorio/      — FormRelatorio, RelatorioReport.rdlc
        Program.cs

    database.sql          — Script completo de criação do banco
    README.md

---

## Referência de status

| Valor | Status |
|-------|--------|
| 1 | Aberta |
| 2 | Em andamento |
| 3 | Concluída |
| 4 | Cancelada |

## Referência de tipo de pessoa

| Valor | Tipo |
|-------|------|
| 0 | Física |
| 1 | Jurídica |

---

## Logs

Os logs são gravados em `logs\app.log` relativo ao diretório do executável. O caminho pode ser alterado no `App.config` via chave `LogPath`.

Formato de exemplo:

    [2026-05-05 10:32:14] [INFO]  Cliente 'Empresa ABC' cadastrado por sistema.
    [2026-05-05 10:33:01] [ERRO]  Conflito de concorrência na OS id=42.
      Exception: ConcorrenciaException: Esta OS foi alterada por outro usuário.
      StackTrace: ...
