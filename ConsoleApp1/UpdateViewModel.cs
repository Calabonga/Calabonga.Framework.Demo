using System;
using Calabonga.EntityFramework;

namespace Calabonga.Framework.Demo
{
    internal class UpdateViewModel : IEntityId
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}