# Organização e Nomenclatura das Pastas na Estrutura Raiz do Projeto

A estrutura raiz do projeto é composta por 3 pastas: Api, Core e Infra. Entenda os conceitos de cada uma delas
na [documentação sobre a arquitetura aqui](./Arquitetura.md).

## Api

A pasta Api é responsável por conter os projetos que implementam os Adaptadores Primários da aplicação. Cada
projeto deve ser nomeado de acordo com o tipo da tecnologia do adaptador primário, segue alguns exemplos:

- **[NomeDaAplicacao].Api.Rest**
- **[NomeDaAplicacao].Api.GraphQL**
- **[NomeDaAplicacao].Api.KafkaConsumer**

## Core

A pasta Core é responsável por conter os projetos que orquestram e que contêm as regras de negócio da aplicação.
O projeto que orquestra a aplicação deve ser nomeado como **[NomeDaAplicacao].Core.Application** e o projeto que
contém as regras de negócio deve ser nomeado como **[NomeDaAplicacao].Core.Domain**.

## Infra

A pasta Infra é responsável por conter os projetos que implementam os adaptadores secundários da aplicação. Cada
projeto deve ser nomeado de acordo com o tipo de tecnologia do adaptador secundário, seguem alguns exemplos:

- **[NomeDaAplicacao].Infra.Persistence**
- **[NomeDaAplicacao].Infra.KafkaProducer**
- **[NomeDaAplicacao].Infra.RestIntegration**
- **[NomeDaAplicacao].Infra.SoapIntegration**

## Configurations

Com o intuito de que cada projeto seja responsável por sua própria configuração de injeção de dependências, a
pasta Configurations foi criada nos projetos presentes no Core e na Infra. A estrutura base da implementação
pode ser vista a seguir:

```csharp
public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
    {
        // TODO: Adicione suas injeções de dependência aqui
        return services;
    }
}
```

Note que para os outros projetos, o nome da classe e do método deve ser alterado para o nome do projeto, por exemplo,
para o projeto de Persistência, a classe deve ser nomeada como **PersistenceConfiguration** e o método como
**AddPersistenceConfiguration**.

## Next Steps

Nesse primeiro momento, estou adicionando à implementação apenas o projeto de API Rest (com swagger e health check)
e Persistência (ainda sem a configuração). A ideia é que tenhamos a evolução da implementação usando como exemplo o
desenvolvimento de algo que solucione um problema real (estou aberto a sugestões rs). Caso tenha algum feedback ou
sugestão, sinta-se à vontade para contribuir!
