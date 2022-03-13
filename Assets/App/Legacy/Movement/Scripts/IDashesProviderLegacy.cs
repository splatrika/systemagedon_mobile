using System;
using System.Collections.Generic;


namespace Systemagedon.App.Movement
{
    public interface IDashesProviderLegacy
    {
        public event Action DashesListUpdated;


        public IEnumerable<LegacyDash> GetDashes();
    }
}
