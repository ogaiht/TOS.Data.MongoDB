using System;
using System.Collections.Generic;
using TOS.Common.MongoDB;
using TOS.Common.Text.Semantics;

namespace TOS.Data.MongoDB
{
    public class CollectionNameProvider : ICollectionNameProvider
    {
        private static readonly object SyncLock = new object();

        private readonly IPluralizer _pluralizer;
        private readonly Dictionary<Type, string> _collectionNames;


        public CollectionNameProvider(IPluralizer pluralizer)
        {
            _pluralizer = pluralizer;
            _collectionNames = new Dictionary<Type, string>();
        }

        public string GetCollectionName<TModel>()
        {
            Type modelType = typeof(TModel);
            if (!_collectionNames.ContainsKey(modelType))
            {
                lock (SyncLock)
                {
                    if (!_collectionNames.ContainsKey(modelType))
                    {
                        object[] collectionName = modelType.GetCustomAttributes(typeof(CollectionNameAttribute), false);
                        string name;
                        if (collectionName != null && collectionName.Length == 1)
                        {
                            name = ((CollectionNameAttribute)collectionName[0]).Name;
                        }
                        else
                        {
                            name = _pluralizer.Pluralize(modelType.Name);
                        }
                        _collectionNames.Add(modelType, name);
                    }
                }
            }
            return _collectionNames[modelType];
        }
    }
}
