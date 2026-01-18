using System;

namespace TicketPortalLibrary.Models;

public class TicketException :Exception
{
    public TicketException(string msg):base(msg)
    {
        
    }

}
