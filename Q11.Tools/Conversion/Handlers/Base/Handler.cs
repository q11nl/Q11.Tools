using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q11.Tools.Conversion.Pocos;

namespace Q11.Tools.Conversion.Handlers.Base
{
    internal abstract class Handler<TFrom, TTo> : HandlerConditional
    {
        public override bool CanHandle<T>(ChangeTypeRequest<T> request)
        {
            return request.IsFromType<TFrom>() && request.IsToType<TTo>();
        }
    }
}
