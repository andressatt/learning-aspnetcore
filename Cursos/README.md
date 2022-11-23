## 1) Criar novo projeto

      ASP.NET Core Web App (Model-View-Controller)

## 2) Adicionar NuGet Packages

* Microsoft.EntityFrameworkCore 6.0.11
* Microsoft.EntityFrameworkCore.SqlServer 6.0.11
* Microsoft.EntityFrameworkCore.Tools 6.0.11
* Microsoft.VisualStudio.Web.CodeGeneration.Design 6.0.11

## 3) Inserir o seguinte trecho em Program.cs

      builder.Services.AddDbContext<SchoolContext>(opt =>
      {
          opt.UseSqlServer(builder.Configuration.GetConnectionString("minhaConexao"));
      });

## 4) Criar a connection string no appsettings.json

      "ConnectionStrings": {
          "minhaConexao": "Data Source=localhost;Initial Catalog=MySchoolDB;Integrated Security=true;Trusted_Connection=True"
      }

# Passos para usar o Code Fisrt com EF Core:

* Criar o Model (ver Student.cs, Grade.cs)
* Criar o DbContext com o DbSet do Model (ver SchoolContext.cs)
* Criar controller com Scaffolding

      (botão direito na pasta Controllers > Add > Controller... > MVC Controller with views, using Entity Framework)

* Abrir Console do Gerenciador de Pacotes

      Add-Migration NomeDaMigration

      Update-Database