using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using miniprojeto_samsys.BLL.Services;
using miniprojeto_samsys.DAL;
using miniprojeto_samsys.DAL.Repositories.Shared;
using miniprojeto_samsys.DAL.Repositories;
using miniprojeto_samsys.Infrastructure.Interfaces.Repositories;
using miniprojeto_samsys.DAL.Repositories.Books;
using miniprojeto_samsys.DAL.Repositories.Authors;
using miniprojeto_samsys.Infrastructure.Entities.Authors;
using miniprojeto_samsys.Infrastructure.Entities.Books;

namespace miniprojeto_samsys.API{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DDDSample1DbContext>(opt =>
                opt.UseInMemoryDatabase("ProjectDB")
                .ReplaceService<IValueConverterSelector, StronglyEntityIdValueConverterSelector>());

            services.AddScoped<DDDSample1DbContext>();

            /*
            services.AddDbContext<DDDSample1DbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("Default"))
                .ReplaceService<IValueConverterSelector, StronglyEntityIdValueConverterSelector>());
            */

            ConfigureMyServices(services);

            services.AddControllers().AddNewtonsoftJson();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));



            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Seed(app);

            }

        public void ConfigureMyServices(IServiceCollection services)
        {

            services.AddTransient<IUnitOfWork,UnitOfWork>();

            services.AddTransient<IBookRepository,BookRepository>();
            services.AddTransient<BookService>();

            services.AddTransient<IAuthorRepository,AuthorRepository>();
            services.AddTransient<AuthorService>();

        }

        public static void Seed(IApplicationBuilder app)
            {
                // Get an instance of the DbContext from the DI container

                    var scope = app.ApplicationServices.CreateScope();
                    var context = scope.ServiceProvider.GetService<DDDSample1DbContext>();

                    // perform database delete
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    //... perform other seed operations

                    Author author1 = new Author("Autor1");
                    Author author2 = new Author("Autor2");

                    Book book1 = new Book("111","Livro1","25.00",author1.Id);
                    Book book2 = new Book("222","Livro2","30.00",author1.Id);
                    Book book3 = new Book("333","Livro3","45.99",author2.Id);

                    context.AddRange(author1, author2, book1, book2, book3);

                    context.SaveChanges();
                    context.Dispose();


                }

    }
}
