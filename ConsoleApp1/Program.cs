using System;
using System.Linq;
using Autofac;
using Calabonga.EntityFramework;
using Calabonga.PagedListLite;

namespace Calabonga.Framework.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ApplicationDbContext>().As<IEntityFrameworkContext>();
            builder.RegisterType<Mapper>().As<IEntityFrameworkMapper>();
            builder.RegisterType<Logger>().As<IEntityFrameworkLogService>();
            builder.RegisterType<PeopleRepostiory>().AsSelf();
            var container = builder.Build();

            var repostiory = container.Resolve<PeopleRepostiory>();
            var context = container.Resolve<IEntityFrameworkContext>();
            if (!context.Database.Exists())
            {
                //Add items
                AddItems(repostiory);
            }

            // Getting pagedList
            Print(repostiory, ((ApplicationDbContext)context).People.Count());

            // Add demo 
            AddNewPerson(repostiory);

            // Update demo
            UpdatePerson(repostiory);
        }

        private static void UpdatePerson(PeopleRepostiory repostiory)
        {
            //You alwaiys have access to DbContext 
            var context = (ApplicationDbContext)repostiory.AppContext;
            var last = context.People.First();
            var id = last.Id;

            var viewModel = new UpdateViewModel
            {
                Id = id,
                Name = "Updated data for entity"
            };

            var operationResult = repostiory.Update(viewModel);
            if (operationResult.Ok)
            {
                Console.WriteLine("Updated successfully");
            }
        }

        private static void AddNewPerson(PeopleRepostiory repostiory)
        {
            var model = new CreateViewModel
            {
                Name = "JUST ADDED ENTITY"
            };

            var operationResult = repostiory.Add(model);
            if (operationResult.Ok)
            {
                Console.WriteLine("Successfully added");
                return;
            }
            Console.WriteLine(operationResult.Error);
        }

        private static void Print(PeopleRepostiory repo, int total)
        {
            var result = GetCollection<PersonViewModel>(repo, 1, total);
            Output(repo, result);
        }

        private static void AddItems(PeopleRepostiory repo)
        {
            var words = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Donec sed pede. Etiam ante purus, consectetuer id,bibendum fermentum, accumsan sed, nunc. Vestibulum dignissim, leo vel aliquam dignissim, nibh ligula cursus tortor,in vulputate neque lorem eu sem. Morbi nibh nulla, tempus ac, varius sed, porta sit amet, nulla. Proin laoreet.Phasellus quis tortor eu augue rutrum posuere. Suspendisse quis dolor vitae nisl fermentum rutrum. Etiam mi mauris,imperdiet sed, volutpat ac, ornare in, urna. Sed laoreet turpis nec sem. Nam consectetuer, tellus rhoncus laciniaeuismod, augue tellus consectetuer sem, a molestie tellus nisi a dolor. Nunc tempus ante sit amet arcu. Lorem ipsumdolor sit amet, consectetuer adipiscing elit. Aliquam vitae quam. Integer consectetuer lacinia ligula. Praesentadipiscing viverra libero. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Donec sed pede.".Split(' ');
            foreach (string t in words)
            {
                repo.Add(new CreateViewModel { Name = $"{t.ToUpper().PadRight(20, '.')} {DateTime.Now.ToLocalTime()}" });
            }
        }

        private static PagedList<T> GetCollection<T>(PeopleRepostiory peopleRepostiory, int pageIndex, int total)
        {
            Console.WriteLine($"Page: {pageIndex} of {total}");
            var op = peopleRepostiory.GetPagedResult<T, string>(pageIndex, 20, x => x.Name, SortDirection.Descending);
            if (op.Ok)
            {
                return op.Result;
            }
            Console.WriteLine("Something went wrong");
            throw op.Error;
        }

        private static void Output<T>(PeopleRepostiory repo, PagedList<T> result)
        {
            foreach (var item in result.Items)
            {
                if (item is IHaveName)
                {
                    Console.WriteLine(((IHaveName)item).Name);
                }
            }
            if (result.PageIndex < result.TotalPages)
            {
                var items = GetCollection<T>(repo, result.PageIndex + 1, result.TotalPages);
                Output(repo, items);
            }
        }
    }
}
