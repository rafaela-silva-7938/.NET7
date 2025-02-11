# API em .NET 7 com Entity Framework Code First, Mensageria com RabbitMQ, DDD, TDD e Arquitetura de Eventos
## Descrição
Esta API foi desenvolvida em .NET 7 e utiliza o padrão de arquitetura DDD (Domain-Driven Design), TDD (Test-Driven Development) e a arquitetura de eventos para garantir um código limpo, organizado e escalável.

A API também utiliza o Entity Framework Code First para o gerenciamento de banco de dados e o RabbitMQ como serviço de mensageria.
## Requisitos
- .NET SDK 7.0 ou superior
- RabbitMQ instalado e configurado
- SQL Server instalado e configurado
- Visual Studio 2022
## Como executar
1. Clone o repositório
2. Abra a solução `ApiPedidos.sln` no Visual Studio
3. No arquivo `appsettings.json`, configure a conexão com o banco de dados e o servidor do RabbitMQ
4. Abra o Package Manager Console e execute o comando `Update-Database` para criar as tabelas no banco de dados
5. Execute a aplicação