# Mentoria de um novo projeto .Net  
Com o Mestre Rafael Miranda.  

## Projeto de aprendizado  
- Fazer um CRUD de produtos:  
- Criação/Update/Delete  
- Sql sever  
- Usando docker  
- Testes unitários e integrados  
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
  Pacotes a serem utilizados para trabalhar com sql server e EF na aplicação .NET:  
  ````
  Install-Package Microsoft.EntityFrameworkCore
  Install-Package Microsoft.EntityFrameworkCore.SqlServer
  Install-Package Microsoft.EntityFrameworkCore.Tools
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
  No arquivo appsettings.json do seu projeto, adicione uma seção para configurar a conexão com o banco de dados SQL Server:  
  ````
    "ConnectionStrings": {
    "DefaultConnection": "Server=seu_servidor;Database=seu_banco_de_dados;User Id=seu_usuario;Password=sua_senha;"
    }
  ````
  Depois é necessário conectar o banco de dados na aplicação, como fazer isso? Aonde?
  Pegando como base a última versão do .NET no momento (.NET 7.0) a injeção de depêndencia fica na classe Program.cs.
  Para realizar a esta injeção podemos fazer assim:  
   ````
   builder.Services.AddDbContext<DataBaseContext>(options =>
   {
      options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
   });
   ````
   * Lembrando que este trecho deve ser adicionada antes do comando ``var app = builder.Build();`` 
  Depois para criar as migrations e criar o banco de dados. São executados os dois comandos abaixo no Packager Manager Console:  
   ````
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ````
   Ou de forma reduzida:  
   ````
   Add-Migration InitialCreate
   Update-Database
   ````
  Para realizar a relação muitos para muitos recorri a documentação da microsoft (EF), onde encontrei o exemplo para seguir com a implementação.  
  [link da doc](https://learn.microsoft.com/pt-br/ef/core/modeling/relationships/many-to-many)

  Exemplo das Classes:    
   ````
   public class Post
   {
      public int Id { get; set; }
      public List<Tag> Tags { get; } = new();
   }

   public class Tag
   {
      public int Id { get; set; }
      public List<Post> Posts { get; } = new();
   }

   public class PostTag
   {
      public int PostId { get; set; }
      public int TagId { get; set; }
   }
   ````
  Exemplo da configuração da relação muitos para muitos:  
   ````
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<Post>()
          .HasMany(e => e.Tags)
          .WithMany(e => e.Posts)
          .UsingEntity<PostTag>();
   }
   ````
  Explicação:  
  1)modelBuilder.Entity<Product>(): Isso diz ao Entity Framework que você está configurando a entidade Product.  

  2).HasMany(e => e.Attributes): Isso define que a entidade Product tem uma coleção de atributos (relação muitos para muitos). O parâmetro e => e.Attributes especifica a propriedade de navegação na entidade Product que representa essa relação.  

  3).WithMany(e => e.Products): Isso define que a entidade Attribute também tem uma coleção de produtos (relação muitos para muitos). O parâmetro e => e.Products especifica a propriedade de navegação na entidade Attribute que representa essa relação.  

  4).UsingEntity<ProductAttribute>(): Isso indica que você está usando uma entidade intermediária chamada ProductAttribute para representar a relação muitos para muitos entre Product e Attribute. Essa entidade intermediária é automaticamente criada pelo Entity Framework para gerenciar a relação entre as duas entidades principais.  

  Configurar também o DbSet na classe de contexto do database:  
   ````
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
   ````
 
