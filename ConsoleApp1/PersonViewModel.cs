using System;

namespace Calabonga.Framework.Demo
{
    public class PersonViewModel : IHaveName
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}