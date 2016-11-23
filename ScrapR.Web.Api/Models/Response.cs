using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapR.Web.Api.Models
{
    public class Response<T>
    {
        public string Message { get; set; }

        public T Data { get; set; }

        public bool Status { get; set; }

        public Response()
        {
            this.Message = "";
            this.Status = false;
        }

        public Response(string message)
        {
            this.Message = message;
            this.Status = false;
        }

        public Response(bool status)
        {
            this.Message = "";
            this.Status = status;
        }

        public Response(string message = "", T data = default(T), bool status = false)
        {
            if (!String.IsNullOrEmpty(message)) this.Message = message;
            if (data != null) this.Data = data;
            this.Status = status;
        }

        public static Response<T> Create(string message = "", T data = default(T), bool status = false)
        {
            return new Response<T>(message, data, status);
        }

        public static Response<T> Create(T data = default(T), bool status = false)
        {
            return new Response<T>(status ? "success" : "failed", data, status);
        }
    }
}
