using System;
using Calabonga.EntityFramework;

namespace Calabonga.Framework.Demo
{
    public class UpdateViewModel : IEntityId
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}