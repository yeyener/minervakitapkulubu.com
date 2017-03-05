using Evorine.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Helpers
{
    public static class INestableExtensions
    {
        private static void setMembers<TObject, TID>(TObject parent, IEnumerable<TObject> all)
            where TObject : IIdentifier<TID>, INestable<TObject, TID>
        {
            parent.Members = all.Where(x => !x.ParentID.Equals(default(TID)) && x.ParentID.Equals(parent.ObjectID)).OrderBy(x => x.Order);
            foreach (var member in parent.Members)
                setMembers<TObject, TID>(member, all);
        }

        public static IEnumerable<TObject> Nest<TObject, TID>(this IEnumerable<INestable<TObject, TID>> collection)
            where TObject : IIdentifier<TID>, INestable<TObject, TID>
        {
            var parents = collection.Where(x => x.ParentID.Equals(default(TID))).OrderBy(x => x.Order);

            foreach (var item in parents)
                setMembers<TObject, TID>((TObject)item, collection.Cast<TObject>());

            return parents.Cast<TObject>();
        }
    }
}
