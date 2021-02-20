using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoviewRecommend.Service.Interfaces;
using MoviewRecommend.Service.Tasks;
using NCrontab;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoviewRecommend.Service.Tasks
{
    public class GetTheMovieAllScheduleTask : BackgroundService
    {
        private ILogger _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        public GetTheMovieAllScheduleTask(
            IScheduleConfig<GetTheMovieAllScheduleTask> config, 
            ILogger<GetTheMovieAllScheduleTask> logger,
            IServiceScopeFactory serviceScopeFactory
            ) : base(config.CronExpression, config.TimeZoneInfo)
        {
            this._serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        //uygulama ilk host edildiğinde buradan bir kere başlar.
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetTheMovieAllScheduleTask 1 starts.");

            /*Burada using ile service instance alma sebebi ScheduleTask içerisinde IHostedService'in implemente
              edilmesi sebebiyle lifetimeın düzgün yönetilemiyor olmasıdır. 
              Burada kullanılan servsleri AddSingleton olarak register etmemiz halinde sorun ortadan kalkıyor ancak 
              bu da bizim istemediğimiz bir lifetime yöntemidir. Uyguma boyunca aynı nesne sorun oluşturabilir.*/

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _theMovieDbService = scope.ServiceProvider.GetService<ITheMovieDbService>();
                var _movieService = scope.ServiceProvider.GetService<IMovieService>();

                var movies = _theMovieDbService.GetAll();
                var mappingModel = _theMovieDbService.MappingModel(movies);

                
                foreach (var item in mappingModel)
                {
                    var movie = _movieService.GetSingle(x => x.Name == item.Name);
                    if (movie != null)
                    {
                        _movieService.Update(movie);
                        continue;
                    }
                    _movieService.Add(item);
                }

                _movieService.SaveChanges();
            }

            return base.StartAsync(cancellationToken);
        }

        //schedule olarak bu metota girer.
        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} GetTheMovieAllScheduleTask 1 is working.");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _theMovieDbService = scope.ServiceProvider.GetService<ITheMovieDbService>();
                var _movieService = scope.ServiceProvider.GetService<IMovieService>();

                var movies = _theMovieDbService.GetAll();
                var mappingModel = _theMovieDbService.MappingModel(movies);

                foreach (var item in mappingModel)
                {
                    var movie = _movieService.GetSingle(x => x.Name == item.Name);
                    if (movie != null)
                    {
                        _movieService.Update(movie);
                        continue;
                    }
                    _movieService.Add(item);
                }

                _movieService.SaveChanges(); _movieService.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetTheMovieAllScheduleTask 1 is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
