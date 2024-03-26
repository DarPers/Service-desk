﻿using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;
using ServiceDesk.Domain.Enums;

namespace ServiceDesk.BLL.Constants;
public static class MessageConstants
{
    public const string BaseTicketMessage = "Your ticket ";

    public static string CreateMessageAboutAddedTicket(TicketModel ticket)
    {
        return $"New ticket {ticket.Id} was added! Description: {ticket.Description}";
    }

    private static string CreateTicketMessageWithName(string name)
    {
        return BaseTicketMessage + $"'{name}' ";
    }

    public static string CreateMessageAboutChangedStatus(Ticket ticket)
    {
        switch (ticket.Status)
        {
            case Status.InProgress:
                return CreateTicketMessageWithName(ticket.Name) + "in progress!";
            case Status.Ready:
                return CreateTicketMessageWithName(ticket.Name) + "is ready!";
            default:
                return CreateTicketMessageWithName(ticket.Name) + $"has {ticket.Status} status now!";
        }
    }
}
