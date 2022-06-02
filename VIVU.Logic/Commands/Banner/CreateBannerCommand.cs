﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VIVU.Shared.Absstraction;
using VIVU.Shared.Model;

namespace VIVU.Logic.Commands
{
    public class CreateBannerCommand : BannerModel,
        IAuditCommand, IRequest<CommonCommandResultHasData<BannerModel>>
    {
        [JsonIgnore]
        public string UserName { get; set; } = string.Empty;
    }
}
