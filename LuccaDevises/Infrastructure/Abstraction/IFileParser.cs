﻿using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstraction
{
    public interface IFileParser
    {
        BaseData Parse();
    }
}