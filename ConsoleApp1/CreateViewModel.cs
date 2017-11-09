using System;
using Calabonga.EntityFramework;

namespace Calabonga.Framework.Demo
{
    internal class CreateViewModel : IEntityId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}