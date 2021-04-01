using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopersArticle.BLL.Utilities.Results
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
