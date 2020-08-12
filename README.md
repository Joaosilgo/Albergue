# Albergue
Albergue Animal ESW

Métodos de entrega:  

* Atividades metodológicas (Atividades de Framework): 

* Comunicação 

* Planeamento 

* Modelação 

    * Análise dos requisitos 

    * Design 

* Construção 

    * Código 

    * Testes 

* Deployment 

 
# Objetivo da aplicação:  

## Fases do projeto: 

1 - Fase da análise e especificação de requisitos - Analisámos o documento de MSI  

2 - Fase do desenho de alto nível - detalhar o documento da fase anterior
              
 3 - Fases do desenho detalhado:  





Guião para a apresentação de ESW

**Grupo 03 - ESW05:**

-   Bruno Costa nº150221016

-   Inês Norberto nº150221061

-   Inês Reis nº 150221017

-   João Gomes nº 150221001

## Capa: 

	- Damos a conhecer a equipa e o projeto.** **

## Métodos de entrega:

### Atividades metodológicas (Atividades de Framework):

-   Comunicação

-   Planeamento

-   Modelação

-   Análise dos requisitos

-   Design

-   Construção

-   Código

-   Testes

-   Deployment

**     Objetivo da aplicação: **


### Fases do projeto:

1 - Fase da análise e especificação de requisitos - Analisámos o documento de
MSI

2 - Fase do desenho de alto nível - detalhar o documento da fase anterior

3 - Fases do desenho detalhado:

>   Sprint 1 - Utilizadores:

-   Requisitos Funcionais:

| Módulo            | ID          | Descrição                                                                                                                         | Prioridade  |
|-------------------|-------------|-----------------------------------------------------------------------------------------------------------------------------------|-------------|
| M2 – Utilizadores | **RF2.1**   | O sistema deverá permitir ao responsável do canil efetuar operações CRUD sobre a ficha de um utilizador e funcionário/voluntário. | *Must Have* |
|                   | **RF2.2**   | O sistema deverá permitir ao responsável do canil arquivar a ficha de um funcionário/voluntário.                                  | *Must Have* |
|                   | **RF2.6  ** | O sistema deverá permitir ao responsável do canil realizar as operações CRUD numa conta de utilizador.                            | *Must Have* |
|                   | **RF2.7**   | O sistema deverá permitir aos utilizadores e funcionários/voluntários autenticarem-se no sistema.                                 | *Must Have* |
|                   | **RF2.8**   | O sistema deverá permitir ao responsável do canil associar uma conta de utilizador a uma ficha de funcionário.                    | *Must Have* |

-   Burndown:

    (Não temos burndown para o sprint 1)

-   Tecnologias utilizadas:

    ![Resultado de imagem para GOOGLE LOGO](media/19e33d3f851badb808ebace78233e9d9.png)

    \- Google API OAuth 2.0: o protocolo OAuth 2.0 para autenticação e
    autorização.

-   \- SendGrid API: Utilizamos SendGrid como uma plataforma de comunicação
    com o cliente, para email transacional.

![](media/171b06401203d8eb9cde14b31be1f3be.png)

**Métodos **- Roles Authorizations( backbone da aplicação)

- Manipulação da Informação (Administrador Gestor de Recursos Humanos )

**Features **:

-   Manipulação do perfil de Utilizador.

![](media/2311d434e2311341e754a59113d793f9.png)

Figura 1 -

-   Imagem de Perfil -Todas as imagens da aplicação são guardadas seguramente
    por Bites na base de dados.

Bom para o backup da base de dados.

![](media/c699c206ad108120015f79b8f492dc0f.png)

Figura 2 -

![](media/36bf42f3e3017217bfa7805706ceacfd.png)

Figura 3 - Gestão de utilizadores

![](media/5286a8b27bb24a080fb519e2e3019c01.png)

Figura 4 - Lista de utilizadores

![](media/46be0c20ed3a5070acf0e4c354dda0b7.png)

Figura 5 - Lista de funcionários

