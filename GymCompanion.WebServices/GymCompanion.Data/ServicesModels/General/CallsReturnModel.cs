using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GymCompanion.Data.ServicesModels.General
{
    public class CallsReturnModel<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public T Data { get; set; }
    }
}
