# Livraria API
A Livraria API é uma aplicação desenvolvida em C# utilizando ASP.NET Core para gerenciar operações de uma livraria, incluindo vendas, pagamentos, produtos e clientes. 
Esta API permite a criação, leitura, atualização e exclusão de registros, além de facilitar a integração com sistemas externos para gerenciamento de inventário e transações financeiras.

### A API
Funcionalidades Principais
- Gerenciamento de Vendas: Criação de vendas com múltiplos produtos, cálculo automático do valor total e integração com métodos de pagamento.
- Gerenciamento de Produtos: Adição, atualização e remoção de produtos do catálogo da livraria.
- Gerenciamento de Clientes: Registro de clientes, atualização de informações e histórico de compras.
- Relatórios de Vendas: Geração de relatórios detalhados sobre as vendas realizadas, incluindo informações de produtos e pagamentos.

![swagger_API](https://github.com/l3gium/LivrariaAPI/assets/131935219/56dbcaef-76ee-4f5e-a5db-d87255244b60)

### Estrutura do Projeto
O projeto está organizado em camadas para facilitar a manutenção e escalabilidade:

- Controllers: Contêm os endpoints e manipulam as requisições HTTP.
- Interfaces: Guardam os métodos que serão usados nas repositories.
- Repositories: Interagem com o banco de dados para realizar operações CRUD.
- Models: Definem as estruturas de dados utilizadas pela aplicação.
  
### Extra
Essa foi minha primeira API desenvolvida sozinho, então estou aberto a críticas construtivas e sujestões de melhorias.
