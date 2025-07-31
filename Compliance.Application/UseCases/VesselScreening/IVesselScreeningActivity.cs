using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

﻿namespace Compliance.Application.UseCases.VesselScreening
{
    public interface IVesselScreeningActivity
    {
        Task<VesselScreeningResult> VesselProcessScreeningAsync(VesselScreeningCommand command);
    }
}
