﻿using Calabonga.EntityFramework;
using Calabonga.OperationResults;

namespace Calabonga.Framework.Demo
{
    internal class PeopleRepostiory : WritableRepositoryBase<Person, UpdateViewModel, CreateViewModel, PagedListQueryParamsBase>
    {
        public PeopleRepostiory(IEntityFrameworkContext context, IEntityFrameworkLogService logger, IEntityFrameworkMapper mapper)
            : base(context, logger, mapper)
        {
        }
    }
}