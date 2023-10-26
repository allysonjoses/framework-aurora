# Arquitetura Hexagonal, panela velha é quem faz comida boa

Em tempos onde a Clean Architecture vem ganhando bastante notoriedade, iremos trabalhar aqui com uma arquitetura
que foi proposta em 2005 por Alistair Cockburn, chamada de Arquitetura Hexagonal. Você pode ver o artigo original
[aqui](https://alistair.cockburn.us/hexagonal-architecture/).

O conceito central da arquitetura Hexagonal é bem simples e direto:
**separar o código que implementa as regras de negócio do código que implementa os detalhes técnicos**.

![Arquitetura Hexagonal: Conceito central](/docs/imgs/arch-1.svg "Arquitetura Hexagonal: Conceito central")

Como podemos ver na imagem, o CORE da aplicação fica isolado de tecnologias e detalhes técnicos como banco de dados,
frameworks, diferentes implementações de mensageira, etc.
_Os benefícios dessa abordagem serão discutidos mais a frente, na fase de implementação_.

Além disso, é importante entender o Batman Side e o Robin Side, digo na verdade, Driving Side e Driven Side, e para facilitar
o entendimento, brinquei com a analogia dos super heróis da DC.

**Driving Side (Batman Side):** É o protagonista, quem começa a aventura e comanda as ações. Traduzindo literalmente,
**conduz** a aplicação do estado de inércia e a coloca em execução. Dessa forma, é o User Side (API), independente do tipo
deste usuário (API Rest, API GraphQL, CLI, etc).

**Driven Side (Robin Side):** É o coadjuvante e segue as ordens. Traduzindo literalmente, **é dirigido** pela aplicação.
Dessa forma, é o nosso Server Side (Infrasctructure), responsável por fornecer os recursos necessários para que a aplicação
possa funcionar.

## Ports and Adapters

Diferenciar esses dois lados é fundamental para compreender e distinguir os Primary Adapters dos Secondary Adapters.
Conforme a imagem abaixo, os Primary Adapters estão localizados no Driving Side e têm a função de expor as funcionalidades
da nossa aplicação a diferentes tipos de usuários, como, por exemplo, uma API Rest, uma API GraphQL, um CLI, entre outros.
Por outro lado, os Secondary Adapters estão no Driven Side e são encarregados de fornecer funcionalidades do mundo externo
para a nossa aplicação, como um banco de dados, um serviço de mensagens, um serviço de envio de e-mails, e assim por diante.

![Arquitetura Hexagonal: Ports e Adapters](/docs/imgs/arch-2.svg "Arquitetura Hexagonal: Ports e Adapters")

Seguindo o objetivo central abordado no início, os Adapters interagem com o CORE da aplicação através de interfaces, ou
"Ports", garantindo assim o isolamento da mesma. Essa separação se tornará ainda mais evidente quando discutirmos a
relação de dependências.

_É por essa razão que a Arquitetura Hexagonal também é conhecida como Arquitetura de Ports e Adapters._

![Arquitetura Hexagonal: Alistair Cockburn](/docs/imgs/arch-2.1.jpg "Arquitetura Hexagonal: Alistair Cockburn")

Uma observação interessante é que Alistair Cockburn, ao apresentar a Arquitetura Hexagonal, não estabeleceu uma estrutura
de camadas ou diretórios específica. Isso significa que podemos implementar a arquitetura de várias maneiras. Neste contexto,
optamos por seguir uma estrutura que visa simplificar a implementação e manutenção do código.

## Relação de dependências

![Arquitetura Hexagonal: Uma visão sobre as dependências](/docs/imgs/arch-3.svg "Arquitetura Hexagonal: Uma visão sobre as dependências")

### Driving Side

**Primary Adapters UTILIZAM os Ports**:

* Os Primary Adapters, localizados no Driving Side (Batman Side), desempenham um papel essencial na facilitação da
interação entre as funcionalidades da aplicação e os diferentes tipos de usuários ou sistemas externos. Eles atuam como
a "ponte" que conecta a aplicação a esses usuários ou sistemas.
* Os Primary Adapters implementam a lógica específica necessária para essa comunicação, mas não implementam as
funcionalidades reais da aplicação. Em vez disso, eles utilizam interfaces ou contratos chamados "Ports" para se comunicar
com o Core Application.
* Essas interfaces ou contratos (Ports) definem as funcionalidades que a aplicação oferece, permitindo que os Primary
Adapters interajam com a lógica central da aplicação sem precisar conhecer os detalhes da implementação real dessas funcionalidades.

**Core Application IMPLEMENTA os Ports**:

* O Core Application é encarregado de definir e implementar as funcionalidades reais que foram especificadas nos Ports,
localizados no lado do Batman (Driving Side). Ele é o guardião da lógica de negócios e das regras que tornam a aplicação funcional.
* O Core Application implementa as interfaces ou contratos definidos pelos Ports, permitindo que os Primary Adapters interajam
com a essência da aplicação sem depender da implementação real. Esse desacoplamento fortalece a manutenção e a flexibilidade da aplicação.

### Driven Side

**Secondary Adapters IMPLEMENTAM os Ports**:

* Os Secondary adapters, que estão no lado do Robin (Driven Side) são responsáveis por fornecer as implementações concretas
para as interfaces definidas pelos Ports deste lado. Eles são responsáveis por integrar a aplicação com recursos externos,
como bancos de dados, serviços de mensagens, outros sistemas, etc.

**Core Application**:

* O Core Application **UTILIZA** os Ports que estão no lado do Robin (Driven Side), ou seja, ele define as interfaces (Ports)
que descrevem como as interações com recursos externos devem ocorrer. Nesse processo, o Core Application não tem conhecimento
da implementação real dos Secondary Adapters, mas apenas define as interfaces (Ports) que espera que os Secondary Adapters
implementem. Isso promove um desacoplamento eficaz e facilita a manutenção e a evolução da aplicação.

## Estrutura de projeto

![Arquitetura Hexagonal: Estrutura de projeto adotada ](/docs/imgs/solution.png "Arquitetura Hexagonal: Estrutura de projeto adotada")

### Api

Com base na imagem acima, podemos observar uma clara separação através de pastas, alinhada com os princípios da arquitetura hexagonal.
A pasta Api receberá os Adapters do Driving Side, ou traduzindo literalmente, lado condutor, onde as ações e comandos serão originados.
A escolha pelo nome "Api" foi adotado já que simplifica a nomenclatura em comparação a Driving ou Batman, sem perder o significado.

#### O que deve ser implementado na pasta Api?

Aqui teremos a exposição das funcionalidades da aplicação para os diferentes tipos de usuários. Dessa forma, conforme sua necessidade,
você pode implementar uma API Rest, uma API GraphQL, um serviço SOAP, um Consumer de um serviço de mensageria, etc. No Framework Aurora,
iremos focar na implementação de uma API REST e de um Consumer, tendo vista cobrir os cenários mais corriqueiros nos dias atuais.

Os projetos presentes na pasta Api devem depender apenas do projeto Application, localizado na pasta Core.

### Core

Na pasta Core teremos a orquestração sos casos de uso, através do projeto Application, e conter as regras de negócio através do projeto Domain.
Na Application, encontraremos os contratos dos Ports, ou seja, as interfaces que serão implementadas pelos Adapters presentes na camada de
Infraestrutura. Além disso, nesta camada, teremos as implementações dos casos de uso que serão posteriormente expostos pela API. Em suma, a
Application orquestrará a execução dos casos de uso utilizando os Ports "out" e as regras de negócio presentes no Domain. A Application deve depender
apenas do projeto Domain.

Nesse modelo, o Domain fica responsável por conter as regras de negócio da aplicação, ou seja, as entities, value objects, service domain, etc.
Vale salientar que o Domain não deve conter nenhuma dependência com os demais projetos.

### Infra

A pasta Infra receberá os Adapters do Driven Side, ou traduzindo literalmente, lado dirigido. Seguindo a mesma lógica da pasta API,
a escolha pelo nome "Infra" foi adotado já que simplifica a nomenclatura em comparação a Driven Side, sem perder o significado.

#### O que deve ser implementado na pasta Infra?

Toda e qualquer comunicação externa, seja um banco de dados, um serviço de envio de e-mails, outras aplicações, producer de mensageria,
etc. O Framework Aurora concentrará seus esforços em fornecer implementações específicas para a comunicação com bancos de dados, sistemas
de mensageria e integração com APIs REST.

Geralmente, os projetos localizados na pasta Infra dependerão principalmente do projeto Application. No entanto, em casos especiais, podem
também depender do projeto Domain, como por exemplo, projetos de persistência de dados, onde a manipulação do domínio é necessária para o
mapeamento do banco de dados.

![Arquitetura Hexagonal: Visão sobre as dependências ](/docs/imgs/arch-4.svg "Arquitetura Hexagonal: Visão sobre as dependências")

### O maior erro que pode ser cometido

É crucial garantir que nenhuma complexidade da implementação dos adapters na Api e Infra vaze para o Core. Isso implica evitar (podem
existir exceções) dependência em bibliotecas específicas da API ou infraestrutura no núcleo, bem como impedir que os contratos de
integração com APIs externas se tornem parte do núcleo. Um exemplo ilustrativo disso é quando uma equipe adota o desenvolvimento em
inglês, mas consome um serviço externo que retorna dados em um formato diferente, como um JSON com propriedades em português. Essa
separação eficaz garante que o núcleo permaneça independente e focado em sua lógica principal, sem ser afetado pelas complexidades
das camadas periféricas.

Pontuarei na implementação de referencia os pontos que evidenciam essa separação.
