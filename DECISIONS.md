Responda objetivamente:

# 1. **Adequação da arquitetura** — CQRS com banco relacional + NoSQL é adequado para um sistema de biblioteca comunitária? Justifique. **Em que cenário você NÃO usaria essa arquitetura?**

A arquitetura utilizada CQRS com escrita no SQL Server e MongoDB para leitura e RabbitMQ para sincronização assíncrona entre os modelos. 
Sim, julgo adequado se a proposta for separar claramente as operações transacionais de escrita e das consultas/leitura otimizada.
O SQL Server é aplicado como a fonte de verdade e opereção dos empréstimos e devoluções para ter consistência transacional, aplicável também quando o dompinio tem operações de escrita com regras importantes
e consultas usando modelos desnormalizados (Livros, contento lista de empréstimos ativos por exemplo).

Para um sistema com alto fluxo de dados (escrita e leitura) eu usaria essa abordagem, mas não para um sistema pequeno sem muita concorrência, usuário e acesso, para não gastar recurso nem ter tanta complexidade
em algo simples e que pode até prejudicar a equipe de suporte, caso não tenha vivência com eventos e mensageria.


# 2. **Consistência eventual** — sua sincronização é eventual. O que acontece se um usuário solicita empréstimo logo após uma alteração, e o NoSQL ainda não atualizou? Como você lida com essa janela de inconsistência?

Como a sincronização é eventual, pode realmente haver um time pequeno entre a escrita e a atualização da informação para a leitura, como a consistência da operação é tratada no SQL Server, por exemplo, se o usuário
solicitar novamente uma operação de entrada ou saída de um livro a operação não é comprometida, mesmo como a base de leitura ainda sendo atualizada. Como o processo retorna o Id do Empréstimo na operação,
poderiamos adicionar uma mensagem informando ao usuário que pode haver um delay mínimo para a atualização do dados para consulta, isso para melhorar a experiência do mesmo.



# 3. **Localização das regras** — onde você colocou cada regra de domínio (ex.: reduzir `QuantidadeDisponivel`, impedir devolução dupla) e por que nessa camada?

As regras ficaram nas entidades Book e Loan, seguindo:

Na entidade 'Book', coloquei as regras relacionadas ao estoque do livro:

- Não permitir livro sem título ou livro sem autor.
- Não permitir quantidade negativa.
- Reduzir QuantityAvailable ao realizar empréstimo.
- Aumentar QuantityAvailable ao realizar devolução.
- Impedir empréstimo quando não há exemplares disponíveis.

Na entidade 'Loan', coloquei as regras do empréstimo:

- Criar empréstimo com status ativo.
- Associar o empréstimo a um livro.
- Registrar a data do empréstimo.
- Impedir devolução dupla.
- Registrar data de devolução.
- Alterar status para devolvido, quando ocorrer o retorno

Razão, um dos requisitos ser a modelagem de domínio rico e muito também pelo motivo de não ter as regras de negócio espalhadas em controllers, services ou repositórios dificultando a manutenção futura 
e impactos gerais.
O manipuladores da aplicação apenas orquestram os caos de uso carregando as entidades, usando os métodos do domínio e persistindo as alterações, mantendo também o principío da responsabilidade única.


# 4. **Concorrência** — como você garante que duas solicitações simultâneas do **último exemplar** não furam a regra de disponibilidade?

A regra mais sensível é impedir que duas solicitações simultâneas consigam emprestar o último exemplar disponível.

A solução atual protege a regra no domínio, mas em um ambiente com concorrência real seria necessário reforçar essa garantia no banco de dados. 
A abordagem recomendada seria utilizar controle de concorrência otimista com um campo de versão, como RowVersion, na entidade Book.

Nesse modelo, duas transações poderiam carregar o mesmo livro, mas somente a primeira conseguiria persistir a redução da quantidade. 
A segunda receberia uma exceção de concorrência do Entity Framework e deveria recarregar os dados, reavaliar a disponibilidade e retornar erro se não houver mais exemplares.

Outra alternativa seria usar uma atualização condicional diretamente no banco, reduzindo a quantidade apenas se QuantityAvailable > 0. 
Essa abordagem é eficiente, mas desloca parte da regra para a camada de persistência. Para manter o domínio rico, a melhor opção seria domínio + controle otimista por RowVersion.

Também teríamos a opção de usar controle de transação para enfileirar as solicitações ao banco de dados para que só execute a operação de empréstimo se realmente houver disponibilidade.

