using System;

namespace TicketPortalLibrary.Models;

public class TicketException :Exception
{
    public int ErrorNumber;
    public TicketException(string msg,int errNo):base(msg)
    {
        this.ErrorNumber=errNo;
    }

}
