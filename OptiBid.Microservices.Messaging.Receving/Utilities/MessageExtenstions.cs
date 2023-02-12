using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using OptiBid.Microservices.Shared.Messaging.Enumerations;

namespace OptiBid.Microservices.Messaging.Receving.Utilities
{
    public static class MessageExtenstions
    {
        public static string GetAccountNotificationMessage(this Message message)
        {
            StringBuilder sb = new StringBuilder();
            if (message == null || message.AccountMessage == null)
            {
                sb.Append("Not available data for account changes");
                return sb.ToString();
            }

            return message.AccountMessage.AccountMessageType switch
            {
                AccountMessageType.Registration =>
                    sb.Append("User " + message.AccountMessage.UserName + " [role:" + message.AccountMessage.RoleName +
                              ",real name:" + message.AccountMessage.Name + "] successfully register his profile")
                        .ToString(),
                AccountMessageType.Update =>
                    sb.Append("User " + message.AccountMessage.UserName + " [role:" + message.AccountMessage.RoleName +
                              ",real name:" + message.AccountMessage.Name + "] has some update on his profile")
                        .ToString(),
                _ => sb.Append("Not available message type handling currently").ToString()
            };
        }

        public static (string,int) GetAuctionAssetsNotificationMessage(this Message message)
        {
            StringBuilder sb = new StringBuilder();
            if (message == null || message.AuctionMessage == null)
            {
                sb.Append("Not available data for account changes");
                return (sb.ToString(),-1);
            }

            var operationName = string.Empty;
            switch (message.AuctionMessage.ActionType)
            {

                case AuctionMessageType.Added:
                    operationName = "add";
                    break;
                case AuctionMessageType.Updated:
                    operationName = "updated";
                    break;
                case AuctionMessageType.Deleted:
                    operationName = "deleted";
                    break;
                default:
                    break;
            }

            if (string.IsNullOrWhiteSpace(operationName))
            {
                return (sb.Append("Not available message type handling currently").ToString(),-1);
            }
            return (sb.Append("Auction asset " + message.AuctionMessage.Name + " [asset holder:" +
                             message.AuctionMessage.Username + "] is " + operationName + " to auction.").ToString(),message.AuctionMessage.ID);

        }
        public static (string, int,decimal) GetAssetsBidNotificationMessage(this Message message)
        {
            StringBuilder notificationMessageBuilder = new StringBuilder();
            int assetId = -1;
            decimal price = -1;
            if (message == null || message.BidMessage == null)
            {
                notificationMessageBuilder.Append("Not available data for account changes");
                return (notificationMessageBuilder.ToString(), assetId, price);
            }

            assetId = message.BidMessage.AssetId;
            price = message.BidMessage.BidPrice;
            var dateTime = message.BidMessage.BidDateTime.ToString("dd/MM/yyyy HH:mm:ss");
            notificationMessageBuilder.Append("[" + dateTime + "]" + message.BidMessage.Bider + " make bid for asset "+message.BidMessage.AssetName);
            return (notificationMessageBuilder.ToString(),assetId, price);
        }
    }
}
