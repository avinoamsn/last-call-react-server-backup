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

        // The default constructor creates an "everything worked okay, no error" error return.
        public ServerError()
        {
            ErrorNumber = (int)ServerErrorNumber.NoError;
            ErrorMessage = "SUCCEEDED";
            ErrorDetails = "";
        }

        // This constructor creates an error object with the error number and error message filled in
        public ServerError(ServerErrorNumber e)
        {
            (ErrorNumber, ErrorMessage, ErrorDetails) = ServerErrors.SetServerError(e);
        }
    }

    // The enum sequence must match the sequence in the Errors array OR SetServerError needs to do a search through the array
    public enum ServerErrorNumber { NoError = 0, InvalidLogin, UsernameInUse, InvalidPassword, FailedToAddSubscriber, NoOffersAvailable, ThrewException };

    public class ServerErrors
    {
        public static ServerError[] Errors = new ServerError[] {
            new ServerError { ErrorNumber = (int)ServerErrorNumber.NoError, ErrorMessage = "SUCCEEDED", ErrorDetails = "" },
            new ServerError { ErrorNumber = (int)ServerErrorNumber.InvalidLogin, ErrorMessage = "Invalid login", ErrorDetails = "" },
            new ServerError { ErrorNumber = (int)ServerErrorNumber.UsernameInUse, ErrorMessage = "Username is already in use", ErrorDetails = "" },
            new ServerError { ErrorNumber = (int)ServerErrorNumber.InvalidPassword, ErrorMessage = "Invalid password", ErrorDetails = "" },
            new ServerError { ErrorNumber = (int)ServerErrorNumber.FailedToAddSubscriber, ErrorMessage = "Failed to add subscriber", ErrorDetails = "" },
            new ServerError { ErrorNumber = (int)ServerErrorNumber.NoOffersAvailable, ErrorMessage = "No meal offers available today", ErrorDetails = "" },
            new ServerError { ErrorNumber = (int)ServerErrorNumber.ThrewException, ErrorMessage = "Server threw an Exception", ErrorDetails = "" },
            new ServerError { ErrorNumber = 7, ErrorMessage = "", ErrorDetails = "" },
            new ServerError { ErrorNumber = 8, ErrorMessage = "", ErrorDetails = "" },
        };

        internal static (int, string, string) SetServerError(ServerErrorNumber e)
        {
            return ((int)e, Errors[(int)e].ErrorMessage, Errors[(int)e].ErrorDetails);
        }
    }
}