# 5. **Falha de sincronização** — se o evento que atualiza o NoSQL falhar, como você detecta, recupera e garante idempotência?

A sincronização com o MongoDB ocorre por eventos publicados no RabbitMQ e consumidos pelo Worker. Se o processamento de um evento falhar, o MassTransit/RabbitMQ permite retentativas e 
controle de mensagens com erro, também podemos registrar logs estruturados dos eventos processados e dos erros ocorridos. 
Mensagens que falham repetidamente podem ser encaminhadas para uma fila de erro, permitindo análise e reprocessamento posterior, mas deve contar com observabilidade e acompanhamento de um time 
de suporte, aqui, falando de um cenário real.

Para garantir idempotência, os Consumers foram modelados para atualizar o MongoDB usando identificadores estáveis, como BookId e LoanId. Por exemplo, a criação do livro no read model pode 
usar ReplaceOne com IsUpsert = true, evitando duplicidade caso o mesmo evento seja processado mais de uma vez.

Pensando num cenário real, o padrão outbox seria uma opção, pois gravariamos os eventos no banco da operação de escrita e posteriormente teriamos um serviço realizando o processo de 
publicação na fila no caso.

# 6. **Uso de IA** — onde usou IA, o que **aceitou**, o que **rejeitou** do que ela sugeriu, e **por quê**.

Utilizei IA como apoio na construção das configurações, na criação de objetos e nos ajustes de modelos. Tomei a decisão de usar arquitetura hexagonal por entender que é um modelo que facilita
o entendimento, é difundido e fácil de compartilhar com outras equipes se necessário e também defini quais seriam as camadas e serviços, algumas sugestões avaliadas, aceitas e ajustadas:

- Uso de Domain Events e Integration Events separados.
- Modelagem desnormalizada no MongoDB (atender a um dos requisitos)
- Events e Integration Events
- Criação de 3 consumers no mesmo worker (em grande escala eu separo)

E aqui algumas que foram rejeitadas/ajustadas devido ao objetivo:

- Não implementação de Health Check nesta etapa, pois não era requisito essencial e estava gerando complexidade adicional por conflito de pacotes.
- Não usei o Outbox Pattern na implementação atual, pois aumentaria a complexidade do teste, mas foi documentado como melhoria futura.
- Não foi usado CQRS de forma artificial, a leitura pelo MongoDB foi implementada como read model real, atualizado por eventos.
- Não foi mantida dependência direta da Application para o adapter de mensageria. A solução foi ajustada para usar uma abstração, IIntegrationEventPublisher.

A IA foi usada como ferramenta de apoio, mas as decisões finais foram avaliadas e ajustadas conforme os requisitos, o escopo do teste e a coerência arquitetural

# 7. **Trade-offs gerais** — liste as 3 decisões mais importantes que tomou, as alternativas que descartou e o motivo.

Mais importantes:

#. Cumprir os requisitos solicitados com o uso de recursos atuais como MessageBroker e microservio consumer
1. CQRS com SQL Server e MongoDB com sincronização usando Mensageria e Worker Consumer
2. Separação de responsabilidades, Eventos de Domínio e de Integração para a solução da integração dos dados do SQL para o MongoDB desnormalizado
3. Definição de modelo de arquitetura hexagonal e separar as responsabilidades, sem ter o vazamento de regra do domínio para outras camadas e manipuladores com funções específicas.

#. Descartes 

- Criar coleções normalizadas de livros e empréstimos e simular joins na aplicação.
	- Motivo: em CQRS, o read model deve ser otimizado para a consulta. A modelagem desnormalizada reduz complexidade de leitura, evita joins e entrega respostas mais próximas do formato consumido pela API.
- Não adicionar outros recursos neste momento para não adicionar complexidade desnecessária para o resultado final e o avaliador, mas colocado como observação para um projeto real ou evolução futura
- Manter escrita e leitura no SQL Server, mesmo sendo suficiente para uma aplicação com a complexidade proposta aqui no teste, adicionar o mongo db aleém de cumprir um requisito da avaliação 


## Considerações finais

A solução prioriza clareza arquitetural, separação de responsabilidades e aderência aos requisitos técnicos. O SQL Server garante consistência nas operações de escrita, 
o RabbitMQ desacopla a sincronização e o MongoDB oferece um modelo de leitura otimizado.

A arquitetura traz mais complexidade do que um CRUD tradicional, mas essa complexidade é justificada pelo objetivo do teste: demonstrar domínio rico, CQRS, 
mensageria, persistência relacional e NoSQL trabalhando de forma integrada.



