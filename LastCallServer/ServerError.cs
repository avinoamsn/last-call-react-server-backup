using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastCallServer
{
    public class ServerError
    {
        public int ErrorNumber { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }

        public ServerError()
        {
            ErrorNumber = 0;
            ErrorMessage = "";
            ErrorDetails = "";
        }
    }

    public class ServerErrors
    {
        public static ServerError[] Errors = new ServerError[] {
            new ServerError { ErrorNumber = 0, ErrorMessage = "", ErrorDetails = "" },
            new ServerError { ErrorNumber = 1, ErrorMessage = "Invalid login", ErrorDetails = "" },
            new ServerError { ErrorNumber = 2, ErrorMessage = "Username is already in use", ErrorDetails = "" },
            new ServerError { ErrorNumber = 3, ErrorMessage = "Invalid password", ErrorDetails = "" },
            new ServerError { ErrorNumber = 4, ErrorMessage = "Failed to add subscriber", ErrorDetails = "" },
            new ServerError { ErrorNumber = 5, ErrorMessage = "", ErrorDetails = "" },
            new ServerError { ErrorNumber = 6, ErrorMessage = "", ErrorDetails = "" },
            new ServerError { ErrorNumber = 7, ErrorMessage = "", ErrorDetails = "" },
            new ServerError { ErrorNumber = 8, ErrorMessage = "", ErrorDetails = "" },
        };
    }
}