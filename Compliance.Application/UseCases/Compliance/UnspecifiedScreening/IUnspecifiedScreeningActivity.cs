﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.Application.UseCases.Compliance.UnspecifiedScreening
{
    public interface IUnspecifiedScreeningActivity
    {
        Task<UnspecifiedScreeningResult> UnspecifiedProcessScreeningAsync(UnspecifiedScreeningCommand command);
    }
}
