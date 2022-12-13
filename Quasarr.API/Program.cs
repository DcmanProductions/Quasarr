namespace Quasarr.API
{
    public class Program
    {
        #region Fields

        private static int port = 1234;

        #endregion Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder =>
                {
                    builder.UseIISIntegration();
                    builder.UseContentRoot(Directory.GetCurrentDirectory());
                    builder.UseKestrel(options =>
                    {
                        options.ListenAnyIP(port);
                    });
                    builder.UseStartup<Startup>();
                }).Build().Run();
            }
        }

        #endregion Private Methods
    }

    internal class Startup
    {
        #region Public Methods

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpLogging();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Erorr/{0}");
                app.UseForwardedHeaders();
                app.UseHttpsRedirection();
            }

            app.UseMvc();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseDefaultFiles();
        }

        public void ConfigureServices(IServiceCollection service)
        {
            service.AddMvc(action =>
            {
                action.EnableEndpointRouting = false;
            });
        }

        #endregion Public Methods
    }
}