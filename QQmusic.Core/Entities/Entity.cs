using System;
using System.Collections.Generic;
using System.Text;
using QQmusic.Core.Interfaces;

namespace QQmusic.Core.Entities
{
    public abstract class Entity : IEntity
    {
        public string Id { get; set; }
    }
}