>   Resumo: Sprint em que de inicio apenas Implementamos o IdentiTyUser e a
>   autenticação , no sprint 4 onde nos foi aonselhado melhorar as diversos
>   modulos fizemos acrescentos no modulo de utilizador, mais precisamente, na
>   parte de autorizações e man

>   **- Sprint 2 - Animais: **

-   Requisitos Funcionais:

| Módulo       | ID        | Descrição                                                                                                                       | Prioridade   |
|--------------|-----------|---------------------------------------------------------------------------------------------------------------------------------|--------------|
| M4 - Animais | **RF4.1** | O sistema deverá permitir ao responsável do canil efetuar operações CRUD sobre a ficha de um animal.                            | *Must Have*  |
|              | **RF4.2** | O sistema deverá permitir ao responsável do canil/funcionário aimpressão de uma ficha.                                          | *Must Have*  |
|              | **RF4.3** | O sistema deverá permitir ao responsável docanil/funcionário/voluntário filtrar a lista de fichas com determinadaespecificação. | *MustHave*   |
|              | **RF4.4** | O sistema deverá permitir ao responsável do canil ordenar as fichas dos animais por data de nascimento e por nome.              | *ShouldHave* |
|              | **RF4.5** | O sistema deverá permitir aos utilizadores avaliar os animais.                                                                  | *ShouldHave* |

-   Burndown:

    ![](media/95cc2d35f1596ac4fffa6d6956adf7f3.png)

-   Tecnologias utilizadas:

    \- Rotativa.AspNetCore: Package para converter HTML para PDF por WkHtmlToPdf
    para ASP.NET Core em aplicativos da Web do Azure (Imprimir Fichas de
    Animais)

**Métodos** - Roles Authorizations (Adminstrador e Gestor de Animais)

- Filtragem Por Datas;

- Imprimir Ficha de Animais;

- Manipulação das informações do modelo de Animais.

**Features:**

![](media/8405d8a13e29ec110e765c99de47a245.png)

Figura 6 – Visualização a partir do Administrador ou Gestor de animais

![](media/3056edc39e1a4122328a46d79214398b.png)

Figura 7 – Visualização a partir do Utilizador

![](media/726ea759d2d88ca5884a2a6bf1db1199.png)

Figura 8 – Visualização do Animal

>   **- Sprint 3 - Adoções:**

-   Requisitos Funcionais:

| Módulo       | ID        | Descrição                                                                                                                                               | Prioridade  |
|--------------|-----------|---------------------------------------------------------------------------------------------------------------------------------------------------------|-------------|
| M3 – Adoções | **RF3.1** | O sistema deverá permitir ao responsável do canil a realização das operações CRUD sobre uma ficha de saída                                              | Must Have   |
|              | **RF3.2** | O sistema deverá permitir ao responsável do canil juntar anexos a processos arquivados                                                                  | Should Have |
|              | **RF3.3** | O sistema deverá permitir ao responsável do canil/funcionário/voluntário a filtragem de fichas de saída arquivadas, por data e por raça do cão adotado. | Must Have   |
|              | **RF3.4** | O sistema deverá permitir ao responsável do canil/ funcionário filtrar por data de entrada dos animais recém-chegado.                                   | Must Have   |

-   Burndown:

    ![](media/3554426851aaaecf77619923ba09a970.png)

    z

