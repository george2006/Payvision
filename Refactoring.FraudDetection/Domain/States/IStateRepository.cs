using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Domain.States
{
    public interface IStateRepository
    {
        string StateNameByAbbreviature(string abbreviature);
    }
}
