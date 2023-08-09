# Mentoria de um novo projeto .Net  
Com o Mestre Rafael Miranda.  

## Projeto de aprendizado  
- Fazer um CRUD de produtos:  
- Criação/Update/Delete  
- Sql sever  
- testes de integração  
- Usando docker  
- Testes unitários e integerados  
- Monitoria  
- RabbitMQ  

**Dia 1 - 03/08/2023**  
  Definição do escopo do projeto.  
  Criação das Models: foi criado um esboço das entidades Entity para o Id, produto e atributos, com a relação de um para muitos para o CRUD.   
  ````
  Entity:  
    - Id  
  Product:  
    - Sku  
    - Description  
    - Producer  
  Attribute:  
    - Name  
    - Value
   ````  

  **Dia 2 - 09/08/2023**  
    Orientação sobre Dockerfile e docker-compose.yml:  
    O que realmente necessitamos nesta etapa do projeto é criar o arquivo docker-compose.yml para termos um banco local, não a necessidade de criar uma imagem para aplicação com o DockerFile.  
    Também não é necessário que o compose tenha volumes, pois não há necessidade de persistir os dados na máquina.  
    Como ficou o docker-compose.yml:  
    ````
    version: '3.8'  
    services:  
    sql-server:  
    image: mcr.microsoft.com/mssql/server  
    environment:  
      SA_PASSWORD: Password1  
      ACCEPT_EULA: Y  
    ports:  
      - "1433:1433"
    ````
    Comando para executar o docker compose:  
    ````  
    docker compose up -d
    ````  
    Orientação sobre EF:  
    O que é preciso para configurá-lo?  
    Tendo as classes models criadas, devemos criar uma classe de contexto do banco de dados. Uma classe que estenda a classe DbContext. Para isso foi criado o construtor passando o 
    DbContextOptions e os DbSets que serão as tabelas criadas pelo EF.  
    Como ficou a classe extendida pelo DbContext:  
    
    ````
    public class DataBaseContext : DbContext  
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)  
        : base(options)  
        {}  
        public DbSet<Product> Products { get; set; }  
        public DbSet<Models.Attribute> Attributes { get; set; }  
    }  
    ````
