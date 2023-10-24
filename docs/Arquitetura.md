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

![Arquitetura Hexagonal: Estrutura de projeto adotada ](/docs/imgs/arch-4.png "Arquitetura Hexagonal: Estrutura de projeto adotada")

Continua...