-   Tecnologias utilizadas:

    ![https://www.smartspate.com/wp-content/uploads/2018/10/ASPDOTNET-640x400.png](media/5f1a79f98adf267c38c1e99ba10da14f.png)

    \- Rotativa.AspNetCore: Package para converter HTML para PDF por WkHtmlToPdf
    para ASP.NET Core em aplicativos da Web do Azure (Imprimir Fichas de Adoção)

\- SendGrid API: Utilizamos SendGrid como uma plataforma de comunicação com o
cliente, para email transacional (Notificação de Utilizadores quanto ao estado
da aplicação)

**Métodos** - Roles Authorizations( Adminstrador e Gestor de Adoções)

- Imprimir Ficha de Adoção

- Manipulação das informações do modelo de Adoções.

	   - Enviou de Email Transacionais a partir de Template do Sendgrid

**Features:**

![](media/d9784034d5a33ac3b2c978a987c87c69.png)

Figura 9 – Adoções – Visualização por Administrador

![](media/e25eb51290287ff80adee885fb21d141.png)

Figura 10– Adoções – Visão por Utilizador (Membro)

![](media/b2c7eb3d5c5d0d3a6e4cb4f1aab82e8a.png)

Figura 11 – Ficha de Adoção

![](media/dc85152891c645b20123173bbd377a21.png)

Figura 12 – Email template

![](media/fb3b15901c4fa4114c8cc93b59b1b209.png)

Figura 13 – Adoções – Template SendGrid

>   **- Sprint 4 - Stock**

-   Requisitos Funcionais:

| Módulo     | ID        | Descrição                                                                                                                                                             | Prioridade |
|------------|-----------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------|
| M1 – Stock | **RF1.1** | O sistema deverá permitir ao responsável do canil a realização das operações CRUD sobre um produto.                                                                   | Must Have  |
|            | **RF1.2** | O sistema deverá permitir ao responsável do canil / funcionário alterar a quantidade de um produto, enviando uma notificação quando um produto está abaixo do limite. | Must Have  |
|            | **RF1.3** | O sistema deverá permitir ao responsável do canil / funcionário registar os produtos necessários no site.                                                             | MustHave   |
|            | **RF1.4** | O sistema deverá permitir ao responsável do canil / funcionário destacar produtos em excesso de quantidade.                                                           | ShouldHave |

-   Burndown:

    ![](media/1ecd62a4b1bc97a3841703c303470541.png)

-   Tecnologias utilizadas:

    \- SendGrid API- Utilizamos SendGrid como uma plataforma de comunicação com
    o cliente, para email transacional (Notificação de Gestores de Stock quanto
    ao quantidade Minima de Produto)

![Resultado de imagem para SENDGRID LOGO](media/f3a78eded3574526d39188cf5b883454.png)

**Métodos** - Roles Authorizations( Administrador e Gestor de Stock)

- Manipulação das informações do modelo de Stock.

	   - Enviou de Email Transacionais a partir de Template do Sendgrid(opção)

- Requisitar Produto com Notificação de Quantidade Minima (feature que se
destaca neste modulo)

**Features:**

![](media/cbf7b3d5672d021a7288939a8a506666.png)

Figura 14 – Stock de produtos (Gestor de Stock e Administrador)

![](media/878086885ed036caa18bd26f327452df.png)

Figura 15 - Email de notificação de Stock mínimo

**Resumo: **Neste Sprint realizámos todos os requisitos levantados do stock, e
por conselho do nosso cliente fizemos melhorias à aplicação relativamente ao
módulo de Utilizadores, onde começamos a perceber a importância do tratamento
deste (Listas De Utilizadores/Lista de Funcionários) e manipulação do mesmo.

>   **- Sprint 5 - Estatísticas**

-   Requisitos Funcionais:

| Módulo            | ID        | Descrição                                                                              | Prioridade  |
|-------------------|-----------|----------------------------------------------------------------------------------------|-------------|
| M5 – ESTATÍSTICAS | **RF5.1** | Visualizar Métrica de Perfil Completo (Módulo Utilizadores)                            | Must Have   |
|                   | **RF5.2** | Visualizar Métrica de número de animais por Raça (Módulo Animais)                      | Should Have |
|                   | **RF5.3** | Visualizar Métrica de número de adopções por estado de processo (Módulo Adoções)       | Must Have   |
|                   | **RF5.4** | Visualizar Métrica de quantidade de produto por tipo de produto (Módulo Stock)         | Must Have   |
|                   | **RF5.5** | Implementação de métrica consoante o número de visualizações de animal (Módulo Animal) | Must Have   |

-   Burndown:

    ![](media/431123422d6ebeef7e042187dfa50ec8.png)

-   Tecnologias utilizadas:

# \- Gráficos JavaScript e mapas Biblioteca de programação para todos os seus necessidades de visualização de dados.

**Features:**

-   Adoções- estatística para saber o estado de todas as adoções não arquivadas
    (Authorization: Administrador e Gestor de Adoções)

    ![](media/424fc1342d314f8d418de82554d582ac.png)

Figura 16 -

-   Animais-estatística para saber as Raças de todos os Animais não arquivados
    (Adotados) -(Authorization: Administrador e Gestor de Animais)

-   Indiação do numero de vizualizações de Animal

    ![](media/00525744e8cb1d46f2aebcae3d223269.png)

Figura 17 -

![](media/4aa2298fbe2a5633f6683e64c5ea6230.png)

Figura 18 -

-   Stock-estatística para conhecimento de quantidades de todos os Tipos de
    Produto -(Authorization: Administrador e Gestor de Stock)

![](media/0ee5f95e36665cb94e161b84b4bb6c63.png)

Figura 19 -

-   Utizadores - Dashboard para indicação do (Profile Completation)

    ![](media/f67f1e0155264af3317635748b92f0c1.png)

Figura 20 -

-   DashBoard -Visualização do Estado Global da Empresa

    -Utilizadores- Mostra o número de Utilizadores (Membros) que não sejam
    funcionários do site.

    - Animais- Mostra o número de Animais que não estejamarquivados.

    - Adoções- Mostra o número de Adoções que não estejam arquivadas.

    - Stock- Mostra Todos os tipos de Stock

![](media/d5f52134ad763ce970a954af6f62ae27.png)

Figura 21 -

**Resumo: **Apesar de ser o de menor duração, foi o que em termos de
desenvolvimento foi mais fácil de implementar, talvez por ter sido aquele que
demostramos graficamente a informação dos requisitos que gerámos nos sprints
anteriores.

**Resumo Final: **

**Métodos** – Roles Authorizations( Engloba todos as Roles Dividido por cargos)

\- **Tabela com os testes realizados:**

-   Testes de browser - verifica se são compatíveis nos browsers (colocados no
    Trello)

-   Testes Automação (desenho detalhado)

-   Testes de aceitação - verifica se as funcionalidades estão de acordo com os
    requisitos (cliente)

-   Testes unitários - verifica que se os métodos testam o que é suposto (na
    fase de testes do sprint por modulo)

-   Testes de integração - garante a ligação entre os módulos (vê se estão
    integrados)

-   Testes de sistema - testa o sistema final da aplicação como um todo

-   Testes de usabilidade - verifica a facilidade que o utilizador tem de
    compreender e manipular o site

    (Ver anexo abaixo)

-   Testes de carga - o limite de dados processados pelo software

![](media/bb20cce05689466d463ca547aca4b029.png)

(Teste Inconclusivo)

	- Mostrar o diagrama de classes de todos os sprints

	- Mostrar o diagrama de navegabilidade do projeto

Core:

**Mostrar a hierarquia de autorização dos Roles:**

![](media/4b84af9ffcc6c1224ee6cf5584a5812e.png)

**Nível 1:**

-   Acesso Máximo de informaçao e Autorização

**Nível 2:**

-   Gestor de Recursos Humanos- Irá ter acesso ao modulo de Utilizadores e á
    gestão do mesmo

-   Gestor de Animais- Irá ter acesso ao modulo de Animais e á gestão do mesmo

-   Gestor de Stock- Irá ter acesso ao modulo de Stock e á gestão do mesmo

-   Gestor de Adoções- Irá ter acesso ao modulo de Adoções e á gestão do mesmo

**Nível 3:**

-   Acesso em stand by não tem acesso a nenhuma informção pois está á espera de
    atribuição de  cargo

-   Funcionário

    **Nível 4:**

-   Nivel básico de acesso equiparado a utilizador membro do site poderá ver
    todos os animais sem manipulação, e  poderá efectuar um pedido de adoção

-   Utilizadores (Registados)

**Nível 5:**

-   Nivel mais básico de acesso Visitante comum do site poderá ver todos os
    animais Utilizadores (Não Registados)

    **Resumo:** Componente Core da Aplicação-

    Árvore (figura acima) o backbone da nossa aplicação

    Com este Modelo:

    Flexibilidade: Flexibilidade na estrutura empresarial (A nossa aplicação
    está feita para desconhecer o numero de trabalholhadores e adptar-se ao
    mesmo )

    Entrega De um produto final (entrega de uma productKey(email,password) de um
    super admnistrador e a partir daí o cliente estrutura e manipula o seio da
    empresa)

    Nivel de Acesso Restrito a diversos seore(Roles) da empresa

![](media/7aeac750c9b88a0698c37eec2918c427.png)

**Diagrama de classes total:**

>   **Mostrar o vídeo da aplicação**

**Secção:1**

**Pergunta 1:**

![](media/7d111a61050b0ed1806d4e6b46b6585d.png)

**Respostas:**

![C:\\Users\\joaos\\AppData\\Local\\Microsoft\\Windows\\INetCache\\Content.MSO\\7483C1BA.tmp](media/89abcf275bad5b09b6c5f273bc7caaf4.png)

**Secção:1**

**Pergunta 2:**

![](media/65b255f8efe970a0830b5f393237ea48.png)

**Respostas:**

![C:\\Users\\joaos\\AppData\\Local\\Microsoft\\Windows\\INetCache\\Content.MSO\\5B6321D8.tmp](media/242fa762734c444f31f1b52e3fb54330.png)

**Secção:1**

**Pergunta 3:**

![](media/ed36f09ef9b735b908359f754ece80ed.png)

**Resposta:**

![C:\\Users\\joaos\\AppData\\Local\\Microsoft\\Windows\\INetCache\\Content.MSO\\C3F45DA6.tmp](media/de8a51473daf168acfb4b53a57267fd8.png)

**Secção 2**

**Pergunta1:**

![](media/fc6356d5bca12b9c56a277b9061b8874.png)

**Respostas:**

![C:\\Users\\joaos\\AppData\\Local\\Microsoft\\Windows\\INetCache\\Content.MSO\\9462E0A4.tmp](media/7011f6af25da0374d9c3676330949c67.png)

**Secção 2**

**Pergunta 2:**

![](media/7f175575e0f4e8cf8344d228c72c6305.png)

**Respostas:**

![C:\\Users\\joaos\\AppData\\Local\\Microsoft\\Windows\\INetCache\\Content.MSO\\5DFAA252.tmp](media/11eb8f3655f42b7f222b2c999f7e3927.png)

**Secção 3**

**Pergunta 1:**

![](media/481f4de51f857d24134c1d7c850b85e7.png)

**Respostas:**

![C:\\Users\\joaos\\AppData\\Local\\Microsoft\\Windows\\INetCache\\Content.MSO\\FCD40630.tmp](media/66968aae54136a2b26cb4885684762c4.png)

**Secção 3**

**Pergunta 2:**

![](media/70c2bbd2b9b0ea4706078dc75924d156.png)

**Respostas:**

![C:\\Users\\joaos\\AppData\\Local\\Microsoft\\Windows\\INetCache\\Content.MSO\\C4CABBBE.tmp](media/86e7c187262456d661182d08bfff4158.png)

**Secção 4**

**Pergunta 1:**

![](media/c733691d07954183dcf84b9d107d9ada.png)

**Resposta:**

![C:\\Users\\joaos\\AppData\\Local\\Microsoft\\Windows\\INetCache\\Content.MSO\\E8F79E7C.tmp](media/bfdbf6f62eeba8b9f6eafa0f186c08f7.png)

**Secção 5**

**Pergunta 1:**

![](media/b8f3f27be162e41a51d3f73ee96f5d9f.png)

![](media/c54062c770cd39c97f3258200b67c2d6.png)


 
