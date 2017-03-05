using System;
using System.Collections.Generic;

namespace Evorine.Identifiers
{
    public interface INestable<TObject, TID>
        where TObject : IIdentifier<TID>, INestable<TObject, TID>
    {
        TID ParentID { get; }

        IEnumerable<TObject> Members { get; set; }

        TObject Parent { get; }

        uint Order { get; }
    }
}